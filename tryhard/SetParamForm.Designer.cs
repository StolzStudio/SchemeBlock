namespace tryhard
{
    partial class SetParamForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetParamForm));
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.NamePanel = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ObjectParamDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectParamDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryLabel.Location = new System.Drawing.Point(20, 9);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(0, 16);
            this.CategoryLabel.TabIndex = 0;
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TypeLabel.Location = new System.Drawing.Point(130, 9);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(0, 16);
            this.TypeLabel.TabIndex = 1;
            // 
            // NamePanel
            // 
            this.NamePanel.BackColor = System.Drawing.Color.Orange;
            this.NamePanel.Controls.Add(this.CategoryLabel);
            this.NamePanel.Controls.Add(this.TypeLabel);
            this.NamePanel.Location = new System.Drawing.Point(-2, 0);
            this.NamePanel.Name = "NamePanel";
            this.NamePanel.Size = new System.Drawing.Size(287, 35);
            this.NamePanel.TabIndex = 2;
            // 
            // SaveButton
            // 
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveButton.Location = new System.Drawing.Point(10, 383);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(264, 30);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ObjectParamDataGridView
            // 
            this.ObjectParamDataGridView.AllowUserToAddRows = false;
            this.ObjectParamDataGridView.AllowUserToResizeColumns = false;
            this.ObjectParamDataGridView.AllowUserToResizeRows = false;
            this.ObjectParamDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ObjectParamDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ObjectParamDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ObjectParamDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectParamDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.ObjectParamDataGridView.Location = new System.Drawing.Point(10, 41);
            this.ObjectParamDataGridView.Name = "ObjectParamDataGridView";
            this.ObjectParamDataGridView.RowHeadersVisible = false;
            this.ObjectParamDataGridView.RowTemplate.Height = 20;
            this.ObjectParamDataGridView.Size = new System.Drawing.Size(264, 336);
            this.ObjectParamDataGridView.TabIndex = 38;
            this.ObjectParamDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ObjectParamDataGridView_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Название";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Значение";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 140;
            // 
            // SetParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 416);
            this.Controls.Add(this.ObjectParamDataGridView);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NamePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetParamForm";
            this.Text = "Редактирование объекта";
            this.NamePanel.ResumeLayout(false);
            this.NamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectParamDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Panel NamePanel;
        private System.Windows.Forms.Button SaveButton;
        public System.Windows.Forms.DataGridView ObjectParamDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}