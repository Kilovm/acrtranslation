namespace BrfntGenerator
{
	partial class Form1
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
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numCharHeight = new System.Windows.Forms.NumericUpDown();
            this.numCharWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numColumns = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.btnGo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFontName = new System.Windows.Forms.TextBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbOutFileName = new System.Windows.Forms.TextBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.fontPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCharHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCharWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text File:";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(95, 35);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(220, 21);
            this.tbFileName.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(337, 33);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Browse...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Character Width:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Character Height:";
            // 
            // numCharHeight
            // 
            this.numCharHeight.Location = new System.Drawing.Point(145, 156);
            this.numCharHeight.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numCharHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCharHeight.Name = "numCharHeight";
            this.numCharHeight.Size = new System.Drawing.Size(63, 21);
            this.numCharHeight.TabIndex = 4;
            this.numCharHeight.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // numCharWidth
            // 
            this.numCharWidth.Location = new System.Drawing.Point(145, 123);
            this.numCharWidth.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numCharWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCharWidth.Name = "numCharWidth";
            this.numCharWidth.Size = new System.Drawing.Size(63, 21);
            this.numCharWidth.TabIndex = 4;
            this.numCharWidth.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bitmap Width: (2^n)";
            // 
            // numColumns
            // 
            this.numColumns.Location = new System.Drawing.Point(145, 189);
            this.numColumns.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumns.Name = "numColumns";
            this.numColumns.Size = new System.Drawing.Size(63, 21);
            this.numColumns.TabIndex = 4;
            this.numColumns.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Bitmap Height: (2^n)";
            // 
            // numRows
            // 
            this.numRows.Location = new System.Drawing.Point(145, 222);
            this.numRows.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(63, 21);
            this.numRows.TabIndex = 4;
            this.numRows.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(155, 326);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "&GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Font:";
            // 
            // tbFontName
            // 
            this.tbFontName.Location = new System.Drawing.Point(95, 78);
            this.tbFontName.Name = "tbFontName";
            this.tbFontName.ReadOnly = true;
            this.tbFontName.Size = new System.Drawing.Size(100, 21);
            this.tbFontName.TabIndex = 7;
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(213, 76);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 8;
            this.btnFont.Text = "...";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "Output:";
            // 
            // tbOutFileName
            // 
            this.tbOutFileName.Location = new System.Drawing.Point(95, 283);
            this.tbOutFileName.Name = "tbOutFileName";
            this.tbOutFileName.ReadOnly = true;
            this.tbOutFileName.Size = new System.Drawing.Size(220, 21);
            this.tbOutFileName.TabIndex = 10;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(337, 281);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 2;
            this.btnOutput.Text = "Browse...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // fontPreview
            // 
            this.fontPreview.Location = new System.Drawing.Point(492, 33);
            this.fontPreview.Name = "fontPreview";
            this.fontPreview.Size = new System.Drawing.Size(133, 130);
            this.fontPreview.TabIndex = 11;
            this.fontPreview.TabStop = false;
            this.fontPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.fontPreview_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 448);
            this.Controls.Add(this.fontPreview);
            this.Controls.Add(this.tbOutFileName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.tbFontName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.numCharWidth);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.numColumns);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numCharHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BRFNT";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCharHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCharWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbFileName;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numCharHeight;
		private System.Windows.Forms.NumericUpDown numCharWidth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numColumns;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numRows;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbFontName;
		private System.Windows.Forms.Button btnFont;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbOutFileName;
		private System.Windows.Forms.Button btnOutput;
		private System.Windows.Forms.PictureBox fontPreview;

	}
}

