using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace BMG
{
	public class Sentence
	{
		List<string> _commands = new List<string>();

		string _original = "";
		string _translation = "";

		public List<string> Commands
		{
			get
			{
				return _commands;
			}
		}

		public string Original
		{
			get
			{
				return _original;
			}
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

		public Sentence(byte[] binData)
		{
			StringBuilder sb = new StringBuilder();


			foreach (byte b in binData)
			{
				sb.Append(b.ToString("X2"));
			}

			string hexString = sb.ToString();

			Regex regex = new Regex("(?<=1A06FF000005).+?(?=((1A06)|(1A07)|(1A08)))");
			MatchCollection mc = regex.Matches(hexString);

			int pos = 0;

			StringBuilder orgBuilder = new StringBuilder();
			foreach (Match match in mc)
			{
				string str = hexString.Substring(pos, match.Index - pos);
				//byte[] cmd = Extension.StringToBytes(str);

				_commands.Add(str);

				orgBuilder.AppendFormat("[{0}]", _commands.Count-1);

				str = hexString.Substring(match.Index, match.Length);

				byte[] utfBytes = Extension.StringToBytes(str);

				orgBuilder.Append(Encoding.UTF8.GetChars(utfBytes));

				pos = match.Index + match.Length;
			}


			if (pos < hexString.Length)
			{
				string str = hexString.Substring(pos, hexString.Length - pos);

				//byte[] cmd = Extension.StringToBytes(str);

				_commands.Add(str);

				orgBuilder.AppendFormat("[{0}]", _commands.Count-1);
			}

			_original = orgBuilder.ToString();

		}

		public Sentence(XmlElement element)
		{
			XmlNodeList commandNodes = element.SelectNodes("./Commands/Command");
			foreach (XmlElement node in commandNodes)
			{
                _commands.Add(Extension.BytesToString(Convert.FromBase64String(node.InnerText)));
			}

			_original = element.GetElementsByTagName("Original")[0].InnerText;
			_translation = element.GetElementsByTagName("Translation")[0].InnerText;
		}

		public byte[] ToBytes()
		{
			List<byte> byteList = new List<byte>();

			string str = _translation;

			if (string.IsNullOrEmpty(str)) str = _original;

			Encoding enc = Encoding.UTF8;

			char[] tc = new char[1];

			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];

				if (c == '[')
				{
					int j = i + 1;
					int cmdIndex = 0;
					bool isStub = false;

					while (j < str.Length)
					{
						char c2 = str[j];
						if (char.IsDigit(c2))
						{
							j++;
							continue;
						}
						else if (c2 == ']')
						{
							string strNumber = str.Substring(i + 1, j - i - 1);
							cmdIndex = int.Parse(strNumber);

							isStub = true;

							i = j;

							break;
						}
						else
						{
							break;
						}
					}

					if (isStub)
					{
						byteList.AddRange(Extension.StringToBytes(_commands[cmdIndex]));
						continue;
					}
				}

				tc[0] = c;
				byteList.AddRange(enc.GetBytes(tc));
			}

			return byteList.ToArray();
		}

		public XmlElement ToXmlElement(XmlDocument doc)
		{
			XmlElement element = doc.CreateElement("Sentence");

			XmlElement commandsElement = doc.CreateElement("Commands");

			for (int i = 0; i < _commands.Count; i++)
			{
				XmlElement cmdElement = doc.CreateElement("Command");
				//cmdElement.SetAttribute("id", i.ToString());

                cmdElement.InnerText = Convert.ToBase64String(Extension.StringToBytes(_commands[i]));

				commandsElement.AppendChild(cmdElement);
			}

			element.AppendChild(commandsElement);

			XmlElement orgElement = doc.CreateElement("Original");
			orgElement.InnerText = _original;
			element.AppendChild(orgElement);

			XmlElement transElement = doc.CreateElement("Translation");
			transElement.InnerText = _translation;
			element.AppendChild(transElement);

			return element;
		}
	}
}
