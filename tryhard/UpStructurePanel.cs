﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Globalization;


namespace tryhard
{
    public partial class MainForm : Form
    {
        Dictionary<int, List<DownStructure>> DownStructures = new Dictionary<int, List<DownStructure>>();
        Dictionary<int, int> SelectedStructureTypes = new Dictionary<int, int>();
        public int selectedCell = -1;

        private void InitializeUpStructurePanel()
        {
            SetDefaultState();
            FillStructuresGridView();
        }

        public void FillStructuresGridView()
        {
            foreach (int Key in DrawManager.Blocks.Keys)
            {
                BaseObject baseObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[Key].ClassText, DrawManager.Blocks[Key].Id);
                Int64 weight = Convert.ToInt64(baseObject.GetType().GetProperty("Weight").GetValue(baseObject));
                Int64 cost = Convert.ToInt64(baseObject.GetType().GetProperty("Cost").GetValue(baseObject));
                StructuresGridView.Rows.Add(MetaDataManager.Instance.Dictionary[DrawManager.Blocks[Key].ClassText], 
                                            DrawManager.Blocks[Key].ModelText, weight, cost);
                StructuresGridView.Rows[StructuresGridView.Rows.Count - 1].Tag = Key;
                DownStructures.Add(Key, new List<DownStructure>() { new DownStructure(StructureType.Kesson, weight),
                                                                    new DownStructure(StructureType.Monoleg, weight),
                                                                    new DownStructure(StructureType.Multileg, weight) });
                SelectedStructureTypes.Add(Key, 0);
            }
            StructuresGridView.CurrentCell = StructuresGridView[0, 0];
            selectedCell = DrawManager.Blocks.Keys.First();
            SetCheckedRadioButton();
        }

        private void CalculateAllStructures()
        {
            foreach(int Key in DrawManager.Blocks.Keys)
            {
                BaseObject baseObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[Key].ClassText, DrawManager.Blocks[Key].Id);
                int weight = Convert.ToInt32(baseObject.GetType().GetProperty("Weight").GetValue(baseObject));
                Int64 cost = Convert.ToInt64(baseObject.GetType().GetProperty("Cost").GetValue(baseObject));
                foreach (DownStructure structure in DownStructures[Key])
                    structure.CalculateDownStructure(weight);
            }
        }

        public void CalculateStructure(int Key, Int64 weight)
        {
            DownStructures[Key][SelectedStructureTypes[Key]].CalculateDownStructure(weight);
        }

        private string ParseControlName(string aName, string Arg)
        {
            int pos = aName.IndexOf(Arg);
            return aName.Substring(0, pos);
        }

        private void FieldUpDown_ValueChanged(Object sender, EventArgs e)
        {
            if (selectedCell != -1)
            {
                string propertyName = ParseControlName((sender as NumericUpDown).Name, "UpDown");
                var typeField = typeof(Field);
                System.Reflection.PropertyInfo propertyInfo = typeField.GetProperty(propertyName);
                propertyInfo.SetValue(null, Convert.ToSingle((sender as NumericUpDown).Value));
                CalculateAllStructures();
                FillStructureTypeGridView();
            }
        }

        private void StructureUpDown_ValueChanged(Object sender, EventArgs e)
        {
            if (selectedCell != -1)
            {
                string propertyName = ParseControlName((sender as NumericUpDown).Name, "UpDown");
                System.Reflection.PropertyInfo propertyInfo = DownStructures[selectedCell][GetTagSelectedRadioBtn()].GetType().GetProperty(propertyName);
                propertyInfo.SetValue(DownStructures[selectedCell][SelectedStructureTypes[selectedCell]], Convert.ToSingle((sender as NumericUpDown).Value));
                BaseObject baseObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[selectedCell].ClassText, DrawManager.Blocks[selectedCell].Id);
                double weight = Convert.ToDouble(baseObject.GetType().GetProperty("Weight").GetValue(baseObject));
                CalculateStructure(selectedCell, Convert.ToInt64(baseObject.GetType().GetProperty("Weight").GetValue(baseObject)));
                FillStructureTypeGridView();
            }
        }

        private void StructuresGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DrawManager.Blocks.Count)
            {
                selectedCell = (int)StructuresGridView.Rows[e.RowIndex].Tag;
                SetCheckedRadioButton();
                FillStructureTypeGridView();
            }
        }

        private int GetTagSelectedRadioBtn()
        {
            object tag;
            if (KessonRadioButton.Checked) tag = KessonRadioButton.Tag;
            else if (MonolegRadioButton.Checked) tag = MonolegRadioButton.Tag;
            else tag = MultilegRadioButton.Tag;
            return Convert.ToInt32(tag);
        }

        private void FillFieldParametersPanel()
        {
            dLocalWaterUpDown.Value = Convert.ToDecimal(Field.dLocalWater);
            dGlobalWaterUpDown.Value = Convert.ToDecimal(Field.dGlobalWater);
            hWave001UpDown.Value = Convert.ToDecimal(Field.hWave001);
            hWave50UpDown.Value = Convert.ToDecimal(Field.hWave50);
            yWaterUpDown.Value = Convert.ToDecimal(Field.yWater);
            diameterIceUpDown.Value = Convert.ToDecimal(Field.diameterIce);
            dIceUpDown.Value = Convert.ToDecimal(Field.dIce);
            durabilityIceUpDown.Value = Convert.ToDecimal(Field.durabilityIce);
            speedIceUpDown.Value = Convert.ToDecimal(Field.speedIce);
            groundDurabilityUpDown.Value = Convert.ToDecimal(Field.durabilityGround);
        }
            
        private void FillStructureParametersPanel()
        {
            wCellUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].wCell);
            yMatUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].yMat);
            wUpStructureUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].wUpStructure);
            dWallCellUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].dWallCell);
            yMatBallastUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].yMatBallast);
        }

        private void SetDefaultState()
        {
            StructuresGridView.Rows.Clear();
            DownStructures.Clear();
            SelectedStructureTypes.Clear();

            FillFieldParametersPanel();

            wCellUpDown.Value = Convert.ToDecimal(20);
            yMatUpDown.Value = Convert.ToDecimal(2.5);
            wUpStructureUpDown.Value = Convert.ToDecimal(100);
            dWallCellUpDown.Value = Convert.ToDecimal(0.75);
            yMatBallastUpDown.Value = Convert.ToDecimal(1.6);

            StructureTypeGridView.Rows.Add("Диаметр поддерживающей ячейки", 0);
            StructureTypeGridView.Rows.Add("Высота поддерживающей ячейки", 0);
            StructureTypeGridView.Rows.Add("Количество поддерживающих ячеек", 0);
            StructureTypeGridView.Rows.Add("Диаметр ячейки основания", 0);
            StructureTypeGridView.Rows.Add("Высота ячейки основания", 0);
            StructureTypeGridView.Rows.Add("Количество ячеек основания", 0);
            StructureTypeGridView.Rows.Add("Вес нижнего строения", 0);
            StructureTypeGridView.Rows.Add("Стоимость нижнего строения", 0);
        }

        private void FillStructureTypeGridView()
        {
            DownStructure structure = DownStructures[selectedCell][SelectedStructureTypes[selectedCell]];
            StructureTypeGridView.Rows[0].Cells[1].Value = structure.supportCell.wOutside;
            StructureTypeGridView.Rows[1].Cells[1].Value = structure.supportCell.h;
            StructureTypeGridView.Rows[2].Cells[1].Value = structure.countSC;
            StructureTypeGridView.Rows[3].Cells[1].Value = structure.baseCell.wOutside;
            StructureTypeGridView.Rows[4].Cells[1].Value = structure.baseCell.h;
            StructureTypeGridView.Rows[5].Cells[1].Value = structure.countBC;
            StructureTypeGridView.Rows[6].Cells[1].Value = structure.weight;
            StructureTypeGridView.Rows[7].Cells[1].Value = structure.cost;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedStructureTypes[selectedCell] = Convert.ToInt32((sender as RadioButton).Tag);
            FillStructureTypeGridView();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            UpStructurePanel.SendToBack();
            ObjectsTreeView.Focus();
        }

        private void BackToSchemeButton_Click(object sender, EventArgs e)
        {
            UpStructurePanel.SendToBack();
            ObjectsTreeView.Focus();
        }

        private void SetCheckedRadioButton()
        {
            switch (SelectedStructureTypes[selectedCell])
            {
                case 0: KessonRadioButton.Checked = true; break;
                case 1: MonolegRadioButton.Checked = true; break;
                case 2: MultilegRadioButton.Checked = true; break;
            }
        }
    }
}
