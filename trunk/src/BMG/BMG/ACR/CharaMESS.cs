using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NS_BMG = BMG;
using System.IO;

namespace BMG.ACR
{
	public class CharaMESS: BMG
	{
		const string FileTitle = "CHARA.MESS";

		public override string FileType
		{
			get { return "Another Code R - CHARA.MESS"; }
		}

		public override ISentence[] Sentences { get; set; }

		public override string Title { get; set; }

		public CharaMESS(SentenceChara[] ss)
		{
			Sentences = ss;
		}

		public CharaMESS(string filename, bool rawBmg)
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

			if (!Title.Equals(FileTitle))
				throw new NS_BMG.WiiMusic.InvalidXmlException();

			XmlNodeList sentenceNodes = doc.SelectNodes("/Session/Sentence");

			Sentences = new SentenceChara[sentenceNodes.Count];

			int i = 0;
			foreach (XmlElement element in sentenceNodes)
			{
				Sentences[i++] = new SentenceChara(element);
			}

			ReadComments(doc);
		}

		private void ReadBMGFile(string filename)
		{
			FileInfo info = new FileInfo(filename);
			this.Title = info.Name;

			using (FileStream stream = new FileStream(filename, FileMode.Open))
			{
				int count = stream.ReadInt32();

				Sentences = new SentenceChara[count];

				int[] offsets = new int[count];

				for (int i = 0; i < count; i++)
				{
					offsets[i] = stream.ReadInt32();
				}

				for (int i = 0; i < count; i++)
				{
					int offset = offsets[i];
					int size = 0;

					if (i == count - 1)
					{
						size = (int)stream.Length - offset;
					}
					else
					{
						size = offsets[i + 1] - offset;
					}

					stream.Seek(offset, SeekOrigin.Begin);

					byte[] binData = new byte[size];

					stream.Read(binData, 0, size);

					SentenceChara sen = new SentenceChara(binData);

					Sentences[i] = sen;
				}
			}
		}

		public override void WriteBMG(string filename)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Create))
			{
				fs.WriteInt32(Sentences.Length);

				for (int i = 0; i < Sentences.Length; i++)
				{
					fs.WriteInt32(0);
				}

				for(int i=0;i<Sentences.Length;i++)
				{
					ISentence sentence=Sentences[i];

					long pos = fs.Position;

					fs.Seek(4L + 4L * i, SeekOrigin.Begin);

					fs.WriteInt32((int)pos);

					fs.Seek(pos, SeekOrigin.Begin);

					byte[] binData = sentence.ToBytes();

					fs.Write(binData, 0, binData.Length);
				}
			}
		}

		public override void WriteSessionXml(string filename)
		{
			XmlDocument doc = new XmlDocument();

			XmlElement element = doc.CreateElement("Session");
			element.SetAttribute("title", Title);

			int i = 0;
			foreach (Sentence_Mem sentence in Sentences)
			{
				XmlElement sentenceElement = sentence.ToXmlElement(doc);
				sentenceElement.SetAttribute("id", (i++).ToString());

				element.AppendChild(sentenceElement);
			}

			doc.AppendChild(element);

			WriteComments(doc);

			doc.Save(filename);
		}
	}
}
