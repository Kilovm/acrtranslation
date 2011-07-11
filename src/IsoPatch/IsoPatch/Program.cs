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

		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				string currPath = ((FileInfo)new FileInfo(Assembly.GetEntryAssembly().Location)).DirectoryName;

				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "ISO|*.iso";
				if (ofd.ShowDialog() != DialogResult.OK) return;

				Environment.CurrentDirectory = currPath;

				PatchXML patch = null;

				Console.Write("Reading config file...");

				XmlSerializer s = new XmlSerializer(typeof(PatchXML));
				using (Stream stream = new FileStream("patch.xml", FileMode.Open))
				{
					patch = s.Deserialize(stream) as PatchXML;
				}

				Console.WriteLine("OK");

				string isoPath = ofd.FileName;

				int partitionNo = GetPartitionNo(isoPath);

				foreach (var file in patch.Files)
				{
					if (string.IsNullOrEmpty(file.Type) || !file.Type.Equals("Directory", StringComparison.CurrentCultureIgnoreCase))
					{
						Console.WriteLine(file.Replace);
						Environment.CurrentDirectory = currPath;
						ReplaceFiles(isoPath, new string[] { file.Replace }, new string[] { file.With }, 1, false, false, partitionNo, IntPtr.Zero);
					}
					else
					{
						DirectoryInfo rootDir = new DirectoryInfo(file.With);

						Stack<DirectoryInfo> dirs = new Stack<DirectoryInfo>();
						dirs.Push(rootDir);

						while (dirs.Count > 0)
						{
							DirectoryInfo dir = dirs.Pop();

							Console.WriteLine("In folder " + dir.Name);

							DirectoryInfo[] subDirs = dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
							foreach (var d in subDirs)
							{
								dirs.Push(d);
							}

							FileInfo[] files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);

							foreach (var f in files)
							{
								Console.WriteLine("    " + f.Name);

								string relFilePath = f.FullName.Replace(rootDir.FullName, "").Replace('\\','/');

								Environment.CurrentDirectory = currPath;
								ReplaceFiles(isoPath,
									new string[] { file.Replace+relFilePath }, 
									new string[] { file.With+relFilePath }, 
									1, false, false, partitionNo, IntPtr.Zero);
							}
						}
					}
				}

				Console.WriteLine("Finish!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			Console.ReadKey(true);
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
