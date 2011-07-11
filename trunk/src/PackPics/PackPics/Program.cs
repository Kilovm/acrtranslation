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
			Dictionary<int, string> tplTypes = GetTPLTypes();

			DirectoryInfo curDir = new DirectoryInfo(".");

			string curPath = curDir.FullName + "\\";

			XmlDocument doc = new XmlDocument();
			doc.Load("List.xml");

			XmlNodeList nlist = doc.SelectNodes("/List/PNG");
			int i = 1;
			foreach (XmlElement node in nlist)
			{
				string path = node.GetAttribute("Path");
				string name = node.GetAttribute("name");
				string tpl = node.GetAttribute("tpl");
				int type = int.Parse(node.GetAttribute("type"));

				string typeName = tplTypes[type];

				Console.Write("({2}/{3}) Processing {0}{1} ...", path, name,i++,nlist.Count);
				
				if (!File.Exists(path + name))
				{
					Console.WriteLine("not found.");
					continue;
				}

				try
				{
					TplUtil.ConvertToTPL(path + name, tplTypes[type], path + tpl);
					File.Delete(path + name);

					Console.WriteLine("OK");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				//if (InvokeZetsubou(name, path, typeName, tpl) != 0)
				//{
				//    Console.WriteLine("failed");
				//    continue;
				//}
				//else
				//{
				//    Console.WriteLine("OK");
				//    File.Delete(path + name);
				//}
			}

			Console.WriteLine("All done.");
			Console.ReadKey(true);
		}

		static int InvokeZetsubou(string filename, string dir, string type, string tplName)
		{
			ProcessStartInfo ps = new ProcessStartInfo(@"C:\WII\Zet\zetsubou.exe", "wpng "+type+" "+ filename+" "+tplName);
			ps.WorkingDirectory = dir;
			ps.UseShellExecute = false;
			ps.CreateNoWindow = true;
			//ps.RedirectStandardOutput = true;
			Process p = Process.Start(ps);
			p.WaitForExit();

			return p.ExitCode;
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
