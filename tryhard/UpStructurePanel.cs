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
            foreach (int Key in DrawManager.Blocks.Keys)
            {
                BaseObject baseObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[Key].ClassText, DrawManager.Blocks[Key].Id);
                int weight = Convert.ToInt32(baseObject.GetType().GetProperty("Weight").GetValue(baseObject));
                StructuresGridView.Rows.Add(MetaDataManager.Instance.Dictionary[DrawManager.Blocks[Key].ClassText], 
                                            DrawManager.Blocks[Key].ModelText, weight);
                StructuresGridView.Rows[StructuresGridView.Rows.Count - 1].Tag = Key;
                DownStructures.Add(Key, new List<DownStructure>() { new DownStructure(StructureType.Kesson),
                                                                    new DownStructure(StructureType.Monoleg),
                                                                    new DownStructure(StructureType.Multileg) });
                CalculateStructure(Key);
            }
        }

        public void CalculateStructure(int Key)
        {
            foreach (DownStructure downStructure in DownStructures[Key])
            {
                
            }
        }

        public void SetStructureParanetersPanel()
        {

        }

        private void NumericUpDown_ValueChanged(Object sender, EventArgs e)
        {
            string propertyName = "";
            switch ((sender as NumericUpDown).Name)
            {
                case "LocalWaterDepthUpDown": break;
                case "GlobalWaterDepthUpDown": break;
                case "WidthCellUpDown": break;
                case "YMatUpDown": break;
                case "UpStructureSizeUpDown": break;
                case "HWave001UpDown": break;
                case "HWave50UpDown": break;
                case "DWallCellUpDown": break;
                case "YWaterUpDown": break;
            }
        }
    }
}
