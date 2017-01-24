namespace tryhard
{
    partial class WelcomeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ProjectListBox = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.RosneftPicture = new System.Windows.Forms.PictureBox();
            this.AddComplexButton = new System.Windows.Forms.Button();
            this.AddDeviceButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RosneftPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ваши проекты:";
            // 
            // ProjectListBox
            // 
            this.ProjectListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProjectListBox.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProjectListBox.FormattingEnabled = true;
            this.ProjectListBox.ItemHeight = 25;
            this.ProjectListBox.Items.AddRange(new object[] {
            "project1",
            "project2",
            "project3"});
            this.ProjectListBox.Location = new System.Drawing.Point(19, 49);
            this.ProjectListBox.Name = "ProjectListBox";
            this.ProjectListBox.Size = new System.Drawing.Size(275, 477);
            this.ProjectListBox.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // RosneftPicture
            // 
            this.RosneftPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RosneftPicture.ErrorImage = ((System.Drawing.Image)(resources.GetObject("RosneftPicture.ErrorImage")));
            this.RosneftPicture.Image = ((System.Drawing.Image)(resources.GetObject("RosneftPicture.Image")));
            this.RosneftPicture.InitialImage = ((System.Drawing.Image)(resources.GetObject("RosneftPicture.InitialImage")));
            this.RosneftPicture.Location = new System.Drawing.Point(234, 373);
            this.RosneftPicture.Name = "RosneftPicture";
            this.RosneftPicture.Size = new System.Drawing.Size(363, 180);
            this.RosneftPicture.TabIndex = 2;
            this.RosneftPicture.TabStop = false;
            this.RosneftPicture.WaitOnLoad = true;
            // 
            // AddComplexButton
            // 
            this.AddComplexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddComplexButton.Font = new System.Drawing.Font("Nirmala UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddComplexButton.Location = new System.Drawing.Point(324, 49);
            this.AddComplexButton.Name = "AddComplexButton";
            this.AddComplexButton.Size = new System.Drawing.Size(244, 76);
            this.AddComplexButton.TabIndex = 3;
            this.AddComplexButton.Text = "Создать новый комплекс";
            this.AddComplexButton.UseVisualStyleBackColor = true;
            this.AddComplexButton.Click += new System.EventHandler(this.AddComplexButton_Click);
            // 
            // AddDeviceButton
            // 
            this.AddDeviceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddDeviceButton.Font = new System.Drawing.Font("Nirmala UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddDeviceButton.Location = new System.Drawing.Point(324, 131);
            this.AddDeviceButton.Name = "AddDeviceButton";
            this.AddDeviceButton.Size = new System.Drawing.Size(244, 76);
            this.AddDeviceButton.TabIndex = 4;
            this.AddDeviceButton.Text = "Создать новое оборудование";
            this.AddDeviceButton.UseVisualStyleBackColor = true;
            this.AddDeviceButton.Click += new System.EventHandler(this.AddDeviceButton_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(596, 552);
            this.Controls.Add(this.AddDeviceButton);
            this.Controls.Add(this.AddComplexButton);
            this.Controls.Add(this.ProjectListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RosneftPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WelcomeForm";
            this.Text = "Gaby - стартовая страница";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WelcomeForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.RosneftPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ProjectListBox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox RosneftPicture;
        private System.Windows.Forms.Button AddComplexButton;
        private System.Windows.Forms.Button AddDeviceButton;
    }
}