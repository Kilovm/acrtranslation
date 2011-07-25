using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using WiiUtil;
using System.Collections;
/*
#define TPL_FORMAT_I4		0
#define TPL_FORMAT_I8		1
#define TPL_FORMAT_IA4		2
#define TPL_FORMAT_IA8		3
#define TPL_FORMAT_RGB565	4
#define TPL_FORMAT_RGB5A3	5
#define TPL_FORMAT_RGBA8	6
#define TPL_FORMAT_CI4		8
#define TPL_FORMAT_CI8		9
#define TPL_FORMAT_CI14X2	10
#define TPL_FORMAT_CMP		14
 */

namespace PackPics
{
	class Program
	{

		static void Main(string[] args)
		{
            if (args.Length < 3)
            {
                Console.WriteLine(args.Length);
                Console.WriteLine("format: PackageImg in_dir config_file out_dir");
                return;
            }
            Dictionary<int, string> tplTypes = GetTPLTypes();
            String in_dir = args[0];
            String config_file = args[1];
            String out_dir = args[2];

			XmlDocument doc = new XmlDocument();
            doc.Load(config_file);

			XmlNodeList nlist = doc.SelectNodes("/List/PNG");
			foreach (XmlElement node in nlist)
			{
				string png = node.GetAttribute("name");
				string tpl = node.GetAttribute("tpl");
				int type = int.Parse(node.GetAttribute("type"));

				string typeName = tplTypes[type];
			
                String in_name = in_dir + "\\" + png;
                String out_name = out_dir + "\\" + tpl;

				if (!File.Exists(in_name))
				{
					Console.WriteLine("not found.");
					continue;
				}

				try
				{
                    FileInfo tplInfo = new FileInfo(out_name);
                    if (!tplInfo.Directory.Exists)
                    {
                        tplInfo.Directory.Create();
                    }
					TplUtil.ConvertToTPL(in_name, tplTypes[type], out_name);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			Console.WriteLine("All done.");
		}

        private static Dictionary<int, string> GetTPLTypes()
		{

			Dictionary<int, string> types = new Dictionary<int, string>(){
		        {0,"I4"},
		        {1,"I8"},
		        {2,"IA4"},
		        {3,"IA8"},
		        {4,"RGB565"},
		        {5,"RGB5A3"},
		        {6,"RGBA32"},
		        {8,"CI4"},
		        {9,"CI8"},
		        {10,"CI14X2"},
		        {14,"CMPR"}
		        };

			return types;
		}
	}
}
