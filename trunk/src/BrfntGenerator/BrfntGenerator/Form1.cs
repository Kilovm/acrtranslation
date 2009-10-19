using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BrfntGenerator
{
	public partial class Form1 : Form
	{
		Bitmap fontImg = new Bitmap(40, 40);

		public Form1()
		{
			InitializeComponent();
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "*.txt;*.xml|*.txt;*.xml";
            ofd.Multiselect = true;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
                tbFileName.Text = string.Join(";", ofd.FileNames);
			}
		}

		private void btnOutput_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "*.brfnt|*.brfnt";

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				tbOutFileName.Text = sfd.FileName;
			}
		}

		private void btnFont_Click(object sender, EventArgs e)
		{
			FontDialog fd = new FontDialog();
			fd.ShowEffects = false;

			if (fd.ShowDialog() == DialogResult.OK)
			{
				tbFontName.Text = fd.Font.Name;

				Font font = new Font(fd.Font.Name, 40f, GraphicsUnit.Pixel);

				Graphics g = Graphics.FromImage(fontImg);
				g.Clear(Color.White);

				TextRenderer.DrawText(g, "啊", font, new Point(0, 0), Color.Black,Color.White, TextFormatFlags.NoPadding| TextFormatFlags.Top);

				fontPreview.Refresh();
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void fontPreview_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

			g.DrawImage(fontImg, fontPreview.ClientRectangle, 0, 0, fontImg.Width, fontImg.Height, GraphicsUnit.Pixel);
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			CheckControls();

			try
			{
				BrfntWriter bw = new BrfntWriter(tbFileName.Text, tbFontName.Text, (int)numCharWidth.Value, (int)numCharHeight.Value, (int)numColumns.Value, (int)numRows.Value);
				bw.WriteBrfnt(tbOutFileName.Text);

				MessageBox.Show("OK!");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CheckControls()
		{
		}
	}
}
