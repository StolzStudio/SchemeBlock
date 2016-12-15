using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    partial class ResultForm
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
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultGridView
            // 
            this.ResultGridView.AllowUserToAddRows = false;
            this.ResultGridView.AllowUserToResizeColumns = false;
            this.ResultGridView.AllowUserToResizeRows = false;
            this.ResultGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultGridView.ColumnHeadersHeight = 35;
            this.ResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ResultGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClassName,
            this.ModelName,
            this.Count,
            this.Cost});
            this.ResultGridView.EnableHeadersVisualStyles = false;
            this.ResultGridView.Location = new System.Drawing.Point(12, 12);
            this.ResultGridView.MaximumSize = new System.Drawing.Size(483, 458);
            this.ResultGridView.MinimumSize = new System.Drawing.Size(483, 458);
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.ReadOnly = true;
            this.ResultGridView.RowHeadersVisible = false;
            this.ResultGridView.RowHeadersWidth = 100;
            this.ResultGridView.RowTemplate.Height = 20;
            this.ResultGridView.Size = new System.Drawing.Size(483, 458);
            this.ResultGridView.TabIndex = 0;
            // 
            // ClassName
            // 
            this.ClassName.HeaderText = "Класс оборудования";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            this.ClassName.Width = 120;
            // 
            // ModelName
            // 
            this.ModelName.HeaderText = "Название оборудования";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            this.ModelName.Width = 120;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Width = 120;
            // 
            // Cost
            // 
            this.Cost.HeaderText = "Стоимость";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            this.Cost.Width = 120;
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 482);
            this.Controls.Add(this.ResultGridView);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "ResultForm";
            this.Text = "Результаты рассчета";
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView ResultGridView;
        private DataGridViewTextBoxColumn ClassName;
        private DataGridViewTextBoxColumn ModelName;
        private DataGridViewTextBoxColumn Count;
        private DataGridViewTextBoxColumn Cost;
    }
}