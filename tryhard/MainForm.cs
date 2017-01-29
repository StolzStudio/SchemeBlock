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
        public bool isCtrlDown { get; set; }

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

            isCtrlDown = false;
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
            Link[] LinksArr = DrawManager[SchemeManagerNumber].Links.ToArray();
            DrawManager[SchemeManagerNumber].Links.Clear();

            for (int i = 0; i < LinksArr.Length; i++)
            {
                if (!DrawManager[SchemeManagerNumber].isHaveSelectedBlock)
                {
                    if (LinksArr[i].isFocus) { LinksArr[i] = null; }
                }
                else
                {
                    if (LinksArr[i].CheckDeletedLink(DrawManager[SchemeManagerNumber].SelectedBlockIndex)) { LinksArr[i] = null; }
                }
            }

            LinksArr = LinksArr.Where(x => !IsLinkNull(x)).ToArray();
            DrawManager[SchemeManagerNumber].Links = LinksArr.ToList();

            if (DrawManager[SchemeManagerNumber].Blocks[DrawManager[SchemeManagerNumber].SelectedBlockIndex].isFocus)
            {
                DrawManager[SchemeManagerNumber].Blocks.Remove(DrawManager[SchemeManagerNumber].SelectedBlockIndex);
                DrawManager[SchemeManagerNumber].isHaveSelectedBlock = false;
                DrawManager[SchemeManagerNumber].SelectedBlockIndex = -1;
            }


            MainPage_Click(sender, e);
            MainPage.Invalidate();
        }

        private bool IsLinkNull(Link TestLink)
        {
            return TestLink == null;
        }

        private void MainPage_Click(object sender, EventArgs e)
        {
            DrawManager[SchemeManagerNumber].ClearLinksFocus();
            DrawManager[SchemeManagerNumber].ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= DrawingPanelOffset.X;
            ptr.Y -= DrawingPanelOffset.Y;

            if (Control.ModifierKeys == Keys.Control)
            {
                DrawManager[SchemeManagerNumber].isAddBlockButtonClick = true;
            }

            foreach (int Key in DrawManager[SchemeManagerNumber].Blocks.Keys)
            {
                if (DrawManager[SchemeManagerNumber].Blocks[Key].CheckFocus(ptr))
                {
                    DrawManager[SchemeManagerNumber].isAddBlockButtonClick = false;
                }
            }
            
            if (DrawManager[SchemeManagerNumber].isAddBlockButtonClick)
            {
                ptr.X -= SchemeBlock.BlockBodyWidth / 2;
                ptr.Y -= SchemeBlock.BlockBodyHeight / 2;
                DrawManager[SchemeManagerNumber].AddBlock(ptr);
            }
            DrawManager[SchemeManagerNumber].TrySetFocusInLinks(ptr);
            DrawManager[SchemeManagerNumber].isAddBlockButtonClick = false;
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

        private void EditorMenuItem_Click(object sender, EventArgs e)
        {
            EditorForm editorForm = new EditorForm();
            editorForm.Show();
        }
    }
}
