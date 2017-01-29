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
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.TypeStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.CategoryStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CategoryStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.TypeStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel = new System.Windows.Forms.Panel();
            this.PossibilityLinkView = new System.Windows.Forms.DataGridView();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.ObjectsTreeView = new System.Windows.Forms.TreeView();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.PropertiesLabel = new System.Windows.Forms.Label();
            this.AvailableObjectsPanel = new System.Windows.Forms.Panel();
            this.AvailableObjectsLabel = new System.Windows.Forms.Label();
            this.ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PossibilityLinkView)).BeginInit();
            this.PropertiesPanel.SuspendLayout();
            this.AvailableObjectsPanel.SuspendLayout();
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
            this.ToolStrip.Size = new System.Drawing.Size(1302, 25);
            this.ToolStrip.TabIndex = 0;
            this.ToolStrip.Text = "toolStrip1";
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
            this.CategoryStripComboBox.Size = new System.Drawing.Size(121, 25);
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
            this.TypeStripComboBox.Size = new System.Drawing.Size(121, 25);
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
            // PossibilityLinkView
            // 
            this.PossibilityLinkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PossibilityLinkView.Location = new System.Drawing.Point(4, 324);
            this.PossibilityLinkView.Name = "PossibilityLinkView";
            this.PossibilityLinkView.Size = new System.Drawing.Size(243, 336);
            this.PossibilityLinkView.TabIndex = 25;
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
            // ToolStripSeparator
            // 
            this.ToolStripSeparator.Name = "ToolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1302, 693);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.ToolStrip);
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PossibilityLinkView)).EndInit();
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.AvailableObjectsPanel.ResumeLayout(false);
            this.AvailableObjectsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DrawPage MainPage;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripLabel CategoryStripLabel;
        private System.Windows.Forms.ToolStripComboBox CategoryStripComboBox;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.DataGridView PossibilityLinkView;
        private System.Windows.Forms.PropertyGrid PropertyGrid;
        private System.Windows.Forms.TreeView ObjectsTreeView;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Label PropertiesLabel;
        private System.Windows.Forms.Panel AvailableObjectsPanel;
        private System.Windows.Forms.Label AvailableObjectsLabel;
        private System.Windows.Forms.ToolStripLabel TypeStripLabel;
        private System.Windows.Forms.ToolStripComboBox TypeStripComboBox;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator;
    }
}