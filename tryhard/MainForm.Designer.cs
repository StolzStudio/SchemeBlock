using System.Windows.Forms;
using System.Drawing;
using HRGN = System.IntPtr;
using HWND = System.IntPtr;
using System.Runtime.InteropServices;

namespace tryhard
{
    public class DrawTabPage : TabPage
    {
        public DrawTabPage()
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

        public CMeta Meta;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.ObjectTypeCB = new System.Windows.Forms.ComboBox();
            this.ObjectModelCB = new System.Windows.Forms.ComboBox();
            this.EquipmentLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.CalcButton = new System.Windows.Forms.Button();
            this.DeleteBlockButton = new System.Windows.Forms.Button();
            this.MeetPanel = new System.Windows.Forms.Panel();
            this.GoObjectPageButton = new System.Windows.Forms.Button();
            this.GoDrawingPageButton = new System.Windows.Forms.Button();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.PagesControl = new System.Windows.Forms.TabControl();
            this.SchemePage = new tryhard.DrawTabPage();
            this.ObjectPage = new tryhard.DrawTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.MeetPanel.SuspendLayout();
            this.ControlsPanel.SuspendLayout();
            this.PagesControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddBlockButton
            // 
            this.AddBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddBlockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddBlockButton.Location = new System.Drawing.Point(0, 0);
            this.AddBlockButton.Name = "AddBlockButton";
            this.AddBlockButton.Size = new System.Drawing.Size(70, 70);
            this.AddBlockButton.TabIndex = 1;
            this.AddBlockButton.Text = "+";
            this.AddBlockButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddBlockButton.UseVisualStyleBackColor = true;
            this.AddBlockButton.Click += new System.EventHandler(this.AddBlockButton_Click);
            // 
            // ObjectTypeCB
            // 
            this.ObjectTypeCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectTypeCB.BackColor = System.Drawing.Color.White;
            this.ObjectTypeCB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObjectTypeCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectTypeCB.FormattingEnabled = true;
            this.ObjectTypeCB.Location = new System.Drawing.Point(88, 25);
            this.ObjectTypeCB.Name = "ObjectTypeCB";
            this.ObjectTypeCB.Size = new System.Drawing.Size(145, 28);
            this.ObjectTypeCB.TabIndex = 2;
            this.ObjectTypeCB.SelectedIndexChanged += new System.EventHandler(this.ObjectTypeCBSelectedIndexChanged);
            // 
            // ObjectModelCB
            // 
            this.ObjectModelCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectModelCB.BackColor = System.Drawing.Color.White;
            this.ObjectModelCB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObjectModelCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectModelCB.FormattingEnabled = true;
            this.ObjectModelCB.Location = new System.Drawing.Point(247, 25);
            this.ObjectModelCB.Name = "ObjectModelCB";
            this.ObjectModelCB.Size = new System.Drawing.Size(145, 28);
            this.ObjectModelCB.TabIndex = 3;
            this.ObjectModelCB.SelectedIndexChanged += new System.EventHandler(this.ObjectModelCBSelectedIndexChanged);
            // 
            // EquipmentLabel
            // 
            this.EquipmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EquipmentLabel.AutoSize = true;
            this.EquipmentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipmentLabel.Location = new System.Drawing.Point(85, 0);
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
            this.ModelLabel.Location = new System.Drawing.Point(250, 0);
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
            this.DataGridView.Location = new System.Drawing.Point(85, 100);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowTemplate.Height = 20;
            this.DataGridView.Size = new System.Drawing.Size(310, 348);
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
            this.CalcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CalcButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalcButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalcButton.Location = new System.Drawing.Point(275, 455);
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
            this.DeleteBlockButton.Location = new System.Drawing.Point(0, 100);
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
            this.MeetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MeetPanel.Controls.Add(this.GoObjectPageButton);
            this.MeetPanel.Controls.Add(this.GoDrawingPageButton);
            this.MeetPanel.Location = new System.Drawing.Point(15, 15);
            this.MeetPanel.Name = "MeetPanel";
            this.MeetPanel.Size = new System.Drawing.Size(820, 655);
            this.MeetPanel.TabIndex = 14;
            // 
            // GoObjectPageButton
            // 
            this.GoObjectPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GoObjectPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoObjectPageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GoObjectPageButton.Location = new System.Drawing.Point(500, 275);
            this.GoObjectPageButton.Name = "GoObjectPageButton";
            this.GoObjectPageButton.Size = new System.Drawing.Size(260, 75);
            this.GoObjectPageButton.TabIndex = 1;
            this.GoObjectPageButton.Text = "Go to object page";
            this.GoObjectPageButton.UseVisualStyleBackColor = true;
            this.GoObjectPageButton.Click += new System.EventHandler(this.ShowObjectPageButton_Click);
            // 
            // GoDrawingPageButton
            // 
            this.GoDrawingPageButton.AllowDrop = true;
            this.GoDrawingPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GoDrawingPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GoDrawingPageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GoDrawingPageButton.Location = new System.Drawing.Point(500, 145);
            this.GoDrawingPageButton.Name = "GoDrawingPageButton";
            this.GoDrawingPageButton.Size = new System.Drawing.Size(260, 75);
            this.GoDrawingPageButton.TabIndex = 0;
            this.GoDrawingPageButton.Text = "Go to scheme page";
            this.GoDrawingPageButton.UseVisualStyleBackColor = true;
            this.GoDrawingPageButton.Click += new System.EventHandler(this.ShowSchemePageButton_Click);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlsPanel.Controls.Add(this.DataGridView);
            this.ControlsPanel.Controls.Add(this.AddBlockButton);
            this.ControlsPanel.Controls.Add(this.CalcButton);
            this.ControlsPanel.Controls.Add(this.ObjectTypeCB);
            this.ControlsPanel.Controls.Add(this.DeleteBlockButton);
            this.ControlsPanel.Controls.Add(this.ObjectModelCB);
            this.ControlsPanel.Controls.Add(this.EquipmentLabel);
            this.ControlsPanel.Controls.Add(this.ModelLabel);
            this.ControlsPanel.Location = new System.Drawing.Point(855, 15);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(395, 486);
            this.ControlsPanel.TabIndex = 15;
            // 
            // PagesControl
            // 
            this.PagesControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PagesControl.Controls.Add(this.SchemePage);
            this.PagesControl.Controls.Add(this.ObjectPage);
            this.PagesControl.Location = new System.Drawing.Point(15, 15);
            this.PagesControl.Name = "PagesControl";
            this.PagesControl.SelectedIndex = 0;
            this.PagesControl.Size = new System.Drawing.Size(820, 655);
            this.PagesControl.TabIndex = 2;
            this.PagesControl.SelectedIndexChanged += new System.EventHandler(this.PagesControl_SelectedIndexChanged);
            // 
            // SchemePage
            // 
            this.SchemePage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SchemePage.Location = new System.Drawing.Point(4, 22);
            this.SchemePage.Name = "SchemePage";
            this.SchemePage.Padding = new System.Windows.Forms.Padding(3);
            this.SchemePage.Size = new System.Drawing.Size(812, 629);
            this.SchemePage.TabIndex = 0;
            this.SchemePage.Text = "Scheme Page";
            this.SchemePage.UseVisualStyleBackColor = true;
            this.SchemePage.Click += new System.EventHandler(this.SchemePage_Click);
            this.SchemePage.Paint += new System.Windows.Forms.PaintEventHandler(this.SchemePage_Paint);
            // 
            // ObjectPage
            // 
            this.ObjectPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectPage.Location = new System.Drawing.Point(4, 22);
            this.ObjectPage.Name = "ObjectPage";
            this.ObjectPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObjectPage.Size = new System.Drawing.Size(812, 629);
            this.ObjectPage.TabIndex = 1;
            this.ObjectPage.Text = "Object Page";
            this.ObjectPage.UseVisualStyleBackColor = true;
            this.ObjectPage.Click += new System.EventHandler(this.ObjectPage_Click);
            this.ObjectPage.Paint += new System.Windows.Forms.PaintEventHandler(this.ObjectPage_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.MeetPanel);
            this.Controls.Add(this.ControlsPanel);
            this.Controls.Add(this.PagesControl);
            this.Controls.Add(this.PictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.Text = "Gaby";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.MeetPanel.ResumeLayout(false);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.PagesControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button AddBlockButton;
        public  ComboBox ObjectTypeCB;
        public  ComboBox ObjectModelCB;
        private Label EquipmentLabel;
        private Label ModelLabel;
        private DataGridView DataGridView;
        private PictureBox PictureBox;
        private Button CalcButton;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn Parameters;
        public  Button DeleteBlockButton;
        private Panel MeetPanel;
        private Button GoDrawingPageButton;
        private Button GoObjectPageButton;
        private TabControl PagesControl;
        public DrawTabPage SchemePage;
        public DrawTabPage ObjectPage;
        private Panel ControlsPanel;
    }
}

