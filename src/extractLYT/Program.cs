using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Xml;
using WiiUtil;

namespace ConsoleApplication5
{
	class Program
	{

		//List<PngInfo> pngs = new List<PngInfo>();


		static void Main(string[] args)
		{
			try
			{
				DirectoryInfo curDir = new DirectoryInfo(".");

				string curPath=curDir.FullName+"\\";

				List<PngInfo> pngs= new Program().Go();

				//XmlDocument doc = new XmlDocument();

				//XmlElement root = doc.CreateElement("List");

				//doc.AppendChild(root);

				//foreach (var info in pngs)
				//{
				//    XmlElement pngElement = doc.CreateElement("PNG");
				//    pngElement.SetAttribute("Path", info.Path.Replace(curPath, ""));
				//    pngElement.SetAttribute("name", info.PngName);
				//    pngElement.SetAttribute("tpl", info.TplName);
				//    pngElement.SetAttribute("type", info.Type.ToString());

				//    root.AppendChild(pngElement);
				//}

				//doc.Save("List.xml");

				Console.WriteLine("OK");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public List<PngInfo> Go()
		{
			Stack<DirectoryInfo> dirs = new Stack<DirectoryInfo>();
			dirs.Push(new DirectoryInfo("."));

			while (dirs.Count > 0)
			{
				DirectoryInfo dir = dirs.Pop();

				DirectoryInfo[] cd = ProcessDirectory(dir);

				foreach (var d in cd)
				{
					dirs.Push(d);
				}
			}

			return null;
		}

		private DirectoryInfo[] ProcessDirectory(DirectoryInfo d)
		{
			DirectoryInfo[] children = d.GetDirectories("*", SearchOption.TopDirectoryOnly);

			FileInfo[] files = d.GetFiles("*.arc", SearchOption.TopDirectoryOnly);

			foreach (var file in files)
			{
				string arcFolderName = file.Name.Replace('.', '_');

				Console.Write("Extract " + file.Name+" ...");

				if (InvokeDarch(file.Name, d.FullName) != 0)
				{
					Console.WriteLine("Error");

					throw new Exception();
				}
				Console.WriteLine("OK");

				file.Delete();

				DirectoryInfo arcFolder = d.GetDirectories(arcFolderName, SearchOption.TopDirectoryOnly)[0];


				//try
				//{
				//    Console.WriteLine("    Delete anim folder...");
				//    DirectoryInfo animFolder = d.GetDirectories("anim", SearchOption.AllDirectories)[0];
				//    animFolder.Delete(true);
				//}
				//catch { }

				//try
				//{
				//    Console.WriteLine("    Delete blyt folder...");
				//    DirectoryInfo blytFolder = d.GetDirectories("blyt", SearchOption.AllDirectories)[0];
				//    blytFolder.Delete(true);
				//}
				//catch { }

				try
				{
					DirectoryInfo timgFolder = arcFolder.GetDirectories("timg", SearchOption.AllDirectories)[0];
					ProcessImgDirectory(timgFolder);
				}
				catch { }
				 

			}

			return children;
		}

		private void ProcessImgDirectory(DirectoryInfo d)
		{
			FileInfo[] files = d.GetFiles("*.tpl");
			string folderName = d.FullName+"\\";

			foreach (var file in files)
			{
				Console.Write("Processing " + file.Name + " ...");

				string pngName= file.FullName.Replace(".tpl", "00.png").Replace(".TPL", "00.PNG");

				string type = TplUtil.ConvertToPNG(file.FullName,pngName);

				string pngShortName = file.Name.Replace(".tpl",".png").Replace(".TPL",".PNG");

				//if (InvokeZetsubou(file.Name, d.FullName) != 0)
				//{
				//    Console.WriteLine("failed.");

				//    throw new Exception();
				//}
				Console.WriteLine("OK");

				//PngInfo pi = new PngInfo();
				//pi.Path = folderName;
				//pi.PngName = pngShortName;
				//pi.TplName = file.Name;



				//pngs.Add(pi);

				/*
				for (int i = 0; i <= 0xFF; i++)
				{
					string pngName = prefix + i.ToString("X2") + ".png";
					string metName = prefix + i.ToString("X2") + ".met";

					if (File.Exists(folderName+pngName))
					{
						Console.WriteLine("    " + pngName);

						PngInfo pi = new PngInfo();
						pi.Path = folderName;
						pi.PngName = pngName;
						pi.TplName = file.Name;

						using (FileStream fs = new FileStream(folderName + metName, FileMode.Open))
						{
							fs.Seek(8, SeekOrigin.Begin);

							int t = fs.ReadByte();
							t = (t << 8) | fs.ReadByte();
							t = (t << 8) | fs.ReadByte();
							t = (t << 8) | fs.ReadByte();

							pi.Type = t;
						}

						pngs.Add(pi);

						File.Delete(folderName+metName);
					}
				}
				 */

				file.Delete();
			}

		}

		static int InvokeZetsubou(string filename, string dir)
		{
			ProcessStartInfo ps = new ProcessStartInfo(@"C:\WII\Zet\zetsubou.exe", "rpng "+filename);
			ps.WorkingDirectory = dir;
			ps.UseShellExecute = false;
			ps.CreateNoWindow = true;
			//ps.RedirectStandardOutput = true;
			Process p = Process.Start(ps);
			p.WaitForExit();

			return p.ExitCode;
		}

		static int InvokeDarch(string filename, string dir)
		{
			ProcessStartInfo ps = new ProcessStartInfo(@"E:\RVL_SDK\X86\bin\darchD.exe", "-x " + filename.Replace('.', '_') + " " + filename);
			ps.WorkingDirectory = dir;
			ps.UseShellExecute = false;
			ps.CreateNoWindow = true;
			//ps.RedirectStandardOutput = true;
			Process p = Process.Start(ps);
			p.WaitForExit();

			return p.ExitCode;
		}

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
	}

	class PngInfo
	{
		public string Path;

		public string PngName;
		public string TplName;
		public int Type;
	}
}
