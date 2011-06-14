using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMG
{
	public partial class ReplaceForm : Form
	{
		public ReplaceForm()
		{
			InitializeComponent();
		}

		public string SourceString
		{
			get
			{
				return tbSource.Text;
			}
		}

		public string DestString
		{
			get
			{
				return tbDestination.Text;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (SourceString.Equals(string.Empty) || DestString.Equals(string.Empty))
			{
				MessageBox.Show("Source and destination strings cannot be empty!");
				return;
			}

			char[] forbidden = { '[', ']' };

			if (SourceString.IndexOfAny(forbidden) >= 0 || DestString.IndexOfAny(forbidden) >= 0)
			{
				MessageBox.Show("Unreplacable chars detected.");
				return;
			}

			this.DialogResult = DialogResult.OK;
		}
	}
}
