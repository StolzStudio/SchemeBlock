using System;
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


namespace tryhard
{
    public partial class MainForm : Form
    {
        Dictionary<int, List<DownStructure>> DownStructures = new Dictionary<int, List<DownStructure>>();
        Dictionary<int, int> SelectedStructureTypes = new Dictionary<int, int>();
        public int selectedCell = -1;

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

        public void FillStructuresGridView()
        {
            StructuresGridView.Rows.Clear();
            DownStructures.Clear();
            SelectedStructureTypes.Clear();
            SetDefaultValueParametersPanel();
            foreach (int Key in DrawManager.Blocks.Keys)
            {
                BaseObject baseObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[Key].ClassText, DrawManager.Blocks[Key].Id);
                int weight = Convert.ToInt32(baseObject.GetType().GetProperty("Weight").GetValue(baseObject));
                Int64 cost = Convert.ToInt64(baseObject.GetType().GetProperty("Cost").GetValue(baseObject));
                StructuresGridView.Rows.Add(MetaDataManager.Instance.Dictionary[DrawManager.Blocks[Key].ClassText], 
                                            DrawManager.Blocks[Key].ModelText, weight, cost);
                StructuresGridView.Rows[StructuresGridView.Rows.Count - 1].Tag = Key;
                DownStructures.Add(Key, new List<DownStructure>() { new DownStructure(StructureType.Kesson),
                                                                    new DownStructure(StructureType.Monoleg),
                                                                    new DownStructure(StructureType.Multileg) });
                foreach (DownStructure structure in DownStructures[Key])
                    structure.CalculateDownStructure(weight);
                SelectedStructureTypes.Add(Key, 0);
            }
            StructuresGridView.CurrentCell = StructuresGridView[0, 0];
            selectedCell = DrawManager.Blocks.Keys.First();
            StructureTypeGridView.Rows.Add("Стоимость", DownStructures[selectedCell][SelectedStructureTypes[selectedCell]].Cost);
            SetRadioBtnChecked();
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
            string propertyName = ParseControlName((sender as NumericUpDown).Name, "UpDown");
            var typeField = typeof(Field);
            System.Reflection.PropertyInfo propertyInfo = typeField.GetProperty(propertyName);
            propertyInfo.SetValue(null, Convert.ToSingle((sender as NumericUpDown).Value));
            //CalculateAllStructures();
        }

        private void StructureUpDown_ValueChanged(Object sender, EventArgs e)
        {
            string propertyName = ParseControlName((sender as NumericUpDown).Name, "UpDown");
            System.Reflection.PropertyInfo propertyInfo = DownStructures[selectedCell][GetTagSelectedRadioBtn()].GetType().GetProperty(propertyName);
            propertyInfo.SetValue(DownStructures[selectedCell][SelectedStructureTypes[selectedCell]], Convert.ToSingle((sender as NumericUpDown).Value));
            //CalculateStructure(selectedCell);
        }

        private int GetTagSelectedRadioBtn()
        {
            object tag = null;
            if (KessonRadioButton.Checked) tag = KessonRadioButton.Tag;
            else if (MonolegRadioButton.Checked) tag = MonolegRadioButton.Tag;
            else tag = MultilegRadioButton.Tag;
            return Convert.ToInt32(tag);
        }

        private void StructuresGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DrawManager.Blocks.Count)
            {
                selectedCell = (int)StructuresGridView.Rows[e.RowIndex].Tag;
                SetRadioBtnChecked();
                FillStructureParametersPanel();
            }
        }

        private void SetRadioBtnChecked()
        {
            switch (SelectedStructureTypes[selectedCell])
            {
                case 0: KessonRadioButton.Checked = true; break;
                case 1: MonolegRadioButton.Checked = true; break;
                case 2: MultilegRadioButton.Checked = true; break;
            }
            FillStructureTypeGridView();
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
            groundDurabilityUpDown.Value = Convert.ToDecimal(Field.groundDurability);
        }
            
        private void FillStructureParametersPanel()
        {
            wCellUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].wCell);
            yMatUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].yMat);
            wUpStructureUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].wUpStructure);
            dWallCellUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].dWallCell);
            yMatBallastUpDown.Value = Convert.ToDecimal(DownStructures[selectedCell][0].yMatBallast);
        }

        private void SetDefaultValueParametersPanel()
        {
            dLocalWaterUpDown.Value = Convert.ToDecimal(20);
            dGlobalWaterUpDown.Value = Convert.ToDecimal(50);
            hWave001UpDown.Value = Convert.ToDecimal(12);
            hWave50UpDown.Value = Convert.ToDecimal(6);
            yWaterUpDown.Value = Convert.ToDecimal(1.00);
            diameterIceUpDown.Value = Convert.ToDecimal(3000);
            dIceUpDown.Value = Convert.ToDecimal(3);
            durabilityIceUpDown.Value = Convert.ToDecimal(2);
            speedIceUpDown.Value = Convert.ToDecimal(0.5);
            groundDurabilityUpDown.Value = Convert.ToDecimal(2500);

            wCellUpDown.Value = Convert.ToDecimal(20);
            yMatUpDown.Value = Convert.ToDecimal(2.5);
            wUpStructureUpDown.Value = Convert.ToDecimal(100);
            dWallCellUpDown.Value = Convert.ToDecimal(0.75);
            yMatBallastUpDown.Value = Convert.ToDecimal(1.6);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SelectedStructureTypes[selectedCell] = Convert.ToInt32((sender as RadioButton).Tag);
            FillStructureTypeGridView();
        }

        private void FillStructureTypeGridView()
        {
            StructureTypeGridView.Rows.Clear();
            StructureTypeGridView.Rows.Add("Стоимость", DownStructures[selectedCell][SelectedStructureTypes[selectedCell]].Cost);
        }
    }
}
