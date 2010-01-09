namespace BMG
{
    partial class BookmarkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnIndex = new System.Windows.Forms.ColumnHeader();
			this.columnPosition = new System.Windows.Forms.ColumnHeader();
			this.columnComment = new System.Windows.Forms.ColumnHeader();
			this.columnAuthor = new System.Windows.Forms.ColumnHeader();
			this.columnDate = new System.Windows.Forms.ColumnHeader();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnAdd);
			this.flowLayoutPanel1.Controls.Add(this.btnRemove);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(498, 30);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(3, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(33, 21);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(42, 3);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(33, 21);
			this.btnRemove.TabIndex = 0;
			this.btnRemove.Text = "-";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIndex,
            this.columnPosition,
            this.columnComment,
            this.columnAuthor,
            this.columnDate});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(0, 30);
			this.listView1.Name = "listView1";
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(498, 276);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			// 
			// columnIndex
			// 
			this.columnIndex.Text = "No";
			this.columnIndex.Width = 33;
			// 
			// columnPosition
			// 
			this.columnPosition.Text = "Position";
			this.columnPosition.Width = 68;
			// 
			// columnComment
			// 
			this.columnComment.Text = "Comment";
			this.columnComment.Width = 201;
			// 
			// columnAuthor
			// 
			this.columnAuthor.Text = "Create By";
			this.columnAuthor.Width = 71;
			// 
			// columnDate
			// 
			this.columnDate.Text = "Create";
			this.columnDate.Width = 112;
			// 
			// BookmarkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(498, 306);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "BookmarkForm";
			this.Text = "Bookmarks";
			this.Load += new System.EventHandler(this.BookmarkForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BookmarkForm_FormClosing);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnIndex;
		private System.Windows.Forms.ColumnHeader columnPosition;
		private System.Windows.Forms.ColumnHeader columnComment;
		private System.Windows.Forms.ColumnHeader columnAuthor;
		private System.Windows.Forms.ColumnHeader columnDate;
    }
}