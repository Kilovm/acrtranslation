using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace BMG
{
	public partial class Form1 : Form
	{
		protected BMG bmg = null;
		protected int currentIndex = -1;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void openBMGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "*.bmg|*.bmg";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					string filename = ofd.FileName;

					bmg = new BMG(filename, true);

					this.Text = bmg.Title;

					currentIndex = 0;

					RefreshTextBoxes();
				}
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
		}

		private void openSessionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "*.xml|*.xml";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					string filename = ofd.FileName;

					bmg = new BMG(filename, false);

					this.Text = bmg.Title;

					currentIndex = 0;

					RefreshTextBoxes();
				}
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
		}

		private void RefreshTextBoxes()
		{
			if (currentIndex < 0 || bmg == null) return;

			int prevIndex = currentIndex - 1;
			if (prevIndex >= 0)
			{
				tbPrevOriginal.Text = bmg.Sentences[prevIndex].Original;
				tbPrevTranslation.Text = bmg.Sentences[prevIndex].Translation;
			}
			else
			{
				tbPrevOriginal.Text = tbPrevTranslation.Text = "";
			}

			int nextIndex = currentIndex + 1;
			if (nextIndex < bmg.Sentences.Length)
			{
				tbNextOriginal.Text = bmg.Sentences[nextIndex].Original;
				tbNextTranslation.Text = bmg.Sentences[nextIndex].Translation;
			}
			else
			{
				tbNextOriginal.Text = tbNextTranslation.Text = "";
			}

			tbOriginal.Text = bmg.Sentences[currentIndex].Original;
			tbTranslation.Text = bmg.Sentences[currentIndex].Translation;

			lblStatus.Text = string.Format("{0} / {1}", currentIndex+1, bmg.Sentences.Length);
		}

		private void saveSessionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (bmg == null)
				{
					throw new Exception("No BMG loaded!");
				}
				else
				{
					bmg.Sentences[currentIndex].Translation = tbTranslation.Text;

					SaveFileDialog sfd = new SaveFileDialog();
					sfd.Filter = "*.xml|*.xml";

					if (sfd.ShowDialog() == DialogResult.OK)
					{
						string filename = sfd.FileName;

						bmg.WriteSessionXml(filename);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void exportBMGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (bmg == null)
				{
					throw new Exception("No BMG loaded!");
				}
				else
				{
					bmg.Sentences[currentIndex].Translation = tbTranslation.Text;

					SaveFileDialog sfd = new SaveFileDialog();
					sfd.Filter = "*.bmg|*.bmg";

					if (sfd.ShowDialog() == DialogResult.OK)
					{
						string filename = sfd.FileName;

						bmg.WriteBMG(filename);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void toolStripMenuItem5_Click(object sender, EventArgs e)
		{
			if (bmg == null) return;

			tbTranslation.Text = tbOriginal.Text;
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			if (bmg == null) return;

			bmg.Sentences[currentIndex].Translation = tbTranslation.Text;

			int i = currentIndex - 1;
			if (i >= 0) currentIndex = i;

			RefreshTextBoxes();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (bmg == null) return;

			bmg.Sentences[currentIndex].Translation = tbTranslation.Text;

			int i = currentIndex + 1;
			if (i<bmg.Sentences.Length) currentIndex = i;

			RefreshTextBoxes();
		}
	}
}
