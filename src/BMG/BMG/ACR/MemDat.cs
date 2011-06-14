using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NS_BMG = BMG;
using System.IO;

namespace BMG.ACR
{
	public class MemDat : BMG
	{
		public const string FileTitle = "MEMORY.dat";

		public override string FileType
		{
			get { return "Another Code R - Memory.dat"; }
		}

		public override string Title { get; set; }

		public override ISentence[] Sentences { get; set; }

		public MemDat(Sentence_Mem[] ss)
		{
			Sentences = ss;
		}

		public MemDat(string filename, bool rawBmg)
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

			Sentences = new Sentence_Mem[sentenceNodes.Count];

			int i = 0;
			foreach (XmlElement element in sentenceNodes)
			{
				Sentences[i++] = new Sentence_Mem(element);
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

				Sentences = new Sentence_Mem[count];

				for (int i = 0; i < count; i++)
				{
					stream.Seek(4, SeekOrigin.Current);
					int size = stream.ReadInt32();

					stream.Seek(-8, SeekOrigin.Current);
					byte[] binData = new byte[8 + size + 4 + 4 + 1];

					stream.Read(binData, 0, binData.Length);

					Sentence_Mem sentence = new Sentence_Mem(binData);

					Sentences[i] = sentence;
				}
			}
		}

		public override void WriteBMG(string filename)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Create))
			{
				fs.WriteInt32(Sentences.Length);

				foreach (var sentence in Sentences)
				{
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
