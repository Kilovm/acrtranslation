using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace BMG.ACR
{
    public class Sentence_Item: ISentence
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

		public byte[] ToBytes()  //NOTE: will crash if text content contains '['
		{
            MemoryStream ms = new MemoryStream();

            string text = string.IsNullOrEmpty(Translation) ? Original : Translation;

            int p = 0;

            while (p < text.Length)
            {
                char c = text[p];
                if (c == '[')
                {
                    int j = p + 1;
                    while (Char.IsDigit(text[j]))
                    {
                        j += 1;
                    }
                    if (text[j] == ']')
                    {
                        int index = int.Parse(text.Substring(p + 1, j - p - 1));
                        if (index == 0)
                        {
                            j += 1;

                            if (text[j] == '[')
                            {
                                int jj = j + 1;
                                while (Char.IsDigit(text[jj]))
                                {
                                    jj += 1;
                                }
                                int cmdIndex = int.Parse(text.Substring(j + 1, jj - j - 1));
                                byte[] bs = Extension.StringToBytes(_commands[cmdIndex]);

                                ms.WriteInt32(bs.Length);
                                ms.Write(bs, 0, bs.Length);

                                p = jj + 1;
                            }
                            else
                            {
                                int jj = j;
                                while (jj<text.Length&&text[jj] != '[')
                                {
                                    jj += 1;
                                }

                                string content = text.Substring(j, jj - j);
                                byte[] bs = Encoding.UTF8.GetBytes(content);

                                ms.WriteInt32(bs.Length+1);
                                ms.Write(bs, 0, bs.Length);
                                ms.WriteByte(0);

                                p = jj;
                            }
                        }
                        else
                        {
                            byte[] bs = Extension.StringToBytes(_commands[index]);
                            ms.Write(bs, 0, bs.Length);
                            p = j + 1;
                        }

                        continue;
                    }
                    else
                    {
                        throw new Exception("Unexpected text");
                    }
                }
                else
                {
                    throw new Exception("Unexpected text");
                }
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

		public Sentence_Item(XmlElement element)
		{
			_original = element.GetElementsByTagName("Original")[0].InnerText;
			Translation = element.GetElementsByTagName("Translation")[0].InnerText;

			XmlNodeList commandNodes = element.SelectNodes("./Commands/Command");
			foreach (XmlElement node in commandNodes)
			{
				_commands.Add(Extension.BytesToString(Convert.FromBase64String(node.InnerText)));
			}
		}

        public Sentence_Item(byte[] binData)
		{
			MemoryStream ms = new MemoryStream(binData);

            _commands.Add("00");
            StringBuilder sb = new StringBuilder();
            byte[] cmdBin = new byte[8];
            while (ms.Position < ms.Length)
            {
                ms.Read(cmdBin, 0, cmdBin.Length);

                sb.AppendFormat("[{0}]", _commands.Count);
                _commands.Add(Extension.BytesToString(cmdBin));

                sb.Append("[0]");
                int len = ms.ReadInt32();

                byte[] bs = new byte[len];
                ms.Read(bs, 0, len);

                if (bs[0] == 0x1A)
                {
                    sb.AppendFormat("[{0}]", _commands.Count);
                    _commands.Add(Extension.BytesToString(bs));
                }
                else
                {
                    sb.Append(Encoding.UTF8.GetString(bs,0,bs.Length-1));
                }
            }
            _original = sb.ToString();
		}

		public void Validate()
		{
		}

		#endregion
    }
}
