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
            FormsManager.Instance.Initialize(this);
            InitializeComponent();
            FillObjectTreeView();

            DrawManager = new Manager(this.MainPage);
            CalcManager = new CalculationManager();

            DrawingPanelOffset.X = MainPage.Location.X;
            DrawingPanelOffset.Y = MainPage.Location.Y;

            isMouseDown = false;
            isNextStep = false;
            GoNextButton.Enabled = false;

            MainPage.BringToFront();
        }   

        /* Equipment ComboBoxes */

        public List<Dictionary<int, CalcBlock>> GetBlocksCombinations(PageType APageType)
        {
            /* Fill Dict */

            Dictionary<int, CalcBlock> CalcBlocks = new Dictionary<int, CalcBlock>();
            foreach (int Key in DrawManager.Blocks.Keys)
                CalcBlocks.Add(Key, new CalcBlock(Key, DrawManager.Blocks[Key].ClassText,
                                                       DrawManager.Blocks[Key].ModelText,
                                                       DrawManager.Blocks[Key].Count));

            /* Fill Links at Blocks */

            if (APageType == PageType.SchemeType)
            {
                foreach (Link Link in DrawManager.Links)
                {
                    CalcBlocks[Link.FirstBlockIndex].OutputLinks.Add(new LinkInfo(Link.SecondBlockIndex, Link.LinkParameter));
                    CalcBlocks[Link.SecondBlockIndex].InputLinks.Add(new LinkInfo(Link.FirstBlockIndex, Link.LinkParameter));
                }
            }
            return CalcManager.CalculateBlocksCombinations(CalcBlocks, APageType);
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

        private void SelectTreeNode()
        {
            int i = DrawManager.SelectedBlockIndex;
            foreach (TreeNode node in ObjectsTreeView.Nodes)
            {
                if (DrawManager.Blocks[i].ClassText == node.Text)
                {
                    foreach (TreeNode node_child in node.Nodes)
                    {
                        if (DrawManager.Blocks[i].ModelText == node_child.Text)
                        {
                            ObjectsTreeView.SelectedNode = node_child;
                            break;
                        }
                    }
                }
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
                    ClickOffset = new Point(Block.BlockWidth / 2, Block.BlockHeight / 2);
                    DrawManager.AddBlock(ptr, ObjectsTreeView.SelectedNode.Parent.Text, ObjectsTreeView.SelectedNode.Text);
                    this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                }
                else
                {
                    ClickOffset = new Point(ptr.X - DrawManager.Blocks[SelectBlockIndex].Location.X,
                                            ptr.Y - DrawManager.Blocks[SelectBlockIndex].Location.Y);
                    if ((this.SelectBlockIndex != DrawManager.SelectedBlockIndex) && 
                        MetaDataManager.Instance.isPossibleLink("Complex", DrawManager.Blocks[this.SelectBlockIndex].ClassText,
                                                                           DrawManager.Blocks[DrawManager.SelectedBlockIndex].ClassText))
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
                if (this.SelectBlockIndex != -1)
                {
                    ClickOffset = new Point(ptr.X - DrawManager.Blocks[SelectBlockIndex].Location.X,
                                            ptr.Y - DrawManager.Blocks[SelectBlockIndex].Location.Y);
                }
            }
            if (this.SelectBlockIndex != -1)
            {
                SelectTreeNode();
            }
        }

        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown)&&(SelectBlockIndex != -1))
            {
                Point Pnt = this.PointToClient(Cursor.Position);
                DrawManager.Blocks[SelectBlockIndex].Move(Pnt, ClickOffset, new Point(MainPage.Width, MainPage.Height));
            }           
            MainPage.Invalidate();
        }

        private void MainPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
        }

        private void EditorMenuItem_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.AddEditForm(new EditorForm());
            FormsManager.Instance.EditForms.Last().Show();
        }

        private void MainPage_DoubleClick(object sender, EventArgs e)
        {
            DrawManager.ClearLinksFocus();
            DrawManager.ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= MainPage.Location.X;
            ptr.Y -= MainPage.Location.Y;

            DrawManager.TrySetFocusInBlocks(ptr);
            this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
            
            if (this.SelectBlockIndex != -1)
            {
                //FormsManager.Instance.AddEditForm(new EditorForm());
                //FormsManager.Instance.EditForms.Last().Show();
            }
        }

        public void FillObjectTreeView()
        {
            ObjectsTreeView.Nodes.Clear();
            IEnumerable<string> Categories;
            Categories = MetaDataManager.Instance.ObjectCategories.Where(t => t == "Complex");
            foreach (string CategoryName in Categories)
            {
                foreach (string TypeName in MetaDataManager.Instance.GetObjectTypesByCategory(CategoryName))
                {
                    TreeNode node = new TreeNode(TypeName);
                    foreach (IdNameInfo ObjectIdNameInfo in MetaDataManager.Instance.GetObjectsIdNameInfoByType(TypeName))
                    {
                        TreeNode node_child = new TreeNode(ObjectIdNameInfo.Name);
                        node_child.Tag = ObjectIdNameInfo.Id;
                        node.Nodes.Add(node_child);
                    }
                    ObjectsTreeView.Nodes.Add(node);
                    node.ExpandAll();
                }              
            }
        }

        private void FillPropertiesGridView(string ACategory, string AType, int AId)
        {
            PropertiesGridView.Rows.Clear();
            foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[ACategory].Where(obj=>obj.Name == AType))
                foreach (string APropertyName in AObjectInfo.Properties)
                {
                    IEnumerable<BaseObject> base_object = MetaDataManager.Instance.Objects[AType].Where(obj => obj.Id == AId);
                    foreach (BaseObject obj in base_object)
                        PropertiesGridView.Rows.Add(APropertyName, obj.GetType().GetProperty(APropertyName).GetValue(obj));
                }
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

        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ObjectsTreeView.SelectedNode.Parent == null)
                ObjectsTreeView.SelectedNode = ObjectsTreeView.SelectedNode.Nodes[0];
            FillPropertiesGridView("Complex", ObjectsTreeView.SelectedNode.Parent.Text, (int)ObjectsTreeView.SelectedNode.Tag);
        }
    }
}
