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
            this.ProjectsListBox = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.RosneftPicture = new System.Windows.Forms.PictureBox();
            this.WelcomeProgramLabel = new System.Windows.Forms.Label();
            this.DecoratingPanel1 = new System.Windows.Forms.Panel();
            this.ProgramVersionLabel = new System.Windows.Forms.Label();
            this.OpenAnotherProjectLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CreateNewProjectPanel = new System.Windows.Forms.Panel();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.MainDescriptionLabel = new System.Windows.Forms.Label();
            this.CreatingPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.RosneftPicture)).BeginInit();
            this.CreateNewProjectPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectsListBox
            // 
            this.ProjectsListBox.BackColor = System.Drawing.Color.OldLace;
            this.ProjectsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProjectsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ProjectsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectsListBox.FormattingEnabled = true;
            this.ProjectsListBox.ItemHeight = 43;
            this.ProjectsListBox.Items.AddRange(new object[] {
            "project1",
            "project2",
            "project3"});
            this.ProjectsListBox.Location = new System.Drawing.Point(0, 0);
            this.ProjectsListBox.Name = "ProjectsListBox";
            this.ProjectsListBox.Size = new System.Drawing.Size(323, 516);
            this.ProjectsListBox.TabIndex = 1;
            this.ProjectsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ProjectsListBox_DrawItem);
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
            this.RosneftPicture.Location = new System.Drawing.Point(573, 337);
            this.RosneftPicture.Name = "RosneftPicture";
            this.RosneftPicture.Size = new System.Drawing.Size(363, 180);
            this.RosneftPicture.TabIndex = 2;
            this.RosneftPicture.TabStop = false;
            this.RosneftPicture.WaitOnLoad = true;
            // 
            // WelcomeProgramLabel
            // 
            this.WelcomeProgramLabel.AutoSize = true;
            this.WelcomeProgramLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WelcomeProgramLabel.Location = new System.Drawing.Point(362, 82);
            this.WelcomeProgramLabel.Name = "WelcomeProgramLabel";
            this.WelcomeProgramLabel.Size = new System.Drawing.Size(551, 51);
            this.WelcomeProgramLabel.TabIndex = 5;
            this.WelcomeProgramLabel.Text = "Добро пожаловать в Gaby";
            // 
            // DecoratingPanel1
            // 
            this.DecoratingPanel1.BackColor = System.Drawing.Color.Orange;
            this.DecoratingPanel1.Location = new System.Drawing.Point(346, 194);
            this.DecoratingPanel1.Name = "DecoratingPanel1";
            this.DecoratingPanel1.Size = new System.Drawing.Size(577, 2);
            this.DecoratingPanel1.TabIndex = 6;
            // 
            // ProgramVersionLabel
            // 
            this.ProgramVersionLabel.AutoSize = true;
            this.ProgramVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProgramVersionLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ProgramVersionLabel.Location = new System.Drawing.Point(545, 153);
            this.ProgramVersionLabel.Name = "ProgramVersionLabel";
            this.ProgramVersionLabel.Size = new System.Drawing.Size(189, 25);
            this.ProgramVersionLabel.TabIndex = 7;
            this.ProgramVersionLabel.Text = "version 0.1 (0.A127)";
            // 
            // OpenAnotherProjectLabel
            // 
            this.OpenAnotherProjectLabel.AutoSize = true;
            this.OpenAnotherProjectLabel.BackColor = System.Drawing.Color.Beige;
            this.OpenAnotherProjectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OpenAnotherProjectLabel.ForeColor = System.Drawing.Color.Black;
            this.OpenAnotherProjectLabel.Location = new System.Drawing.Point(151, 492);
            this.OpenAnotherProjectLabel.Name = "OpenAnotherProjectLabel";
            this.OpenAnotherProjectLabel.Size = new System.Drawing.Size(175, 17);
            this.OpenAnotherProjectLabel.TabIndex = 8;
            this.OpenAnotherProjectLabel.Text = "Открыть другой проект...";
            this.OpenAnotherProjectLabel.Click += new System.EventHandler(this.OpenAnotherProjectLabel_Click);
            this.OpenAnotherProjectLabel.MouseEnter += new System.EventHandler(this.OpenAnotherProjectLabel_MouseEnter);
            this.OpenAnotherProjectLabel.MouseLeave += new System.EventHandler(this.OpenAnotherProjectLabel_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Location = new System.Drawing.Point(346, 329);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 2);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Orange;
            this.panel2.Location = new System.Drawing.Point(346, 260);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 2);
            this.panel2.TabIndex = 8;
            // 
            // CreateNewProjectPanel
            // 
            this.CreateNewProjectPanel.Controls.Add(this.DescriptionLabel);
            this.CreateNewProjectPanel.Controls.Add(this.MainDescriptionLabel);
            this.CreateNewProjectPanel.Location = new System.Drawing.Point(346, 200);
            this.CreateNewProjectPanel.Name = "CreateNewProjectPanel";
            this.CreateNewProjectPanel.Size = new System.Drawing.Size(577, 57);
            this.CreateNewProjectPanel.TabIndex = 9;
            this.CreateNewProjectPanel.Click += new System.EventHandler(this.CreateNewProjectPanel_Click);
            this.CreateNewProjectPanel.MouseEnter += new System.EventHandler(this.CreateNewProjectPanel_MouseEnter);
            this.CreateNewProjectPanel.MouseLeave += new System.EventHandler(this.CreateNewProjectPanel_MouseLeave);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DescriptionLabel.Location = new System.Drawing.Point(8, 35);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(459, 20);
            this.DescriptionLabel.TabIndex = 1;
            this.DescriptionLabel.Text = "Начните создавать строение, комплекс или оборудование";
            this.DescriptionLabel.Click += new System.EventHandler(this.CreateNewProjectPanel_Click);
            this.DescriptionLabel.MouseEnter += new System.EventHandler(this.CreateNewProjectPanel_MouseEnter);
            this.DescriptionLabel.MouseLeave += new System.EventHandler(this.CreateNewProjectPanel_MouseLeave);
            // 
            // MainDescriptionLabel
            // 
            this.MainDescriptionLabel.AutoSize = true;
            this.MainDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainDescriptionLabel.Location = new System.Drawing.Point(3, 0);
            this.MainDescriptionLabel.Name = "MainDescriptionLabel";
            this.MainDescriptionLabel.Size = new System.Drawing.Size(244, 25);
            this.MainDescriptionLabel.TabIndex = 0;
            this.MainDescriptionLabel.Text = "Создать новый проект";
            this.MainDescriptionLabel.Click += new System.EventHandler(this.CreateNewProjectPanel_Click);
            this.MainDescriptionLabel.MouseEnter += new System.EventHandler(this.CreateNewProjectPanel_MouseEnter);
            this.MainDescriptionLabel.MouseLeave += new System.EventHandler(this.CreateNewProjectPanel_MouseLeave);
            // 
            // CreatingPanel
            // 
            this.CreatingPanel.Location = new System.Drawing.Point(0, 0);
            this.CreatingPanel.Name = "CreatingPanel";
            this.CreatingPanel.Size = new System.Drawing.Size(935, 517);
            this.CreatingPanel.TabIndex = 10;
            this.CreatingPanel.Visible = false;
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(935, 517);
            this.Controls.Add(this.CreateNewProjectPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.OpenAnotherProjectLabel);
            this.Controls.Add(this.ProgramVersionLabel);
            this.Controls.Add(this.DecoratingPanel1);
            this.Controls.Add(this.WelcomeProgramLabel);
            this.Controls.Add(this.ProjectsListBox);
            this.Controls.Add(this.RosneftPicture);
            this.Controls.Add(this.CreatingPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WelcomeForm";
            this.Text = "Gaby - стартовая страница";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WelcomeForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.RosneftPicture)).EndInit();
            this.CreateNewProjectPanel.ResumeLayout(false);
            this.CreateNewProjectPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox ProjectsListBox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox RosneftPicture;
        private System.Windows.Forms.Label WelcomeProgramLabel;
        private System.Windows.Forms.Panel DecoratingPanel1;
        private System.Windows.Forms.Label ProgramVersionLabel;
        private System.Windows.Forms.Label OpenAnotherProjectLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel CreateNewProjectPanel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label MainDescriptionLabel;
        private System.Windows.Forms.Panel CreatingPanel;
    }
}