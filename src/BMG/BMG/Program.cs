using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using WiiUtil;

namespace BMG
{
	static class Program
	{
		[DllImport("kernel32")]
		static extern bool AllocConsole();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        /// 

        static void handleDir(DirectoryInfo in_dir, DirectoryInfo out_dir, bool recursive, bool output_recursive, bool output_bmg)
        {
            if (!in_dir.Exists)
                return;


            FileInfo[] allFiles = in_dir.GetFiles("*.xml", (recursive && !output_recursive) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (allFiles.Length > 0)
            {
                if (!out_dir.Exists)
                    out_dir.Create();
            }
            foreach (FileInfo file in allFiles)
            {
                BMG bmg = BMG.ReadBMG(file.FullName);
                if (output_bmg)
                {
                    String format = ".BMG";
                    if (bmg.FileType == "Another Code R - CHARA.MESS")
                    {
                        format = ".MESS";
                    }
                    else if (bmg.FileType == "Another Code R - ITEM.MESS")
                    {
                        format = ".MESS";
                    }
                    else if (bmg.FileType == "Another Code R - MAIL.MESS")
                    {
                        format = ".MESS";
                    }
                    else if (bmg.FileType == "Another Code R - Memory.dat")
                    {
                        format = ".DAT";
                    }
                    else if (bmg.FileType == "Another Code R - TEXT.MESS")
                    {
                        format = ".MESS";
                    }
                    bmg.WriteBMG(out_dir.FullName + "/" + file.Name.Replace(".xml", format));
                }
                else
                {
                    bmg.WriteSessionXml(out_dir.FullName + "/" + file.Name);
                }
            }
            if (!output_recursive || !recursive)
                return;
            DirectoryInfo[] allSubDirs = in_dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo subDir in allSubDirs)
            {
                handleDir(subDir, new DirectoryInfo(out_dir.FullName + "/" + subDir.Name), recursive, output_recursive, output_bmg);
            }
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

                    CommandHandler handler = new CommandHandler(args, false);
                    if (handler.Args.Length < 2)
                    {
                        Console.WriteLine("format: bmg [option - d=dir r=recursive p=peer directory structure b=output bmg] src, dst");
                        return;
                    }
                    string src = handler.Args[0];
                    string dst = handler.Args[1];
                    if (handler.hasOption("d"))
                    {
                        handleDir(new DirectoryInfo(src), new DirectoryInfo(dst), handler.hasOption("r"), handler.hasOption("p"), handler.hasOption("b"));
                    }
                    else
                    {
                        BMG bmg = BMG.ReadBMG(src);
                        if (handler.hasOption("b"))
                        {
                            bmg.WriteBMG(dst);
                        }
                        else
                        {
                            bmg.WriteSessionXml(dst);
                        }
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
