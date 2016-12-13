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
    public partial class MainForm : Form
    {
        public System.Drawing.Point DrawingPanelOffset;

        public int  SelectedBlockIndex;
        public bool isHaveSelectedBlock = false;

        public List<SchemeBlock> Blocks = new List<SchemeBlock>();
        public List<SchemeLink> Links = new List<SchemeLink>();
        private List<string> ItemsIdList = new List<string>();

        public MainForm()
        {
            Meta = new CMeta("../Databases/database.db");
            InitializeComponent();
            FillEquipmentCB();
            DrawingPanelOffset = DrawingPanel.Location;
        }

        public void AddSchemeLink(SchemeLink ANewLink)
        {
            Links.Add(ANewLink);
            DrawingPanel.Invalidate();
        }

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            Point Pos = new Point(10, 90 * (Blocks.Count) + 10);
            Blocks.Add(new SchemeBlock(Blocks.Count - 1, (string)EquipmentCB.SelectedItem,
                       ItemsIdList[ModelCB.SelectedIndex], Pos, this));
            foreach (SchemeBlock block in Blocks)
            {
                block.ClearFocus();
            }
            Blocks.Last().SetFocus();
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (Links.Count != 0)
            {
                foreach(SchemeLink link in Links)
                {
                    link.Draw(this, e);
                }
            }
        }

        public bool CheckLink(int AFirstBlockIndex, int ASecondBlockIndex)
        {
            foreach(SchemeLink link in Links)
            {
                if ((link.FirstBlockIndex == AFirstBlockIndex) && (link.SecondBlockIndex== ASecondBlockIndex))
                {
                    return true;
                }
            }
            return false;
        }

        private int FoundLinkIndex(int ASecondBlockIndex)
        {
            for (int i = 0; i < Links.Count; i++)
            {
                if (Links[i].SecondBlockIndex == ASecondBlockIndex)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindLastLink()
        {
            int LastElement = 0;
            int[] CountLinksToBlock = new int[Links.Count + 1];
            for (int i = 0; i < Links.Count; i++)
            {
                CountLinksToBlock[Links[i].FirstBlockIndex]++;
            }
            for (int i = 0; i < CountLinksToBlock.Length; i++)
            {
                if (CountLinksToBlock[i] == 0)
                {
                    return i;
                }
            }
            return LastElement;
        }

        /* Equipment ComboBoxes */

        private void EquipmentCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ModelCB.Items.Clear();
            FillModelCB((string)EquipmentCB.SelectedItem);
        }

        private void ModelCBSelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillParametersGrid((string)EquipmentCB.SelectedItem, ItemsIdList[ModelCB.SelectedIndex]);
        }

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
                DataGridView.Rows.Add(Meta.DictionaryName[NameCols[i]], FieldData[i]);
            }
        }

        private void DrawingPanel_Click(object sender, EventArgs e)
        {
            foreach(SchemeBlock block in Blocks)
            {
                block.ClearFocus();
            }
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            /*
            int a = FindLastLink();
            int b = FoundLinkIndex(a);

            List<CalcContainer> Containers = new List<CalcContainer>();

            for (int i = 0; i < Links.Length; i++)
            {
                if (b == -1)
                {
                    break;
                }
                Containers.Add(new CalcContainer(Blocks[b].BlockId, Blocks[b].BlockClass,
                                                 Blocks[a].BlockId, Blocks[a].BlockClass));
                a = b;
                b = FoundLinkIndex(a);
            } */

            List<string> res = new List<string>();
        }
    }
}
