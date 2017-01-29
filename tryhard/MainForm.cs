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
    public enum PageType { SchemeType, ObjectType }

    public partial class MainForm : Form
    {
        /* Fields */

        public System.Drawing.Point DrawingPanelOffset;
        public int SchemeManagerNumber = 0;
        public Manager[] DrawManager;
        public CalculationManager CalcManager;
        private int SelectBlockIndex;
        private bool isCtrlDown  { get; set; }
        private bool isMouseDown { get; set; }
        public Point ClickOffset { get; set; }


        /* Methods */

        public MainForm()
        {
            MetaDataManager.Instance.Initialize("../Databases/objectsinfo.json");
            InitializeComponent();
            FillTree();

            DrawManager = new Manager[2];
            DrawManager[0] = new Manager(this.MainPage);
            DrawManager[1] = new Manager(this.MainPage);
            CalcManager = new CalculationManager();

            DrawingPanelOffset.X = MainPage.Location.X;
            DrawingPanelOffset.Y = MainPage.Location.Y;

            isCtrlDown  = false;
            isMouseDown = false;
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
                foreach (Link Link in DrawManager[SchemeManagerNumber].Links)
                {
                    CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(Link.SecondBlockIndex);
                    CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(Link.FirstBlockIndex);
                }
            }
            return null; // CalcManager.CalculateBlocksCombinations(Meta, CalcBlocks, APageType);
        }  

        private void DeleteElement(object sender, KeyPressEventArgs e)
        {
            DrawManager[SchemeManagerNumber].DeleteElements();

            //MainPage_MouseDown(sender, e);
            MainPage.Invalidate();
        }

        private bool IsLinkNull(Link TestLink)
        {
            return TestLink == null;
        }

        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            DrawManager[SchemeManagerNumber].DrawElements(e.Graphics);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                DeleteElement(sender, e);
            }
        }

        private void MainPage_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            DrawManager[SchemeManagerNumber].ClearLinksFocus();
            DrawManager[SchemeManagerNumber].ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= MainPage.Location.X;
            ptr.Y -= MainPage.Location.Y;

            ClickOffset = ptr;

            if (Control.ModifierKeys == Keys.Control)
            { 
                DrawManager[SchemeManagerNumber].TrySetFocusInBlocks(ptr);

                if (DrawManager[SchemeManagerNumber].SelectedBlockIndex == -1)
                {
                    ptr.X -= Block.BlockWidth / 2;
                    ptr.Y -= Block.BlockHeight / 2;
                    DrawManager[SchemeManagerNumber].AddBlock(ptr);
                    this.SelectBlockIndex = DrawManager[SchemeManagerNumber].SelectedBlockIndex;
                }
                else
                {
                    if (this.SelectBlockIndex != DrawManager[SchemeManagerNumber].SelectedBlockIndex)
                    {
                        DrawManager[SchemeManagerNumber].ClearLinksFocus();
                        DrawManager[SchemeManagerNumber].AddLink(new Link(this.SelectBlockIndex, DrawManager[SchemeManagerNumber].SelectedBlockIndex));
                    }
                }
            }
            else
            {
                DrawManager[SchemeManagerNumber].TrySetFocusInLinks(ptr);
                DrawManager[SchemeManagerNumber].TrySetFocusInBlocks(ptr);
                
                this.SelectBlockIndex = DrawManager[SchemeManagerNumber].SelectedBlockIndex;        
            }
            if (this.SelectBlockIndex != -1)
            {
                ClickOffset = new Point(ptr.X - DrawManager[SchemeManagerNumber].Blocks[SelectBlockIndex].Location.X,
                                        ptr.Y - DrawManager[SchemeManagerNumber].Blocks[SelectBlockIndex].Location.Y);
            }
            //MainPage_Click(sender, e);
            //this.isMouseDown = true;

            //if (Control.ModifierKeys == Keys.Control) { isCtrlDown = true; }
            //else { isCtrlDown = false; }

            // if ((DrawManager[SchemeManagerNumber].isHaveSelectedBlock) &&
            //   (DrawManager[SchemeManagerNumber].SelectedBlockIndex != SelectBlockIndex) && isCtrlDown && (SelectBlockIndex != -1))
            //{
            //  if (!DrawManager[SchemeManagerNumber].CheckLink(DrawManager[SchemeManagerNumber].SelectedBlockIndex, SelectBlockIndex))
            //{
            //  DrawManager[SchemeManagerNumber].isHaveSelectedBlock = false;
            //isCtrlDown = false;

            //DrawManager[SchemeManagerNumber].ClearLinksFocus();
            //DrawManager[SchemeManagerNumber].AddLink(new Link(DrawManager[SchemeManagerNumber].SelectedBlockIndex, SelectBlockIndex));
            // }
            //}
            //else
            //{
            //   DrawManager[SchemeManagerNumber].isHaveSelectedBlock = true;
            //  DrawManager[SchemeManagerNumber].SelectedBlockIndex = SelectBlockIndex;
            //}

            //Form.SetComboBoxes(this.BlockClass, this.BlockId);
            //DrawManager[SchemeManagerNumber].ClearLinksFocus();


        }

        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown)&&(SelectBlockIndex != -1))
            {
                Point Pnt = this.PointToClient(Cursor.Position);
                DrawManager[SchemeManagerNumber].Blocks[SelectBlockIndex].Move(Pnt, ClickOffset);
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
    }
}
