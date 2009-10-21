using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace BMG
{
	public class MessageEntry
	{


		public MessageEntry(byte[] binData)
		{

		}

		public MessageEntry(XmlElement element)
		{
		}

		public byte[] ToBytes()
		{
            throw new Exception();
		}

		public XmlElement ToXmlElement(XmlDocument doc)
		{
            throw new Exception();
		}
	}


}
