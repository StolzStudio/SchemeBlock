﻿using System.Windows.Forms;
using System.Drawing;
using HRGN = System.IntPtr;
using HWND = System.IntPtr;
using System.Runtime.InteropServices;

namespace tryhard
{
    public class DrawPage : Panel
    {
        public DrawPage()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }
    }

    partial class MainForm
    {
        [DllImport("Gdi32.dll")]
        public static extern System.IntPtr CreatePolygonRgn(
                      Point[] lppt, 
                      int cPoints,
                      int fnPolyFillMode
                    );

        [DllImport("User32.dll")]
        public static extern int SetWindowRgn(
                      HWND hWnd,
                      HRGN hRgn,
                      bool bRedraw
                    );
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

        private void SetRegionToPanel(Panel APanel, Point[] APoints)
        {
            HRGN Region = CreatePolygonRgn(APoints, APoints.Length, 1);
            SetWindowRgn(APanel.Handle, Region, true);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        ///

        public CMeta Meta;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ProjectTreeListBox = new System.Windows.Forms.ListBox();
            this.ProjectTreeLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PropertiesLabel = new System.Windows.Forms.Label();
            this.MainPage = new tryhard.DrawPage();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileMenuItem.Text = "Файл";
            // 
            // EditingMenuItem
            // 
            this.EditingMenuItem.Name = "EditingMenuItem";
            this.EditingMenuItem.Size = new System.Drawing.Size(59, 20);
            this.EditingMenuItem.Text = "Правка";
            // 
            // InfoMenuItem
            // 
            this.InfoMenuItem.Name = "InfoMenuItem";
            this.InfoMenuItem.Size = new System.Drawing.Size(65, 20);
            this.InfoMenuItem.Text = "Справка";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.EditingMenuItem,
            this.InfoMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ProjectTreeListBox
            // 
            this.ProjectTreeListBox.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProjectTreeListBox.FormattingEnabled = true;
            this.ProjectTreeListBox.ItemHeight = 16;
            this.ProjectTreeListBox.Items.AddRange(new object[] {
            "Месторождение",
            "КД",
            "УПК"});
            this.ProjectTreeListBox.Location = new System.Drawing.Point(1009, 49);
            this.ProjectTreeListBox.Name = "ProjectTreeListBox";
            this.ProjectTreeListBox.Size = new System.Drawing.Size(243, 308);
            this.ProjectTreeListBox.TabIndex = 17;
            // 
            // ProjectTreeLabel
            // 
            this.ProjectTreeLabel.AutoSize = true;
            this.ProjectTreeLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProjectTreeLabel.Location = new System.Drawing.Point(3, 6);
            this.ProjectTreeLabel.Name = "ProjectTreeLabel";
            this.ProjectTreeLabel.Size = new System.Drawing.Size(95, 16);
            this.ProjectTreeLabel.TabIndex = 18;
            this.ProjectTreeLabel.Text = "Дерево проекта";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Controls.Add(this.ProjectTreeLabel);
            this.panel1.Location = new System.Drawing.Point(1009, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 22);
            this.panel1.TabIndex = 19;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1009, 380);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(243, 300);
            this.dataGridView1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Orange;
            this.panel2.Controls.Add(this.PropertiesLabel);
            this.panel2.Location = new System.Drawing.Point(1009, 357);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 22);
            this.panel2.TabIndex = 21;
            // 
            // PropertiesLabel
            // 
            this.PropertiesLabel.AutoSize = true;
            this.PropertiesLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PropertiesLabel.Location = new System.Drawing.Point(3, 6);
            this.PropertiesLabel.Name = "PropertiesLabel";
            this.PropertiesLabel.Size = new System.Drawing.Size(58, 16);
            this.PropertiesLabel.TabIndex = 18;
            this.PropertiesLabel.Text = "Свойства";
            // 
            // MainPage
            // 
            this.MainPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainPage.Location = new System.Drawing.Point(0, 27);
            this.MainPage.Name = "MainPage";
            this.MainPage.Size = new System.Drawing.Size(1003, 653);
            this.MainPage.TabIndex = 22;
            this.MainPage.Click += new System.EventHandler(this.MainPage_Click);
            this.MainPage.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPage_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.MainPage);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ProjectTreeListBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.Text = "Gaby";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem EditingMenuItem;
        private ToolStripMenuItem InfoMenuItem;
        private MenuStrip menuStrip1;
        private ListBox ProjectTreeListBox;
        private Label ProjectTreeLabel;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Panel panel2;
        private Label PropertiesLabel;
        public DrawPage MainPage;
    }
}

