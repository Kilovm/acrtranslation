using System;
namespace BMG
{
	public interface ISentence
	{
		System.Collections.Generic.List<string> Commands { get; }
		string Original { get; }
		byte[] ToBytes();
		System.Xml.XmlElement ToXmlElement(System.Xml.XmlDocument doc);
		string Translation { get; set; }

		void Validate();
	}
}
