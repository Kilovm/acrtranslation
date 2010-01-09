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
    public partial class BookmarkForm : Form
    {
        protected Form1 parentForm = null;
        protected List<Comment> comments = null;

        public BookmarkForm(Form1 parent)
        {
            InitializeComponent();

            parentForm = parent;
            comments = parent.GetComments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			Comment c = new Comment();
			c.Position = parentForm.GetCurrentPosition();
			c.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			BookmarkEdit editForm = new BookmarkEdit(c);

			if (editForm.ShowDialog(this) == DialogResult.OK)
			{
				c = editForm.Comment;
				comments.Add(c);

				BindList();

				parentForm.SetComments(comments);
			}
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
			if (listView1.SelectedIndices.Count == 0) return;

			if (MessageBox.Show("Delete these bookmarks?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				for (int i = listView1.SelectedIndices.Count; i > 0; i--)
				{
					comments.RemoveAt(listView1.SelectedIndices[i-1]);
				}

				parentForm.SetComments(comments);
				BindList();
			}
        }

        private void BookmarkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel=true;
        }

        private void BookmarkForm_Load(object sender, EventArgs e)
        {
			BindList();
        }

		private void BindList()
		{
			listView1.Items.Clear();

			for (int i = 0; i < comments.Count; i++)
			{
				var c = comments[i];
				ListViewItem item = new ListViewItem(
					new string[]{
						(i+1).ToString(),
						c.Position.ToString(),
						c.Text,
						c.Author,
						c.CreateDate
					});

				item.ToolTipText = c.Text;

				listView1.Items.Add(item);
			}
		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listView1.SelectedIndices.Count > 0)
			{
				parentForm.GotoPosition(comments[listView1.SelectedIndices[0]].Position);
			}
		}
    }
}
