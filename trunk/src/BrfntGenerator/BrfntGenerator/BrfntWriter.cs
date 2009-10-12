using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BrfntGenerator
{
	class BrfntWriter
	{
		protected char[] chars = null;

		protected Font font;

		protected int charWidth,charHeight;

		protected int nColumns, nRows;

		protected Bitmap buf;

		public BrfntWriter(string textFileName,string fontName, int cw, int ch, int nc, int nr)
		{
			charWidth = cw;
			charHeight = ch;
			nColumns = nc;
			nRows = nr;

			FileStream stream = new FileStream(textFileName, FileMode.Open);

			ReadTextFile(stream);

			font = new Font(fontName, cw, GraphicsUnit.Pixel);


			int imgWidth = (int)Math.Pow(2, (int)(Math.Log(charWidth * nColumns, 2.0) + .5));
			int imgHeight = (int)Math.Pow(2, (int)(Math.Log(charHeight * nRows, 2.0) + .5));
			buf = new Bitmap(imgWidth, imgHeight);
			
		}

		private void ReadTextFile(FileStream stream)
		{
			using (BinaryReader reader = new BinaryReader(stream))
			{

				List<char> list = new List<char>();

                for (int i = 0x20; i < 0x7e; i++)
                {
                    list.Add((char)i);
                }

				try
				{
					while (true)
					{

						char c = reader.ReadChar();

                        if (c <= 0x7e) continue;

						if (!list.Contains(c))
							list.Add(c);
					}
				}
				catch (EndOfStreamException) { }
				catch (Exception ex)
				{
					System.Windows.Forms.MessageBox.Show(ex.Message);
				}

				list.Sort();
				chars=list.ToArray();
			}
		}

		public void WriteBrfnt(string outputFileName)
		{
			long posFileSize;
			long posCWDH;
			long posCMAP;
			long posTGLPSize;
			long posTemp;

			Graphics g = Graphics.FromImage(buf);

			using (FileStream outfile = new FileStream(outputFileName, FileMode.Create))
			{
				int nImages = (int)(1.0 * chars.Length / (nRows * nColumns) + .5);

				#region RFNT head
				outfile.Write(new byte[] { (byte)'R', (byte)'F', (byte)'N', (byte)'T' }, 0, 4);
				outfile.Write(new byte[] { 0xFE, 0xFF, 0x01, 0x04 }, 0, 4);

				posFileSize = outfile.Position;
				outfile.WriteInt32(0);

				outfile.Write(new byte[] { 0x00, 0x10, 0x00, 0x06 }, 0, 4);
				#endregion


				#region FINF head
				outfile.Write(new byte[] { (byte)'F', (byte)'I', (byte)'N', (byte)'F' }, 0, 4);
				outfile.WriteInt32(0x20);
				outfile.WriteInt32(0x011B000A);

				outfile.WriteByte(0x00);

				outfile.WriteByte((byte)charWidth);
				outfile.WriteByte((byte)charHeight);

				outfile.WriteByte(0x00);

				outfile.WriteInt32(0x00000038);

				posCWDH = outfile.Position;
				outfile.WriteInt32(0);

				posCMAP = outfile.Position;
				outfile.WriteInt32(0);

				outfile.WriteInt32(0); //???
				
				#endregion

				#region TGLP head
				outfile.Write(new byte[] { (byte)'T', (byte)'G', (byte)'L', (byte)'P' }, 0, 4);

				posTGLPSize = outfile.Position;
				outfile.WriteInt32(0);

				outfile.WriteByte((byte)charWidth);// ?
				outfile.WriteByte((byte)charHeight);// ?

				outfile.WriteByte((byte)charWidth);// ??
				outfile.WriteByte((byte)charHeight);// ??

				outfile.WriteInt16(0);
				outfile.WriteInt16(0x4000);//?

				outfile.WriteInt16(nImages);
				outfile.WriteInt16(0x0002);

				outfile.WriteInt16(nColumns);
				outfile.WriteInt16(nRows);

				outfile.WriteInt16(buf.Width);
				outfile.WriteInt16(buf.Height);

				outfile.WriteInt32(0x00000060);

				outfile.WriteInt32(0);
				outfile.WriteInt32(0);
				outfile.WriteInt32(0);
				outfile.WriteInt32(0);
				#endregion

				#region TGLP data
				int nChars = nColumns * nRows;
				int current = 0;

				while (current < chars.Length)
				{
					g.Clear(Color.Black);
					for (int y = 0; y < nRows; y++)
					{
						if(current>=chars.Length) break;

						for (int x = 0; x < nColumns; x++)
						{
							if(current>=chars.Length) break;

							char c = chars[current];

							TextRenderer.DrawText(g, c.ToString(), font, new Point(x * charWidth ,y * charHeight), Color.White, TextFormatFlags.NoPadding);

							current++;
						}
					}

					BitmapData bd = buf.LockBits(new Rectangle(0, 0, buf.Width, buf.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					byte[] rgb = new byte[bd.Stride * buf.Height];
					Marshal.Copy(bd.Scan0, rgb, 0, bd.Stride * buf.Height);

					for (int line = 0; line < buf.Height; line+=4)
					{
						for (int i = 0; i < buf.Width; i+=8)
						{
							for (int j = 0; j < 4; j++)
							{
								for (int k = 0; k < 8; k++)
								{
									int pos = (i + k) * 3 + (line + j) * bd.Stride;
									int color = (rgb[pos] + rgb[pos + 1] + rgb[pos + 2]) / 3;

									outfile.WriteByte((byte)color);
								}
							}
						}

					}

					buf.UnlockBits(bd);
				}
				#endregion

				posTemp = outfile.Position;
				outfile.Seek(posTGLPSize, SeekOrigin.Begin);
				outfile.WriteInt32((int)posTemp - 0x30);
				outfile.Seek(posTemp, SeekOrigin.Begin);

				#region CWDH
				outfile.Write(new byte[] { (byte)'C', (byte)'W', (byte)'D', (byte)'H' }, 0, 4);

				outfile.WriteInt32(chars.Length * 3 + 16 + 3); // cwdh length

				posTemp = outfile.Position;
				outfile.Seek(posCWDH, SeekOrigin.Begin);
				outfile.WriteInt32((int)posTemp);
				outfile.Seek(posTemp, SeekOrigin.Begin);   

				outfile.WriteInt32(chars.Length - 1);
				outfile.WriteInt32(0);

				foreach (char c in chars)
				{
					outfile.WriteByte(0x18);
					outfile.WriteByte(0x18);
					outfile.WriteByte(0x00);
				}

				outfile.WriteByte(0);
				outfile.WriteByte(0);
				outfile.WriteByte(0);
				#endregion

				#region CMAP0
				outfile.Write(new byte[] { (byte)'C', (byte)'M', (byte)'A', (byte)'P' }, 0, 4);

				outfile.WriteInt32(0x18);

				posTemp = outfile.Position;
				outfile.Seek(posCMAP, SeekOrigin.Begin);
				outfile.WriteInt32((int)posTemp);
				outfile.Seek(posTemp, SeekOrigin.Begin);

				outfile.WriteInt32(0x0020007E);
				outfile.WriteInt32(0);

				posTemp = outfile.Position;
				outfile.WriteInt32((int)posTemp + 16);

				outfile.WriteInt32(0);

				#endregion

				#region CMAP1
				outfile.Write(new byte[] { (byte)'C', (byte)'M', (byte)'A', (byte)'P' }, 0, 4);

				outfile.WriteInt32(chars.Length * 4 + 2 + 2 + 20);

				outfile.WriteInt32(0x0000FFFF);

				outfile.WriteInt32(0x00200000);

				outfile.WriteInt32(0);

				outfile.WriteInt16(chars.Length);

				for (int i = (0x7e-0x20+1); i < chars.Length; i++)
				{
					char c = chars[i];

					outfile.WriteInt16((int)c);

					outfile.WriteInt16(i);
				}

				outfile.WriteInt16(0);
				#endregion

				int size = (int)outfile.Position;
				outfile.Seek(posFileSize, SeekOrigin.Begin);
				outfile.WriteInt32(size);


			}

		}
	}

	static class Extension
	{
		public static void WriteInt32(this Stream stream,int n)
		{
			stream.WriteByte((byte)(n >> 24));
			stream.WriteByte((byte)((n >> 16) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)(n & 0xFF));
		}

		public static void WriteInt16(this Stream stream, int n)
		{
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)(n & 0xFF));
		}
	}
}
