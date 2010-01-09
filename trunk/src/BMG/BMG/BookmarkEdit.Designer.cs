namespace BMG
{
	partial class BookmarkEdit
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
			this.label1 = new System.Windows.Forms.Label();
			this.numPosition = new System.Windows.Forms.NumericUpDown();
			this.tbComment = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbAuthor = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.dateDate = new System.Windows.Forms.DateTimePicker();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numPosition)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(36, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Position";
			// 
			// numPosition
			// 
			this.numPosition.Location = new System.Drawing.Point(135, 35);
			this.numPosition.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.numPosition.Name = "numPosition";
			this.numPosition.Size = new System.Drawing.Size(120, 21);
			this.numPosition.TabIndex = 1;
			this.numPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tbComment
			// 
			this.tbComment.Location = new System.Drawing.Point(135, 81);
			this.tbComment.Multiline = true;
			this.tbComment.Name = "tbComment";
			this.tbComment.Size = new System.Drawing.Size(198, 89);
			this.tbComment.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(36, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "Comments";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(36, 203);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "Author";
			// 
			// tbAuthor
			// 
			this.tbAuthor.Location = new System.Drawing.Point(135, 200);
			this.tbAuthor.Name = "tbAuthor";
			this.tbAuthor.Size = new System.Drawing.Size(198, 21);
			this.tbAuthor.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(38, 250);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "Date";
			// 
			// dateDate
			// 
			this.dateDate.Location = new System.Drawing.Point(135, 246);
			this.dateDate.Name = "dateDate";
			this.dateDate.Size = new System.Drawing.Size(200, 21);
			this.dateDate.TabIndex = 7;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(100, 297);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "&Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(219, 297);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 9;
			this.button2.Text = "&Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// BookmarkEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 346);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dateDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tbAuthor);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbComment);
			this.Controls.Add(this.numPosition);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BookmarkEdit";
			this.Text = "Bookmark";
			this.Load += new System.EventHandler(this.BookmarkEdit_Load);
			((System.ComponentModel.ISupportInitialize)(this.numPosition)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numPosition;
		private System.Windows.Forms.TextBox tbComment;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbAuthor;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateDate;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}