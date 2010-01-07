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

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void BookmarkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel=true;
        }

        private void BookmarkForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = comments;
        }
    }
}
