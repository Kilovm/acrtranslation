using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace PackACRResource
{
	class Program
	{
        static void handleDir(String darch_path, DirectoryInfo in_dir, DirectoryInfo out_dir)
        {
            if (!in_dir.Exists)
                return;
            if (!out_dir.Exists)
                out_dir.Create();

            DirectoryInfo[] allSubDirs = in_dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo subDir in allSubDirs)
            {
                if (subDir.Name.EndsWith("_ARC"))
                {
                    Console.Write("Processing ARC folder " + subDir.Name + " ...");
                    try
                    {
                        DirectoryInfo[] subDirectories = subDir.GetDirectories("*", SearchOption.TopDirectoryOnly);
                        if (subDirectories.Length == 0) throw new Exception("empty ARC folder.");

                        DirectoryInfo arcRoot = subDirectories[0];
                        if (InvokeDarch(darch_path, out_dir.FullName + "\\" + subDir.Name.Replace("_ARC", ".ARC").Replace("_arc", ".arc"), arcRoot) != 0)
                        {
                            Console.WriteLine("failed.");
                        }
                        else
                        {
                            Console.WriteLine("OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    handleDir(darch_path, subDir, new DirectoryInfo(out_dir.FullName + "\\" + subDir.Name));
                }
            }
        }

		static void Main(string[] args)
		{
            if (args.Length < 3)
            {
                Console.WriteLine(args.Length);
                Console.WriteLine("format: PackageACR darch_path in_dir out_dir");
                return;
            }
            String darch_path = args[0];
            String in_dir = args[1];
            String out_dir = args[2];

            handleDir(darch_path, new DirectoryInfo(in_dir), new DirectoryInfo(out_dir));

			Console.WriteLine("Finish.");
		}

		static int InvokeDarch(string darchName, string filename, DirectoryInfo arcInfo)
		{
			ProcessStartInfo ps = new ProcessStartInfo(darchName, "-c " + arcInfo.Name + " "+ filename);
			ps.WorkingDirectory = arcInfo.Parent.FullName;
			ps.UseShellExecute = false;
			ps.CreateNoWindow = true;
			//ps.RedirectStandardOutput = true;
			Process p = Process.Start(ps);
			p.WaitForExit();

			return p.ExitCode;
		}
	}
}
