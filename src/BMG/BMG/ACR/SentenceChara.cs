using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace BMG.ACR
{
	public class SentenceChara: ISentence
	{
		#region ISentence Members

		string _original = "";
		string _translation = "";

		protected List<string> _commands = new List<string>();
		public List<string> Commands
		{
			get { return _commands; }
		}

		public string Original { get { return _original; } }

		public string Translation { get { return _translation; } set { _translation = value; } }

		protected int index;

		public byte[] ToBytes()
		{
			MemoryStream ms = new MemoryStream();

			ms.WriteInt32(index);
			ms.WriteInt32(_commands.Count);

			string text = string.IsNullOrEmpty(Translation) ? Original : Translation;

			Regex regex = new Regex(@"(\[\d+\])");

			string[] lines = new string[_commands.Count];
			Array.Copy(regex.Replace(text, "###").Split(new string[] { "###" }, StringSplitOptions.None), 1, lines, 0, _commands.Count);

			for (int i = 0; i < _commands.Count; i++)
			{
				byte[] bs = Extension.StringToBytes(_commands[i]);

				ms.Write(bs, 0, bs.Length);

				bs = Encoding.UTF8.GetBytes(lines[i]);

				ms.WriteInt32(bs.Length + 1);

				ms.Write(bs, 0, bs.Length);

				ms.WriteByte(0);
			}

			return ms.ToArray();
		}

		public XmlElement ToXmlElement(XmlDocument doc)
		{
			XmlElement element = doc.CreateElement("Sentence");
			element.SetAttribute("Index", index.ToString());

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

		public SentenceChara(XmlElement element)
		{
			index = int.Parse(element.GetAttribute("Index"));
			_original = element.GetElementsByTagName("Original")[0].InnerText;
			Translation = element.GetElementsByTagName("Translation")[0].InnerText;

			XmlNodeList commandNodes = element.SelectNodes("./Commands/Command");
			foreach (XmlElement node in commandNodes)
			{
				_commands.Add(Extension.BytesToString(Convert.FromBase64String(node.InnerText)));
			}
		}

		public SentenceChara(byte[] binData)
		{
			MemoryStream ms = new MemoryStream(binData);

			index = ms.ReadInt32();

			int count = ms.ReadInt32();

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < count; i++)
			{
				byte[] bs = new byte[8];
				ms.Read(bs, 0, 8);

				_commands.Add(Extension.BytesToString(bs));

				sb.AppendFormat("[{0}]", _commands.Count - 1);

				int len = ms.ReadInt32();

				bs = new byte[len-1];

				ms.Read(bs, 0, bs.Length);

				sb.Append(Encoding.UTF8.GetString(bs));


				ms.Seek(1, SeekOrigin.Current);
			}

			_original = sb.ToString();
		}

		public void Validate()
		{
		}

		#endregion
	}
}
