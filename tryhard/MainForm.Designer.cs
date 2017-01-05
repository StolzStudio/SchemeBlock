using System.Windows.Forms;

namespace tryhard
{
    public class DrawPanel : Panel
    {
        public DrawPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }
    }

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
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.EquipmentCB = new System.Windows.Forms.ComboBox();
            this.ModelCB = new System.Windows.Forms.ComboBox();
            this.EquipmentLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.CalcButton = new System.Windows.Forms.Button();
            this.DeleteBlockButton = new System.Windows.Forms.Button();
            this.MeetPanel = new System.Windows.Forms.Panel();
            this.GoCalcPanelButton = new System.Windows.Forms.Button();
            this.GoDrawingPanelButton = new System.Windows.Forms.Button();
            this.DrawingPanel = new tryhard.DrawPanel();
            this.ShowDrawingPanelButton = new System.Windows.Forms.Button();
            this.CalcPanel = new System.Windows.Forms.Panel();
            this.ShowCalcPanelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.MeetPanel.SuspendLayout();
            this.SuspendLayout();
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
            this.EquipmentCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipmentCB.FormattingEnabled = true;
            this.EquipmentCB.Location = new System.Drawing.Point(940, 50);
            this.EquipmentCB.Name = "EquipmentCB";
            this.EquipmentCB.Size = new System.Drawing.Size(145, 28);
            this.EquipmentCB.TabIndex = 2;
            this.EquipmentCB.SelectedIndexChanged += new System.EventHandler(this.EquipmentCBSelectedIndexChanged);
            // 
            // ModelCB
            // 
            this.ModelCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelCB.BackColor = System.Drawing.Color.White;
            this.ModelCB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ModelCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelCB.FormattingEnabled = true;
            this.ModelCB.Location = new System.Drawing.Point(1105, 50);
            this.ModelCB.Name = "ModelCB";
            this.ModelCB.Size = new System.Drawing.Size(145, 28);
            this.ModelCB.TabIndex = 3;
            this.ModelCB.SelectedIndexChanged += new System.EventHandler(this.ModelCBSelectedIndexChanged);
            // 
            // EquipmentLabel
            // 
            this.EquipmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EquipmentLabel.AutoSize = true;
            this.EquipmentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipmentLabel.Location = new System.Drawing.Point(940, 15);
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
            this.ModelLabel.Location = new System.Drawing.Point(1105, 15);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(68, 18);
            this.ModelLabel.TabIndex = 5;
            this.ModelLabel.Text = "Модель:";
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToResizeColumns = false;
            this.DataGridView.AllowUserToResizeRows = false;
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView.ColumnHeadersHeight = 24;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.Parameters});
            this.DataGridView.EnableHeadersVisualStyles = false;
            this.DataGridView.Location = new System.Drawing.Point(940, 115);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowTemplate.Height = 20;
            this.DataGridView.Size = new System.Drawing.Size(310, 299);
            this.DataGridView.TabIndex = 10;
            // 
            // name
            // 
            this.name.HeaderText = "Название";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.name.Width = 154;
            // 
            // Parameters
            // 
            this.Parameters.HeaderText = "Параметры";
            this.Parameters.Name = "Parameters";
            this.Parameters.ReadOnly = true;
            this.Parameters.Width = 153;
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PictureBox.ErrorImage")));
            this.PictureBox.ImageLocation = "Pictures/formpicture.jpg";
            this.PictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox.InitialImage")));
            this.PictureBox.Location = new System.Drawing.Point(855, 502);
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
            this.CalcButton.Location = new System.Drawing.Point(715, 15);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(120, 30);
            this.CalcButton.TabIndex = 12;
            this.CalcButton.Text = "Рассчитать";
            this.CalcButton.UseVisualStyleBackColor = true;
            this.CalcButton.Click += new System.EventHandler(this.CalcButton_Click);
            // 
            // DeleteBlockButton
            // 
            this.DeleteBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteBlockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteBlockButton.Location = new System.Drawing.Point(855, 115);
            this.DeleteBlockButton.Name = "DeleteBlockButton";
            this.DeleteBlockButton.Size = new System.Drawing.Size(70, 70);
            this.DeleteBlockButton.TabIndex = 13;
            this.DeleteBlockButton.Text = "-";
            this.DeleteBlockButton.UseVisualStyleBackColor = true;
            this.DeleteBlockButton.Visible = false;
            this.DeleteBlockButton.Click += new System.EventHandler(this.DeleteBlockButton_Click);
            // 
            // MeetPanel
            // 
            this.MeetPanel.Controls.Add(this.GoCalcPanelButton);
            this.MeetPanel.Controls.Add(this.GoDrawingPanelButton);
            this.MeetPanel.Location = new System.Drawing.Point(15, 50);
            this.MeetPanel.Name = "MeetPanel";
            this.MeetPanel.Size = new System.Drawing.Size(820, 620);
            this.MeetPanel.TabIndex = 14;
            // 
            // GoCalcPanelButton
            // 
            this.GoCalcPanelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoCalcPanelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GoCalcPanelButton.Location = new System.Drawing.Point(254, 271);
            this.GoCalcPanelButton.Name = "GoCalcPanelButton";
            this.GoCalcPanelButton.Size = new System.Drawing.Size(266, 75);
            this.GoCalcPanelButton.TabIndex = 1;
            this.GoCalcPanelButton.Text = "Go to calc panel";
            this.GoCalcPanelButton.UseVisualStyleBackColor = true;
            this.GoCalcPanelButton.Click += new System.EventHandler(this.ShowCalcPanelButton_Click);
            // 
            // GoDrawingPanelButton
            // 
            this.GoDrawingPanelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoDrawingPanelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GoDrawingPanelButton.Location = new System.Drawing.Point(254, 143);
            this.GoDrawingPanelButton.Name = "GoDrawingPanelButton";
            this.GoDrawingPanelButton.Size = new System.Drawing.Size(266, 75);
            this.GoDrawingPanelButton.TabIndex = 0;
            this.GoDrawingPanelButton.Text = "Go to drawing panel";
            this.GoDrawingPanelButton.UseVisualStyleBackColor = true;
            this.GoDrawingPanelButton.Click += new System.EventHandler(this.ShowDrawingPanelButton_Click);
            // 
            // DrawingPanel
            // 
            this.DrawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            // ShowDrawingPanelButton
            // 
            this.ShowDrawingPanelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ShowDrawingPanelButton.Location = new System.Drawing.Point(15, 15);
            this.ShowDrawingPanelButton.Name = "ShowDrawingPanelButton";
            this.ShowDrawingPanelButton.Size = new System.Drawing.Size(120, 30);
            this.ShowDrawingPanelButton.TabIndex = 15;
            this.ShowDrawingPanelButton.Text = "Drawing panel";
            this.ShowDrawingPanelButton.UseVisualStyleBackColor = true;
            this.ShowDrawingPanelButton.Click += new System.EventHandler(this.ShowDrawingPanelButton_Click);
            // 
            // CalcPanel
            // 
            this.CalcPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CalcPanel.Location = new System.Drawing.Point(15, 50);
            this.CalcPanel.Name = "CalcPanel";
            this.CalcPanel.Size = new System.Drawing.Size(820, 620);
            this.CalcPanel.TabIndex = 16;
            // 
            // ShowCalcPanelButton
            // 
            this.ShowCalcPanelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ShowCalcPanelButton.Location = new System.Drawing.Point(141, 14);
            this.ShowCalcPanelButton.Name = "ShowCalcPanelButton";
            this.ShowCalcPanelButton.Size = new System.Drawing.Size(120, 30);
            this.ShowCalcPanelButton.TabIndex = 17;
            this.ShowCalcPanelButton.Text = "Calc panel";
            this.ShowCalcPanelButton.UseVisualStyleBackColor = true;
            this.ShowCalcPanelButton.Click += new System.EventHandler(this.ShowCalcPanelButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.ShowCalcPanelButton);
            this.Controls.Add(this.ShowDrawingPanelButton);
            this.Controls.Add(this.MeetPanel);
            this.Controls.Add(this.DeleteBlockButton);
            this.Controls.Add(this.CalcButton);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.ModelLabel);
            this.Controls.Add(this.EquipmentLabel);
            this.Controls.Add(this.ModelCB);
            this.Controls.Add(this.EquipmentCB);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.DrawingPanel);
            this.Controls.Add(this.CalcPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.Text = "Gaby";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.MeetPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  DrawPanel DrawingPanel;
        private Button AddBlockButton;
        public  ComboBox EquipmentCB;
        public  ComboBox ModelCB;
        private Label EquipmentLabel;
        private Label ModelLabel;
        private DataGridView DataGridView;
        private PictureBox PictureBox;
        private Button CalcButton;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn Parameters;
        public  Button DeleteBlockButton;
        private Panel MeetPanel;
        private Button GoDrawingPanelButton;
        private Button ShowDrawingPanelButton;
        private Button GoCalcPanelButton;
        private Panel CalcPanel;
        private Button ShowCalcPanelButton;
    }
}

