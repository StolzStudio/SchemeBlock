using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing.Text;
using System.IO;

namespace tryhard
{
    public partial class MainForm : Form
    {
        public System.Drawing.Point DrawingPanelOffset;

        public int  InputSchemeIndex;
        public int  InputSchemePointIndex;
        public bool isBlockPointClick = false;

        public SchemeBlock[] Blocks;
        public  SchemeLink[]  Links;

        private List<string> ItemsIdList = new List<string>();

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            Blocks = new SchemeBlock[0];
            Links  = new SchemeLink[0];
            InitializeComponent();
            FillEquipmentCB();
            FillModelCB((string)EquipmentCB.Items[0]);
            DrawingPanelOffset = DrawingPanel.Location;
        }

        public void AddSchemeLink(SchemeLink ANewLink)
        {
            Array.Resize(ref Links, Links.Length + 1);
            Links[Links.Length - 1] = ANewLink;
            DrawingPanel.Invalidate();
        }

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            Array.Resize(ref Blocks, Blocks.Length + 1);
            Point Pos = new Point(10, 10 * Blocks.Length + 60 * Blocks.Length);
            Blocks[Blocks.Length - 1] = new SchemeBlock(Blocks.Length - 1, 
                                                        (string)EquipmentCB.SelectedItem, 
                                                        ModelCB.SelectedIndex, Pos, this);
            for (int i = 0; i < Blocks.Length; i++)
            {
               Blocks[i].ClearFocus();
            }
            Blocks[Blocks.Length - 1].SetFocus();
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Pen BlackPen   = new Pen(Color.DarkSlateBlue);
            BlackPen.Width = 1.5F;
            if (Links.Length > 0)
            {
                for (int i = 0; i < Links.Length; i++)
                {
                    e.Graphics.DrawLine(BlackPen, Links[i].GetInputSchemePointLocation(this), Links[i].GetOutputSchemePointLocation(this));
                }
            }
        }

        private void EquipmentCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ModelCB.Items.Clear();
            FillModelCB((string)EquipmentCB.SelectedItem);
            FillParametersGrid((string)EquipmentCB.SelectedItem, ItemsIdList[ModelCB.SelectedIndex]);
        }

        /* Equipment ComboBoxes */

        private void FillEquipmentCB()
        {
            for (int i = 0; i < Meta.TablesList.Count; i++)
            {
                EquipmentCB.Items.Add(Meta.TablesList[i]);
            }
            EquipmentCB.SelectedIndex = 0;
            EquipmentCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void FillModelCB(string AEquipmentName)
        {
            ModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            List<string> items = Meta.GetListRecordsWithId(AEquipmentName, "name");
            ItemsIdList.Clear();
            for (int i = 0; i < items.Count; i += 2)
            {
                ItemsIdList.Add(items[i]);
                ModelCB.Items.Add(items[i + 1]);
            }
            ModelCB.SelectedIndex = 0;
            ModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void FillParametersGrid(string ATableName, string AFieldId)
        {
            DataGridView.Rows.Clear();
            List<string> FieldData = Meta.GetFieldData(ATableName, AFieldId);
            List<string> NameCols = Meta.GetListFieldOfTableName(ATableName);
            for (int i = 0; i < FieldData.Count(); i++)
            {
                DataGridView.Rows.Add(NameCols[i], FieldData[i]);
            }
        }

        private void DrawingPanel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Blocks.Length; i++)
            {
                Blocks[i].ClearFocus();
            }
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {

        }
    }
}
