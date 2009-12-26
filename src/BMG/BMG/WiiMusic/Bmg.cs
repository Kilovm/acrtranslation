using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BMG.WiiMusic
{
	public class BMG_WM:BMG
	{
		const string GameFlag = "WiiMusic";

		public override string Title { get; set; }

		public override ISentence[] Sentences { get; set; }

		public byte[] MID1 { get; private set; }

		private BMG_WM(Sentence_WM[] ss)
		{
			Sentences = ss;
		}

		public BMG_WM(string filename, bool rawBmg)
		{
			if (rawBmg)
			{
				ReadBMGFile(filename);
			}
			else // xml
			{
				ReadXmlFile(filename);
			}
		}

		private void ReadXmlFile(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			Title = doc.DocumentElement.GetAttribute("title");

			string game = doc.DocumentElement.GetAttribute("game");
			if (!game.Equals(GameFlag))
			{
				throw new InvalidXmlException();
			}
			

			XmlNodeList sentenceNodes = doc.SelectNodes("/Session/Sentence");

			Sentences = new Sentence_WM[sentenceNodes.Count];

			int i = 0;
			foreach (XmlElement element in sentenceNodes)
			{
				Sentences[i++] = new Sentence_WM(element);
			}

			XmlElement midElement = doc.SelectSingleNode("/Session/MID1") as XmlElement;
			MID1 = Convert.FromBase64String(midElement.InnerText);
		}

		private void ReadBMGFile(string filename)
		{
			FileInfo info = new FileInfo(filename);
			this.Title = info.Name;

			using (FileStream stream = new FileStream(filename, FileMode.Open))
			{
				stream.Seek(0x20, SeekOrigin.Current);

				stream.Seek(4, SeekOrigin.Current);

				int inf1Size = stream.ReadInt32();

				int sentencesCount = stream.ReadInt16();

				stream.Seek(6, SeekOrigin.Current);

				int[] offsetList = new int[sentencesCount];
				int[] i1List = new int[sentencesCount];
				int[] i2List = new int[sentencesCount];

				for (int i = 0; i < sentencesCount; i++)
				{
					offsetList[i] = stream.ReadInt32();
					//stream.Seek(4, SeekOrigin.Current);
					i1List[i] = stream.ReadInt32();
					i2List[i] = stream.ReadInt32();
				}

				stream.Seek(0x20, SeekOrigin.Begin);
				stream.Seek(inf1Size, SeekOrigin.Current);

				stream.Seek(4, SeekOrigin.Current);
				int datSize = stream.ReadInt32();

				long begin = stream.Position;

				byte[][] outbytes = new byte[offsetList.Length][];

				for (int i = 0; i < offsetList.Length; i++)
				{
					int offset = offsetList[i];

					if (offset == 0) continue;

					stream.Seek(begin, SeekOrigin.Begin);

					stream.Seek(offset, SeekOrigin.Current);

					//int size = i != offsetList.Length - 1 ? offsetList[i + 1] - offset : (int)(stream.Length - begin - offset);
					int size = 0;
					if (i != offsetList.Length - 1)
					{
						int j = i + 1;
						int offset2 = 0;

						while (j < offsetList.Length)
						{
							offset2 = offsetList[j];
							if (offset2 != 0) break;
							j += 1;
						}

						if (offset2 == 0)
						{
							size = datSize - 8 - offset;
						}
						else
						{
							size = offset2 - offset;
						}
					}
					else
					{
						size = datSize - 8 - offset;
					}

					outbytes[i] = new byte[size];

					stream.Read(outbytes[i], 0, size);
				}

				Sentences = new Sentence_WM[outbytes.Length];

				for (int i = 0; i < Sentences.Length; i++)
				{
					Sentence_WM sen=new Sentence_WM(outbytes[i]);
					sen.I1 = i1List[i];
					sen.I2 = i2List[i];
					Sentences[i] = sen;
				}

				MID1 = new byte[stream.Length - stream.Position];
				stream.Read(MID1, 0, MID1.Length);
			}
		}

		public override void WriteBMG(string filename)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Create))
			{
				long posFileSize;
				long posInfSize;
				long posTemp;
				long posDatSize;

				#region MESG head
				fs.Write(new byte[]{(byte)'M',(byte)'E',(byte)'S',(byte)'G',
					(byte)'b',(byte)'m',(byte)'g',(byte)'1'}, 0, 8);

				posFileSize = fs.Position;
				fs.WriteInt32(0);

				fs.WriteInt32(3); 

				fs.WriteInt32(0x02000000);
				fs.WriteInt32(0);
				fs.WriteInt32(0);
				fs.WriteInt32(0);
				#endregion 


				#region INF1 head
				fs.Write(new byte[] { (byte)'I', (byte)'N', (byte)'F', (byte)'1' }, 0, 4);

				posInfSize = fs.Position;
				fs.WriteInt32(0);

				fs.WriteInt16(Sentences.Length);
				fs.WriteInt16(0x0C);

				fs.WriteInt32(0);

				for (int i = 0; i < Sentences.Length; i++)
				{
					fs.WriteInt32(0);
					//fs.WriteInt32(0x00640002);

					Sentence_WM sen = Sentences[i] as Sentence_WM;

					fs.WriteInt32(sen.I1);
					fs.WriteInt32(sen.I2);
				}

				if (Sentences.Length % 2 == 0)
				{
					fs.WriteInt32(0);
					fs.WriteInt32(0);
				}

				fs.WriteInt32(0);
				fs.WriteInt32(0);

				posTemp = fs.Position;
				fs.Seek(posInfSize, SeekOrigin.Begin);
				fs.WriteInt32((int)posTemp - 0x20);
				fs.Seek(posTemp, SeekOrigin.Begin);
				#endregion

				#region DAT
				fs.Write(new byte[] { (byte)'D', (byte)'A', (byte)'T', (byte)'1' }, 0, 4);

				posDatSize = fs.Position;
				fs.WriteInt32(0);

				fs.WriteByte(0);

				int p = 1;
				for (int i = 0; i < Sentences.Length; i++)
				{
					Sentence_WM sentence = Sentences[i] as Sentence_WM;

					byte[] data = sentence.ToBytes();

					fs.Write(data, 0, data.Length);

					posTemp = fs.Position;

					fs.Seek(0x30 + i * 8, SeekOrigin.Begin);
					fs.WriteInt32(data.Length == 0 ? 0 : p);

					p += data.Length;

					fs.Seek(posTemp, SeekOrigin.Begin);
				}
				#endregion

				posTemp = fs.Position;

				for (int i = 16 - (int)posTemp % 16; i > 0; i--)
				{
					fs.WriteByte(0);
				}

				posTemp = fs.Position;

				fs.Seek(posDatSize, SeekOrigin.Begin);
				fs.WriteInt32((int)(posTemp - posDatSize + 4));

				fs.Seek(posTemp, SeekOrigin.Begin);
				fs.Write(MID1, 0, MID1.Length);

				fs.Seek(posFileSize, SeekOrigin.Begin);
				fs.WriteInt32((int)fs.Length);
			}

		}

		public override void WriteSessionXml(string filename)
		{
			XmlDocument doc = new XmlDocument();

			XmlElement element = doc.CreateElement("Session");
			element.SetAttribute("title", Title);
			element.SetAttribute("game", GameFlag);

            int i = 0;
			foreach (Sentence_WM sentence in Sentences)
			{
				XmlElement sentenceElement = sentence.ToXmlElement(doc);
                sentenceElement.SetAttribute("id", (i++).ToString());

				element.AppendChild(sentenceElement);
			}


			XmlElement midElement = doc.CreateElement("MID1");
			midElement.InnerText = Convert.ToBase64String(MID1);

			element.AppendChild(midElement);

			doc.AppendChild(element);

			doc.Save(filename);
		}
	}

	public class InvalidXmlException : Exception { }

}
