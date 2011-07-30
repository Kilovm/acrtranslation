using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chadsoft.CTools.Image.Tpl;
using System.IO;
using Chadsoft.CTools.Image;
using System.Drawing;
using System.Drawing.Imaging;
using NSProperties = tpl.Properties;

namespace WiiUtil
{
	public class TplUtil
	{
		public static string ConvertToPNG(string tplFileName, string pngFileName)
		{
			TplImage tpl = null;
			using (FileStream fs = new FileStream(tplFileName, FileMode.Open, FileAccess.Read))
			{
				tpl = new TplImage(fs);
			}

			Bitmap bmp = ImageData.ToBitmap(tpl.GetData(0), tpl.Width, tpl.Height);

			bmp.Save(pngFileName, ImageFormat.Png);

			return tpl.Format.Name;
		}

		private static ImageDataFormat FormatFromName(string typeName)
		{
			ImageDataFormat format = null;

			switch (typeName)
			{
				case "I4":
					format = ImageDataFormat.I4;
					break;
				case "I8":
					format = ImageDataFormat.I8;
					break;
				case "IA4":
					format = ImageDataFormat.IA4;
					break;
				case "IA8":
					format = ImageDataFormat.IA8;
					break;
				case "RGB565":
					format = ImageDataFormat.RGB565;
					break;
				case "RGB5A3":
					format = ImageDataFormat.RGB5A3;
					break;
				case "RGBA32":
					format = ImageDataFormat.Rgba32;
					break;
				case "CMPR":
					format = ImageDataFormat.Cmpr;
					break;
				default:
					throw new Exception("Unsupported format!");
			}

			return format;
		}

        public static void ConvertToTPLSelfTemplate(string pngFileName, string typeName, string tplFileName, string templateFileName)
		{
			ImageDataFormat format=FormatFromName(typeName);

			Bitmap bmp = new Bitmap(pngFileName);
            FileStream template = new FileStream(templateFileName, FileMode.Open, FileAccess.Read);
            TplImage tpl = new TplImage(template);
            template.Close();

			tpl.Import(ImageData.GetData(bmp), format, 1, bmp.Width, bmp.Height);

			using (FileStream fs = new FileStream(tplFileName, FileMode.Create))
			{
				tpl.Save(fs);
			}

			bmp.Dispose();
		}

        public static void ConvertToTPL(string pngFileName, string typeName, string tplFileName)
        {
            ImageDataFormat format = FormatFromName(typeName);

            Bitmap bmp = new Bitmap(pngFileName);
            TplImage tpl = new TplImage(new MemoryStream(NSProperties.Resources.FormatNewTpl));

            tpl.Import(ImageData.GetData(bmp), format, 1, bmp.Width, bmp.Height);

            using (FileStream fs = new FileStream(tplFileName, FileMode.Create))
            {
                tpl.Save(fs);
            }

            bmp.Dispose();
        }
	}
}
