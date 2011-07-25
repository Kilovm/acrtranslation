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
        static List<PngInfo> pngs = new List<PngInfo>();
        static string png_root_dir = "";
        static string tpl_root_dir = "";

        static DirectoryInfo uncompress(FileInfo darchD, FileInfo file, DirectoryInfo tplDir)
        {
            DirectoryInfo ret = new DirectoryInfo(tplDir.FullName + "\\" + file.Name.Replace('.', '_'));
            if (InvokeDarch(darchD, file, ret) != 0)
            {
                Console.WriteLine("Error");

                throw new Exception();
            }
            return ret;
        }

        static int convert(FileInfo tpl, FileInfo png)
        {
            if (!png.Directory.Exists)
            {
                png.Directory.Create();
            }
            return FormatFromName(TplUtil.ConvertToPNG(tpl.FullName, png.FullName));
        }

        static void convert(DirectoryInfo tplArc, DirectoryInfo pngArc)
        {
            FileInfo[] files = tplArc.GetFiles("*.tpl", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                // add new item.
                FileInfo png = new FileInfo(pngArc.FullName + "\\" + file.Name.Replace(".tpl", ".png").Replace(".TPL", ".PNG"));
                int type = convert(file, png);
                PngInfo newitem = new PngInfo();
                newitem.Type = type;
                newitem.Tpl = file.FullName.Replace(tpl_root_dir, "");
                newitem.Png = png.FullName.Replace(png_root_dir, "");
                pngs.Add(newitem);
            }
            DirectoryInfo[] subDirs = tplArc.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo dir in subDirs)
            {
                convert(dir, new DirectoryInfo(pngArc.FullName + "\\" + dir.Name));
            }
        }

        static void handleDir(FileInfo darchD, DirectoryInfo rawDir, DirectoryInfo tplDir, DirectoryInfo pngDir)
        {
            if (!rawDir.Exists)
                return;
            if (!tplDir.Exists)
            {
                tplDir.Create();
            }
            FileInfo[] files = rawDir.GetFiles("*.arc", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                DirectoryInfo tplArcDir = uncompress(darchD, file, tplDir);

                convert(tplArcDir, new DirectoryInfo(pngDir.FullName + "\\" + tplArcDir.Name));
            }

            DirectoryInfo[] subDirs = rawDir.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo dir in subDirs)
            {
                handleDir(darchD, dir, new DirectoryInfo(tplDir.FullName + "\\" + dir.Name), new DirectoryInfo(pngDir.FullName + "\\" + dir.Name));
            }
        }

        static void writeConfig(string config)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("List");

            doc.AppendChild(root);

            foreach (PngInfo info in pngs)
            {
                XmlElement pngElement = doc.CreateElement("PNG");
                pngElement.SetAttribute("name", info.Png);
                pngElement.SetAttribute("tpl", info.Tpl);
                pngElement.SetAttribute("type", info.Type.ToString());

                root.AppendChild(pngElement);
            }

            FileInfo configFileInfo = new FileInfo(config);
            if (!configFileInfo.Directory.Exists)
            {
                configFileInfo.Directory.Create();
            }
            doc.Save(configFileInfo.FullName);
        }

		static void Main(string[] args)
		{
            CommandHandler handler = new CommandHandler(args, true);
            if (handler.hasOption("f"))
            {
                string tplFile = handler.Args[0];
                string pngFile = handler.Args[1];
                convert(new FileInfo(tplFile), new FileInfo(pngFile));
                return;
            }
            if (handler.Args.Length < 4)
            {
                Console.WriteLine("format: extractImgs darchD raw_dir tpl_dir png_dir config");
                return;
            }
			try
			{
				string darchD = handler.Args[0];
                string rawDir = handler.Args[1];
                string tplDir = handler.Args[2];
                string pngDir = handler.Args[3];
                string configXML = handler.Args[4];
                tpl_root_dir = new DirectoryInfo(tplDir).FullName + "\\";
                png_root_dir = new DirectoryInfo(pngDir).FullName + "\\";

                handleDir(new FileInfo(darchD), new DirectoryInfo(rawDir), new DirectoryInfo(tplDir), new DirectoryInfo(pngDir));

                writeConfig(configXML);

				Console.WriteLine("OK");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        static int InvokeDarch(FileInfo darchD, FileInfo src, DirectoryInfo dest)
        {
            ProcessStartInfo ps = new ProcessStartInfo(darchD.FullName, "-x " + dest.FullName.Replace('.', '_') + " " + src.Name);
            ps.WorkingDirectory = src.Directory.FullName;
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

        private static int FormatFromName(string typeName)
        {
            int format = 0;
            switch (typeName)
            {
                case "I4":
                    format = 0;
                    break;
                case "I8":
                    format = 1;
                    break;
                case "IA4":
                    format = 2;
                    break;
                case "IA8":
                    format = 3;
                    break;
                case "RGB565":
                    format = 4;
                    break;
                case "RGB5A3":
                    format = 5;
                    break;
                case "RGBA32":
                    format = 6;
                    break;
                case "CMPR":
                    format = 14;
                    break;
                default:
                    throw new Exception("Unsupported format!");
            }

            return format;
        }

        class PngInfo
        {
            public string Png;
            public string Tpl;
            public int Type;
        }
	}
}
