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
            this.GoNextButton = new System.Windows.Forms.Button();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.PropertiesGridView = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkInfoPanel = new System.Windows.Forms.Panel();
            this.WorkPanel = new System.Windows.Forms.Panel();
            this.UpStructurePanel = new System.Windows.Forms.Panel();
            this.ParametersLabel = new System.Windows.Forms.Label();
            this.StructureParametersPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.YWaterUpDown = new System.Windows.Forms.NumericUpDown();
            this.YWaterLabel = new System.Windows.Forms.Label();
            this.YMatUpDown = new System.Windows.Forms.NumericUpDown();
            this.YMatLabel = new System.Windows.Forms.Label();
            this.DWallCellUpDown = new System.Windows.Forms.NumericUpDown();
            this.DWallCellLabel = new System.Windows.Forms.Label();
            this.WidthCellUpDown = new System.Windows.Forms.NumericUpDown();
            this.WidthCellLabel = new System.Windows.Forms.Label();
            this.HWave50UpDown = new System.Windows.Forms.NumericUpDown();
            this.HWave001UpDown = new System.Windows.Forms.NumericUpDown();
            this.HWave50Label = new System.Windows.Forms.Label();
            this.HWave001Label = new System.Windows.Forms.Label();
            this.GlobalWaterDepthUpDown = new System.Windows.Forms.NumericUpDown();
            this.LocalWaterDepthUpDown = new System.Windows.Forms.NumericUpDown();
            this.LocalWaterDepthLabel = new System.Windows.Forms.Label();
            this.GlobalWaterDepthLabel = new System.Windows.Forms.Label();
            this.StructuresGridView = new System.Windows.Forms.DataGridView();
            this.StructuresList = new System.Windows.Forms.Label();
            this.CalculatedStructures = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.BackToSchemeButton = new System.Windows.Forms.Button();
            this.UpStructureSizeLabel = new System.Windows.Forms.Label();
            this.UpStructureSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.MainPage = new tryhard.DrawPage();
            this.TypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StructureTypesDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuStrip.SuspendLayout();
            this.AvailableObjectsPanel.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).BeginInit();
            this.UpStructurePanel.SuspendLayout();
            this.StructureParametersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YWaterUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMatUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DWallCellUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthCellUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HWave50UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HWave001UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalWaterDepthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalWaterDepthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StructuresGridView)).BeginInit();
            this.CalculatedStructures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpStructureSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StructureTypesDataGrid)).BeginInit();
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
            this.MenuStrip.Size = new System.Drawing.Size(1348, 24);
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
            this.PropertiesPanel.Location = new System.Drawing.Point(5, 345);
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
            this.ObjectsTreeView.Size = new System.Drawing.Size(243, 320);
            this.ObjectsTreeView.TabIndex = 23;
            this.ObjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ObjectsTreeView_AfterSelect);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.GoNextButton);
            this.panel.Controls.Add(this.GoBackButton);
            this.panel.Controls.Add(this.ObjectsTreeView);
            this.panel.Controls.Add(this.PropertiesPanel);
            this.panel.Controls.Add(this.AvailableObjectsPanel);
            this.panel.Controls.Add(this.PropertiesGridView);
            this.panel.Controls.Add(this.LinkInfoPanel);
            this.panel.Location = new System.Drawing.Point(1094, 28);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(254, 696);
            this.panel.TabIndex = 0;
            // 
            // GoNextButton
            // 
            this.GoNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoNextButton.BackColor = System.Drawing.Color.White;
            this.GoNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoNextButton.Location = new System.Drawing.Point(126, 655);
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
            this.GoBackButton.Location = new System.Drawing.Point(5, 655);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(122, 37);
            this.GoBackButton.TabIndex = 28;
            this.GoBackButton.Text = "back";
            this.GoBackButton.UseVisualStyleBackColor = false;
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
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
            this.PropertiesGridView.Location = new System.Drawing.Point(5, 367);
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
            // LinkInfoPanel
            // 
            this.LinkInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkInfoPanel.BackColor = System.Drawing.Color.White;
            this.LinkInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LinkInfoPanel.Location = new System.Drawing.Point(5, 367);
            this.LinkInfoPanel.Name = "LinkInfoPanel";
            this.LinkInfoPanel.Size = new System.Drawing.Size(243, 285);
            this.LinkInfoPanel.TabIndex = 31;
            // 
            // WorkPanel
            // 
            this.WorkPanel.BackColor = System.Drawing.Color.White;
            this.WorkPanel.Location = new System.Drawing.Point(4, 29);
            this.WorkPanel.Name = "WorkPanel";
            this.WorkPanel.Size = new System.Drawing.Size(1090, 650);
            this.WorkPanel.TabIndex = 0;
            this.WorkPanel.Visible = false;
            // 
            // UpStructurePanel
            // 
            this.UpStructurePanel.BackColor = System.Drawing.Color.White;
            this.UpStructurePanel.Controls.Add(this.BackToSchemeButton);
            this.UpStructurePanel.Controls.Add(this.SaveButton);
            this.UpStructurePanel.Controls.Add(this.CalculatedStructures);
            this.UpStructurePanel.Controls.Add(this.ParametersLabel);
            this.UpStructurePanel.Controls.Add(this.StructureParametersPanel);
            this.UpStructurePanel.Controls.Add(this.StructuresGridView);
            this.UpStructurePanel.Controls.Add(this.StructuresList);
            this.UpStructurePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpStructurePanel.Location = new System.Drawing.Point(0, 24);
            this.UpStructurePanel.Name = "UpStructurePanel";
            this.UpStructurePanel.Size = new System.Drawing.Size(1348, 704);
            this.UpStructurePanel.TabIndex = 0;
            // 
            // ParametersLabel
            // 
            this.ParametersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ParametersLabel.AutoSize = true;
            this.ParametersLabel.Location = new System.Drawing.Point(31, 400);
            this.ParametersLabel.Name = "ParametersLabel";
            this.ParametersLabel.Size = new System.Drawing.Size(69, 13);
            this.ParametersLabel.TabIndex = 29;
            this.ParametersLabel.Text = "Параметры:";
            // 
            // StructureParametersPanel
            // 
            this.StructureParametersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StructureParametersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StructureParametersPanel.Controls.Add(this.UpStructureSizeUpDown);
            this.StructureParametersPanel.Controls.Add(this.UpStructureSizeLabel);
            this.StructureParametersPanel.Controls.Add(this.button1);
            this.StructureParametersPanel.Controls.Add(this.YWaterUpDown);
            this.StructureParametersPanel.Controls.Add(this.YWaterLabel);
            this.StructureParametersPanel.Controls.Add(this.YMatUpDown);
            this.StructureParametersPanel.Controls.Add(this.YMatLabel);
            this.StructureParametersPanel.Controls.Add(this.DWallCellUpDown);
            this.StructureParametersPanel.Controls.Add(this.DWallCellLabel);
            this.StructureParametersPanel.Controls.Add(this.WidthCellUpDown);
            this.StructureParametersPanel.Controls.Add(this.WidthCellLabel);
            this.StructureParametersPanel.Controls.Add(this.HWave50UpDown);
            this.StructureParametersPanel.Controls.Add(this.HWave001UpDown);
            this.StructureParametersPanel.Controls.Add(this.HWave50Label);
            this.StructureParametersPanel.Controls.Add(this.HWave001Label);
            this.StructureParametersPanel.Controls.Add(this.GlobalWaterDepthUpDown);
            this.StructureParametersPanel.Controls.Add(this.LocalWaterDepthUpDown);
            this.StructureParametersPanel.Controls.Add(this.LocalWaterDepthLabel);
            this.StructureParametersPanel.Controls.Add(this.GlobalWaterDepthLabel);
            this.StructureParametersPanel.Location = new System.Drawing.Point(34, 424);
            this.StructureParametersPanel.Name = "StructureParametersPanel";
            this.StructureParametersPanel.Size = new System.Drawing.Size(634, 231);
            this.StructureParametersPanel.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(257, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 27);
            this.button1.TabIndex = 30;
            this.button1.Text = "Рассчитать";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // YWaterUpDown
            // 
            this.YWaterUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.YWaterUpDown.Location = new System.Drawing.Point(553, 126);
            this.YWaterUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.YWaterUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.YWaterUpDown.Name = "YWaterUpDown";
            this.YWaterUpDown.Size = new System.Drawing.Size(50, 20);
            this.YWaterUpDown.TabIndex = 18;
            this.YWaterUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // YWaterLabel
            // 
            this.YWaterLabel.AutoSize = true;
            this.YWaterLabel.Location = new System.Drawing.Point(340, 128);
            this.YWaterLabel.Name = "YWaterLabel";
            this.YWaterLabel.Size = new System.Drawing.Size(126, 13);
            this.YWaterLabel.TabIndex = 17;
            this.YWaterLabel.Text = "Удельный вес воды, кг";
            // 
            // YMatUpDown
            // 
            this.YMatUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.YMatUpDown.Location = new System.Drawing.Point(234, 126);
            this.YMatUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.YMatUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.YMatUpDown.Name = "YMatUpDown";
            this.YMatUpDown.Size = new System.Drawing.Size(50, 20);
            this.YMatUpDown.TabIndex = 16;
            this.YMatUpDown.Value = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            // 
            // YMatLabel
            // 
            this.YMatLabel.AutoSize = true;
            this.YMatLabel.Location = new System.Drawing.Point(23, 128);
            this.YMatLabel.Name = "YMatLabel";
            this.YMatLabel.Size = new System.Drawing.Size(155, 13);
            this.YMatLabel.TabIndex = 15;
            this.YMatLabel.Text = "Удельный вес материала, кг";
            // 
            // DWallCellUpDown
            // 
            this.DWallCellUpDown.DecimalPlaces = 2;
            this.DWallCellUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.DWallCellUpDown.Location = new System.Drawing.Point(553, 92);
            this.DWallCellUpDown.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.DWallCellUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.DWallCellUpDown.Name = "DWallCellUpDown";
            this.DWallCellUpDown.Size = new System.Drawing.Size(50, 20);
            this.DWallCellUpDown.TabIndex = 14;
            this.DWallCellUpDown.Value = new decimal(new int[] {
            75,
            0,
            0,
            131072});
            // 
            // DWallCellLabel
            // 
            this.DWallCellLabel.AutoSize = true;
            this.DWallCellLabel.Location = new System.Drawing.Point(340, 94);
            this.DWallCellLabel.Name = "DWallCellLabel";
            this.DWallCellLabel.Size = new System.Drawing.Size(143, 13);
            this.DWallCellLabel.TabIndex = 13;
            this.DWallCellLabel.Text = "Толщина стенки ячейки, м";
            // 
            // WidthCellUpDown
            // 
            this.WidthCellUpDown.Location = new System.Drawing.Point(234, 92);
            this.WidthCellUpDown.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.WidthCellUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.WidthCellUpDown.Name = "WidthCellUpDown";
            this.WidthCellUpDown.Size = new System.Drawing.Size(50, 20);
            this.WidthCellUpDown.TabIndex = 12;
            this.WidthCellUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // WidthCellLabel
            // 
            this.WidthCellLabel.AutoSize = true;
            this.WidthCellLabel.Location = new System.Drawing.Point(23, 94);
            this.WidthCellLabel.Name = "WidthCellLabel";
            this.WidthCellLabel.Size = new System.Drawing.Size(188, 13);
            this.WidthCellLabel.TabIndex = 11;
            this.WidthCellLabel.Text = "Размер ребра основания ячейки, м";
            // 
            // HWave50UpDown
            // 
            this.HWave50UpDown.Location = new System.Drawing.Point(553, 58);
            this.HWave50UpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.HWave50UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HWave50UpDown.Name = "HWave50UpDown";
            this.HWave50UpDown.Size = new System.Drawing.Size(50, 20);
            this.HWave50UpDown.TabIndex = 10;
            this.HWave50UpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // HWave001UpDown
            // 
            this.HWave001UpDown.Location = new System.Drawing.Point(553, 24);
            this.HWave001UpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.HWave001UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HWave001UpDown.Name = "HWave001UpDown";
            this.HWave001UpDown.Size = new System.Drawing.Size(50, 20);
            this.HWave001UpDown.TabIndex = 9;
            this.HWave001UpDown.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // HWave50Label
            // 
            this.HWave50Label.AutoSize = true;
            this.HWave50Label.Location = new System.Drawing.Point(340, 60);
            this.HWave50Label.Name = "HWave50Label";
            this.HWave50Label.Size = new System.Drawing.Size(202, 13);
            this.HWave50Label.TabIndex = 8;
            this.HWave50Label.Text = "Высота волны 50% обеспеченности, м";
            // 
            // HWave001Label
            // 
            this.HWave001Label.AutoSize = true;
            this.HWave001Label.Location = new System.Drawing.Point(340, 26);
            this.HWave001Label.Name = "HWave001Label";
            this.HWave001Label.Size = new System.Drawing.Size(211, 13);
            this.HWave001Label.TabIndex = 7;
            this.HWave001Label.Text = "Высота волны 0.01% обеспеченности, м";
            // 
            // GlobalWaterDepthUpDown
            // 
            this.GlobalWaterDepthUpDown.Location = new System.Drawing.Point(234, 58);
            this.GlobalWaterDepthUpDown.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.GlobalWaterDepthUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.GlobalWaterDepthUpDown.Name = "GlobalWaterDepthUpDown";
            this.GlobalWaterDepthUpDown.Size = new System.Drawing.Size(50, 20);
            this.GlobalWaterDepthUpDown.TabIndex = 6;
            this.GlobalWaterDepthUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // LocalWaterDepthUpDown
            // 
            this.LocalWaterDepthUpDown.Location = new System.Drawing.Point(234, 24);
            this.LocalWaterDepthUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.LocalWaterDepthUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.LocalWaterDepthUpDown.Name = "LocalWaterDepthUpDown";
            this.LocalWaterDepthUpDown.Size = new System.Drawing.Size(50, 20);
            this.LocalWaterDepthUpDown.TabIndex = 5;
            this.LocalWaterDepthUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // LocalWaterDepthLabel
            // 
            this.LocalWaterDepthLabel.AutoSize = true;
            this.LocalWaterDepthLabel.Location = new System.Drawing.Point(23, 26);
            this.LocalWaterDepthLabel.Name = "LocalWaterDepthLabel";
            this.LocalWaterDepthLabel.Size = new System.Drawing.Size(207, 13);
            this.LocalWaterDepthLabel.TabIndex = 3;
            this.LocalWaterDepthLabel.Text = "Глубина залива в месте возведения, м";
            // 
            // GlobalWaterDepthLabel
            // 
            this.GlobalWaterDepthLabel.AutoSize = true;
            this.GlobalWaterDepthLabel.Location = new System.Drawing.Point(23, 60);
            this.GlobalWaterDepthLabel.Name = "GlobalWaterDepthLabel";
            this.GlobalWaterDepthLabel.Size = new System.Drawing.Size(159, 13);
            this.GlobalWaterDepthLabel.TabIndex = 1;
            this.GlobalWaterDepthLabel.Text = "Эксплуатационная глубина, м";
            // 
            // StructuresGridView
            // 
            this.StructuresGridView.AllowUserToAddRows = false;
            this.StructuresGridView.AllowUserToResizeColumns = false;
            this.StructuresGridView.AllowUserToResizeRows = false;
            this.StructuresGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.StructuresGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.StructuresGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StructuresGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeColumn,
            this.dataGridViewTextBoxColumn1,
            this.WeightColumn});
            this.StructuresGridView.Location = new System.Drawing.Point(34, 51);
            this.StructuresGridView.Name = "StructuresGridView";
            this.StructuresGridView.ReadOnly = true;
            this.StructuresGridView.RowHeadersVisible = false;
            this.StructuresGridView.RowTemplate.Height = 20;
            this.StructuresGridView.Size = new System.Drawing.Size(634, 331);
            this.StructuresGridView.TabIndex = 27;
            this.StructuresGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // StructuresList
            // 
            this.StructuresList.AutoSize = true;
            this.StructuresList.Location = new System.Drawing.Point(31, 28);
            this.StructuresList.Name = "StructuresList";
            this.StructuresList.Size = new System.Drawing.Size(112, 13);
            this.StructuresList.TabIndex = 1;
            this.StructuresList.Text = "Список комплексов:";
            // 
            // CalculatedStructures
            // 
            this.CalculatedStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalculatedStructures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CalculatedStructures.Controls.Add(this.StructureTypesDataGrid);
            this.CalculatedStructures.Location = new System.Drawing.Point(716, 51);
            this.CalculatedStructures.Name = "CalculatedStructures";
            this.CalculatedStructures.Size = new System.Drawing.Size(594, 604);
            this.CalculatedStructures.TabIndex = 30;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.White;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Location = new System.Drawing.Point(1188, 666);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(122, 27);
            this.SaveButton.TabIndex = 31;
            this.SaveButton.Text = "Сохранить проект";
            this.SaveButton.UseVisualStyleBackColor = false;
            // 
            // BackToSchemeButton
            // 
            this.BackToSchemeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BackToSchemeButton.BackColor = System.Drawing.Color.White;
            this.BackToSchemeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BackToSchemeButton.Location = new System.Drawing.Point(34, 666);
            this.BackToSchemeButton.Name = "BackToSchemeButton";
            this.BackToSchemeButton.Size = new System.Drawing.Size(122, 27);
            this.BackToSchemeButton.TabIndex = 32;
            this.BackToSchemeButton.Text = "Вернуться к схеме";
            this.BackToSchemeButton.UseVisualStyleBackColor = false;
            // 
            // UpStructureSizeLabel
            // 
            this.UpStructureSizeLabel.AutoSize = true;
            this.UpStructureSizeLabel.Location = new System.Drawing.Point(23, 160);
            this.UpStructureSizeLabel.Name = "UpStructureSizeLabel";
            this.UpStructureSizeLabel.Size = new System.Drawing.Size(199, 13);
            this.UpStructureSizeLabel.TabIndex = 31;
            this.UpStructureSizeLabel.Text = "Размер площадки верхнего строения";
            // 
            // UpStructureSizeUpDown
            // 
            this.UpStructureSizeUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.UpStructureSizeUpDown.Location = new System.Drawing.Point(234, 158);
            this.UpStructureSizeUpDown.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.UpStructureSizeUpDown.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.UpStructureSizeUpDown.Name = "UpStructureSizeUpDown";
            this.UpStructureSizeUpDown.Size = new System.Drawing.Size(50, 20);
            this.UpStructureSizeUpDown.TabIndex = 32;
            this.UpStructureSizeUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // MainPage
            // 
            this.MainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPage.BackColor = System.Drawing.Color.White;
            this.MainPage.Location = new System.Drawing.Point(4, 29);
            this.MainPage.Name = "MainPage";
            this.MainPage.Size = new System.Drawing.Size(1090, 695);
            this.MainPage.TabIndex = 22;
            this.MainPage.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPage_Paint);
            this.MainPage.DoubleClick += new System.EventHandler(this.MainPage_DoubleClick);
            this.MainPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseDown);
            this.MainPage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseMove);
            this.MainPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseUp);
            // 
            // TypeColumn
            // 
            this.TypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TypeColumn.FillWeight = 131.5019F;
            this.TypeColumn.HeaderText = "Тип";
            this.TypeColumn.Name = "TypeColumn";
            this.TypeColumn.ReadOnly = true;
            this.TypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 147.9397F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Название";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WeightColumn
            // 
            this.WeightColumn.HeaderText = "Масса, кг";
            this.WeightColumn.Name = "WeightColumn";
            this.WeightColumn.ReadOnly = true;
            this.WeightColumn.Width = 130;
            // 
            // StructureTypesDataGrid
            // 
            this.StructureTypesDataGrid.AllowUserToAddRows = false;
            this.StructureTypesDataGrid.AllowUserToResizeColumns = false;
            this.StructureTypesDataGrid.AllowUserToResizeRows = false;
            this.StructureTypesDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.StructureTypesDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.StructureTypesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StructureTypesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.StructureTypesDataGrid.Location = new System.Drawing.Point(-1, -1);
            this.StructureTypesDataGrid.Name = "StructureTypesDataGrid";
            this.StructureTypesDataGrid.ReadOnly = true;
            this.StructureTypesDataGrid.RowHeadersVisible = false;
            this.StructureTypesDataGrid.RowTemplate.Height = 20;
            this.StructureTypesDataGrid.Size = new System.Drawing.Size(594, 604);
            this.StructureTypesDataGrid.TabIndex = 28;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 131.5019F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Тип";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.FillWeight = 147.9397F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Название";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Масса, кг";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 130;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1348, 728);
            this.Controls.Add(this.UpStructurePanel);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.WorkPanel);
            this.Controls.Add(this.MainPage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1364, 766);
            this.Name = "MainForm";
            this.Text = "Gaby";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.AvailableObjectsPanel.ResumeLayout(false);
            this.AvailableObjectsPanel.PerformLayout();
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).EndInit();
            this.UpStructurePanel.ResumeLayout(false);
            this.UpStructurePanel.PerformLayout();
            this.StructureParametersPanel.ResumeLayout(false);
            this.StructureParametersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YWaterUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMatUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DWallCellUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthCellUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HWave50UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HWave001UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalWaterDepthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocalWaterDepthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StructuresGridView)).EndInit();
            this.CalculatedStructures.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UpStructureSizeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StructureTypesDataGrid)).EndInit();
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
        private Panel LinkInfoPanel;
        private Panel UpStructurePanel;
        private Label StructuresList;
        public DataGridView StructuresGridView;
        private Panel StructureParametersPanel;
        private Label ParametersLabel;
        private Label LocalWaterDepthLabel;
        private Label GlobalWaterDepthLabel;
        private NumericUpDown GlobalWaterDepthUpDown;
        private NumericUpDown LocalWaterDepthUpDown;
        private Label HWave001Label;
        private Label HWave50Label;
        private NumericUpDown HWave50UpDown;
        private NumericUpDown HWave001UpDown;
        private NumericUpDown WidthCellUpDown;
        private Label WidthCellLabel;
        private Label DWallCellLabel;
        private NumericUpDown DWallCellUpDown;
        private NumericUpDown YMatUpDown;
        private Label YMatLabel;
        private Label YWaterLabel;
        private NumericUpDown YWaterUpDown;
        private Button button1;
        private Panel CalculatedStructures;
        private Button BackToSchemeButton;
        private Button SaveButton;
        private NumericUpDown UpStructureSizeUpDown;
        private Label UpStructureSizeLabel;
        private DataGridViewTextBoxColumn TypeColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn WeightColumn;
        public DataGridView StructureTypesDataGrid;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}

