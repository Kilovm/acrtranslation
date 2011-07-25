using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace IsoPatch
{
	class Program
	{
		[DllImport("WiiScrubber.dll")]
		static extern void ExtractFiles([MarshalAs(UnmanagedType.LPStr)]string isoFileName, string[] files, string[] dests, int fileCount, bool caseSensitive, int partNo, IntPtr hwnd);

		[DllImport("WiiScrubber.dll")]
		static extern void ExtractFiles([MarshalAs(UnmanagedType.LPStr)]string isoFileName, string[] files, string[] dests, int fileCount, bool caseSensitive, IntPtr hwnd);

		[DllImport("WiiScrubber.dll")]
		static extern void ReplaceFiles([MarshalAs(UnmanagedType.LPStr)]string isoFileName, string[] files, string[] paths, int fileCount, bool caseSensitive, bool appendDiffSizeFiles, int partNo, IntPtr hwnd);

		[DllImport("WiiScrubber.dll")]
		static extern void ReplaceFiles([MarshalAs(UnmanagedType.LPStr)]string isoFileName, string[] files, string[] paths, int fileCount, bool caseSensitive, IntPtr hwnd);

		[DllImport("WiiScrubber.dll")]
		static extern IntPtr GetDiscId([MarshalAs(UnmanagedType.LPStr)]string isoPath);

		[DllImport("WiiScrubber.dll")]
		static extern bool FileExists([MarshalAs(UnmanagedType.LPStr)]string isoFileName, string findFileName, int partNo, bool caseSensitive);

        static void replaceDir(DirectoryInfo src, String dest, String isoPath, int partitionNo)
        {
            DirectoryInfo[] srcSubDirs = src.GetDirectories("*", SearchOption.TopDirectoryOnly);

            FileInfo[] files = src.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo f in files)
            {
                ReplaceFiles(isoPath,
                        new string[] { dest + "/" + f.Name },
                        new string[] { f.FullName },
                        1, false, false, partitionNo, IntPtr.Zero);
            }

            foreach (DirectoryInfo subDir in srcSubDirs)
            {
                replaceDir(subDir, dest + "/" + subDir.Name, isoPath, partitionNo);
            }
        }

		[STAThread]
		static void Main(string[] args)
		{
            try
            {
                if (args.Length < 3)
                {
                    Console.WriteLine(args.Length);
                    Console.WriteLine("format: iso_patch source_path iso_path dest_path");
                    return;
                }

                String sourcePath = args[0];
                String isoPath = args[1];
                String destPath = args[2];
                int partitionNo = GetPartitionNo(isoPath);
                replaceDir(new DirectoryInfo(sourcePath), destPath, isoPath, partitionNo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
		}

		private static int GetPartitionNo(string isoPath)
		{
			int p = 2;
			bool found = false;

			while (p>=0)
			{
				if (FileExists(isoPath, "opening.bnr", p, true))
				{
					found = true;
					break;
				}
				p--;
			}
			if (!found)
				throw new Exception("Data partition not found!");

			return p;
		}
	}

	[XmlRoot("Patch")]
	public class PatchXML
	{
		[XmlElement("File")]
		public FileNode[] Files;
	}

	public class FileNode
	{
		[XmlAttribute("Type")]
		public string Type;

		[XmlElement("Replace")]
		public string Replace;

		[XmlElement("With")]
		public string With;
	}
}
