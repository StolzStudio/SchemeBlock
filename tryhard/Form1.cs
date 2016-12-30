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
        /* Fields */

        public System.Drawing.Point DrawingPanelOffset;
      
        public SchemeManager Manager;

        /* Methods */

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            InitializeComponent();
            Manager = new SchemeManager(this);
            FillEquipmentCB();
            DrawingPanelOffset = DrawingPanel.Location;
        }   

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            Manager.isAddBlockButtonClick = true;
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (Manager.Links.Count != 0)
            {
                foreach(SchemeLink Link in Manager.Links)
                {
                    Link.Draw(this, e);
                }
            }
        }

        /* Equipment ComboBoxes */

        private void EquipmentCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ModelCB.Items.Clear();
            FillModelCB(CMeta.DictionaryName[(string)EquipmentCB.SelectedItem]);
        }

        private void ModelCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillParametersGrid(CMeta.DictionaryName[(string)EquipmentCB.SelectedItem], Manager.ItemsIdList[ModelCB.SelectedIndex]);
        }

        public void SetComboBoxes(string AEquipmentName, string AModelName)
        {
            EquipmentCB.SelectedIndex = Meta.TablesList.IndexOf(AEquipmentName);
            ModelCB.SelectedIndex = Manager.ItemsIdList.IndexOf(AModelName);
        }

        private void FillEquipmentCB()
        {
            foreach (string Equipment in Meta.TablesList)
            {
                EquipmentCB.Items.Add(CMeta.DictionaryName[Equipment]);
            }
            EquipmentCB.SelectedIndex = 0;
            EquipmentCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void FillModelCB(string AEquipmentName)
        {
            ModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            List<string> items = Meta.GetListRecordsWithId(AEquipmentName, "name");
            Manager.ItemsIdList.Clear();
            for (int i = 0; i < items.Count; i += 2)
            {
                Manager.ItemsIdList.Add(items[i]);
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
                DataGridView.Rows.Add(CMeta.DictionaryName[NameCols[i]], FieldData[i]);
            }
        }

        private void DrawingPanel_Click(object sender, EventArgs e)
        {
            Manager.ClearLinksFocus();
            Manager.ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= DrawingPanel.Location.X;
            ptr.Y -= DrawingPanel.Location.Y;
            if (Manager.isAddBlockButtonClick)
            {
                ptr.X -= SchemeBlock.BlockBodyWidth / 2;
                ptr.Y -= SchemeBlock.BlockBodyHeight / 2;
                Manager.AddBlock(ptr);
            }
            Manager.TrySetFocusInLinks(ptr);
        }

        public List<Dictionary<int, CalcBlock>> GetBlocksCombinations()
        {
            /* Fill Dict */

            Dictionary<int, CalcBlock> CalcBlocks = new Dictionary<int, CalcBlock>();
            foreach (int Key in Manager.Blocks.Keys)
            {
                CalcBlocks.Add(Key, new CalcBlock(Key, Manager.Blocks[Key].BlockClass, Manager.Blocks[Key].BlockId));
            }

            /* Fill Links at blocks */

            foreach (SchemeLink Link in Manager.Links)
            {
                CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(Link.SecondBlockIndex);
                CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(Link.FirstBlockIndex);
            }
            return CalculationManager.CalculateBlocksCombinations(Meta, CalcBlocks);
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            if (!Manager.isOilFieldAdd)
            {
                string message = "Кажется вы забыли добавить месторождение";
                string caption = "Ошибка в составлении схемы";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                return;
            }
            ResultForm ResForm = new ResultForm(this, GetBlocksCombinations());
            ResForm.ShowDialog();
        }

        private void DeleteBlockButton_Click(object sender, EventArgs e)
        {
            SchemeLink[] LinksArr = Manager.Links.ToArray();
            Manager.Links.Clear();

            for (int i = 0; i < LinksArr.Length; i++)
            {
                if (LinksArr[i].CheckDeletedLink(Manager.SelectedBlockIndex))
                {
                    LinksArr[i] = null;
                }
            }

            LinksArr = LinksArr.Where(x => !IsLinkNull(x)).ToArray();
            Manager.Links = LinksArr.ToList();

            if (Manager.Blocks[Manager.SelectedBlockIndex].isFocus)
            {
                DrawingPanel.Controls.Remove(Manager.Blocks[Manager.SelectedBlockIndex].BlockBody);
                Manager.Blocks.Remove(Manager.SelectedBlockIndex);
                Manager.isHaveSelectedBlock = false;
                Manager.SelectedBlockIndex = -1;
            }


            DrawingPanel_Click(sender, e);
            DrawingPanel.Invalidate();
        }

        private bool IsLinkNull(SchemeLink TestLink)
        {
            if (TestLink == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
