﻿using System;
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
      
        public SchemeManager SchemeManager;
        public CalculationManager CalcManager;

        private string[] EquipmentLabelText = new string[2] { "Класс оборудования:", "Класс детали:" };

        private Point PageOffsetInPageControl = new Point(20, 38);
        /* Methods */

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            InitializeComponent();
            ControlsPanel.Visible = false;
            SchemeManager = new SchemeManager(this);
            CalcManager = new CalculationManager();
        }   

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            SchemeManager.isAddBlockButtonClick = true;
        }

        private void CountTectBox_TextChanged(object sender, System.EventArgs e)
        {

        }

        /* Equipment ComboBoxes */

        private void ObjectTypeCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ObjectModelCB.Items.Clear();
            SchemeManager.ClearBlocksFocus();
            FillObjectModelCB(CMeta.DictionaryName[(string)((ComboBox)sender).SelectedItem]);
        }

        private void ObjectModelCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillParametersGrid(CMeta.DictionaryName[(string)ObjectTypeCB.SelectedItem], SchemeManager.ItemsIdList[ObjectModelCB.SelectedIndex]);

            if (SchemeManager.isHaveSelectedBlock)
            {
                SchemeManager.ChangeSelectBlock();
            }
        }

        public void SetComboBoxes(string AObjectTypeName, string AObjectModelName)
        {
            ObjectTypeCB.SelectedIndex = Meta.TablesList.IndexOf(AObjectTypeName);
            ObjectModelCB.SelectedIndex = SchemeManager.ItemsIdList.IndexOf(AObjectModelName);
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
            SchemeManager.ItemsIdList.Clear();
            for (int i = 0; i < items.Count; i += 2)
            {
                SchemeManager.ItemsIdList.Add(items[i]);
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
            foreach (int Key in SchemeManager.Blocks.Keys)
                CalcBlocks.Add(Key, new CalcBlock(Key, SchemeManager.Blocks[Key].BlockClass, 
                                                       SchemeManager.Blocks[Key].BlockId, 
                                                       SchemeManager.Blocks[Key].Count));

            /* Fill Links at Blocks */

            if (APageType == PageType.SchemeType)
            {
                foreach (SchemeLink Link in SchemeManager.Links)
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
                if (!SchemeManager.isOilFieldAdd)
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
                if (SchemeManager.Blocks.Count == 0)
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
            SchemeLink[] LinksArr = SchemeManager.Links.ToArray();
            SchemeManager.Links.Clear();

            for (int i = 0; i < LinksArr.Length; i++)
            {
                if (!SchemeManager.isHaveSelectedBlock)
                {
                    if (LinksArr[i].isFocus) { LinksArr[i] = null; }
                }
                else
                {
                    if (LinksArr[i].CheckDeletedLink(SchemeManager.SelectedBlockIndex)) { LinksArr[i] = null; }
                }
            }

            LinksArr = LinksArr.Where(x => !IsLinkNull(x)).ToArray();
            SchemeManager.Links = LinksArr.ToList();

            if (SchemeManager.Blocks[SchemeManager.SelectedBlockIndex].isFocus)
            {
                SchemePage.Controls.Remove(SchemeManager.Blocks[SchemeManager.SelectedBlockIndex].BlockBody);
                SchemeManager.Blocks.Remove(SchemeManager.SelectedBlockIndex);
                SchemeManager.isHaveSelectedBlock = false;
                SchemeManager.SelectedBlockIndex = -1;
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
            SchemeManager.ClearLinksFocus();
            SchemeManager.ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= PageOffsetInPageControl.X;
            ptr.Y -= PageOffsetInPageControl.Y;
            if (SchemeManager.isAddBlockButtonClick)
            {
                ptr.X -= SchemeBlock.BlockBodyWidth / 2;
                ptr.Y -= SchemeBlock.BlockBodyHeight / 2;
                SchemeManager.AddBlock(ptr);
            }
            SchemeManager.TrySetFocusInLinks(ptr);
        }

        private void SchemePage_Paint(object sender, PaintEventArgs e)
        {
            if (SchemeManager.Links.Count != 0)
            {
                foreach (SchemeLink Link in SchemeManager.Links)
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
            }
            else if (PagesControl.SelectedTab == ObjectPage)
            {
                SetControlsPanel(PageType.ObjectType);
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
    }
}
