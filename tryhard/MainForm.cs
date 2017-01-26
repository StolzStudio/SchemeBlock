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

        private Point PageOffsetInPageControl = new Point(20, 38);

        
        /* Methods */

        public MainForm()
        {
            MetaManager = new MetaDataManager("../Databases/");
            InitializeComponent();
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
            return null; // CalcManager.CalculateBlocksCombinations(Meta, CalcBlocks, APageType);
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
            //DeleteBlockButton.Visible = false;
        }

        private bool IsLinkNull(SchemeLink TestLink)
        {
            return TestLink == null;
        }

        private void ShowObjectPageButton_Click(object sender, EventArgs e)
        {
            PagesControl.SelectTab(ObjectPage);
        }

        private void ShowSchemePageButton_Click(object sender, EventArgs e)
        {
            PagesControl.SelectTab(SchemePage);
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

        private void ObjectPage_Click(object sender, EventArgs e)
        {
            SchemePage_Click(sender, e);
        }

        private void ObjectPage_Paint(object sender, PaintEventArgs e)
        {
            SchemePage_Paint(sender, e);
        }
    }
}
