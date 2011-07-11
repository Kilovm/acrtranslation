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
		static void Main(string[] args)
		{
			DirectoryInfo curDir = new DirectoryInfo(".");

			string curPath = curDir.FullName + "\\";

			DirectoryInfo[] dirs = curDir.GetDirectories("*_ARC", SearchOption.AllDirectories);

			foreach (var di in dirs)
			{
				Console.Write("Processing ARC folder " + di.Name+" ...");
				try
				{
					DirectoryInfo[] subDirectories = di.GetDirectories("*", SearchOption.TopDirectoryOnly);
					if (subDirectories.Length == 0) throw new Exception("empty ARC folder.");

					DirectoryInfo arcRoot = subDirectories[0];

					if (InvokeDarch(arcRoot.Name, di.FullName.Replace("_ARC", ".ARC").Replace("_arc",".arc"), di.FullName) != 0)
					{
						Console.WriteLine("failed.");
					}
					else
					{

						di.Delete(true);

						Console.WriteLine("OK");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			Console.WriteLine("Finish.");
			Console.ReadKey(true);
		}

		static int InvokeDarch(string filename,string arcName, string dir)
		{
			ProcessStartInfo ps = new ProcessStartInfo(@"E:\RVL_SDK\X86\bin\darchD.exe", "-c " +  filename+" "+arcName);
			ps.WorkingDirectory = dir;
			ps.UseShellExecute = false;
			ps.CreateNoWindow = true;
			//ps.RedirectStandardOutput = true;
			Process p = Process.Start(ps);
			p.WaitForExit();

			return p.ExitCode;
		}
	}
}
