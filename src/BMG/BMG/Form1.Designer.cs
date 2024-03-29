﻿namespace BMG
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
            this.saveSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportBMGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastTranslatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleTranslateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextUntranslatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbPrevOriginal = new System.Windows.Forms.TextBox();
            this.tbOriginal = new System.Windows.Forms.TextBox();
            this.tbNextOriginal = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbPrevTranslation = new System.Windows.Forms.TextBox();
            this.tbNextTranslation = new System.Windows.Forms.TextBox();
            this.tbTranslation = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
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
            this.toolStripMenuItem5,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(847, 25);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openBMGToolStripMenuItem
            // 
            this.openBMGToolStripMenuItem.Name = "openBMGToolStripMenuItem";
            this.openBMGToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openBMGToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openBMGToolStripMenuItem.Text = "Open BMG...";
            this.openBMGToolStripMenuItem.Click += new System.EventHandler(this.openBMGToolStripMenuItem_Click);
            // 
            // openSessionToolStripMenuItem
            // 
            this.openSessionToolStripMenuItem.Name = "openSessionToolStripMenuItem";
            this.openSessionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.openSessionToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openSessionToolStripMenuItem.Text = "Open Session...";
            this.openSessionToolStripMenuItem.Click += new System.EventHandler(this.openSessionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(206, 6);
            // 
            // saveSessionToolStripMenuItem
            // 
            this.saveSessionToolStripMenuItem.Name = "saveSessionToolStripMenuItem";
            this.saveSessionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveSessionToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.saveSessionToolStripMenuItem.Text = "Save Session...";
            this.saveSessionToolStripMenuItem.Click += new System.EventHandler(this.saveSessionToolStripMenuItem_Click);
            // 
            // exportBMGToolStripMenuItem
            // 
            this.exportBMGToolStripMenuItem.Name = "exportBMGToolStripMenuItem";
            this.exportBMGToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.exportBMGToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exportBMGToolStripMenuItem.Text = "Export BMG...";
            this.exportBMGToolStripMenuItem.Click += new System.EventHandler(this.exportBMGToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(206, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.ShowShortcutKeys = false;
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(38, 21);
            this.toolStripMenuItem4.Text = "↑↑↑";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(38, 21);
            this.toolStripMenuItem3.Text = "↓↓↓";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(56, 21);
            this.toolStripMenuItem5.Text = "→→→";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lastTranslatedToolStripMenuItem,
            this.fontToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.googleTranslateToolStripMenuItem,
            this.bookmarksToolStripMenuItem,
            this.nextUntranslatedToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // lastTranslatedToolStripMenuItem
            // 
            this.lastTranslatedToolStripMenuItem.Name = "lastTranslatedToolStripMenuItem";
            this.lastTranslatedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.lastTranslatedToolStripMenuItem.Text = "Last Translated";
            this.lastTranslatedToolStripMenuItem.Click += new System.EventHandler(this.lastTranslatedToolStripMenuItem_Click);
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.fontToolStripMenuItem.Text = "Font...";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.replaceToolStripMenuItem.Text = "Replace...";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // googleTranslateToolStripMenuItem
            // 
            this.googleTranslateToolStripMenuItem.Name = "googleTranslateToolStripMenuItem";
            this.googleTranslateToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.googleTranslateToolStripMenuItem.Text = "Google Translate";
            this.googleTranslateToolStripMenuItem.Click += new System.EventHandler(this.googleTranslateToolStripMenuItem_Click);
            // 
            // bookmarksToolStripMenuItem
            // 
            this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
            this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookmarksToolStripMenuItem.Text = "Bookmarks...";
            this.bookmarksToolStripMenuItem.Click += new System.EventHandler(this.bookmarksToolStripMenuItem_Click);
            // 
            // nextUntranslatedToolStripMenuItem
            // 
            this.nextUntranslatedToolStripMenuItem.Name = "nextUntranslatedToolStripMenuItem";
            this.nextUntranslatedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.nextUntranslatedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.nextUntranslatedToolStripMenuItem.Text = "Next Untranslated";
            this.nextUntranslatedToolStripMenuItem.Click += new System.EventHandler(this.nextUntranslatedToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(830, 448);
            this.splitContainer1.SplitterDistance = 395;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(395, 448);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbPrevOriginal
            // 
            this.tbPrevOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPrevOriginal.Location = new System.Drawing.Point(3, 3);
            this.tbPrevOriginal.Multiline = true;
            this.tbPrevOriginal.Name = "tbPrevOriginal";
            this.tbPrevOriginal.ReadOnly = true;
            this.tbPrevOriginal.Size = new System.Drawing.Size(389, 106);
            this.tbPrevOriginal.TabIndex = 0;
            // 
            // tbOriginal
            // 
            this.tbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOriginal.Location = new System.Drawing.Point(3, 115);
            this.tbOriginal.Multiline = true;
            this.tbOriginal.Name = "tbOriginal";
            this.tbOriginal.ReadOnly = true;
            this.tbOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOriginal.Size = new System.Drawing.Size(389, 218);
            this.tbOriginal.TabIndex = 1;
            this.tbOriginal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbOriginal_MouseUp);
            // 
            // tbNextOriginal
            // 
            this.tbNextOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNextOriginal.Location = new System.Drawing.Point(3, 339);
            this.tbNextOriginal.Multiline = true;
            this.tbNextOriginal.Name = "tbNextOriginal";
            this.tbNextOriginal.ReadOnly = true;
            this.tbNextOriginal.Size = new System.Drawing.Size(389, 106);
            this.tbNextOriginal.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tbPrevTranslation, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbNextTranslation, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbTranslation, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(431, 448);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tbPrevTranslation
            // 
            this.tbPrevTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPrevTranslation.Location = new System.Drawing.Point(3, 3);
            this.tbPrevTranslation.Multiline = true;
            this.tbPrevTranslation.Name = "tbPrevTranslation";
            this.tbPrevTranslation.ReadOnly = true;
            this.tbPrevTranslation.Size = new System.Drawing.Size(425, 106);
            this.tbPrevTranslation.TabIndex = 0;
            // 
            // tbNextTranslation
            // 
            this.tbNextTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNextTranslation.Location = new System.Drawing.Point(3, 339);
            this.tbNextTranslation.Multiline = true;
            this.tbNextTranslation.Name = "tbNextTranslation";
            this.tbNextTranslation.ReadOnly = true;
            this.tbNextTranslation.Size = new System.Drawing.Size(425, 106);
            this.tbNextTranslation.TabIndex = 2;
            // 
            // tbTranslation
            // 
            this.tbTranslation.DetectUrls = false;
            this.tbTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTranslation.Location = new System.Drawing.Point(3, 115);
            this.tbTranslation.Name = "tbTranslation";
            this.tbTranslation.Size = new System.Drawing.Size(425, 218);
            this.tbTranslation.TabIndex = 3;
            this.tbTranslation.Text = "";
            this.tbTranslation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbTranslation_KeyUp);
            this.tbTranslation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbTranslation_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 473);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(847, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.lblStatus.Size = new System.Drawing.Size(52, 17);
            this.lblStatus.Text = "0/0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(830, 25);
            this.vScrollBar1.Minimum = 1;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 448);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Value = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 495);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "BMG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
		private System.Windows.Forms.TextBox tbNextOriginal;
		private System.Windows.Forms.TextBox tbNextTranslation;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.RichTextBox tbTranslation;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lastTranslatedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleTranslateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookmarksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nextUntranslatedToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
	}
}

