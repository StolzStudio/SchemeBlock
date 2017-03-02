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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.EditorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AvailableObjectsPanel = new System.Windows.Forms.Panel();
            this.AvailableObjectsLabel = new System.Windows.Forms.Label();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.PropertiesLabel = new System.Windows.Forms.Label();
            this.ObjectsTreeView = new System.Windows.Forms.TreeView();
            this.panel = new System.Windows.Forms.Panel();
            this.FieldComboBox = new System.Windows.Forms.ComboBox();
            this.FieldPanel = new System.Windows.Forms.Panel();
            this.FieldLabel = new System.Windows.Forms.Label();
            this.CalculateButton = new System.Windows.Forms.Button();
            this.PropertiesGridView = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkInfoPanel = new System.Windows.Forms.Panel();
            this.UpStructurePanel = new System.Windows.Forms.Panel();
            this.CommonCostTextBox = new System.Windows.Forms.TextBox();
            this.CommonCostLabel = new System.Windows.Forms.Label();
            this.StructureLabel = new System.Windows.Forms.Label();
            this.StructurePanel = new System.Windows.Forms.Panel();
            this.yMatBallastUpDown = new System.Windows.Forms.NumericUpDown();
            this.wCellLabel = new System.Windows.Forms.Label();
            this.yMatBallastLabel = new System.Windows.Forms.Label();
            this.yMatLabel = new System.Windows.Forms.Label();
            this.yMatUpDown = new System.Windows.Forms.NumericUpDown();
            this.dWallCellUpDown = new System.Windows.Forms.NumericUpDown();
            this.dWallCellLabel = new System.Windows.Forms.Label();
            this.wCellUpDown = new System.Windows.Forms.NumericUpDown();
            this.wUpStructureLabel = new System.Windows.Forms.Label();
            this.wUpStructureUpDown = new System.Windows.Forms.NumericUpDown();
            this.StructureTypeLabel = new System.Windows.Forms.Label();
            this.StructureTypePanel = new System.Windows.Forms.Panel();
            this.StructureTypeGridView = new System.Windows.Forms.DataGridView();
            this.ParameterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StructureTypesPanel = new System.Windows.Forms.Panel();
            this.KessonRadioButton = new System.Windows.Forms.RadioButton();
            this.MonolegRadioButton = new System.Windows.Forms.RadioButton();
            this.MultilegRadioButton = new System.Windows.Forms.RadioButton();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.BackToSchemeButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.FieldParametersLabel = new System.Windows.Forms.Label();
            this.FieldParametersPanel = new System.Windows.Forms.Panel();
            this.groundDurabilityUpDown = new System.Windows.Forms.NumericUpDown();
            this.groundDurabilityLabel = new System.Windows.Forms.Label();
            this.speedIceUpDown = new System.Windows.Forms.NumericUpDown();
            this.speedIceLabel = new System.Windows.Forms.Label();
            this.durabilityIceUpDown = new System.Windows.Forms.NumericUpDown();
            this.durabilityIceLabel = new System.Windows.Forms.Label();
            this.dIceUpDown = new System.Windows.Forms.NumericUpDown();
            this.dIceLabel = new System.Windows.Forms.Label();
            this.diameterIceUpDown = new System.Windows.Forms.NumericUpDown();
            this.diameterIceLabel = new System.Windows.Forms.Label();
            this.dLocalWaterLabel = new System.Windows.Forms.Label();
            this.dGlobalWaterLabel = new System.Windows.Forms.Label();
            this.dLocalWaterUpDown = new System.Windows.Forms.NumericUpDown();
            this.yWaterUpDown = new System.Windows.Forms.NumericUpDown();
            this.dGlobalWaterUpDown = new System.Windows.Forms.NumericUpDown();
            this.yWaterLabel = new System.Windows.Forms.Label();
            this.hWave001Label = new System.Windows.Forms.Label();
            this.hWave50Label = new System.Windows.Forms.Label();
            this.hWave001UpDown = new System.Windows.Forms.NumericUpDown();
            this.hWave50UpDown = new System.Windows.Forms.NumericUpDown();
            this.StructuresGridView = new System.Windows.Forms.DataGridView();
            this.StructuresList = new System.Windows.Forms.Label();
            this.MainPage = new tryhard.DrawPage();
            this.TypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuStrip.SuspendLayout();
            this.AvailableObjectsPanel.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.panel.SuspendLayout();
            this.FieldPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).BeginInit();
            this.UpStructurePanel.SuspendLayout();
            this.StructurePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yMatBallastUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yMatUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dWallCellUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wCellUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wUpStructureUpDown)).BeginInit();
            this.StructureTypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StructureTypeGridView)).BeginInit();
            this.StructureTypesPanel.SuspendLayout();
            this.FieldParametersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groundDurabilityUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedIceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.durabilityIceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dIceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diameterIceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dLocalWaterUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yWaterUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGlobalWaterUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hWave001UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hWave50UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StructuresGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditorMenuItem});
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
            this.AvailableObjectsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AvailableObjectsPanel.Controls.Add(this.AvailableObjectsLabel);
            this.AvailableObjectsPanel.Location = new System.Drawing.Point(5, 54);
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
            this.PropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PropertiesPanel.Controls.Add(this.PropertiesLabel);
            this.PropertiesPanel.Location = new System.Drawing.Point(5, 351);
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
            this.ObjectsTreeView.Location = new System.Drawing.Point(5, 75);
            this.ObjectsTreeView.Name = "ObjectsTreeView";
            this.ObjectsTreeView.Size = new System.Drawing.Size(243, 272);
            this.ObjectsTreeView.TabIndex = 23;
            this.ObjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ObjectsTreeView_AfterSelect);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.FieldComboBox);
            this.panel.Controls.Add(this.FieldPanel);
            this.panel.Controls.Add(this.CalculateButton);
            this.panel.Controls.Add(this.ObjectsTreeView);
            this.panel.Controls.Add(this.PropertiesPanel);
            this.panel.Controls.Add(this.AvailableObjectsPanel);
            this.panel.Controls.Add(this.PropertiesGridView);
            this.panel.Controls.Add(this.LinkInfoPanel);
            this.panel.Location = new System.Drawing.Point(1095, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(254, 704);
            this.panel.TabIndex = 0;
            // 
            // FieldComboBox
            // 
            this.FieldComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FieldComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FieldComboBox.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F);
            this.FieldComboBox.FormattingEnabled = true;
            this.FieldComboBox.Location = new System.Drawing.Point(6, 25);
            this.FieldComboBox.Name = "FieldComboBox";
            this.FieldComboBox.Size = new System.Drawing.Size(243, 24);
            this.FieldComboBox.TabIndex = 32;
            this.FieldComboBox.SelectedIndexChanged += new System.EventHandler(this.FieldComboBox_SelectedIndexChanged);
            // 
            // FieldPanel
            // 
            this.FieldPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldPanel.BackColor = System.Drawing.Color.Orange;
            this.FieldPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FieldPanel.Controls.Add(this.FieldLabel);
            this.FieldPanel.Location = new System.Drawing.Point(6, 3);
            this.FieldPanel.Name = "FieldPanel";
            this.FieldPanel.Size = new System.Drawing.Size(243, 22);
            this.FieldPanel.TabIndex = 20;
            // 
            // FieldLabel
            // 
            this.FieldLabel.AutoSize = true;
            this.FieldLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FieldLabel.Location = new System.Drawing.Point(3, 4);
            this.FieldLabel.Name = "FieldLabel";
            this.FieldLabel.Size = new System.Drawing.Size(196, 16);
            this.FieldLabel.TabIndex = 18;
            this.FieldLabel.Text = "Разрабатываемое месторождение";
            // 
            // CalculateButton
            // 
            this.CalculateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CalculateButton.BackColor = System.Drawing.Color.White;
            this.CalculateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalculateButton.Location = new System.Drawing.Point(5, 661);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(243, 37);
            this.CalculateButton.TabIndex = 29;
            this.CalculateButton.Text = "Рассчитать";
            this.CalculateButton.UseVisualStyleBackColor = false;
            this.CalculateButton.Click += new System.EventHandler(this.CalculateButton_Click);
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
            this.PropertiesGridView.Location = new System.Drawing.Point(5, 372);
            this.PropertiesGridView.Name = "PropertiesGridView";
            this.PropertiesGridView.ReadOnly = true;
            this.PropertiesGridView.RowHeadersVisible = false;
            this.PropertiesGridView.RowTemplate.Height = 20;
            this.PropertiesGridView.Size = new System.Drawing.Size(243, 286);
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
            this.LinkInfoPanel.Location = new System.Drawing.Point(5, 373);
            this.LinkInfoPanel.Name = "LinkInfoPanel";
            this.LinkInfoPanel.Size = new System.Drawing.Size(243, 285);
            this.LinkInfoPanel.TabIndex = 31;
            // 
            // UpStructurePanel
            // 
            this.UpStructurePanel.BackColor = System.Drawing.Color.White;
            this.UpStructurePanel.Controls.Add(this.CommonCostTextBox);
            this.UpStructurePanel.Controls.Add(this.CommonCostLabel);
            this.UpStructurePanel.Controls.Add(this.StructureLabel);
            this.UpStructurePanel.Controls.Add(this.StructurePanel);
            this.UpStructurePanel.Controls.Add(this.StructureTypeLabel);
            this.UpStructurePanel.Controls.Add(this.StructureTypePanel);
            this.UpStructurePanel.Controls.Add(this.NameLabel);
            this.UpStructurePanel.Controls.Add(this.NameTextBox);
            this.UpStructurePanel.Controls.Add(this.BackToSchemeButton);
            this.UpStructurePanel.Controls.Add(this.SaveButton);
            this.UpStructurePanel.Controls.Add(this.FieldParametersLabel);
            this.UpStructurePanel.Controls.Add(this.FieldParametersPanel);
            this.UpStructurePanel.Controls.Add(this.StructuresGridView);
            this.UpStructurePanel.Controls.Add(this.StructuresList);
            this.UpStructurePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpStructurePanel.Location = new System.Drawing.Point(0, 24);
            this.UpStructurePanel.Name = "UpStructurePanel";
            this.UpStructurePanel.Size = new System.Drawing.Size(1348, 704);
            this.UpStructurePanel.TabIndex = 0;
            // 
            // CommonCostTextBox
            // 
            this.CommonCostTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CommonCostTextBox.Location = new System.Drawing.Point(1008, 467);
            this.CommonCostTextBox.Name = "CommonCostTextBox";
            this.CommonCostTextBox.ReadOnly = true;
            this.CommonCostTextBox.Size = new System.Drawing.Size(303, 26);
            this.CommonCostTextBox.TabIndex = 40;
            // 
            // CommonCostLabel
            // 
            this.CommonCostLabel.AutoSize = true;
            this.CommonCostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CommonCostLabel.Location = new System.Drawing.Point(745, 467);
            this.CommonCostLabel.Name = "CommonCostLabel";
            this.CommonCostLabel.Size = new System.Drawing.Size(235, 20);
            this.CommonCostLabel.TabIndex = 39;
            this.CommonCostLabel.Text = "Общая стоимость проекта";
            // 
            // StructureLabel
            // 
            this.StructureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StructureLabel.AutoSize = true;
            this.StructureLabel.Location = new System.Drawing.Point(354, 336);
            this.StructureLabel.Name = "StructureLabel";
            this.StructureLabel.Size = new System.Drawing.Size(119, 13);
            this.StructureLabel.TabIndex = 38;
            this.StructureLabel.Text = "Параметры строения:";
            // 
            // StructurePanel
            // 
            this.StructurePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StructurePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StructurePanel.Controls.Add(this.yMatBallastUpDown);
            this.StructurePanel.Controls.Add(this.wCellLabel);
            this.StructurePanel.Controls.Add(this.yMatBallastLabel);
            this.StructurePanel.Controls.Add(this.yMatLabel);
            this.StructurePanel.Controls.Add(this.yMatUpDown);
            this.StructurePanel.Controls.Add(this.dWallCellUpDown);
            this.StructurePanel.Controls.Add(this.dWallCellLabel);
            this.StructurePanel.Controls.Add(this.wCellUpDown);
            this.StructurePanel.Controls.Add(this.wUpStructureLabel);
            this.StructurePanel.Controls.Add(this.wUpStructureUpDown);
            this.StructurePanel.Location = new System.Drawing.Point(357, 353);
            this.StructurePanel.Name = "StructurePanel";
            this.StructurePanel.Size = new System.Drawing.Size(311, 278);
            this.StructurePanel.TabIndex = 37;
            // 
            // yMatBallastUpDown
            // 
            this.yMatBallastUpDown.DecimalPlaces = 2;
            this.yMatBallastUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.yMatBallastUpDown.Location = new System.Drawing.Point(236, 118);
            this.yMatBallastUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.yMatBallastUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yMatBallastUpDown.Name = "yMatBallastUpDown";
            this.yMatBallastUpDown.Size = new System.Drawing.Size(50, 20);
            this.yMatBallastUpDown.TabIndex = 42;
            this.yMatBallastUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            65536});
            // 
            // wCellLabel
            // 
            this.wCellLabel.AutoSize = true;
            this.wCellLabel.Location = new System.Drawing.Point(25, 24);
            this.wCellLabel.Name = "wCellLabel";
            this.wCellLabel.Size = new System.Drawing.Size(188, 13);
            this.wCellLabel.TabIndex = 11;
            this.wCellLabel.Text = "Размер ребра основания ячейки, м";
            // 
            // yMatBallastLabel
            // 
            this.yMatBallastLabel.AutoSize = true;
            this.yMatBallastLabel.Location = new System.Drawing.Point(25, 120);
            this.yMatBallastLabel.Name = "yMatBallastLabel";
            this.yMatBallastLabel.Size = new System.Drawing.Size(157, 13);
            this.yMatBallastLabel.TabIndex = 41;
            this.yMatBallastLabel.Text = "Удельный вес балласта т/м3";
            // 
            // yMatLabel
            // 
            this.yMatLabel.AutoSize = true;
            this.yMatLabel.Location = new System.Drawing.Point(25, 48);
            this.yMatLabel.Name = "yMatLabel";
            this.yMatLabel.Size = new System.Drawing.Size(168, 13);
            this.yMatLabel.TabIndex = 15;
            this.yMatLabel.Text = "Удельный вес материала, т/м3";
            // 
            // yMatUpDown
            // 
            this.yMatUpDown.DecimalPlaces = 2;
            this.yMatUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.yMatUpDown.Location = new System.Drawing.Point(236, 46);
            this.yMatUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.yMatUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yMatUpDown.Name = "yMatUpDown";
            this.yMatUpDown.Size = new System.Drawing.Size(50, 20);
            this.yMatUpDown.TabIndex = 16;
            this.yMatUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.yMatUpDown.ValueChanged += new System.EventHandler(this.StructureUpDown_ValueChanged);
            // 
            // dWallCellUpDown
            // 
            this.dWallCellUpDown.DecimalPlaces = 2;
            this.dWallCellUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.dWallCellUpDown.Location = new System.Drawing.Point(236, 94);
            this.dWallCellUpDown.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.dWallCellUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.dWallCellUpDown.Name = "dWallCellUpDown";
            this.dWallCellUpDown.Size = new System.Drawing.Size(50, 20);
            this.dWallCellUpDown.TabIndex = 14;
            this.dWallCellUpDown.Value = new decimal(new int[] {
            75,
            0,
            0,
            131072});
            this.dWallCellUpDown.ValueChanged += new System.EventHandler(this.StructureUpDown_ValueChanged);
            // 
            // dWallCellLabel
            // 
            this.dWallCellLabel.AutoSize = true;
            this.dWallCellLabel.Location = new System.Drawing.Point(25, 96);
            this.dWallCellLabel.Name = "dWallCellLabel";
            this.dWallCellLabel.Size = new System.Drawing.Size(143, 13);
            this.dWallCellLabel.TabIndex = 13;
            this.dWallCellLabel.Text = "Толщина стенки ячейки, м";
            // 
            // wCellUpDown
            // 
            this.wCellUpDown.Location = new System.Drawing.Point(236, 22);
            this.wCellUpDown.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.wCellUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.wCellUpDown.Name = "wCellUpDown";
            this.wCellUpDown.Size = new System.Drawing.Size(50, 20);
            this.wCellUpDown.TabIndex = 12;
            this.wCellUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.wCellUpDown.ValueChanged += new System.EventHandler(this.StructureUpDown_ValueChanged);
            // 
            // wUpStructureLabel
            // 
            this.wUpStructureLabel.AutoSize = true;
            this.wUpStructureLabel.Location = new System.Drawing.Point(25, 72);
            this.wUpStructureLabel.Name = "wUpStructureLabel";
            this.wUpStructureLabel.Size = new System.Drawing.Size(199, 13);
            this.wUpStructureLabel.TabIndex = 31;
            this.wUpStructureLabel.Text = "Размер площадки верхнего строения";
            // 
            // wUpStructureUpDown
            // 
            this.wUpStructureUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.wUpStructureUpDown.Location = new System.Drawing.Point(236, 70);
            this.wUpStructureUpDown.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.wUpStructureUpDown.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.wUpStructureUpDown.Name = "wUpStructureUpDown";
            this.wUpStructureUpDown.Size = new System.Drawing.Size(50, 20);
            this.wUpStructureUpDown.TabIndex = 32;
            this.wUpStructureUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.wUpStructureUpDown.ValueChanged += new System.EventHandler(this.StructureUpDown_ValueChanged);
            // 
            // StructureTypeLabel
            // 
            this.StructureTypeLabel.AutoSize = true;
            this.StructureTypeLabel.Location = new System.Drawing.Point(719, 27);
            this.StructureTypeLabel.Name = "StructureTypeLabel";
            this.StructureTypeLabel.Size = new System.Drawing.Size(84, 13);
            this.StructureTypeLabel.TabIndex = 36;
            this.StructureTypeLabel.Text = "Типы строения";
            // 
            // StructureTypePanel
            // 
            this.StructureTypePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StructureTypePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StructureTypePanel.Controls.Add(this.StructureTypeGridView);
            this.StructureTypePanel.Controls.Add(this.StructureTypesPanel);
            this.StructureTypePanel.Location = new System.Drawing.Point(722, 51);
            this.StructureTypePanel.Name = "StructureTypePanel";
            this.StructureTypePanel.Size = new System.Drawing.Size(589, 267);
            this.StructureTypePanel.TabIndex = 35;
            // 
            // StructureTypeGridView
            // 
            this.StructureTypeGridView.AllowUserToAddRows = false;
            this.StructureTypeGridView.AllowUserToResizeColumns = false;
            this.StructureTypeGridView.AllowUserToResizeRows = false;
            this.StructureTypeGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StructureTypeGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.StructureTypeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StructureTypeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterColumn,
            this.ValueColumn});
            this.StructureTypeGridView.Location = new System.Drawing.Point(162, 23);
            this.StructureTypeGridView.MultiSelect = false;
            this.StructureTypeGridView.Name = "StructureTypeGridView";
            this.StructureTypeGridView.ReadOnly = true;
            this.StructureTypeGridView.RowHeadersVisible = false;
            this.StructureTypeGridView.RowTemplate.Height = 20;
            this.StructureTypeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StructureTypeGridView.Size = new System.Drawing.Size(397, 219);
            this.StructureTypeGridView.TabIndex = 28;
            // 
            // ParameterColumn
            // 
            this.ParameterColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterColumn.FillWeight = 165.9628F;
            this.ParameterColumn.HeaderText = "Параметр";
            this.ParameterColumn.Name = "ParameterColumn";
            this.ParameterColumn.ReadOnly = true;
            this.ParameterColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValueColumn
            // 
            this.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValueColumn.FillWeight = 113.4788F;
            this.ValueColumn.HeaderText = "Значение";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            this.ValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StructureTypesPanel
            // 
            this.StructureTypesPanel.Controls.Add(this.KessonRadioButton);
            this.StructureTypesPanel.Controls.Add(this.MonolegRadioButton);
            this.StructureTypesPanel.Controls.Add(this.MultilegRadioButton);
            this.StructureTypesPanel.Location = new System.Drawing.Point(26, 23);
            this.StructureTypesPanel.Name = "StructureTypesPanel";
            this.StructureTypesPanel.Size = new System.Drawing.Size(110, 100);
            this.StructureTypesPanel.TabIndex = 5;
            // 
            // KessonRadioButton
            // 
            this.KessonRadioButton.AutoSize = true;
            this.KessonRadioButton.Location = new System.Drawing.Point(14, 19);
            this.KessonRadioButton.Name = "KessonRadioButton";
            this.KessonRadioButton.Size = new System.Drawing.Size(62, 17);
            this.KessonRadioButton.TabIndex = 2;
            this.KessonRadioButton.TabStop = true;
            this.KessonRadioButton.Tag = "0";
            this.KessonRadioButton.Text = "Кессон";
            this.KessonRadioButton.UseVisualStyleBackColor = true;
            this.KessonRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // MonolegRadioButton
            // 
            this.MonolegRadioButton.AutoSize = true;
            this.MonolegRadioButton.Location = new System.Drawing.Point(14, 42);
            this.MonolegRadioButton.Name = "MonolegRadioButton";
            this.MonolegRadioButton.Size = new System.Drawing.Size(69, 17);
            this.MonolegRadioButton.TabIndex = 4;
            this.MonolegRadioButton.TabStop = true;
            this.MonolegRadioButton.Tag = "1";
            this.MonolegRadioButton.Text = "Монолэг";
            this.MonolegRadioButton.UseVisualStyleBackColor = true;
            this.MonolegRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // MultilegRadioButton
            // 
            this.MultilegRadioButton.AutoSize = true;
            this.MultilegRadioButton.Location = new System.Drawing.Point(14, 65);
            this.MultilegRadioButton.Name = "MultilegRadioButton";
            this.MultilegRadioButton.Size = new System.Drawing.Size(79, 17);
            this.MultilegRadioButton.TabIndex = 3;
            this.MultilegRadioButton.TabStop = true;
            this.MultilegRadioButton.Tag = "2";
            this.MultilegRadioButton.Text = "Мультилэг";
            this.MultilegRadioButton.UseVisualStyleBackColor = true;
            this.MultilegRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(823, 649);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(104, 13);
            this.NameLabel.TabIndex = 34;
            this.NameLabel.Text = "Название проекта:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameTextBox.Location = new System.Drawing.Point(933, 642);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(227, 27);
            this.NameTextBox.TabIndex = 33;
            // 
            // BackToSchemeButton
            // 
            this.BackToSchemeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BackToSchemeButton.BackColor = System.Drawing.Color.White;
            this.BackToSchemeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BackToSchemeButton.Location = new System.Drawing.Point(34, 642);
            this.BackToSchemeButton.Name = "BackToSchemeButton";
            this.BackToSchemeButton.Size = new System.Drawing.Size(122, 27);
            this.BackToSchemeButton.TabIndex = 32;
            this.BackToSchemeButton.Text = "Вернуться к схеме";
            this.BackToSchemeButton.UseVisualStyleBackColor = false;
            this.BackToSchemeButton.Click += new System.EventHandler(this.BackToSchemeButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.White;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Location = new System.Drawing.Point(1188, 642);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(122, 27);
            this.SaveButton.TabIndex = 31;
            this.SaveButton.Text = "Сохранить проект";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FieldParametersLabel
            // 
            this.FieldParametersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FieldParametersLabel.AutoSize = true;
            this.FieldParametersLabel.Location = new System.Drawing.Point(31, 336);
            this.FieldParametersLabel.Name = "FieldParametersLabel";
            this.FieldParametersLabel.Size = new System.Drawing.Size(153, 13);
            this.FieldParametersLabel.TabIndex = 29;
            this.FieldParametersLabel.Text = "Параметры месторождения:";
            // 
            // FieldParametersPanel
            // 
            this.FieldParametersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FieldParametersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FieldParametersPanel.Controls.Add(this.groundDurabilityUpDown);
            this.FieldParametersPanel.Controls.Add(this.groundDurabilityLabel);
            this.FieldParametersPanel.Controls.Add(this.speedIceUpDown);
            this.FieldParametersPanel.Controls.Add(this.speedIceLabel);
            this.FieldParametersPanel.Controls.Add(this.durabilityIceUpDown);
            this.FieldParametersPanel.Controls.Add(this.durabilityIceLabel);
            this.FieldParametersPanel.Controls.Add(this.dIceUpDown);
            this.FieldParametersPanel.Controls.Add(this.dIceLabel);
            this.FieldParametersPanel.Controls.Add(this.diameterIceUpDown);
            this.FieldParametersPanel.Controls.Add(this.diameterIceLabel);
            this.FieldParametersPanel.Controls.Add(this.dLocalWaterLabel);
            this.FieldParametersPanel.Controls.Add(this.dGlobalWaterLabel);
            this.FieldParametersPanel.Controls.Add(this.dLocalWaterUpDown);
            this.FieldParametersPanel.Controls.Add(this.yWaterUpDown);
            this.FieldParametersPanel.Controls.Add(this.dGlobalWaterUpDown);
            this.FieldParametersPanel.Controls.Add(this.yWaterLabel);
            this.FieldParametersPanel.Controls.Add(this.hWave001Label);
            this.FieldParametersPanel.Controls.Add(this.hWave50Label);
            this.FieldParametersPanel.Controls.Add(this.hWave001UpDown);
            this.FieldParametersPanel.Controls.Add(this.hWave50UpDown);
            this.FieldParametersPanel.Location = new System.Drawing.Point(34, 353);
            this.FieldParametersPanel.Name = "FieldParametersPanel";
            this.FieldParametersPanel.Size = new System.Drawing.Size(317, 278);
            this.FieldParametersPanel.TabIndex = 28;
            // 
            // groundDurabilityUpDown
            // 
            this.groundDurabilityUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.groundDurabilityUpDown.Location = new System.Drawing.Point(241, 238);
            this.groundDurabilityUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.groundDurabilityUpDown.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.groundDurabilityUpDown.Name = "groundDurabilityUpDown";
            this.groundDurabilityUpDown.Size = new System.Drawing.Size(50, 20);
            this.groundDurabilityUpDown.TabIndex = 42;
            this.groundDurabilityUpDown.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // groundDurabilityLabel
            // 
            this.groundDurabilityLabel.AutoSize = true;
            this.groundDurabilityLabel.Location = new System.Drawing.Point(24, 240);
            this.groundDurabilityLabel.Name = "groundDurabilityLabel";
            this.groundDurabilityLabel.Size = new System.Drawing.Size(127, 13);
            this.groundDurabilityLabel.TabIndex = 41;
            this.groundDurabilityLabel.Text = "Прочность грунта, т/м2";
            // 
            // speedIceUpDown
            // 
            this.speedIceUpDown.DecimalPlaces = 2;
            this.speedIceUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.speedIceUpDown.Location = new System.Drawing.Point(241, 214);
            this.speedIceUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.speedIceUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.speedIceUpDown.Name = "speedIceUpDown";
            this.speedIceUpDown.Size = new System.Drawing.Size(50, 20);
            this.speedIceUpDown.TabIndex = 40;
            this.speedIceUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.speedIceUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // speedIceLabel
            // 
            this.speedIceLabel.AutoSize = true;
            this.speedIceLabel.Location = new System.Drawing.Point(24, 216);
            this.speedIceLabel.Name = "speedIceLabel";
            this.speedIceLabel.Size = new System.Drawing.Size(107, 13);
            this.speedIceLabel.TabIndex = 39;
            this.speedIceLabel.Text = "Скорость льда, м/с";
            // 
            // durabilityIceUpDown
            // 
            this.durabilityIceUpDown.DecimalPlaces = 2;
            this.durabilityIceUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.durabilityIceUpDown.Location = new System.Drawing.Point(241, 190);
            this.durabilityIceUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.durabilityIceUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.durabilityIceUpDown.Name = "durabilityIceUpDown";
            this.durabilityIceUpDown.Size = new System.Drawing.Size(50, 20);
            this.durabilityIceUpDown.TabIndex = 38;
            this.durabilityIceUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.durabilityIceUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // durabilityIceLabel
            // 
            this.durabilityIceLabel.AutoSize = true;
            this.durabilityIceLabel.Location = new System.Drawing.Point(24, 192);
            this.durabilityIceLabel.Name = "durabilityIceLabel";
            this.durabilityIceLabel.Size = new System.Drawing.Size(116, 13);
            this.durabilityIceLabel.TabIndex = 37;
            this.durabilityIceLabel.Text = "Прочность льда, мПа";
            // 
            // dIceUpDown
            // 
            this.dIceUpDown.DecimalPlaces = 2;
            this.dIceUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.dIceUpDown.Location = new System.Drawing.Point(241, 166);
            this.dIceUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.dIceUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.dIceUpDown.Name = "dIceUpDown";
            this.dIceUpDown.Size = new System.Drawing.Size(50, 20);
            this.dIceUpDown.TabIndex = 36;
            this.dIceUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.dIceUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // dIceLabel
            // 
            this.dIceLabel.AutoSize = true;
            this.dIceLabel.Location = new System.Drawing.Point(24, 168);
            this.dIceLabel.Name = "dIceLabel";
            this.dIceLabel.Size = new System.Drawing.Size(94, 13);
            this.dIceLabel.TabIndex = 35;
            this.dIceLabel.Text = "Толщина льда, м";
            // 
            // diameterIceUpDown
            // 
            this.diameterIceUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.diameterIceUpDown.Location = new System.Drawing.Point(241, 142);
            this.diameterIceUpDown.Maximum = new decimal(new int[] {
            7000,
            0,
            0,
            0});
            this.diameterIceUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.diameterIceUpDown.Name = "diameterIceUpDown";
            this.diameterIceUpDown.Size = new System.Drawing.Size(50, 20);
            this.diameterIceUpDown.TabIndex = 34;
            this.diameterIceUpDown.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.diameterIceUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // diameterIceLabel
            // 
            this.diameterIceLabel.AutoSize = true;
            this.diameterIceLabel.Location = new System.Drawing.Point(24, 144);
            this.diameterIceLabel.Name = "diameterIceLabel";
            this.diameterIceLabel.Size = new System.Drawing.Size(186, 13);
            this.diameterIceLabel.TabIndex = 33;
            this.diameterIceLabel.Text = "Диаметр ледяного образования, м";
            // 
            // dLocalWaterLabel
            // 
            this.dLocalWaterLabel.AutoSize = true;
            this.dLocalWaterLabel.Location = new System.Drawing.Point(24, 24);
            this.dLocalWaterLabel.Name = "dLocalWaterLabel";
            this.dLocalWaterLabel.Size = new System.Drawing.Size(207, 13);
            this.dLocalWaterLabel.TabIndex = 3;
            this.dLocalWaterLabel.Text = "Глубина залива в месте возведения, м";
            // 
            // dGlobalWaterLabel
            // 
            this.dGlobalWaterLabel.AutoSize = true;
            this.dGlobalWaterLabel.Location = new System.Drawing.Point(24, 48);
            this.dGlobalWaterLabel.Name = "dGlobalWaterLabel";
            this.dGlobalWaterLabel.Size = new System.Drawing.Size(159, 13);
            this.dGlobalWaterLabel.TabIndex = 1;
            this.dGlobalWaterLabel.Text = "Эксплуатационная глубина, м";
            // 
            // dLocalWaterUpDown
            // 
            this.dLocalWaterUpDown.Location = new System.Drawing.Point(241, 22);
            this.dLocalWaterUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.dLocalWaterUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dLocalWaterUpDown.Name = "dLocalWaterUpDown";
            this.dLocalWaterUpDown.Size = new System.Drawing.Size(50, 20);
            this.dLocalWaterUpDown.TabIndex = 5;
            this.dLocalWaterUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.dLocalWaterUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // yWaterUpDown
            // 
            this.yWaterUpDown.DecimalPlaces = 2;
            this.yWaterUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.yWaterUpDown.Location = new System.Drawing.Point(241, 118);
            this.yWaterUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.yWaterUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yWaterUpDown.Name = "yWaterUpDown";
            this.yWaterUpDown.Size = new System.Drawing.Size(50, 20);
            this.yWaterUpDown.TabIndex = 18;
            this.yWaterUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yWaterUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // dGlobalWaterUpDown
            // 
            this.dGlobalWaterUpDown.Location = new System.Drawing.Point(241, 46);
            this.dGlobalWaterUpDown.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.dGlobalWaterUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.dGlobalWaterUpDown.Name = "dGlobalWaterUpDown";
            this.dGlobalWaterUpDown.Size = new System.Drawing.Size(50, 20);
            this.dGlobalWaterUpDown.TabIndex = 6;
            this.dGlobalWaterUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.dGlobalWaterUpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // yWaterLabel
            // 
            this.yWaterLabel.AutoSize = true;
            this.yWaterLabel.Location = new System.Drawing.Point(24, 120);
            this.yWaterLabel.Name = "yWaterLabel";
            this.yWaterLabel.Size = new System.Drawing.Size(139, 13);
            this.yWaterLabel.TabIndex = 17;
            this.yWaterLabel.Text = "Удельный вес воды, т/м3";
            // 
            // hWave001Label
            // 
            this.hWave001Label.AutoSize = true;
            this.hWave001Label.Location = new System.Drawing.Point(24, 72);
            this.hWave001Label.Name = "hWave001Label";
            this.hWave001Label.Size = new System.Drawing.Size(211, 13);
            this.hWave001Label.TabIndex = 7;
            this.hWave001Label.Text = "Высота волны 0.01% обеспеченности, м";
            // 
            // hWave50Label
            // 
            this.hWave50Label.AutoSize = true;
            this.hWave50Label.Location = new System.Drawing.Point(24, 96);
            this.hWave50Label.Name = "hWave50Label";
            this.hWave50Label.Size = new System.Drawing.Size(202, 13);
            this.hWave50Label.TabIndex = 8;
            this.hWave50Label.Text = "Высота волны 50% обеспеченности, м";
            // 
            // hWave001UpDown
            // 
            this.hWave001UpDown.Location = new System.Drawing.Point(241, 70);
            this.hWave001UpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.hWave001UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hWave001UpDown.Name = "hWave001UpDown";
            this.hWave001UpDown.Size = new System.Drawing.Size(50, 20);
            this.hWave001UpDown.TabIndex = 9;
            this.hWave001UpDown.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.hWave001UpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
            // 
            // hWave50UpDown
            // 
            this.hWave50UpDown.Location = new System.Drawing.Point(241, 94);
            this.hWave50UpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.hWave50UpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hWave50UpDown.Name = "hWave50UpDown";
            this.hWave50UpDown.Size = new System.Drawing.Size(50, 20);
            this.hWave50UpDown.TabIndex = 10;
            this.hWave50UpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.hWave50UpDown.ValueChanged += new System.EventHandler(this.FieldUpDown_ValueChanged);
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
            this.WeightColumn,
            this.CostColumn});
            this.StructuresGridView.Location = new System.Drawing.Point(34, 51);
            this.StructuresGridView.MultiSelect = false;
            this.StructuresGridView.Name = "StructuresGridView";
            this.StructuresGridView.ReadOnly = true;
            this.StructuresGridView.RowHeadersVisible = false;
            this.StructuresGridView.RowTemplate.Height = 20;
            this.StructuresGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StructuresGridView.Size = new System.Drawing.Size(634, 267);
            this.StructuresGridView.TabIndex = 27;
            this.StructuresGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StructuresGridView_CellClick);
            // 
            // StructuresList
            // 
            this.StructuresList.AutoSize = true;
            this.StructuresList.Location = new System.Drawing.Point(31, 35);
            this.StructuresList.Name = "StructuresList";
            this.StructuresList.Size = new System.Drawing.Size(112, 13);
            this.StructuresList.TabIndex = 1;
            this.StructuresList.Text = "Список комплексов:";
            // 
            // MainPage
            // 
            this.MainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPage.BackColor = System.Drawing.Color.White;
            this.MainPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPage.Location = new System.Drawing.Point(-1, 25);
            this.MainPage.Name = "MainPage";
            this.MainPage.Size = new System.Drawing.Size(1097, 704);
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
            this.WeightColumn.HeaderText = "Масса, т";
            this.WeightColumn.Name = "WeightColumn";
            this.WeightColumn.ReadOnly = true;
            this.WeightColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WeightColumn.Width = 130;
            // 
            // CostColumn
            // 
            dataGridViewCellStyle1.Format = "C3";
            this.CostColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CostColumn.HeaderText = "Стоимость";
            this.CostColumn.Name = "CostColumn";
            this.CostColumn.ReadOnly = true;
            this.CostColumn.Width = 120;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1348, 728);
            this.Controls.Add(this.UpStructurePanel);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.MainPage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1364, 736);
            this.Name = "MainForm";
            this.Text = "Gaby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.AvailableObjectsPanel.ResumeLayout(false);
            this.AvailableObjectsPanel.PerformLayout();
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            this.FieldPanel.ResumeLayout(false);
            this.FieldPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).EndInit();
            this.UpStructurePanel.ResumeLayout(false);
            this.UpStructurePanel.PerformLayout();
            this.StructurePanel.ResumeLayout(false);
            this.StructurePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yMatBallastUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yMatUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dWallCellUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wCellUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wUpStructureUpDown)).EndInit();
            this.StructureTypePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StructureTypeGridView)).EndInit();
            this.StructureTypesPanel.ResumeLayout(false);
            this.StructureTypesPanel.PerformLayout();
            this.FieldParametersPanel.ResumeLayout(false);
            this.FieldParametersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groundDurabilityUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedIceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.durabilityIceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dIceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diameterIceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dLocalWaterUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yWaterUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGlobalWaterUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hWave001UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hWave50UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StructuresGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip MenuStrip;
        public DrawPage MainPage;
        private ToolStripMenuItem EditorMenuItem;
        private Panel AvailableObjectsPanel;
        private Label AvailableObjectsLabel;
        private Panel PropertiesPanel;
        private Label PropertiesLabel;
        private TreeView ObjectsTreeView;
        private Panel panel;
        private Button CalculateButton;
        public DataGridView PropertiesGridView;
        private DataGridViewTextBoxColumn NameCol;
        private DataGridViewTextBoxColumn ValueCol;
        private Panel LinkInfoPanel;
        private Panel UpStructurePanel;
        private Label StructuresList;
        public DataGridView StructuresGridView;
        private Panel FieldParametersPanel;
        private Label FieldParametersLabel;
        private Label dLocalWaterLabel;
        private Label dGlobalWaterLabel;
        private NumericUpDown dGlobalWaterUpDown;
        private NumericUpDown dLocalWaterUpDown;
        private Label hWave001Label;
        private Label hWave50Label;
        private NumericUpDown hWave50UpDown;
        private NumericUpDown hWave001UpDown;
        private NumericUpDown wCellUpDown;
        private Label wCellLabel;
        private Label dWallCellLabel;
        private NumericUpDown dWallCellUpDown;
        private NumericUpDown yMatUpDown;
        private Label yMatLabel;
        private Label yWaterLabel;
        private NumericUpDown yWaterUpDown;
        private Button BackToSchemeButton;
        private Button SaveButton;
        private NumericUpDown wUpStructureUpDown;
        private Label wUpStructureLabel;
        private TextBox NameTextBox;
        private Label NameLabel;
        private Panel StructureTypePanel;
        private RadioButton MonolegRadioButton;
        private RadioButton MultilegRadioButton;
        private RadioButton KessonRadioButton;
        private Panel StructureTypesPanel;
        public DataGridView StructureTypeGridView;
        private Label StructureTypeLabel;
        private ComboBox FieldComboBox;
        private Panel FieldPanel;
        private Label FieldLabel;
        private DataGridViewTextBoxColumn ParameterColumn;
        private DataGridViewTextBoxColumn ValueColumn;
        private NumericUpDown diameterIceUpDown;
        private Label diameterIceLabel;
        private NumericUpDown dIceUpDown;
        private Label dIceLabel;
        private NumericUpDown durabilityIceUpDown;
        private Label durabilityIceLabel;
        private NumericUpDown speedIceUpDown;
        private Label speedIceLabel;
        private Panel StructurePanel;
        private Label StructureLabel;
        private NumericUpDown yMatBallastUpDown;
        private Label yMatBallastLabel;
        private NumericUpDown groundDurabilityUpDown;
        private Label groundDurabilityLabel;
        private TextBox CommonCostTextBox;
        private Label CommonCostLabel;
        private DataGridViewTextBoxColumn TypeColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn WeightColumn;
        private DataGridViewTextBoxColumn CostColumn;
    }
}

