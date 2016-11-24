namespace tryhard
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
            this.DrawingPanel = new System.Windows.Forms.Panel();
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.EquipmentCB = new System.Windows.Forms.ComboBox();
            this.ModelCB = new System.Windows.Forms.ComboBox();
            this.EquipmentLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DrawingPanel
            // 
            this.DrawingPanel.AutoSize = true;
            this.DrawingPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DrawingPanel.Location = new System.Drawing.Point(12, 12);
            this.DrawingPanel.Name = "DrawingPanel";
            this.DrawingPanel.Size = new System.Drawing.Size(913, 586);
            this.DrawingPanel.TabIndex = 0;
            this.DrawingPanel.Click += new System.EventHandler(this.DrawingPanel_Click);
            this.DrawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingPanel_Paint);
            // 
            // AddBlockButton
            // 
            this.AddBlockButton.Location = new System.Drawing.Point(931, 27);
            this.AddBlockButton.Name = "AddBlockButton";
            this.AddBlockButton.Size = new System.Drawing.Size(49, 49);
            this.AddBlockButton.TabIndex = 1;
            this.AddBlockButton.Text = "+";
            this.AddBlockButton.UseVisualStyleBackColor = true;
            this.AddBlockButton.Click += new System.EventHandler(this.AddBlockButton_Click);
            // 
            // EquipmentCB
            // 
            this.EquipmentCB.FormattingEnabled = true;
            this.EquipmentCB.Location = new System.Drawing.Point(988, 42);
            this.EquipmentCB.Name = "EquipmentCB";
            this.EquipmentCB.Size = new System.Drawing.Size(159, 21);
            this.EquipmentCB.TabIndex = 2;
            this.EquipmentCB.SelectedIndexChanged += new System.EventHandler(this.EquipmentCBSelectedIndexChanged);
            // 
            // ModelCB
            // 
            this.ModelCB.FormattingEnabled = true;
            this.ModelCB.Location = new System.Drawing.Point(1164, 42);
            this.ModelCB.Name = "ModelCB";
            this.ModelCB.Size = new System.Drawing.Size(159, 21);
            this.ModelCB.TabIndex = 3;
            // 
            // EquipmentLabel
            // 
            this.EquipmentLabel.AutoSize = true;
            this.EquipmentLabel.Location = new System.Drawing.Point(986, 26);
            this.EquipmentLabel.Name = "EquipmentLabel";
            this.EquipmentLabel.Size = new System.Drawing.Size(112, 13);
            this.EquipmentLabel.TabIndex = 4;
            this.EquipmentLabel.Text = "Класс оборудования";
            // 
            // ModelLabel
            // 
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Location = new System.Drawing.Point(1161, 27);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(46, 13);
            this.ModelLabel.TabIndex = 5;
            this.ModelLabel.Text = "Модель";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 610);
            this.Controls.Add(this.ModelLabel);
            this.Controls.Add(this.EquipmentLabel);
            this.Controls.Add(this.ModelCB);
            this.Controls.Add(this.EquipmentCB);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.DrawingPanel);
            this.Name = "MainForm";
            this.Text = "Form1";
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
    }
}

