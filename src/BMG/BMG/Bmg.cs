﻿using System;
using System.IO;
using BMG.WiiMusic;
using BMG.ACR;
using System.Collections.Generic;
using System.Xml;
namespace BMG
{
	public abstract class BMG
	{
		public abstract string FileType { get; }

		public abstract ISentence[] Sentences { get; set; }
		public abstract string Title { get; set; }
		public abstract void WriteBMG(string filename);
		public abstract void WriteSessionXml(string filename);

        public List<Comment> Comments = new List<Comment>();

        protected void ReadComments(XmlDocument doc)
        {
            XmlElement commentsElement = doc.SelectSingleNode("/Session/Comments") as XmlElement;
            if (commentsElement == null) return;

            XmlNodeList commentsNodes = commentsElement.SelectNodes("./Comment");
            foreach (XmlElement element in commentsNodes)
            {
                Comment comment = new Comment();
                comment.Author = element.GetAttribute("Author");
                comment.CreateDate = element.GetAttribute("Date");
                comment.Position = int.Parse(element.GetAttribute("Position"));
                comment.Text = element.InnerText;

                Comments.Add(comment);
            }
        }

        protected void WriteComments(XmlDocument doc)
        {
            XmlElement commentsElement = doc.CreateElement("Comments");

            foreach (var comment in Comments)
            {
                XmlElement commentElement = doc.CreateElement("Comment");
                commentElement.SetAttribute("Author", comment.Author);
                commentElement.SetAttribute("Date", comment.CreateDate);
                commentElement.SetAttribute("Position", comment.Position.ToString());
                commentElement.InnerText = comment.Text;

                commentsElement.AppendChild(commentElement);
            }

            doc.DocumentElement.AppendChild(commentsElement);
        }

		public static BMG ReadBMG(string filename)
		{
			BMG bmg = null;
			FileInfo fi = new FileInfo(filename);
			if (fi.Extension.ToLower().Equals(".xml"))
			{
                try
                {
                    bmg = new TextMESS(filename, false);
                }
                catch
                {
                    try
                    {
                        bmg = new MailMESS(filename, false);
                    }
                    catch
                    {
                        try
                        {
                            bmg = new ItemMESS(filename, false);
                        }
                        catch
                        {
                            try
                            {
                                bmg = new CharaMESS(filename, false);
                            }
                            catch
                            {
                                try
                                {
                                    bmg = new MemDat(filename, false);
                                }
                                catch
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
                            }
                        }
                    }
                }
			}
			else
			{
                if(fi.Name.Equals(MailMESS.FileTitle) ||
                    (fi.Name.IndexOf(".MESS") != -1 && fi.Name.IndexOf("MAIL") != -1))
                {
                    bmg = new MailMESS(filename, true);
                }
                else if (fi.Name.Equals(ItemMESS.FileTitle))
                {
                    bmg = new ItemMESS(filename, true);
                }
                else if (fi.Name.Equals(CharaMESS.FileTitle))
                {
                    bmg = new CharaMESS(filename, true);
                }
                else if (fi.Name.ToLower().Equals(MemDat.FileTitle))
                {
                    bmg = new MemDat(filename, true);
                }
                else if (fi.Name.ToLower().Equals(BMG_WM.FileTitle))
                {
                    bmg = new BMG_WM(filename, true);
                }
                else if (fi.Name.ToLower().IndexOf(".mess") != -1 && fi.Name.IndexOf("TXT") != -1)
                {
                    bmg = new TextMESS(filename, true);
                }
                else
                {
                    bmg = new BMG_ACR(filename, true);
                }
			}

			return bmg;
		}

	}

    public class Comment
    {
        public string Text { get; set; }
        public string CreateDate { get; set; }
        public string Author { get; set; }
        public int Position { get; set; }
    }
}
