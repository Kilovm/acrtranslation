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
	public partial class BookmarkEdit : Form
	{
		public Comment Comment { get; set; }

		public BookmarkEdit(Comment c)
		{
			InitializeComponent();

			Comment = c;
		}

		private void BookmarkEdit_Load(object sender, EventArgs e)
		{
			numPosition.Value = Comment.Position;
			tbAuthor.Text = Comment.Author;
			tbComment.Text = Comment.Text;
			dateDate.Value = DateTime.Parse(Comment.CreateDate);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Comment.Position = (int)numPosition.Value;
			Comment.Author = tbAuthor.Text;
			Comment.Text = tbComment.Text;
			Comment.CreateDate = dateDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

			this.DialogResult = DialogResult.OK;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
