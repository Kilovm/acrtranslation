using System;
using System.IO;
using BMG.WiiMusic;
using BMG.ACR;
namespace BMG
{
	public abstract class BMG
	{
		public abstract ISentence[] Sentences { get; set; }
		public abstract string Title { get; set; }
		public abstract void WriteBMG(string filename);
		public abstract void WriteSessionXml(string filename);

		public static BMG ReadBMG(string filename)
		{
			BMG bmg = null;
			FileInfo fi = new FileInfo(filename);
			if (fi.Extension.ToLower().Equals(".xml"))
			{
				try
				{
					bmg = new BMG_WM(filename, false);
				}
				catch (InvalidXmlException)
				{
					bmg = new BMG_ACR(filename, false);
				}
				catch (Exception)
				{
					throw;
				}
			}
			else
			{
				if (fi.Name.ToLower().Equals("new_music_message.bmg"))
				{
					bmg = new BMG_WM(filename, true);
				}
				else
				{
					bmg = new BMG_ACR(filename, true);
				}
			}

			return bmg;
		}

	}
}
