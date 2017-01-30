using System.Windows.Forms;
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

        public MetaDataManager MetaManager;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.EditorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AvailableObjectsPanel = new System.Windows.Forms.Panel();
            this.AvailableObjectsLabel = new System.Windows.Forms.Label();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.PropertiesLabel = new System.Windows.Forms.Label();
            this.ObjectsTreeView = new System.Windows.Forms.TreeView();
            this.panel = new System.Windows.Forms.Panel();
            this.PropertiesGridView = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoNextButton = new System.Windows.Forms.Button();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.WorkPanel = new System.Windows.Forms.Panel();
            this.MainPage = new tryhard.DrawPage();
            this.MenuStrip.SuspendLayout();
            this.AvailableObjectsPanel.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).BeginInit();
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
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.EditingMenuItem,
            this.EditorMenuItem,
            this.InfoMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1264, 24);
            this.MenuStrip.TabIndex = 16;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // EditorMenuItem
            // 
            this.EditorMenuItem.Name = "EditorMenuItem";
            this.EditorMenuItem.Size = new System.Drawing.Size(69, 20);
            this.EditorMenuItem.Text = "Редактор";
            this.EditorMenuItem.Click += new System.EventHandler(this.EditorMenuItem_Click);
            // 
            // AvailableObjectsPanel
            // 
            this.AvailableObjectsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableObjectsPanel.BackColor = System.Drawing.Color.Orange;
            this.AvailableObjectsPanel.Controls.Add(this.AvailableObjectsLabel);
            this.AvailableObjectsPanel.Location = new System.Drawing.Point(5, 0);
            this.AvailableObjectsPanel.Name = "AvailableObjectsPanel";
            this.AvailableObjectsPanel.Size = new System.Drawing.Size(243, 22);
            this.AvailableObjectsPanel.TabIndex = 19;
            // 
            // AvailableObjectsLabel
            // 
            this.AvailableObjectsLabel.AutoSize = true;
            this.AvailableObjectsLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AvailableObjectsLabel.Location = new System.Drawing.Point(3, 4);
            this.AvailableObjectsLabel.Name = "AvailableObjectsLabel";
            this.AvailableObjectsLabel.Size = new System.Drawing.Size(119, 16);
            this.AvailableObjectsLabel.TabIndex = 18;
            this.AvailableObjectsLabel.Text = "Доступные объекты";
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertiesPanel.BackColor = System.Drawing.Color.Orange;
            this.PropertiesPanel.Controls.Add(this.PropertiesLabel);
            this.PropertiesPanel.Location = new System.Drawing.Point(5, 303);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Size = new System.Drawing.Size(243, 22);
            this.PropertiesPanel.TabIndex = 21;
            // 
            // PropertiesLabel
            // 
            this.PropertiesLabel.AutoSize = true;
            this.PropertiesLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PropertiesLabel.ForeColor = System.Drawing.Color.Black;
            this.PropertiesLabel.Location = new System.Drawing.Point(3, 4);
            this.PropertiesLabel.Name = "PropertiesLabel";
            this.PropertiesLabel.Size = new System.Drawing.Size(58, 16);
            this.PropertiesLabel.TabIndex = 18;
            this.PropertiesLabel.Text = "Свойства";
            // 
            // ObjectsTreeView
            // 
            this.ObjectsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectsTreeView.Location = new System.Drawing.Point(5, 22);
            this.ObjectsTreeView.Name = "ObjectsTreeView";
            this.ObjectsTreeView.Size = new System.Drawing.Size(243, 278);
            this.ObjectsTreeView.TabIndex = 23;
            this.ObjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ObjectsTreeView_AfterSelect);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.PropertiesGridView);
            this.panel.Controls.Add(this.GoNextButton);
            this.panel.Controls.Add(this.GoBackButton);
            this.panel.Controls.Add(this.ObjectsTreeView);
            this.panel.Controls.Add(this.PropertiesPanel);
            this.panel.Controls.Add(this.AvailableObjectsPanel);
            this.panel.Location = new System.Drawing.Point(1010, 28);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(254, 654);
            this.panel.TabIndex = 0;
            // 
            // PropertiesGridView
            // 
            this.PropertiesGridView.AllowUserToAddRows = false;
            this.PropertiesGridView.AllowUserToResizeColumns = false;
            this.PropertiesGridView.AllowUserToResizeRows = false;
            this.PropertiesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertiesGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.PropertiesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PropertiesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameCol,
            this.ValueCol});
            this.PropertiesGridView.Location = new System.Drawing.Point(5, 325);
            this.PropertiesGridView.Name = "PropertiesGridView";
            this.PropertiesGridView.ReadOnly = true;
            this.PropertiesGridView.RowHeadersVisible = false;
            this.PropertiesGridView.RowTemplate.Height = 20;
            this.PropertiesGridView.Size = new System.Drawing.Size(243, 285);
            this.PropertiesGridView.TabIndex = 30;
            // 
            // NameCol
            // 
            this.NameCol.HeaderText = "Название";
            this.NameCol.Name = "NameCol";
            this.NameCol.ReadOnly = true;
            this.NameCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NameCol.Width = 120;
            // 
            // ValueCol
            // 
            this.ValueCol.HeaderText = "Значение";
            this.ValueCol.Name = "ValueCol";
            this.ValueCol.ReadOnly = true;
            this.ValueCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValueCol.Width = 120;
            // 
            // GoNextButton
            // 
            this.GoNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoNextButton.BackColor = System.Drawing.Color.White;
            this.GoNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoNextButton.Location = new System.Drawing.Point(126, 613);
            this.GoNextButton.Name = "GoNextButton";
            this.GoNextButton.Size = new System.Drawing.Size(122, 37);
            this.GoNextButton.TabIndex = 29;
            this.GoNextButton.Text = "next";
            this.GoNextButton.UseVisualStyleBackColor = false;
            this.GoNextButton.Click += new System.EventHandler(this.GoNextButton_Click);
            // 
            // GoBackButton
            // 
            this.GoBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoBackButton.BackColor = System.Drawing.Color.White;
            this.GoBackButton.Enabled = false;
            this.GoBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoBackButton.Location = new System.Drawing.Point(5, 613);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(122, 37);
            this.GoBackButton.TabIndex = 28;
            this.GoBackButton.Text = "back";
            this.GoBackButton.UseVisualStyleBackColor = false;
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
            // 
            // WorkPanel
            // 
            this.WorkPanel.BackColor = System.Drawing.Color.White;
            this.WorkPanel.Location = new System.Drawing.Point(3, 28);
            this.WorkPanel.Name = "WorkPanel";
            this.WorkPanel.Size = new System.Drawing.Size(1006, 650);
            this.WorkPanel.TabIndex = 0;
            // 
            // MainPage
            // 
            this.MainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPage.BackColor = System.Drawing.Color.White;
            this.MainPage.Location = new System.Drawing.Point(4, 28);
            this.MainPage.Name = "MainPage";
            this.MainPage.Size = new System.Drawing.Size(1006, 650);
            this.MainPage.TabIndex = 22;
            this.MainPage.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPage_Paint);
            this.MainPage.DoubleClick += new System.EventHandler(this.MainPage_DoubleClick);
            this.MainPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseDown);
            this.MainPage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseMove);
            this.MainPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.MainPage);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.WorkPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.Text = "Gaby";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.AvailableObjectsPanel.ResumeLayout(false);
            this.AvailableObjectsPanel.PerformLayout();
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem EditingMenuItem;
        private ToolStripMenuItem InfoMenuItem;
        private MenuStrip MenuStrip;
        public DrawPage MainPage;
        private ToolStripMenuItem EditorMenuItem;
        private Panel AvailableObjectsPanel;
        private Label AvailableObjectsLabel;
        private Panel PropertiesPanel;
        private Label PropertiesLabel;
        private TreeView ObjectsTreeView;
        private Panel panel;
        private Button GoBackButton;
        private Button GoNextButton;
        private Panel WorkPanel;
        public DataGridView PropertiesGridView;
        private DataGridViewTextBoxColumn NameCol;
        private DataGridViewTextBoxColumn ValueCol;
    }
}

