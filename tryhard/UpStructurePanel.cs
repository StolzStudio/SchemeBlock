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
                BaseObject baseObject= MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[Key].ClassText, DrawManager.Blocks[Key].Id);
                int weight = Convert.ToInt32(baseObject.GetType().GetProperty("Weight").GetValue(baseObject));
                StructuresGridView.Rows.Add(DrawManager.Blocks[Key].ClassText, DrawManager.Blocks[Key].ModelText, weight);
                StructuresGridView.Rows[StructuresGridView.Rows.Count - 1].Tag = Key;
                DownStructures.Add(Key, new List<DownStructure>() { new DownStructure(StructureType.Kesson),
                                                                    new DownStructure(StructureType.Monoleg),
                                                                    new DownStructure(StructureType.Multileg) });
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
    }
}
