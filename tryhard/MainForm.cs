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
      
        public SchemeManager SchemeManager;
        public CalculationManager CalcManager;

        /* Methods */

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            InitializeComponent();
            SchemeManager = new SchemeManager(this);
            CalcManager = new CalculationManager();
            FillEquipmentCB();
            DrawingPanelOffset = DrawingPanel.Location;
        }   

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            SchemeManager.isAddBlockButtonClick = true;
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (SchemeManager.Links.Count != 0)
            {
                foreach(SchemeLink Link in SchemeManager.Links)
                {
                    Link.Draw(this, e);
                }
            }
        }

        /* Equipment ComboBoxes */

        private void EquipmentCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ModelCB.Items.Clear();
            FillModelCB(CMeta.DictionaryName[(string)((ComboBox)sender).SelectedItem]);
        }

        private void ModelCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillParametersGrid(CMeta.DictionaryName[(string)EquipmentCB.SelectedItem], SchemeManager.ItemsIdList[ModelCB.SelectedIndex]);
        }

        public void SetComboBoxes(string AEquipmentName, string AModelName)
        {
            EquipmentCB.SelectedIndex = Meta.TablesList.IndexOf(AEquipmentName);
            ModelCB.SelectedIndex = SchemeManager.ItemsIdList.IndexOf(AModelName);
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
            SchemeManager.ItemsIdList.Clear();
            for (int i = 0; i < items.Count; i += 2)
            {
                SchemeManager.ItemsIdList.Add(items[i]);
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
            SchemeManager.ClearLinksFocus();
            SchemeManager.ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= DrawingPanel.Location.X;
            ptr.Y -= DrawingPanel.Location.Y;
            if (SchemeManager.isAddBlockButtonClick)
            {
                ptr.X -= SchemeBlock.BlockBodyWidth / 2;
                ptr.Y -= SchemeBlock.BlockBodyHeight / 2;
                SchemeManager.AddBlock(ptr);
            }
            SchemeManager.TrySetFocusInLinks(ptr);
        }

        public List<Dictionary<int, CalcBlock>> GetBlocksCombinations()
        {
            /* Fill Dict */

            Dictionary<int, CalcBlock> CalcBlocks = new Dictionary<int, CalcBlock>();
            foreach (int Key in SchemeManager.Blocks.Keys)
                CalcBlocks.Add(Key, new CalcBlock(Key, SchemeManager.Blocks[Key].BlockClass, SchemeManager.Blocks[Key].BlockId));

            /* Fill Links at Blocks */

            foreach (SchemeLink Link in SchemeManager.Links)
            {
                CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(Link.SecondBlockIndex);
                CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(Link.FirstBlockIndex);
            }
            return CalcManager.CalculateBlocksCombinations(Meta, CalcBlocks);
        }  

        private void CalcButton_Click(object sender, EventArgs e)
        {
            if (!SchemeManager.isOilFieldAdd)
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
            SchemeLink[] LinksArr = SchemeManager.Links.ToArray();
            SchemeManager.Links.Clear();

            for (int i = 0; i < LinksArr.Length; i++)
            {
                if (LinksArr[i].CheckDeletedLink(SchemeManager.SelectedBlockIndex))
                {
                    LinksArr[i] = null;
                }
            }

            LinksArr = LinksArr.Where(x => !IsLinkNull(x)).ToArray();
            SchemeManager.Links = LinksArr.ToList();

            if (SchemeManager.Blocks[SchemeManager.SelectedBlockIndex].isFocus)
            {
                DrawingPanel.Controls.Remove(SchemeManager.Blocks[SchemeManager.SelectedBlockIndex].BlockBody);
                SchemeManager.Blocks.Remove(SchemeManager.SelectedBlockIndex);
                SchemeManager.isHaveSelectedBlock = false;
                SchemeManager.SelectedBlockIndex = -1;
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

        private void ShowDrawingPanelButton_Click(object sender, EventArgs e)
        {
            DrawingPanel.BringToFront();
        }

        private void ShowCalcPanelButton_Click(object sender, EventArgs e)
        {
            CalcPanel.BringToFront();
        }
    }
}
