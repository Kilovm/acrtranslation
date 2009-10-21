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

			List<byte> tmpList = new List<byte>();
			int p = 0,len;
			bool bCmd = false;
			int start=0;
			while (p < binData.Length)
			{
				byte b = binData[p];

				if (b == 0x1A)
				{
					if (!bCmd)
					{
						start = p;
						bCmd = true;
					}

					len = binData[p + 1];
					p += len;
				}
				else if (b == 0x0A)
				{
					if (!bCmd)
					{
						start = p;
						bCmd = true;
					}
					p += 1;
				}
				else if (b == 0)
				{
					if (!bCmd)
					{
						start = p;
						bCmd = true;
					}
					p += 1;
				}
				else
				{
					if (bCmd)
					{
						len = p - start;
						byte[] bs = new byte[len];

						Array.Copy(binData, start, bs, 0, len);

						_commands.Add(Extension.BytesToString(bs));

						sb.AppendFormat("[{0}]", _commands.Count - 1);

						bCmd = false;
					}

					start = p;
					p += 1;
					while (p < binData.Length)
					{
						byte b2 = binData[p];

						if (b2 == 0x1A || b2 == 0x0A || b2 == 0x00)
						{
							break;
						}
						else if ((b2 & 0xE0) == 0xE0)
						{
							if ((p + 2) < binData.Length && (binData[p + 1] & 0x80) == 0x80 && (binData[p + 2] & 0x80) == 0x80)
							{
								p += 3;
							}
							else
							{
								p += 1;
							}
						}
						else
						{
							p += 1;
						}
					}

					len = p - start;
					sb.Append(Encoding.UTF8.GetString(binData, start, len));
				}
			}

			if (bCmd)
			{
				len = p - start;
				byte[] bs = new byte[len];

				Array.Copy(binData, start, bs, 0, len);

				_commands.Add(Extension.BytesToString(bs));

				sb.AppendFormat("[{0}]", _commands.Count - 1);

				bCmd = false;
			}

			_original = sb.ToString();
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
