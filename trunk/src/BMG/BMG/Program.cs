using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BMG
{
	static class Program
	{
		[DllImport("kernel32")]
		static extern bool AllocConsole();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
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

					string src = args[1];
					string dst = args[2];

					BMG bmg = BMG.ReadBMG(src);
					if (src.ToLower().EndsWith(".bmg"))
					{
						bmg.WriteSessionXml(dst);
					}
					else
					{
						bmg.WriteBMG(dst);
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
