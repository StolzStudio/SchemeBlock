﻿namespace tryhard
{
    partial class EditorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.TypeStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.CategoryStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CategoryStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.TypeStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel = new System.Windows.Forms.Panel();
            this.ObjectsTreeView = new System.Windows.Forms.TreeView();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.PropertiesLabel = new System.Windows.Forms.Label();
            this.AvailableObjectsPanel = new System.Windows.Forms.Panel();
            this.AvailableObjectsLabel = new System.Windows.Forms.Label();
            this.PropertiesGridView = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkInfoPanel = new System.Windows.Forms.Panel();
            this.GoNextButton = new System.Windows.Forms.Button();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.WorkPanel = new System.Windows.Forms.Panel();
            this.SelectedComplexDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalculateButton = new System.Windows.Forms.Button();
            this.FieldPropertyPanel = new System.Windows.Forms.Panel();
            this.WorkPropertiesLabel = new System.Windows.Forms.Label();
            this.FieldPropertyLabel = new System.Windows.Forms.Label();
            this.FieldPropertyDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldComboBox = new System.Windows.Forms.ComboBox();
            this.WorkFieldPanel = new System.Windows.Forms.Panel();
            this.WorkFieldLabel = new System.Windows.Forms.Label();
            this.WorkPropetriesPanel = new System.Windows.Forms.Panel();
            this.ObjectPropertiesLabel = new System.Windows.Forms.Label();
            this.WorkCombinationPanel = new System.Windows.Forms.Panel();
            this.WorkCombinationLabel = new System.Windows.Forms.Label();
            this.CombinationDataGridView = new System.Windows.Forms.DataGridView();
            this.CheckColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolumeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedItemDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddNewObjectButton = new System.Windows.Forms.Button();
            this.EditObjectButton = new System.Windows.Forms.Button();
            this.DrawPage = new tryhard.DrawPage();
            this.ToolStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.AvailableObjectsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).BeginInit();
            this.WorkPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedComplexDataGridView)).BeginInit();
            this.FieldPropertyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FieldPropertyDataGridView)).BeginInit();
            this.WorkFieldPanel.SuspendLayout();
            this.WorkPropetriesPanel.SuspendLayout();
            this.WorkCombinationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedItemDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStrip
            // 
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripSeparator,
            this.TypeStripLabel,
            this.CategoryStripComboBox,
            this.ToolStripSeparator1,
            this.CategoryStripLabel,
            this.TypeStripComboBox,
            this.ToolStripSeparator2});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(1248, 25);
            this.ToolStrip.TabIndex = 0;
            this.ToolStrip.Text = "toolStrip1";
            // 
            // ToolStripSeparator
            // 
            this.ToolStripSeparator.Name = "ToolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // TypeStripLabel
            // 
            this.TypeStripLabel.Name = "TypeStripLabel";
            this.TypeStripLabel.Size = new System.Drawing.Size(66, 22);
            this.TypeStripLabel.Text = "Категория:";
            // 
            // CategoryStripComboBox
            // 
            this.CategoryStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryStripComboBox.Name = "CategoryStripComboBox";
            this.CategoryStripComboBox.Size = new System.Drawing.Size(160, 25);
            this.CategoryStripComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoryStripComboBox_SelectedIndexChanged);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CategoryStripLabel
            // 
            this.CategoryStripLabel.Name = "CategoryStripLabel";
            this.CategoryStripLabel.Size = new System.Drawing.Size(31, 22);
            this.CategoryStripLabel.Text = "Тип:";
            // 
            // TypeStripComboBox
            // 
            this.TypeStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeStripComboBox.Name = "TypeStripComboBox";
            this.TypeStripComboBox.Size = new System.Drawing.Size(160, 25);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.ObjectsTreeView);
            this.panel.Controls.Add(this.PropertiesPanel);
            this.panel.Controls.Add(this.AvailableObjectsPanel);
            this.panel.Controls.Add(this.PropertiesGridView);
            this.panel.Controls.Add(this.LinkInfoPanel);
            this.panel.Location = new System.Drawing.Point(993, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(255, 594);
            this.panel.TabIndex = 1;
            // 
            // ObjectsTreeView
            // 
            this.ObjectsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectsTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.ObjectsTreeView.Location = new System.Drawing.Point(6, 22);
            this.ObjectsTreeView.Name = "ObjectsTreeView";
            this.ObjectsTreeView.Size = new System.Drawing.Size(243, 219);
            this.ObjectsTreeView.TabIndex = 23;
            this.ObjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ObjectsTreeView_AfterSelect);
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertiesPanel.BackColor = System.Drawing.Color.Orange;
            this.PropertiesPanel.Controls.Add(this.PropertiesLabel);
            this.PropertiesPanel.Location = new System.Drawing.Point(6, 244);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Size = new System.Drawing.Size(243, 22);
            this.PropertiesPanel.TabIndex = 21;
            // 
            // PropertiesLabel
            // 
            this.PropertiesLabel.AutoSize = true;
            this.PropertiesLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PropertiesLabel.Location = new System.Drawing.Point(3, 4);
            this.PropertiesLabel.Name = "PropertiesLabel";
            this.PropertiesLabel.Size = new System.Drawing.Size(58, 16);
            this.PropertiesLabel.TabIndex = 18;
            this.PropertiesLabel.Text = "Свойства";
            // 
            // AvailableObjectsPanel
            // 
            this.AvailableObjectsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableObjectsPanel.BackColor = System.Drawing.Color.Orange;
            this.AvailableObjectsPanel.Controls.Add(this.AvailableObjectsLabel);
            this.AvailableObjectsPanel.Location = new System.Drawing.Point(6, 0);
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
            this.PropertiesGridView.Location = new System.Drawing.Point(6, 266);
            this.PropertiesGridView.Name = "PropertiesGridView";
            this.PropertiesGridView.ReadOnly = true;
            this.PropertiesGridView.RowHeadersVisible = false;
            this.PropertiesGridView.RowTemplate.Height = 20;
            this.PropertiesGridView.Size = new System.Drawing.Size(243, 284);
            this.PropertiesGridView.TabIndex = 26;
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
            this.LinkInfoPanel.Location = new System.Drawing.Point(6, 266);
            this.LinkInfoPanel.Name = "LinkInfoPanel";
            this.LinkInfoPanel.Size = new System.Drawing.Size(243, 285);
            this.LinkInfoPanel.TabIndex = 32;
            // 
            // GoNextButton
            // 
            this.GoNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoNextButton.BackColor = System.Drawing.Color.White;
            this.GoNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoNextButton.Location = new System.Drawing.Point(1120, 578);
            this.GoNextButton.Name = "GoNextButton";
            this.GoNextButton.Size = new System.Drawing.Size(122, 37);
            this.GoNextButton.TabIndex = 28;
            this.GoNextButton.Text = "Далее";
            this.GoNextButton.UseVisualStyleBackColor = false;
            this.GoNextButton.Click += new System.EventHandler(this.GoNextButton_Click);
            // 
            // GoBackButton
            // 
            this.GoBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoBackButton.BackColor = System.Drawing.Color.White;
            this.GoBackButton.Enabled = false;
            this.GoBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoBackButton.Location = new System.Drawing.Point(999, 578);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(122, 37);
            this.GoBackButton.TabIndex = 27;
            this.GoBackButton.Text = "Назад";
            this.GoBackButton.UseVisualStyleBackColor = false;
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
            // 
            // WorkPanel
            // 
            this.WorkPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkPanel.BackColor = System.Drawing.Color.White;
            this.WorkPanel.Controls.Add(this.SelectedComplexDataGridView);
            this.WorkPanel.Controls.Add(this.CalculateButton);
            this.WorkPanel.Controls.Add(this.FieldPropertyPanel);
            this.WorkPanel.Controls.Add(this.FieldPropertyDataGridView);
            this.WorkPanel.Controls.Add(this.FieldComboBox);
            this.WorkPanel.Controls.Add(this.WorkFieldPanel);
            this.WorkPanel.Controls.Add(this.WorkPropetriesPanel);
            this.WorkPanel.Controls.Add(this.WorkCombinationPanel);
            this.WorkPanel.Controls.Add(this.CombinationDataGridView);
            this.WorkPanel.Controls.Add(this.SelectedItemDataGridView);
            this.WorkPanel.Location = new System.Drawing.Point(0, 25);
            this.WorkPanel.MaximumSize = new System.Drawing.Size(1264, 657);
            this.WorkPanel.MinimumSize = new System.Drawing.Size(1264, 657);
            this.WorkPanel.Name = "WorkPanel";
            this.WorkPanel.Size = new System.Drawing.Size(1264, 657);
            this.WorkPanel.TabIndex = 2;
            this.WorkPanel.Visible = false;
            // 
            // SelectedComplexDataGridView
            // 
            this.SelectedComplexDataGridView.AllowUserToAddRows = false;
            this.SelectedComplexDataGridView.AllowUserToResizeColumns = false;
            this.SelectedComplexDataGridView.AllowUserToResizeRows = false;
            this.SelectedComplexDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectedComplexDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.SelectedComplexDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SelectedComplexDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.SelectedComplexDataGridView.Location = new System.Drawing.Point(4, 334);
            this.SelectedComplexDataGridView.Name = "SelectedComplexDataGridView";
            this.SelectedComplexDataGridView.RowHeadersVisible = false;
            this.SelectedComplexDataGridView.RowTemplate.Height = 20;
            this.SelectedComplexDataGridView.Size = new System.Drawing.Size(419, 217);
            this.SelectedComplexDataGridView.TabIndex = 37;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Название";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Значение";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 215;
            // 
            // CalculateButton
            // 
            this.CalculateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalculateButton.Location = new System.Drawing.Point(4, 53);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(419, 28);
            this.CalculateButton.TabIndex = 36;
            this.CalculateButton.Text = "Расчитать";
            this.CalculateButton.UseVisualStyleBackColor = true;
            this.CalculateButton.Click += new System.EventHandler(this.CalculateButton_Click);
            // 
            // FieldPropertyPanel
            // 
            this.FieldPropertyPanel.BackColor = System.Drawing.Color.Orange;
            this.FieldPropertyPanel.Controls.Add(this.WorkPropertiesLabel);
            this.FieldPropertyPanel.Controls.Add(this.FieldPropertyLabel);
            this.FieldPropertyPanel.Location = new System.Drawing.Point(4, 84);
            this.FieldPropertyPanel.Name = "FieldPropertyPanel";
            this.FieldPropertyPanel.Size = new System.Drawing.Size(419, 27);
            this.FieldPropertyPanel.TabIndex = 35;
            // 
            // WorkPropertiesLabel
            // 
            this.WorkPropertiesLabel.AutoSize = true;
            this.WorkPropertiesLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WorkPropertiesLabel.Location = new System.Drawing.Point(212, 6);
            this.WorkPropertiesLabel.Name = "WorkPropertiesLabel";
            this.WorkPropertiesLabel.Size = new System.Drawing.Size(114, 16);
            this.WorkPropertiesLabel.TabIndex = 0;
            this.WorkPropertiesLabel.Text = "Количество блоков";
            // 
            // FieldPropertyLabel
            // 
            this.FieldPropertyLabel.AutoSize = true;
            this.FieldPropertyLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FieldPropertyLabel.Location = new System.Drawing.Point(3, 6);
            this.FieldPropertyLabel.Name = "FieldPropertyLabel";
            this.FieldPropertyLabel.Size = new System.Drawing.Size(149, 16);
            this.FieldPropertyLabel.TabIndex = 0;
            this.FieldPropertyLabel.Text = "Свойства месторождения";
            // 
            // FieldPropertyDataGridView
            // 
            this.FieldPropertyDataGridView.AllowUserToAddRows = false;
            this.FieldPropertyDataGridView.AllowUserToResizeColumns = false;
            this.FieldPropertyDataGridView.AllowUserToResizeRows = false;
            this.FieldPropertyDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FieldPropertyDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.FieldPropertyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FieldPropertyDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.FieldPropertyDataGridView.Location = new System.Drawing.Point(4, 110);
            this.FieldPropertyDataGridView.Name = "FieldPropertyDataGridView";
            this.FieldPropertyDataGridView.RowHeadersVisible = false;
            this.FieldPropertyDataGridView.RowTemplate.Height = 20;
            this.FieldPropertyDataGridView.Size = new System.Drawing.Size(204, 191);
            this.FieldPropertyDataGridView.TabIndex = 34;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Название";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Значение";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FieldComboBox
            // 
            this.FieldComboBox.FormattingEnabled = true;
            this.FieldComboBox.Location = new System.Drawing.Point(4, 29);
            this.FieldComboBox.Name = "FieldComboBox";
            this.FieldComboBox.Size = new System.Drawing.Size(419, 21);
            this.FieldComboBox.TabIndex = 33;
            this.FieldComboBox.SelectedIndexChanged += new System.EventHandler(this.FieldComboBox_SelectedIndexChanged);
            // 
            // WorkFieldPanel
            // 
            this.WorkFieldPanel.BackColor = System.Drawing.Color.Orange;
            this.WorkFieldPanel.Controls.Add(this.WorkFieldLabel);
            this.WorkFieldPanel.Location = new System.Drawing.Point(4, 4);
            this.WorkFieldPanel.Name = "WorkFieldPanel";
            this.WorkFieldPanel.Size = new System.Drawing.Size(419, 27);
            this.WorkFieldPanel.TabIndex = 32;
            // 
            // WorkFieldLabel
            // 
            this.WorkFieldLabel.AutoSize = true;
            this.WorkFieldLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WorkFieldLabel.Location = new System.Drawing.Point(3, 6);
            this.WorkFieldLabel.Name = "WorkFieldLabel";
            this.WorkFieldLabel.Size = new System.Drawing.Size(99, 16);
            this.WorkFieldLabel.TabIndex = 0;
            this.WorkFieldLabel.Text = "Месторождение";
            // 
            // WorkPropetriesPanel
            // 
            this.WorkPropetriesPanel.BackColor = System.Drawing.Color.Orange;
            this.WorkPropetriesPanel.Controls.Add(this.ObjectPropertiesLabel);
            this.WorkPropetriesPanel.Location = new System.Drawing.Point(4, 305);
            this.WorkPropetriesPanel.Name = "WorkPropetriesPanel";
            this.WorkPropetriesPanel.Size = new System.Drawing.Size(419, 27);
            this.WorkPropetriesPanel.TabIndex = 31;
            // 
            // ObjectPropertiesLabel
            // 
            this.ObjectPropertiesLabel.AutoSize = true;
            this.ObjectPropertiesLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ObjectPropertiesLabel.Location = new System.Drawing.Point(4, 6);
            this.ObjectPropertiesLabel.Name = "ObjectPropertiesLabel";
            this.ObjectPropertiesLabel.Size = new System.Drawing.Size(105, 16);
            this.ObjectPropertiesLabel.TabIndex = 1;
            this.ObjectPropertiesLabel.Text = "Свойства объекта";
            // 
            // WorkCombinationPanel
            // 
            this.WorkCombinationPanel.BackColor = System.Drawing.Color.Orange;
            this.WorkCombinationPanel.Controls.Add(this.WorkCombinationLabel);
            this.WorkCombinationPanel.Location = new System.Drawing.Point(429, 4);
            this.WorkCombinationPanel.Name = "WorkCombinationPanel";
            this.WorkCombinationPanel.Size = new System.Drawing.Size(810, 27);
            this.WorkCombinationPanel.TabIndex = 30;
            // 
            // WorkCombinationLabel
            // 
            this.WorkCombinationLabel.AutoSize = true;
            this.WorkCombinationLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WorkCombinationLabel.Location = new System.Drawing.Point(3, 6);
            this.WorkCombinationLabel.Name = "WorkCombinationLabel";
            this.WorkCombinationLabel.Size = new System.Drawing.Size(81, 16);
            this.WorkCombinationLabel.TabIndex = 0;
            this.WorkCombinationLabel.Text = " Комбинации";
            // 
            // CombinationDataGridView
            // 
            this.CombinationDataGridView.AllowUserToAddRows = false;
            this.CombinationDataGridView.AllowUserToResizeColumns = false;
            this.CombinationDataGridView.AllowUserToResizeRows = false;
            this.CombinationDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CombinationDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.CombinationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CombinationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckColumn,
            this.NameColumn,
            this.CostColumn,
            this.VolumeColumn,
            this.WeightColumn});
            this.CombinationDataGridView.Location = new System.Drawing.Point(429, 30);
            this.CombinationDataGridView.Name = "CombinationDataGridView";
            this.CombinationDataGridView.RowHeadersVisible = false;
            this.CombinationDataGridView.RowTemplate.Height = 20;
            this.CombinationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CombinationDataGridView.Size = new System.Drawing.Size(810, 521);
            this.CombinationDataGridView.TabIndex = 29;
            this.CombinationDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CombinationDataGridView_CellValueChanged);
            this.CombinationDataGridView.SelectionChanged += new System.EventHandler(this.CombinationDataGridView_SelectionChanged);
            // 
            // CheckColumn
            // 
            this.CheckColumn.HeaderText = "";
            this.CheckColumn.Name = "CheckColumn";
            this.CheckColumn.Width = 25;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = " Название";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.Width = 180;
            // 
            // CostColumn
            // 
            dataGridViewCellStyle1.Format = "C3";
            dataGridViewCellStyle1.NullValue = null;
            this.CostColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CostColumn.HeaderText = "Цена";
            this.CostColumn.Name = "CostColumn";
            this.CostColumn.ReadOnly = true;
            this.CostColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CostColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostColumn.Width = 200;
            // 
            // VolumeColumn
            // 
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.VolumeColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.VolumeColumn.HeaderText = "Объем";
            this.VolumeColumn.Name = "VolumeColumn";
            this.VolumeColumn.ReadOnly = true;
            this.VolumeColumn.Width = 200;
            // 
            // WeightColumn
            // 
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.WeightColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.WeightColumn.HeaderText = "Вес";
            this.WeightColumn.Name = "WeightColumn";
            this.WeightColumn.ReadOnly = true;
            this.WeightColumn.Width = 200;
            // 
            // SelectedItemDataGridView
            // 
            this.SelectedItemDataGridView.AllowUserToAddRows = false;
            this.SelectedItemDataGridView.AllowUserToResizeColumns = false;
            this.SelectedItemDataGridView.AllowUserToResizeRows = false;
            this.SelectedItemDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectedItemDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.SelectedItemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SelectedItemDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.SelectedItemDataGridView.Location = new System.Drawing.Point(219, 110);
            this.SelectedItemDataGridView.Name = "SelectedItemDataGridView";
            this.SelectedItemDataGridView.RowHeadersVisible = false;
            this.SelectedItemDataGridView.RowTemplate.Height = 20;
            this.SelectedItemDataGridView.Size = new System.Drawing.Size(204, 191);
            this.SelectedItemDataGridView.TabIndex = 28;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Название блока";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Количество";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AddNewObjectButton
            // 
            this.AddNewObjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewObjectButton.BackColor = System.Drawing.Color.White;
            this.AddNewObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddNewObjectButton.Location = new System.Drawing.Point(1100, 0);
            this.AddNewObjectButton.Name = "AddNewObjectButton";
            this.AddNewObjectButton.Size = new System.Drawing.Size(142, 23);
            this.AddNewObjectButton.TabIndex = 2;
            this.AddNewObjectButton.Text = "Создать новый объект";
            this.AddNewObjectButton.UseVisualStyleBackColor = false;
            this.AddNewObjectButton.Click += new System.EventHandler(this.AddNewObjectButton_Click);
            // 
            // EditObjectButton
            // 
            this.EditObjectButton.BackColor = System.Drawing.Color.White;
            this.EditObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditObjectButton.Location = new System.Drawing.Point(999, 0);
            this.EditObjectButton.Name = "EditObjectButton";
            this.EditObjectButton.Size = new System.Drawing.Size(101, 23);
            this.EditObjectButton.TabIndex = 29;
            this.EditObjectButton.Text = "Редактировать";
            this.EditObjectButton.UseVisualStyleBackColor = false;
            this.EditObjectButton.Click += new System.EventHandler(this.EditObjectButton_Click);
            // 
            // DrawPage
            // 
            this.DrawPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawPage.BackColor = System.Drawing.Color.White;
            this.DrawPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawPage.Location = new System.Drawing.Point(0, 25);
            this.DrawPage.Name = "DrawPage";
            this.DrawPage.Size = new System.Drawing.Size(993, 592);
            this.DrawPage.TabIndex = 0;
            this.DrawPage.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPage_Paint);
            this.DrawPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPage_MouseDown);
            this.DrawPage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPage_MouseMove);
            this.DrawPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPage_MouseUp);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1248, 618);
            this.Controls.Add(this.EditObjectButton);
            this.Controls.Add(this.GoNextButton);
            this.Controls.Add(this.GoBackButton);
            this.Controls.Add(this.AddNewObjectButton);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.WorkPanel);
            this.Controls.Add(this.DrawPage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1264, 657);
            this.MinimumSize = new System.Drawing.Size(1264, 657);
            this.Name = "EditorForm";
            this.Text = "Редактор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditorForm_KeyPress);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.AvailableObjectsPanel.ResumeLayout(false);
            this.AvailableObjectsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesGridView)).EndInit();
            this.WorkPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectedComplexDataGridView)).EndInit();
            this.FieldPropertyPanel.ResumeLayout(false);
            this.FieldPropertyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FieldPropertyDataGridView)).EndInit();
            this.WorkFieldPanel.ResumeLayout(false);
            this.WorkFieldPanel.PerformLayout();
            this.WorkPropetriesPanel.ResumeLayout(false);
            this.WorkPropetriesPanel.PerformLayout();
            this.WorkCombinationPanel.ResumeLayout(false);
            this.WorkCombinationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedItemDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripLabel CategoryStripLabel;
        public System.Windows.Forms.ToolStripComboBox CategoryStripComboBox;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TreeView ObjectsTreeView;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Label PropertiesLabel;
        private System.Windows.Forms.Panel AvailableObjectsPanel;
        private System.Windows.Forms.Label AvailableObjectsLabel;
        private System.Windows.Forms.ToolStripLabel TypeStripLabel;
        private System.Windows.Forms.ToolStripComboBox TypeStripComboBox;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator;
        private DrawPage DrawPage;
        public System.Windows.Forms.DataGridView PropertiesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueCol;
        private System.Windows.Forms.Button GoNextButton;
        private System.Windows.Forms.Button GoBackButton;
        private System.Windows.Forms.Panel WorkPanel;
        private System.Windows.Forms.Button AddNewObjectButton;
        private System.Windows.Forms.Button EditObjectButton;
        private System.Windows.Forms.Panel LinkInfoPanel;
        public System.Windows.Forms.DataGridView CombinationDataGridView;
        public System.Windows.Forms.DataGridView SelectedItemDataGridView;
        private System.Windows.Forms.Panel WorkFieldPanel;
        private System.Windows.Forms.Label WorkFieldLabel;
        private System.Windows.Forms.Panel WorkPropetriesPanel;
        private System.Windows.Forms.Label WorkPropertiesLabel;
        private System.Windows.Forms.Panel WorkCombinationPanel;
        private System.Windows.Forms.Label WorkCombinationLabel;
        private System.Windows.Forms.ComboBox FieldComboBox;
        private System.Windows.Forms.Panel FieldPropertyPanel;
        private System.Windows.Forms.Label FieldPropertyLabel;
        public System.Windows.Forms.DataGridView FieldPropertyDataGridView;
        private System.Windows.Forms.Button CalculateButton;
        public System.Windows.Forms.DataGridView SelectedComplexDataGridView;
        private System.Windows.Forms.Label ObjectPropertiesLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolumeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeightColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}