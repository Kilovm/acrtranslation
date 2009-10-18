using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMG
{
	static class StreamExtension
	{
		public static void WriteInt16(this Stream stream, int n)
		{
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)(n & 0xFF));
		}

		public static void WriteInt32(this Stream stream, int n)
		{
			stream.WriteByte((byte)((n >> 24) & 0xFF));
			stream.WriteByte((byte)((n >> 16) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)((n) & 0xFF));
		}

		public static int ReadInt32(this Stream stream)
		{
			int i = stream.ReadByte();
			i = (i << 8) | stream.ReadByte();
			i = (i << 8) | stream.ReadByte();
			i = (i << 8) | stream.ReadByte();

			return i;
		}

		public static int ReadInt16(this Stream stream)
		{
			int i = stream.ReadByte();
			i = (i << 8) | stream.ReadByte();

			return i;
		}
	}


	static class Extension
	{
		public static byte[] StringToBytes(string str)
		{
			int size = str.Length / 2;
			byte[] bs = new byte[size];

			for (int i = 0; i < str.Length; i += 2)
			{
				string hex = str.Substring(i, 2);

				bs[i / 2] = Convert.ToByte(hex, 16);
			}

			return bs;
		}

		public static string BytesToString(byte[] bs)
		{
			StringBuilder sb = new StringBuilder();

			foreach (byte b in bs)
			{
				sb.Append(b.ToString("X2"));
			}

			return sb.ToString();
		}
	}
}
