﻿using System;
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
        protected bool googleTranslateEnabled = false;

        protected DictForm dictForm = null;

        Regex regex = new Regex(@"(\[\d+\])");

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

					bmg = BMG.ReadBMG(filename);

					this.Text = bmg.Title;

					currentIndex = 0;

					RefreshTextBoxes();

					vScrollBar1.Maximum = bmg.Sentences.Length;
					vScrollBar1.Value = 1;

                    tbTranslation.Focus();
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

					bmg = BMG.ReadBMG(filename);

					this.Text = bmg.Title;

					currentIndex = 0;

					RefreshTextBoxes();

					vScrollBar1.Maximum = bmg.Sentences.Length;
					vScrollBar1.Value = 1;

                    tbTranslation.Focus();
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

            tbOriginal.Text = regex.Replace(bmg.Sentences[currentIndex].Original, "$1\r\n");
            tbTranslation.Text = regex.Replace(bmg.Sentences[currentIndex].Translation, "$1\r\n");

			lblStatus.Text = string.Format("{0} / {1}", currentIndex+1, bmg.Sentences.Length);

            ColorTextBox(tbTranslation);
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
                    SaveTranslation();

					SaveFileDialog sfd = new SaveFileDialog();
					sfd.Filter = "*.xml|*.xml";

                    if (bmg.Title.Contains("."))
                    {
                        sfd.FileName = bmg.Title.Remove(bmg.Title.LastIndexOf('.')) + ".xml";
                    }
                    else
                    {
                        sfd.FileName = bmg.Title + ".xml";
                    }

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

        private void SaveTranslation()
        {
            bmg.Sentences[currentIndex].Translation = tbTranslation.Text.Replace("\n", "");
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
                    SaveTranslation();

					SaveFileDialog sfd = new SaveFileDialog();
					sfd.Filter = "*.bmg|*.bmg";
                    sfd.FileName = bmg.Title;

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

            if (string.IsNullOrEmpty(tbTranslation.Text) ||
                MessageBox.Show(this,"Overwrite current translation?", "Overwrite", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                tbTranslation.Text = tbOriginal.Text;

                ColorTextBox(tbTranslation);
            }
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			if (bmg == null) return;

            SaveTranslation();

			int i = currentIndex - 1;
			if (i >= 0) currentIndex = i;

			vScrollBar1.Value = currentIndex+1; 
			RefreshTextBoxes();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (bmg == null) return;

            SaveTranslation();

			int i = currentIndex + 1;
			if (i<bmg.Sentences.Length) currentIndex = i;

			vScrollBar1.Value = currentIndex+1;
			RefreshTextBoxes();
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			if (bmg != null)
			{
				SaveTranslation();

				currentIndex = e.NewValue - 1;
				RefreshTextBoxes();
			}
        }

        private void tbTranslation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                toolStripMenuItem4_Click(null, null);

				e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                toolStripMenuItem3_Click(null, null);

				e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right && e.Modifiers == Keys.Alt)
            {
                toolStripMenuItem5_Click(null, null);

				e.Handled = true;
            }
        }

        private void ColorTextBox(RichTextBox rtb)
        {
            rtb.Focus();

            string text = rtb.Text;

            if (string.IsNullOrEmpty(text)) return;

            rtb.Clear();
            rtb.Text = text;

            int selectionStarts = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;

            MatchCollection mc = regex.Matches(text);

            if (mc.Count == 0) return;

            foreach (Match m in mc)
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = Color.LightGray;
            }

            rtb.DeselectAll();
            rtb.SelectionColor = Color.Black;

            rtb.SelectionStart = selectionStarts;
            rtb.SelectionLength = selectionLength;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bmg != null &&
                MessageBox.Show(this, "Exit?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

		private void lastTranslatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (bmg != null)
			{
				SaveTranslation();

				int lastIndex = -1;
				for (int i = 0; i < bmg.Sentences.Length; i++)
				{
					ISentence s = bmg.Sentences[i];

					if (!string.IsNullOrEmpty(s.Translation))
						lastIndex = i;
				}

				if (lastIndex != -1)
				{
					currentIndex = lastIndex;

					vScrollBar1.Value = currentIndex + 1;
					RefreshTextBoxes();
				}
			}
		}

		private void fontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FontDialog fd = new FontDialog();

			fd.Font = tbOriginal.Font;

			if (fd.ShowDialog() == DialogResult.OK)
			{
				tbOriginal.Font = tbTranslation.Font = fd.Font;
			}
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (bmg == null) return;

			SaveTranslation();

			ReplaceForm form = new ReplaceForm();

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				string src = form.SourceString;
				string dest = form.DestString;

				int n = 0;
				foreach (var sen in bmg.Sentences)
				{
					if (!string.IsNullOrEmpty(sen.Translation)&&sen.Translation.Contains(src))
					{
						sen.Translation = sen.Translation.Replace(src, dest);
						n++;
					}
				}

				RefreshTextBoxes();

				MessageBox.Show(string.Format("{0} occurrences replaced.", n));
			}
		}

        private void googleTranslateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            googleTranslateEnabled = !googleTranslateEnabled;

            googleTranslateToolStripMenuItem.Checked = googleTranslateEnabled;

            if (!googleTranslateEnabled && dictForm != null)
            {
                dictForm.Hide();
                dictForm.Dispose();
                dictForm = null;
            }
        }

        private void tbTranslation_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && googleTranslateToolStripMenuItem.Checked)
            {
                if (dictForm == null)
                {
                    dictForm = new DictForm();
                }

                string text;

                if (string.IsNullOrEmpty(tbTranslation.SelectedText))
                {
                    text = tbTranslation.Text;
                }
                else
                {
                    text = tbTranslation.SelectedText;
                }

                text =regex.Replace(text, " ");

                dictForm.Translation(text);
                dictForm.Show();
            }
        }
	}
}
