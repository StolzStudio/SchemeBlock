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

        public Point DrawingPanelOffset;
        public Manager DrawManager;
        public CalculationManager CalcManager;
        private int SelectBlockIndex;
        private bool isMouseDown { get; set; }
        public Point ClickOffset { get; set; }
        private bool isNextStep;


        /* Methods */

        public MainForm()
        {
            MetaDataManager.Instance.Initialize("../Databases/objectsinfo.json");
            InitializeComponent();
            FillTree();

            DrawManager = new Manager(this.MainPage);
            CalcManager = new CalculationManager();

            DrawingPanelOffset.X = MainPage.Location.X;
            DrawingPanelOffset.Y = MainPage.Location.Y;

            isMouseDown = false;
            isNextStep = false;

            MainPage.BringToFront();
        }   

        public void FillTree()
        {
            TreeNode node = new TreeNode("Node");
            node.Nodes.Add(new TreeNode("Node 1.1"));
            node.Nodes.Add(new TreeNode("Node 1.2"));
            node.Nodes.Add(new TreeNode("Node 1.3"));
            ObjectsTreeView.Nodes.Add(node);
        }

        /* Equipment ComboBoxes */

        public List<Dictionary<int, CalcBlock>> GetBlocksCombinations(PageType APageType)
        {
            /* Fill Dict */

            Dictionary<int, CalcBlock> CalcBlocks = new Dictionary<int, CalcBlock>();
            //foreach (int Key in SchemeManager[SchemeManagerNumber].Blocks.Keys)
            //    CalcBlocks.Add(Key, new CalcBlock(Key, SchemeManager[SchemeManagerNumber].Blocks[Key].ClassText,
            //                                           SchemeManager[SchemeManagerNumber].Blocks[Key].ModelText,
            //                                           SchemeManager[SchemeManagerNumber].Blocks[Key].Count));

            /* Fill Links at Blocks */

            if (APageType == PageType.SchemeType)
            {
                foreach (Link Link in DrawManager.Links)
                {
                    CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(Link.SecondBlockIndex);
                    CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(Link.FirstBlockIndex);
                }
            }
            return null; // CalcManager.CalculateBlocksCombinations(Meta, CalcBlocks, APageType);
        }  

        private bool IsLinkNull(Link TestLink)
        {
            return TestLink == null;
        }

        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            DrawManager.DrawElements(e.Graphics);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                DrawManager.DeleteElements();
                MainPage.Invalidate();
            }
        }

        private void MainPage_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            DrawManager.ClearLinksFocus();
            DrawManager.ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= MainPage.Location.X;
            ptr.Y -= MainPage.Location.Y;

            ClickOffset = ptr;

            if (Control.ModifierKeys == Keys.Control)
            { 
                DrawManager.TrySetFocusInBlocks(ptr);

                if (DrawManager.SelectedBlockIndex == -1)
                {
                    ptr.X -= Block.BlockWidth / 2;
                    ptr.Y -= Block.BlockHeight / 2;
                    DrawManager.AddBlock(ptr);
                    this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                }
                else
                {
                    if (this.SelectBlockIndex != DrawManager.SelectedBlockIndex)
                    {
                        DrawManager.ClearLinksFocus();
                        DrawManager.AddLink(new Link(this.SelectBlockIndex, DrawManager.SelectedBlockIndex));
                    }
                }
            }
            else
            {
                DrawManager.TrySetFocusInLinks(ptr);
                DrawManager.TrySetFocusInBlocks(ptr);
                
                this.SelectBlockIndex = DrawManager.SelectedBlockIndex;        
            }
            if (this.SelectBlockIndex != -1)
            {
                ClickOffset = new Point(ptr.X - DrawManager.Blocks[SelectBlockIndex].Location.X,
                                        ptr.Y - DrawManager.Blocks[SelectBlockIndex].Location.Y);
            }
        }

        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown)&&(SelectBlockIndex != -1))
            {
                Point Pnt = this.PointToClient(Cursor.Position);
                DrawManager.Blocks[SelectBlockIndex].Move(Pnt, ClickOffset);
            }
            
            MainPage.Invalidate();
        }

        private void MainPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
        }

        private void EditorMenuItem_Click(object sender, EventArgs e)
        {
            EditorForm editorForm = new EditorForm();
            editorForm.Show();
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            MainPage.BringToFront();
            GoBackButton.Enabled = false;
            isNextStep = false;
            GoNextButton.Text = "next";
        }

        private void GoNextButton_Click(object sender, EventArgs e)
        {
            if (!isNextStep)
            {
                WorkPanel.BringToFront();
                GoBackButton.Enabled = true;
                GoNextButton.Text = "save";
                //код заполнения гридов
                isNextStep = true;
            }
            else
            {
                //сохранение
            }
        }
    }
}
