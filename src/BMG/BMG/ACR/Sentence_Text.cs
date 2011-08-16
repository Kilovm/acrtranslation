using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace BMG.ACR
{
    public class Sentence_Text: ISentence
    {
        string _original = "";
        string _translation = "";

        protected List<string> _commands = new List<string>();
        public List<string> Commands
        {
            get { return _commands; }
        }

        public string Original { get { return _original; } }

        public string Translation { get { return _translation; } set { _translation = value; } }

        public Sentence_Text(XmlElement element)
		{
			_original = element.GetElementsByTagName("Original")[0].InnerText;
			Translation = element.GetElementsByTagName("Translation")[0].InnerText;

			XmlNodeList commandNodes = element.SelectNodes("./Commands/Command");
			foreach (XmlElement node in commandNodes)
			{
				_commands.Add(Extension.BytesToString(Convert.FromBase64String(node.InnerText)));
			}
		}

        public Sentence_Text(byte[] binData)
		{
            MemoryStream ms = new MemoryStream(binData);

            StringBuilder sb = new StringBuilder();

            _commands.Add("00");

            int size1 = ms.ReadInt32();

            byte[] bs = new byte[size1-1]; // skip '00'
            ms.Read(bs, 0, bs.Length);
            ms.ReadByte();
            sb.Append(Encoding.UTF8.GetString(bs));
            sb.Append("[0]");

            _original = sb.ToString();
        }

        public byte[] ToBytes()
        {
            MemoryStream ms = new MemoryStream();

            string text = string.IsNullOrEmpty(Translation) ? Original : Translation;

            string[] lines = text.Split(new string[] { "[0]" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length != 1)
                throw new Exception("Unexpected format!");

            byte[][] bs = new byte[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                bs[i] = Encoding.UTF8.GetBytes(lines[i]);
                ms.WriteInt32(bs[i].Length + 1);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                ms.Write(bs[i], 0, bs[i].Length);
                ms.WriteByte(0);
            }

            return ms.ToArray();
        }

        public XmlElement ToXmlElement(XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("Sentence");

            XmlElement commandsElement = doc.CreateElement("Commands");

            for (int i = 0; i < _commands.Count; i++)
            {
                XmlElement cmdElement = doc.CreateElement("Command");

                cmdElement.InnerText = Convert.ToBase64String(Extension.StringToBytes(_commands[i]));

                commandsElement.AppendChild(cmdElement);
            }

            element.AppendChild(commandsElement);
            XmlElement orgElement = doc.CreateElement("Original");
            orgElement.InnerText = Original;
            element.AppendChild(orgElement);

            XmlElement transElement = doc.CreateElement("Translation");
            transElement.InnerText = Translation;
            element.AppendChild(transElement);

            return element;
        }

        public void Validate()
        {
        }
    }
}
