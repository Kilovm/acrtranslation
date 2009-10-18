namespace BMG
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openBMGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportBMGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tbPrevOriginal = new System.Windows.Forms.TextBox();
			this.tbPrevTranslation = new System.Windows.Forms.TextBox();
			this.tbOriginal = new System.Windows.Forms.TextBox();
			this.tbTranslation = new System.Windows.Forms.TextBox();
			this.tbNextOriginal = new System.Windows.Forms.TextBox();
			this.tbNextTranslation = new System.Windows.Forms.TextBox();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem5});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(734, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBMGToolStripMenuItem,
            this.openSessionToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveSessionToolStripMenuItem,
            this.exportBMGToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openBMGToolStripMenuItem
			// 
			this.openBMGToolStripMenuItem.Name = "openBMGToolStripMenuItem";
			this.openBMGToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openBMGToolStripMenuItem.Text = "Open BMG...";
			this.openBMGToolStripMenuItem.Click += new System.EventHandler(this.openBMGToolStripMenuItem_Click);
			// 
			// openSessionToolStripMenuItem
			// 
			this.openSessionToolStripMenuItem.Name = "openSessionToolStripMenuItem";
			this.openSessionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openSessionToolStripMenuItem.Text = "Open Session...";
			this.openSessionToolStripMenuItem.Click += new System.EventHandler(this.openSessionToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// exportBMGToolStripMenuItem
			// 
			this.exportBMGToolStripMenuItem.Name = "exportBMGToolStripMenuItem";
			this.exportBMGToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exportBMGToolStripMenuItem.Text = "Export BMG...";
			this.exportBMGToolStripMenuItem.Click += new System.EventHandler(this.exportBMGToolStripMenuItem_Click);
			// 
			// saveSessionToolStripMenuItem
			// 
			this.saveSessionToolStripMenuItem.Name = "saveSessionToolStripMenuItem";
			this.saveSessionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveSessionToolStripMenuItem.Text = "Save Session...";
			this.saveSessionToolStripMenuItem.Click += new System.EventHandler(this.saveSessionToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.splitContainer1.Size = new System.Drawing.Size(734, 471);
			this.splitContainer1.SplitterDistance = 350;
			this.splitContainer1.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tbPrevOriginal, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbOriginal, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbNextOriginal, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 471);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tbPrevTranslation, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tbTranslation, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.tbNextTranslation, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(380, 471);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// tbPrevOriginal
			// 
			this.tbPrevOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPrevOriginal.Location = new System.Drawing.Point(3, 3);
			this.tbPrevOriginal.Multiline = true;
			this.tbPrevOriginal.Name = "tbPrevOriginal";
			this.tbPrevOriginal.ReadOnly = true;
			this.tbPrevOriginal.Size = new System.Drawing.Size(344, 150);
			this.tbPrevOriginal.TabIndex = 0;
			// 
			// tbPrevTranslation
			// 
			this.tbPrevTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPrevTranslation.Location = new System.Drawing.Point(3, 3);
			this.tbPrevTranslation.Multiline = true;
			this.tbPrevTranslation.Name = "tbPrevTranslation";
			this.tbPrevTranslation.ReadOnly = true;
			this.tbPrevTranslation.Size = new System.Drawing.Size(374, 150);
			this.tbPrevTranslation.TabIndex = 0;
			// 
			// tbOriginal
			// 
			this.tbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOriginal.Location = new System.Drawing.Point(3, 159);
			this.tbOriginal.Multiline = true;
			this.tbOriginal.Name = "tbOriginal";
			this.tbOriginal.ReadOnly = true;
			this.tbOriginal.Size = new System.Drawing.Size(344, 150);
			this.tbOriginal.TabIndex = 1;
			// 
			// tbTranslation
			// 
			this.tbTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbTranslation.Location = new System.Drawing.Point(3, 159);
			this.tbTranslation.Multiline = true;
			this.tbTranslation.Name = "tbTranslation";
			this.tbTranslation.Size = new System.Drawing.Size(374, 150);
			this.tbTranslation.TabIndex = 1;
			// 
			// tbNextOriginal
			// 
			this.tbNextOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbNextOriginal.Location = new System.Drawing.Point(3, 315);
			this.tbNextOriginal.Multiline = true;
			this.tbNextOriginal.Name = "tbNextOriginal";
			this.tbNextOriginal.ReadOnly = true;
			this.tbNextOriginal.Size = new System.Drawing.Size(344, 153);
			this.tbNextOriginal.TabIndex = 2;
			// 
			// tbNextTranslation
			// 
			this.tbNextTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbNextTranslation.Location = new System.Drawing.Point(3, 315);
			this.tbNextTranslation.Multiline = true;
			this.tbNextTranslation.Name = "tbNextTranslation";
			this.tbNextTranslation.ReadOnly = true;
			this.tbNextTranslation.Size = new System.Drawing.Size(374, 153);
			this.tbNextTranslation.TabIndex = 2;
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(55, 20);
			this.toolStripMenuItem3.Text = "↓↓↓";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(55, 20);
			this.toolStripMenuItem4.Text = "↑↑↑";
			this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 473);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(734, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(23, 17);
			this.lblStatus.Text = "0/0";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(55, 20);
			this.toolStripMenuItem5.Text = "→→→";
			this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 495);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openBMGToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openSessionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveSessionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportBMGToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox tbPrevOriginal;
		private System.Windows.Forms.TextBox tbOriginal;
		private System.Windows.Forms.TextBox tbPrevTranslation;
		private System.Windows.Forms.TextBox tbTranslation;
		private System.Windows.Forms.TextBox tbNextOriginal;
		private System.Windows.Forms.TextBox tbNextTranslation;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
	}
}

