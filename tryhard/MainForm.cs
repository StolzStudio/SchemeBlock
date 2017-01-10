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
    public enum PageType { SchemeType, ObjectType }

    public partial class MainForm : Form
    {
        /* Fields */

        public System.Drawing.Point DrawingPanelOffset;
        public int SchemeManagerNumber = 0;
        public SchemeManager[] SchemeManager;
        public CalculationManager CalcManager;

        private string[] EquipmentLabelText = new string[2] { "Класс оборудования:", "Класс детали:" };

        private Point PageOffsetInPageControl = new Point(20, 38);

        
        /* Methods */

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            InitializeComponent();
            ControlsPanel.Visible = false;
            SchemeManager = new SchemeManager[2];
            SchemeManager[0] = new SchemeManager(this);
            SchemeManager[1] = new SchemeManager(this);
            CalcManager = new CalculationManager();
        }   

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            SchemeManager[SchemeManagerNumber].isAddBlockButtonClick = true;
        }

        /* Equipment ComboBoxes */

        private void ObjectTypeCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ObjectModelCB.Items.Clear();
            SchemeManager[SchemeManagerNumber].ClearBlocksFocus();
            FillObjectModelCB(CMeta.DictionaryName[(string)((ComboBox)sender).SelectedItem]);
        }

        private void ObjectModelCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillParametersGrid(CMeta.DictionaryName[(string)ObjectTypeCB.SelectedItem], 
                               SchemeManager[SchemeManagerNumber].ItemsIdList[ObjectModelCB.SelectedIndex]
                              );

            if (SchemeManager[SchemeManagerNumber].isHaveSelectedBlock)
            {
                SchemeManager[SchemeManagerNumber].ChangeSelectBlock();
            }
        }

        public void SetComboBoxes(string AObjectTypeName, string AObjectModelName)
        {
           ObjectTypeCB.SelectedIndex = Meta.TablesList.IndexOf(AObjectTypeName);
           ObjectModelCB.SelectedIndex = SchemeManager[SchemeManagerNumber].ItemsIdList.IndexOf(AObjectModelName);
        }

        private void FillObjectTypeCB(List<string> AResource)
        {

            foreach (string Item in AResource)
            {
                ObjectTypeCB.Items.Add(CMeta.DictionaryName[Item]);
            }
            ObjectTypeCB.SelectedIndex = 0;
            ObjectTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void FillObjectModelCB(string AObjectTypeName)
        {
            ObjectModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            List<string> items = Meta.GetListRecordsWithId(AObjectTypeName, "name");
            SchemeManager[SchemeManagerNumber].ItemsIdList.Clear();
            for (int i = 0; i < items.Count; i += 2)
            {
                SchemeManager[SchemeManagerNumber].ItemsIdList.Add(items[i]);
                ObjectModelCB.Items.Add(items[i + 1]);
            }
            ObjectModelCB.SelectedIndex = 0;
            ObjectModelCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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

        public List<Dictionary<int, CalcBlock>> GetBlocksCombinations(PageType APageType)
        {
            /* Fill Dict */

            Dictionary<int, CalcBlock> CalcBlocks = new Dictionary<int, CalcBlock>();
            foreach (int Key in SchemeManager[SchemeManagerNumber].Blocks.Keys)
                CalcBlocks.Add(Key, new CalcBlock(Key, SchemeManager[SchemeManagerNumber].Blocks[Key].BlockClass, 
                                                       SchemeManager[SchemeManagerNumber].Blocks[Key].BlockId, 
                                                       SchemeManager[SchemeManagerNumber].Blocks[Key].Count));

            /* Fill Links at Blocks */

            if (APageType == PageType.SchemeType)
            {
                foreach (SchemeLink Link in SchemeManager[SchemeManagerNumber].Links)
                {
                    CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(Link.SecondBlockIndex);
                    CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(Link.FirstBlockIndex);
                }
            }
            return CalcManager.CalculateBlocksCombinations(Meta, CalcBlocks, APageType);
        }  

        private void CalcButton_Click(object sender, EventArgs e)
        {
            ResultForm ResForm = null;
            if (PagesControl.SelectedTab == SchemePage)
            {
                if (!SchemeManager[SchemeManagerNumber].isOilFieldAdd)
                {
                    string message = "Кажется вы забыли добавить месторождение";
                    string caption = "Ошибка в составлении схемы";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    return;
                }
                ResForm = new ResultForm(this, GetBlocksCombinations(PageType.SchemeType));
            }
            else if (PagesControl.SelectedTab == ObjectPage)
            {
                if (SchemeManager[SchemeManagerNumber].Blocks.Count == 0)
                {
                    string message = "Кажется вы не выбрали ни одной детали";
                    string caption = "Ошибка в составлении схемы";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    return;
                }
                else
                {
                    ResForm = new ResultForm(this, GetBlocksCombinations(PageType.ObjectType));
                }
            }         
            ResForm.ShowDialog();
        }

        private void DeleteBlockButton_Click(object sender, EventArgs e)
        {
            SchemeLink[] LinksArr = SchemeManager[SchemeManagerNumber].Links.ToArray();
            SchemeManager[SchemeManagerNumber].Links.Clear();

            for (int i = 0; i < LinksArr.Length; i++)
            {
                if (!SchemeManager[SchemeManagerNumber].isHaveSelectedBlock)
                {
                    if (LinksArr[i].isFocus) { LinksArr[i] = null; }
                }
                else
                {
                    if (LinksArr[i].CheckDeletedLink(SchemeManager[SchemeManagerNumber].SelectedBlockIndex)) { LinksArr[i] = null; }
                }
            }

            LinksArr = LinksArr.Where(x => !IsLinkNull(x)).ToArray();
            SchemeManager[SchemeManagerNumber].Links = LinksArr.ToList();

            if (SchemeManager[SchemeManagerNumber].Blocks[SchemeManager[SchemeManagerNumber].SelectedBlockIndex].isFocus)
            {
                SchemePage.Controls.Remove(SchemeManager[SchemeManagerNumber].Blocks[SchemeManager[SchemeManagerNumber].SelectedBlockIndex].BlockBody);
                SchemeManager[SchemeManagerNumber].Blocks.Remove(SchemeManager[SchemeManagerNumber].SelectedBlockIndex);
                SchemeManager[SchemeManagerNumber].isHaveSelectedBlock = false;
                SchemeManager[SchemeManagerNumber].SelectedBlockIndex = -1;
            }


            SchemePage_Click(sender, e);
            SchemePage.Invalidate();
            DeleteBlockButton.Visible = false;
        }

        private bool IsLinkNull(SchemeLink TestLink)
        {
            return TestLink == null;
        }

        private void ShowObjectPageButton_Click(object sender, EventArgs e)
        {
            MeetPanel.Visible = false;
            PagesControl.SelectTab(ObjectPage);
            ControlsPanel.Visible = true;
            SetControlsPanel(PageType.ObjectType);
        }

        private void ShowSchemePageButton_Click(object sender, EventArgs e)
        {
            MeetPanel.Visible = false;
            PagesControl.SelectTab(SchemePage);
            ControlsPanel.Visible = true;
            SetControlsPanel(PageType.SchemeType);
        }

        private void SchemePage_Click(object sender, EventArgs e)
        {
            SchemeManager[SchemeManagerNumber].ClearLinksFocus();
            SchemeManager[SchemeManagerNumber].ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= PageOffsetInPageControl.X;
            ptr.Y -= PageOffsetInPageControl.Y;
            if (SchemeManager[SchemeManagerNumber].isAddBlockButtonClick)
            {
                ptr.X -= SchemeBlock.BlockBodyWidth / 2;
                ptr.Y -= SchemeBlock.BlockBodyHeight / 2;
                SchemeManager[SchemeManagerNumber].AddBlock(ptr);
            }
            SchemeManager[SchemeManagerNumber].TrySetFocusInLinks(ptr);
        }

        private void SchemePage_Paint(object sender, PaintEventArgs e)
        {
            if (SchemeManager[SchemeManagerNumber].Links.Count != 0)
            {
                foreach (SchemeLink Link in SchemeManager[SchemeManagerNumber].Links)
                {
                    Link.Draw(this, e);
                }
            }
        }

        private void PagesControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteBlockButton.Visible = false;

            if (PagesControl.SelectedTab == SchemePage)
            {
                SetControlsPanel(PageType.SchemeType);
                SchemeManagerNumber = 0;
            }
            else if (PagesControl.SelectedTab == ObjectPage)
            {
                SetControlsPanel(PageType.ObjectType);
                SchemeManagerNumber = 1;
            }
        }

        /* Заполнение CB в зависимости от типа Page */

        private void SetControlsPanel(PageType APageType)
        {
            ObjectModelCB.Items.Clear();
            ObjectTypeCB.Items.Clear();
            if (APageType == PageType.SchemeType)
            {
                this.EquipmentLabel.Text = EquipmentLabelText[0];
                FillObjectTypeCB(Meta.EquipmentTablesList);
            }
            else if (APageType == PageType.ObjectType)
            {
                this.EquipmentLabel.Text = EquipmentLabelText[1];
                FillObjectTypeCB(Meta.ObjectTablesList);
            }
        }

        private void ObjectPage_Click(object sender, EventArgs e)
        {
            SchemePage_Click(sender, e);
        }

        private void ObjectPage_Paint(object sender, PaintEventArgs e)
        {
            SchemePage_Paint(sender, e);
        }

        private void CountDomain_SelectedItemChanged(object sender, EventArgs e)
        {
            if (SchemeManager[SchemeManagerNumber].isHaveSelectedBlock)
            {
                SchemeManager[SchemeManagerNumber].Blocks[SchemeManager[SchemeManagerNumber].SelectedBlockIndex].Count = (int)CountDomain.SelectedItem;
            }
            //SchemeManager.Blocks[SchemeManager.SelectedBlockIndex]
        }
    }
}
