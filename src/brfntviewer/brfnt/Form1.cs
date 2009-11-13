using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace brfnt
{
	public partial class Form1 : Form
	{
		FileStream stream = null;
		Bitmap buf = new Bitmap(1280, 800);

		int ii = 16;

		long pos = 0x60;

		public Form1()
		{
			InitializeComponent();

		}


		private void Form1_Load(object sender, EventArgs e)
		{
			//stream = new FileStream("FONT_A_21.BRFNT", FileMode.Open);

			//stream.Seek(pos, SeekOrigin.Begin);

			//DrawBuffer();
		}

		private void DrawBuffer()
		{
			int jj = 4, kk = 8;
			int dw = 1;

			Graphics g = Graphics.FromImage(buf);
			g.Clear(Color.White);

			for (int column = 0; column < 10; column++)
			{

				for (int line = 0; line < 180; line++)
				{
					for (int i = 0; i < ii; i++)
					{
						for (int j = 0; j < jj; j++)
						{
							for (int k = 0; k < kk; k++)
							{
								int b = stream.ReadByte();

								if (b == -1) return;

								using (Brush brush = new SolidBrush(Get8BitColor(b)))
								{
									g.FillRectangle(brush, column*ii*kk*dw + (i * kk + k) * dw, (line * jj + j) + dw, dw, dw);

								}

							}
						}
					}
				}

			}
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			g.DrawImage(buf, 0, 0);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (stream != null)
				stream.Close();
		}

		private Color Get8BitColor(int b)
		{

			return Color.FromArgb(b/4, b/2, b);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				if (stream != null) stream.Close();

				stream = new FileStream(ofd.FileName, FileMode.Open);

				RefreshCanvas();

				this.Text = ofd.FileName;
			}
		}

		private void RefreshCanvas()
		{
			stream.Seek(pos, SeekOrigin.Begin);
			DrawBuffer();

			canvas.Refresh();
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			ii = 8;
			RefreshCanvas();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			ii = 16;
			RefreshCanvas();
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			ii = 32;
			RefreshCanvas();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();

			sfd.Filter = "*.png|*.png";

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				buf.Save(sfd.FileName);
			}
		}

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            if (f2.ShowDialog(this) == DialogResult.OK)
            {
                ii = f2.ImageWidth;
                RefreshCanvas();

                toolStripMenuItem5.Text = ii.ToString()+"...";
            }
        }
	}
}
