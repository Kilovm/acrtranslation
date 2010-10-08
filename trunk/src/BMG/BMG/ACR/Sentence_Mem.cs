using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace BMG.ACR
{
	public class Sentence_Mem: ISentence
	{
		public int Number { get; set; }

		string _original = "";
		string _translation = "";

		#region ISentence Members

		public List<string> Commands
		{
			get { throw new NotImplementedException(); }
		}

		public string Original
		{
			get { return _original; }
		}

		public byte[] ToBytes()
		{
			MemoryStream ms = new MemoryStream();

			ms.WriteInt32(Number);

			string str = _translation;
			if (string.IsNullOrEmpty(str)) str = _original;

			byte[] bs = Encoding.UTF8.GetBytes(str);

			ms.WriteInt32(bs.Length + 1);
			ms.Write(bs, 0, bs.Length);

			ms.WriteByte((byte)0);

			ms.WriteInt32(1);
			ms.WriteInt32(1);

			ms.WriteByte((byte)0);

			return ms.ToArray();
		}

		public XmlElement ToXmlElement(XmlDocument doc)
		{
			XmlElement element = doc.CreateElement("Sentence");
			element.SetAttribute("Number", Number.ToString());

			XmlElement orgElement = doc.CreateElement("Original");
			orgElement.InnerText = _original;
			element.AppendChild(orgElement);

			XmlElement transElement = doc.CreateElement("Translation");
			transElement.InnerText = _translation;
			element.AppendChild(transElement);

			return element;
		}

		public string Translation
		{
			get
			{
				return _translation;
			}
			set
			{
				_translation = value;
			}
		}

		#endregion

		public Sentence_Mem(byte[] binData)
		{
			Number = (binData[0] << 24) | (binData[1] << 16) | (binData[2] << 8) | binData[3];
			int size = (binData[4] << 24) | (binData[5] << 16) | (binData[6] << 8) | binData[7];

			_original = Encoding.UTF8.GetString(binData, 8, size - 1);
		}

		public Sentence_Mem(XmlElement element)
		{
			Number = int.Parse(element.GetAttribute("Number"));
			_original = element.GetElementsByTagName("Original")[0].InnerText;
			_translation = element.GetElementsByTagName("Translation")[0].InnerText;
		}
	}
}
