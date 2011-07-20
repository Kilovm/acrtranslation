using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace BrfntGenerator
{
	static class Program
	{
        [DllImport("kernel32")]
        static extern bool AllocConsole();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        /// 

        static void handleDirectory(List<String> names, DirectoryInfo info)
        {
            DirectoryInfo[] dirs = info.GetDirectories("*", SearchOption.TopDirectoryOnly);
            FileInfo[] files = info.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                names.Add(file.FullName);
            }
            foreach (DirectoryInfo dir in dirs)
            {
                handleDirectory(names, dir);
            }
        }

        static String[] getAllFileNamesFromDirectory(String dir)
        {
            List<String> names = new List<string>();

            handleDirectory(names, new DirectoryInfo(dir));

            return names.ToArray();
        }

		[STAThread]
		static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                try
                {
                    AllocConsole();

                    if (args.Length < 3)
                    {
                        Console.WriteLine("format: FontGenerator char_width char_height bmp_w bmp_h text_dir dst_font [font]");
                        return;
                    }
                    int char_width = int.Parse(args[1]);
                    int char_height = int.Parse(args[2]);
                    int bmp_width = int.Parse(args[3]);
                    int bmp_height = int.Parse(args[4]);
                    string text_dir = args[5];
                    string dst_font = args[6];

                    if (bmp_width > 12 || bmp_height > 12)
                    {
                        Console.WriteLine("bmp_width and bmp_height max is 2^12. the value must be set < 12.");
                        return;
                    }
                   
                    string font;
                    if (args.Length > 7)
                    {
                        font = args[7];
                    }
                    else
                    {
                        font = "";
                    }

                    String[] names = getAllFileNamesFromDirectory(text_dir);
                    if (names.Length > 0)
                    {
                        BrfntWriter bw = new BrfntWriter(names, dst_font, char_width, char_height, bmp_width, bmp_height);
                        bw.WriteBrfnt(dst_font);

                        Console.WriteLine("font generate ok");
                    }
                    else
                    {
                        Console.WriteLine("text dir is invalid.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    Console.ReadKey(true);
                }
            }
        }
	}
}
