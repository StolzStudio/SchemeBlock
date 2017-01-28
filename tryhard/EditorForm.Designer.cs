namespace tryhard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CategoryStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.CategoryStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ModelNameStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.ModelNameStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.ObjectsTreeView = new System.Windows.Forms.TreeView();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.PropertiesLabel = new System.Windows.Forms.Label();
            this.AvailableObjectsPanel = new System.Windows.Forms.Panel();
            this.AvailableObjectsLabel = new System.Windows.Forms.Label();
            this.PossibilityLinkView = new System.Windows.Forms.DataGridView();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panel.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.AvailableObjectsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PossibilityLinkView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.SaveStripButton,
            this.toolStripSeparator1,
            this.CategoryStripLabel,
            this.CategoryStripComboBox,
            this.toolStripSeparator2,
            this.ModelNameStripLabel,
            this.ModelNameStripTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1302, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveStripButton
            // 
            this.SaveStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveStripButton.Image")));
            this.SaveStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveStripButton.Name = "SaveStripButton";
            this.SaveStripButton.Size = new System.Drawing.Size(23, 22);
            this.SaveStripButton.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CategoryStripLabel
            // 
            this.CategoryStripLabel.Name = "CategoryStripLabel";
            this.CategoryStripLabel.Size = new System.Drawing.Size(66, 22);
            this.CategoryStripLabel.Text = "Категория:";
            // 
            // CategoryStripComboBox
            // 
            this.CategoryStripComboBox.Name = "CategoryStripComboBox";
            this.CategoryStripComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ModelNameStripLabel
            // 
            this.ModelNameStripLabel.Name = "ModelNameStripLabel";
            this.ModelNameStripLabel.Size = new System.Drawing.Size(107, 22);
            this.ModelNameStripLabel.Text = "Название модели:";
            // 
            // ModelNameStripTextBox
            // 
            this.ModelNameStripTextBox.Name = "ModelNameStripTextBox";
            this.ModelNameStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.PossibilityLinkView);
            this.panel.Controls.Add(this.PropertyGrid);
            this.panel.Controls.Add(this.ObjectsTreeView);
            this.panel.Controls.Add(this.PropertiesPanel);
            this.panel.Controls.Add(this.AvailableObjectsPanel);
            this.panel.Location = new System.Drawing.Point(1047, 26);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(255, 663);
            this.panel.TabIndex = 1;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Location = new System.Drawing.Point(4, 323);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(243, 336);
            this.PropertyGrid.TabIndex = 24;
            // 
            // ObjectsTreeView
            // 
            this.ObjectsTreeView.Location = new System.Drawing.Point(4, 25);
            this.ObjectsTreeView.Name = "ObjectsTreeView";
            this.ObjectsTreeView.Size = new System.Drawing.Size(243, 274);
            this.ObjectsTreeView.TabIndex = 23;
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertiesPanel.BackColor = System.Drawing.Color.Orange;
            this.PropertiesPanel.Controls.Add(this.PropertiesLabel);
            this.PropertiesPanel.Location = new System.Drawing.Point(4, 302);
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
            this.AvailableObjectsPanel.Location = new System.Drawing.Point(4, 3);
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
            // PossibilityLinkView
            // 
            this.PossibilityLinkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PossibilityLinkView.Location = new System.Drawing.Point(4, 324);
            this.PossibilityLinkView.Name = "PossibilityLinkView";
            this.PossibilityLinkView.Size = new System.Drawing.Size(243, 336);
            this.PossibilityLinkView.TabIndex = 25;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1302, 693);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel.ResumeLayout(false);
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.AvailableObjectsPanel.ResumeLayout(false);
            this.AvailableObjectsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PossibilityLinkView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DrawPage MainPage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel CategoryStripLabel;
        private System.Windows.Forms.ToolStripComboBox CategoryStripComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel ModelNameStripLabel;
        private System.Windows.Forms.ToolStripTextBox ModelNameStripTextBox;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.DataGridView PossibilityLinkView;
        private System.Windows.Forms.PropertyGrid PropertyGrid;
        private System.Windows.Forms.TreeView ObjectsTreeView;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Label PropertiesLabel;
        private System.Windows.Forms.Panel AvailableObjectsPanel;
        private System.Windows.Forms.Label AvailableObjectsLabel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}