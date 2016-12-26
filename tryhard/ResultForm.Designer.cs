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
            this.CombinationGridView = new System.Windows.Forms.DataGridView();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolumeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CombinationGridView
            // 
            this.CombinationGridView.AllowUserToAddRows = false;
            this.CombinationGridView.AllowUserToResizeColumns = false;
            this.CombinationGridView.AllowUserToResizeRows = false;
            this.CombinationGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CombinationGridView.ColumnHeadersHeight = 35;
            this.CombinationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CombinationGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClassName,
            this.ModelName,
            this.Count,
            this.WeightCol,
            this.VolumeCol,
            this.Cost});
            this.CombinationGridView.EnableHeadersVisualStyles = false;
            this.CombinationGridView.Location = new System.Drawing.Point(361, 12);
            this.CombinationGridView.MaximumSize = new System.Drawing.Size(483, 458);
            this.CombinationGridView.MinimumSize = new System.Drawing.Size(483, 458);
            this.CombinationGridView.Name = "CombinationGridView";
            this.CombinationGridView.ReadOnly = true;
            this.CombinationGridView.RowHeadersVisible = false;
            this.CombinationGridView.RowHeadersWidth = 100;
            this.CombinationGridView.RowTemplate.Height = 20;
            this.CombinationGridView.Size = new System.Drawing.Size(483, 458);
            this.CombinationGridView.TabIndex = 0;
            // 
            // ClassName
            // 
            this.ClassName.HeaderText = "Класс оборудования";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            // 
            // ModelName
            // 
            this.ModelName.HeaderText = "Название оборудования";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Width = 70;
            // 
            // WeightCol
            // 
            this.WeightCol.HeaderText = "Масса";
            this.WeightCol.Name = "WeightCol";
            this.WeightCol.ReadOnly = true;
            // 
            // VolumeCol
            // 
            this.VolumeCol.HeaderText = "Объем";
            this.VolumeCol.Name = "VolumeCol";
            this.VolumeCol.ReadOnly = true;
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
            this.ClientSize = new System.Drawing.Size(856, 482);
            this.Controls.Add(this.CombinationGridView);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "ResultForm";
            this.Text = "Результаты рассчета";
            ((System.ComponentModel.ISupportInitialize)(this.CombinationGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView CombinationGridView;
        private DataGridViewTextBoxColumn ClassName;
        private DataGridViewTextBoxColumn ModelName;
        private DataGridViewTextBoxColumn Count;
        private DataGridViewTextBoxColumn WeightCol;
        private DataGridViewTextBoxColumn VolumeCol;
        private DataGridViewTextBoxColumn Cost;
    }
}