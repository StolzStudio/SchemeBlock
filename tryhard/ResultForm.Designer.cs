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
            this.CombinationsGridView = new System.Windows.Forms.DataGridView();
            this.CombinationNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombinationWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombinationVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombinationCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CombinationGridView
            // 
            this.CombinationGridView.AllowUserToAddRows = false;
            this.CombinationGridView.AllowUserToResizeColumns = false;
            this.CombinationGridView.AllowUserToResizeRows = false;
            this.CombinationGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CombinationGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CombinationGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
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
            this.CombinationGridView.Location = new System.Drawing.Point(12, 342);
            this.CombinationGridView.Name = "CombinationGridView";
            this.CombinationGridView.ReadOnly = true;
            this.CombinationGridView.RowHeadersVisible = false;
            this.CombinationGridView.RowHeadersWidth = 100;
            this.CombinationGridView.RowTemplate.Height = 20;
            this.CombinationGridView.Size = new System.Drawing.Size(565, 182);
            this.CombinationGridView.TabIndex = 0;
            // 
            // ClassName
            // 
            this.ClassName.HeaderText = "Класс оборудования";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            this.ClassName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // ModelName
            // 
            this.ModelName.HeaderText = "Название оборудования";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            this.ModelName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // WeightCol
            // 
            this.WeightCol.HeaderText = "Масса";
            this.WeightCol.Name = "WeightCol";
            this.WeightCol.ReadOnly = true;
            this.WeightCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // VolumeCol
            // 
            this.VolumeCol.HeaderText = "Объем";
            this.VolumeCol.Name = "VolumeCol";
            this.VolumeCol.ReadOnly = true;
            this.VolumeCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Cost
            // 
            this.Cost.HeaderText = "Стоимость";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            this.Cost.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // CombinationsGridView
            // 
            this.CombinationsGridView.AllowUserToAddRows = false;
            this.CombinationsGridView.AllowUserToResizeColumns = false;
            this.CombinationsGridView.AllowUserToResizeRows = false;
            this.CombinationsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CombinationsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CombinationsGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.CombinationsGridView.ColumnHeadersHeight = 35;
            this.CombinationsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CombinationsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CombinationNum,
            this.CombinationWeight,
            this.CombinationVolume,
            this.CombinationCost});
            this.CombinationsGridView.EnableHeadersVisualStyles = false;
            this.CombinationsGridView.Location = new System.Drawing.Point(12, 12);
            this.CombinationsGridView.Name = "CombinationsGridView";
            this.CombinationsGridView.ReadOnly = true;
            this.CombinationsGridView.RowHeadersVisible = false;
            this.CombinationsGridView.RowHeadersWidth = 100;
            this.CombinationsGridView.RowTemplate.Height = 20;
            this.CombinationsGridView.Size = new System.Drawing.Size(565, 324);
            this.CombinationsGridView.TabIndex = 1;
            this.CombinationsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CombinationsGridView_CellClick);
            // 
            // CombinationNum
            // 
            this.CombinationNum.HeaderText = "Номер комбинации";
            this.CombinationNum.Name = "CombinationNum";
            this.CombinationNum.ReadOnly = true;
            this.CombinationNum.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // CombinationWeight
            // 
            this.CombinationWeight.HeaderText = "Масса";
            this.CombinationWeight.Name = "CombinationWeight";
            this.CombinationWeight.ReadOnly = true;
            this.CombinationWeight.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // CombinationVolume
            // 
            this.CombinationVolume.HeaderText = "Объем";
            this.CombinationVolume.Name = "CombinationVolume";
            this.CombinationVolume.ReadOnly = true;
            this.CombinationVolume.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // CombinationCost
            // 
            this.CombinationCost.HeaderText = "Стоимость";
            this.CombinationCost.Name = "CombinationCost";
            this.CombinationCost.ReadOnly = true;
            this.CombinationCost.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 536);
            this.Controls.Add(this.CombinationsGridView);
            this.Controls.Add(this.CombinationGridView);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MinimumSize = new System.Drawing.Size(605, 575);
            this.Name = "ResultForm";
            this.Text = "Результаты рассчета";
            ((System.ComponentModel.ISupportInitialize)(this.CombinationGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationsGridView)).EndInit();
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
        private DataGridView CombinationsGridView;
        private DataGridViewTextBoxColumn CombinationNum;
        private DataGridViewTextBoxColumn CombinationWeight;
        private DataGridViewTextBoxColumn CombinationVolume;
        private DataGridViewTextBoxColumn CombinationCost;
    }
}