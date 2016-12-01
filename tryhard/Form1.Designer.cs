﻿namespace tryhard
{
    partial class MainForm
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
        ///

        public CMeta Meta;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DrawingPanel = new System.Windows.Forms.Panel();
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.EquipmentCB = new System.Windows.Forms.ComboBox();
            this.ModelCB = new System.Windows.Forms.ComboBox();
            this.EquipmentLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.OutputSchemeLabel = new System.Windows.Forms.Label();
            this.InputSchemeLabel = new System.Windows.Forms.Label();
            this.OutputSchemeComboBox = new System.Windows.Forms.ComboBox();
            this.InputSchemeComboBox = new System.Windows.Forms.ComboBox();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.CalcButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawingPanel
            // 
            this.DrawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingPanel.AutoSize = true;
            this.DrawingPanel.BackColor = System.Drawing.Color.White;
            this.DrawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawingPanel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.DrawingPanel.Location = new System.Drawing.Point(15, 50);
            this.DrawingPanel.Name = "DrawingPanel";
            this.DrawingPanel.Size = new System.Drawing.Size(820, 620);
            this.DrawingPanel.TabIndex = 0;
            this.DrawingPanel.Click += new System.EventHandler(this.DrawingPanel_Click);
            this.DrawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingPanel_Paint);
            // 
            // AddBlockButton
            // 
            this.AddBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddBlockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddBlockButton.Location = new System.Drawing.Point(855, 15);
            this.AddBlockButton.Name = "AddBlockButton";
            this.AddBlockButton.Size = new System.Drawing.Size(70, 70);
            this.AddBlockButton.TabIndex = 1;
            this.AddBlockButton.Text = "+";
            this.AddBlockButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddBlockButton.UseVisualStyleBackColor = true;
            this.AddBlockButton.Click += new System.EventHandler(this.AddBlockButton_Click);
            // 
            // EquipmentCB
            // 
            this.EquipmentCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EquipmentCB.BackColor = System.Drawing.Color.White;
            this.EquipmentCB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EquipmentCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipmentCB.FormattingEnabled = true;
            this.EquipmentCB.Location = new System.Drawing.Point(942, 50);
            this.EquipmentCB.Name = "EquipmentCB";
            this.EquipmentCB.Size = new System.Drawing.Size(145, 33);
            this.EquipmentCB.TabIndex = 2;
            this.EquipmentCB.SelectedIndexChanged += new System.EventHandler(this.EquipmentCBSelectedIndexChanged);
            // 
            // ModelCB
            // 
            this.ModelCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelCB.BackColor = System.Drawing.Color.White;
            this.ModelCB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ModelCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelCB.FormattingEnabled = true;
            this.ModelCB.Location = new System.Drawing.Point(1107, 50);
            this.ModelCB.Name = "ModelCB";
            this.ModelCB.Size = new System.Drawing.Size(145, 33);
            this.ModelCB.TabIndex = 3;
            // 
            // EquipmentLabel
            // 
            this.EquipmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EquipmentLabel.AutoSize = true;
            this.EquipmentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipmentLabel.Location = new System.Drawing.Point(939, 15);
            this.EquipmentLabel.Name = "EquipmentLabel";
            this.EquipmentLabel.Size = new System.Drawing.Size(159, 18);
            this.EquipmentLabel.TabIndex = 4;
            this.EquipmentLabel.Text = "Класс оборудования:";
            // 
            // ModelLabel
            // 
            this.ModelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelLabel.Location = new System.Drawing.Point(1104, 15);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(68, 18);
            this.ModelLabel.TabIndex = 5;
            this.ModelLabel.Text = "Модель:";
            // 
            // OutputSchemeLabel
            // 
            this.OutputSchemeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputSchemeLabel.AutoSize = true;
            this.OutputSchemeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputSchemeLabel.Location = new System.Drawing.Point(1104, 100);
            this.OutputSchemeLabel.Name = "OutputSchemeLabel";
            this.OutputSchemeLabel.Size = new System.Drawing.Size(153, 18);
            this.OutputSchemeLabel.TabIndex = 9;
            this.OutputSchemeLabel.Text = "Выходной параметр:";
            // 
            // InputSchemeLabel
            // 
            this.InputSchemeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InputSchemeLabel.AutoSize = true;
            this.InputSchemeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputSchemeLabel.Location = new System.Drawing.Point(939, 100);
            this.InputSchemeLabel.Name = "InputSchemeLabel";
            this.InputSchemeLabel.Size = new System.Drawing.Size(142, 18);
            this.InputSchemeLabel.TabIndex = 8;
            this.InputSchemeLabel.Text = "Входной параметр:";
            // 
            // OutputSchemeComboBox
            // 
            this.OutputSchemeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputSchemeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OutputSchemeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputSchemeComboBox.FormattingEnabled = true;
            this.OutputSchemeComboBox.Location = new System.Drawing.Point(1107, 135);
            this.OutputSchemeComboBox.Name = "OutputSchemeComboBox";
            this.OutputSchemeComboBox.Size = new System.Drawing.Size(145, 33);
            this.OutputSchemeComboBox.TabIndex = 7;
            // 
            // InputSchemeComboBox
            // 
            this.InputSchemeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InputSchemeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.InputSchemeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputSchemeComboBox.FormattingEnabled = true;
            this.InputSchemeComboBox.Location = new System.Drawing.Point(942, 135);
            this.InputSchemeComboBox.Name = "InputSchemeComboBox";
            this.InputSchemeComboBox.Size = new System.Drawing.Size(145, 33);
            this.InputSchemeComboBox.TabIndex = 6;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.Parameters});
            this.DataGridView.EnableHeadersVisualStyles = false;
            this.DataGridView.Location = new System.Drawing.Point(942, 191);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.Size = new System.Drawing.Size(310, 300);
            this.DataGridView.TabIndex = 10;
            // 
            // name
            // 
            this.name.HeaderText = "Название";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Parameters
            // 
            this.Parameters.HeaderText = "Параметры";
            this.Parameters.Name = "Parameters";
            this.Parameters.ReadOnly = true;
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PictureBox.ErrorImage")));
            this.PictureBox.ImageLocation = "Pictures/formpicture.jpg";
            this.PictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox.InitialImage")));
            this.PictureBox.Location = new System.Drawing.Point(855, 503);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(410, 180);
            this.PictureBox.TabIndex = 11;
            this.PictureBox.TabStop = false;
            // 
            // CalcButton
            // 
            this.CalcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CalcButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalcButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalcButton.Location = new System.Drawing.Point(730, 15);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(105, 30);
            this.CalcButton.TabIndex = 12;
            this.CalcButton.Text = "Рассчитать";
            this.CalcButton.UseVisualStyleBackColor = true;
            this.CalcButton.Click += new System.EventHandler(this.CalcButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.CalcButton);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.OutputSchemeLabel);
            this.Controls.Add(this.InputSchemeLabel);
            this.Controls.Add(this.OutputSchemeComboBox);
            this.Controls.Add(this.InputSchemeComboBox);
            this.Controls.Add(this.ModelLabel);
            this.Controls.Add(this.EquipmentLabel);
            this.Controls.Add(this.ModelCB);
            this.Controls.Add(this.EquipmentCB);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.DrawingPanel);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.Text = "A$IRIA";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.Panel DrawingPanel;
        private System.Windows.Forms.Button AddBlockButton;
        private System.Windows.Forms.ComboBox EquipmentCB;
        private System.Windows.Forms.ComboBox ModelCB;
        private System.Windows.Forms.Label EquipmentLabel;
        private System.Windows.Forms.Label ModelLabel;
        private System.Windows.Forms.Label OutputSchemeLabel;
        private System.Windows.Forms.Label InputSchemeLabel;
        private System.Windows.Forms.ComboBox OutputSchemeComboBox;
        private System.Windows.Forms.ComboBox InputSchemeComboBox;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button CalcButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameters;
    }
}

