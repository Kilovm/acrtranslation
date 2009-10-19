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
        protected int bmpWidth, bmpHeight;

        protected int nColumns, nRows;

        protected readonly int[] presetRanges = {0x20, 0x7e, 0xa0, 0xff};

		protected Bitmap buf;

		public BrfntWriter(string textFileName,string fontName, int cw, int ch, int nc, int nr)
		{
			charWidth = cw;
			charHeight = ch;
			bmpWidth = 1 << nc;
			bmpHeight = 1 << nr;

            ReadTextFile(textFileName, "gbk");

			font = new Font(fontName, cw, GraphicsUnit.Pixel);

            nColumns = bmpWidth / charWidth;
            nRows = bmpHeight / charHeight;

            buf = new Bitmap(bmpWidth, bmpHeight);
			
		}

        private void ReadTextFile(string fileName, string encoding)
        {
            string enc;

            List<char> list = new List<char>();
            List<char> tmpList = new List<char>();

            for (int i = 0; i < presetRanges.Length; i += 2)
            {
                for (int j = presetRanges[i]; j <= presetRanges[i + 1]; j++)
                    list.Add((char)j);
            }

            string[] names = fileName.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string name in names)
            {
                if (fileName.ToLower().EndsWith(".xml")) enc = "utf-8";
                else enc = encoding;

                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open), Encoding.GetEncoding(enc)))
                {
                    try
                    {
                        while (true)
                        {

                            char c = (char)reader.ReadChar();

                            if (c < 0x20)
                                continue;

                            if (!list.Contains(c) && !tmpList.Contains(c))
                                tmpList.Add(c);
                        }
                    }
                    catch (EndOfStreamException) { }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            }

            tmpList.Sort(); // We must sort here, because the font engine may use binary search
            list.AddRange(tmpList);
            chars = list.ToArray();
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
                outfile.WriteByte(0x01);
                outfile.WriteByte((byte)(charHeight - 1));
                outfile.WriteInt16(0x000A);

				outfile.WriteByte(0x00);

				outfile.WriteByte((byte)(charWidth - 1));
				outfile.WriteByte((byte)(charWidth - 1));

				outfile.WriteByte(0x00);

				outfile.WriteInt32(0x00000038);

				posCWDH = outfile.Position;
				outfile.WriteInt32(0);

				posCMAP = outfile.Position;
				outfile.WriteInt32(0);

                outfile.WriteByte((byte)(charHeight - 1));
                outfile.WriteByte((byte)(charWidth - 1));
                outfile.WriteByte((byte)(charWidth - 3));
                outfile.WriteByte((byte)(0x00));
				
				#endregion

				#region TGLP head
				outfile.Write(new byte[] { (byte)'T', (byte)'G', (byte)'L', (byte)'P' }, 0, 4);

				posTGLPSize = outfile.Position;
				outfile.WriteInt32(0);

				outfile.WriteByte((byte)(charWidth - 1));
				outfile.WriteByte((byte)(charHeight - 1));

				outfile.WriteByte((byte)(charWidth - 3));
				outfile.WriteByte((byte)(charWidth - 3));

				outfile.WriteInt32(bmpWidth * bmpHeight);

				outfile.WriteInt16(nImages);
				outfile.WriteInt16(0x0002);

				outfile.WriteInt16(nColumns);
				outfile.WriteInt16(nRows);

				outfile.WriteInt16(bmpWidth);
				outfile.WriteInt16(bmpHeight);

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
									int color = GetColor(rgb[pos], rgb[pos + 1], rgb[pos + 2]);

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
                long posCwdhStart = outfile.Position;
				outfile.Write(new byte[] { (byte)'C', (byte)'W', (byte)'D', (byte)'H' }, 0, 4);

                long posCwdhLen = outfile.Position;
				outfile.WriteInt32(chars.Length * 3 + 16 + 3); // cwdh length

				posTemp = outfile.Position;
				outfile.Seek(posCWDH, SeekOrigin.Begin);
				outfile.WriteInt32((int)posTemp);
				outfile.Seek(posTemp, SeekOrigin.Begin);   

				outfile.WriteInt32(chars.Length - 1);
				outfile.WriteInt32(0);

				foreach (char c in chars)
				{
                    outfile.WriteByte(0xff); // FIXME: buggy value (see issue 1)
                    outfile.WriteByte((byte)(charWidth - 3));
                    if (c < 0x100) // Latin character
                        outfile.WriteByte((byte)((charWidth - 3) / 2));
                    else
                        outfile.WriteByte((byte)(charWidth - 3));
                }

                for (int i = (4 - (((int)outfile.Position + 1) % 4)) % 4; i >= 0; i--)
                    outfile.WriteByte(0);
                long posCwdhEnd = outfile.Position;

                posTemp = outfile.Position;
                outfile.Seek(posCwdhLen, SeekOrigin.Begin);
                outfile.WriteInt32((int)(posCwdhEnd - posCwdhStart));
                outfile.Seek(posTemp, SeekOrigin.Begin);
                #endregion

				#region CMAP0
                int presetCharCount = 0;
                for (int i = 0; i < presetRanges.Length; i += 2)
                {
                    outfile.Write(new byte[] { (byte)'C', (byte)'M', (byte)'A', (byte)'P' }, 0, 4);

                    outfile.WriteInt32(0x18);

                    if (i == 0)
                    {
                        posTemp = outfile.Position;
                        outfile.Seek(posCMAP, SeekOrigin.Begin);
                        outfile.WriteInt32((int)posTemp);
                        outfile.Seek(posTemp, SeekOrigin.Begin);
                    }

                    outfile.WriteInt16(presetRanges[i]);
                    outfile.WriteInt16(presetRanges[i + 1]);

                    outfile.WriteInt32(0);

                    posTemp = outfile.Position;
                    outfile.WriteInt32((int)posTemp + 16);

                    outfile.WriteInt16(presetCharCount);
                    outfile.WriteInt16(0);
                    presetCharCount += presetRanges[i + 1] - presetRanges[i] + 1;
                }

				#endregion

				#region CMAP1
                long posCmapStart = outfile.Position;
                outfile.Write(new byte[] { (byte)'C', (byte)'M', (byte)'A', (byte)'P' }, 0, 4);

                long posCmapLen = outfile.Position;
                outfile.WriteInt32(chars.Length * 4 + 2 + 2 + 20);

				outfile.WriteInt32(0x0000FFFF);

				outfile.WriteInt32(0x00020000);

				outfile.WriteInt32(0);

                outfile.WriteInt16(chars.Length - presetCharCount);

                for (int i = presetCharCount; i < chars.Length; i++)
				{
					char c = chars[i];

					outfile.WriteInt16((int)c);

					outfile.WriteInt16(i);
				}

                for (int i = (4 - (((int)outfile.Position + 1) % 4)) % 4; i >= 0; i--)
                    outfile.WriteByte(0);
                long posCmapEnd = outfile.Position;

                posTemp = outfile.Position;
                outfile.Seek(posCmapLen, SeekOrigin.Begin);
                outfile.WriteInt32((int)(posCmapEnd - posCmapStart));
                outfile.Seek(posTemp, SeekOrigin.Begin);
                #endregion

                posTemp = outfile.Position;
                int size = (int)outfile.Position;
				outfile.Seek(posFileSize, SeekOrigin.Begin);
				outfile.WriteInt32(size);
                outfile.Seek(posTemp, SeekOrigin.Begin);
			}

		}

		private int GetColor(byte r, byte g, byte b)
		{
			int color = r * 100 / 256;
			color = (color << 2) | (g * 100 / 256);
			color = (color << 2) | (b * 100 / 256);
			color = (color << 2) | 3;

			return color;
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
