using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace BMG.WiiMusic
{
	public class Sentence_WM : ISentence
	{
		List<string> _commands = new List<string>();

		int _i1 = 0;
		int _i2 = 0;

		bool isEmpty = false;

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

		public int I1
		{
			get
			{
				return _i1;
			}
			set
			{
				_i1 = value;
			}
		}

		public int I2
		{
			get
			{
				return _i2;
			}
			set
			{
				_i2 = value;
			}
		}

		public Sentence_WM(byte[] binData)
		{
			StringBuilder sb = new StringBuilder();

			if (binData == null)
			{
				isEmpty = true;
				return;
			}

			int p = 0;

			Commands.Add(Extension.BytesToString(new byte[] { 0x0A }));

			while (p < binData.Length)
			{
				byte b = binData[p];

				if (b == 0)
				{
					p++;
				}
				else if (b == 0x1A)
				{
					int len = binData[p + 1];
					byte[] cmd = new byte[len];
					Array.Copy(binData, p, cmd, 0, len);

					Commands.Add(Extension.BytesToString(cmd));
					sb.AppendFormat("[{0}]", Commands.Count - 1);

					p = p + len;
				}
				else if (b == 0x0A)
				{
					sb.Append("[0]");
					p++;
				}
				else if ((b & 0xE0) == 0xE0)
				{
					if ((p + 2) < binData.Length && (binData[p + 1] & 0x80) == 0x80 && (binData[p + 2] & 0x80) == 0x80)
					{
						sb.Append(Encoding.UTF8.GetString(binData, p, 3));
						p += 3;
					}
					else if(b==0xE0||b==0xFF)
					{
						int len = 2;
						byte[] cmd = new byte[len];
						Array.Copy(binData, p, cmd, 0, len);

						Commands.Add(Extension.BytesToString(cmd));
						sb.AppendFormat("[{0}]", Commands.Count - 1);

						p = p + 2;
					}
					else
					{
						sb.Append((char)b);
						p += 1;
					}
				}
				else
				{
					sb.Append((char)b);
					p += 1;
				}
			}

			_original = sb.ToString();
		}

		private static bool IsCharacter(char c)
		{
			return char.IsLetterOrDigit(c)
				|| char.IsSeparator(c)
				|| char.IsSymbol(c)
				|| char.IsWhiteSpace(c)
				|| char.IsPunctuation(c);
		}

		public Sentence_WM(XmlElement element)
		{
			XmlNodeList commandNodes = element.SelectNodes("./Commands/Command");
			foreach (XmlElement node in commandNodes)
			{
                _commands.Add(Extension.BytesToString(Convert.FromBase64String(node.InnerText)));
			}

			_original = element.GetElementsByTagName("Original")[0].InnerText;
			_translation = element.GetElementsByTagName("Translation")[0].InnerText;

			_i1 = Convert.ToInt32(element.GetAttribute("I1"));
			_i2 = Convert.ToInt32(element.GetAttribute("I2"));
			isEmpty = Convert.ToBoolean(element.GetAttribute("Empty"));
		}

		public byte[] ToBytes()
		{
			if (isEmpty) return new byte[0];

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

			byteList.Add(0);
			byteList.Add(0);

			return byteList.ToArray();
		}

		public XmlElement ToXmlElement(XmlDocument doc)
		{
			XmlElement element = doc.CreateElement("Sentence");
			element.SetAttribute("Empty", isEmpty.ToString());
			element.SetAttribute("I1", I1.ToString());
			element.SetAttribute("I2", I2.ToString());

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


		public void Validate()
		{

		}
	}
}
