using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    public partial class EditorForm : Form
    {
        public bool isEditMode { get; set; } = true;
        public Manager DrawManager;
        private int SelectBlockIndex;
        private bool isMouseDown { get; set; }
        public Point ClickOffset { get; set; }

        public EditorForm()
        {
            InitializeComponent();
            FillCategoryStripComboBox();
            FillObjectTreeView();

            DrawManager = new Manager(this.DrawPage);
            isMouseDown = false;
        }

        private void FillObjectTreeView()
        {
            ObjectsTreeView.Nodes.Clear();
            IEnumerable<string> Categories;
            if (isEditMode)
            {
                string needed_category = "";
                switch ((string)(CategoryStripComboBox.SelectedItem))
                {
                    case "Equipment": needed_category = "Detail"; break;
                    case "Complex": needed_category = "Equipment"; break;
                }
                Categories = MetaDataManager.Instance.ObjectCategories.Where(t => t == needed_category);
            }
            else
                Categories = MetaDataManager.Instance.ObjectCategories.Where(t => t != "Complex");
            foreach (string CategoryName in Categories)
            {
                TreeNode node = new TreeNode(CategoryName);
                foreach (string TypeName in MetaDataManager.Instance.GetObjectTypesByCategory(CategoryName))
                    node.Nodes.Add(new TreeNode(TypeName));
                ObjectsTreeView.Nodes.Add(node);
                node.ExpandAll();
            }
        }

        private void FillCategoryStripComboBox(string ACategoryPriopity = null)
        {
            CategoryStripComboBox.Items.Clear();
            foreach (string CategoryName in MetaDataManager.Instance.ObjectCategories.Where(t => t != "Complex"))
                CategoryStripComboBox.Items.Add(CategoryName);
            if (ACategoryPriopity != null)
                CategoryStripComboBox.SelectedIndex = CategoryStripComboBox.Items.IndexOf(ACategoryPriopity);
            else
                CategoryStripComboBox.SelectedIndex = 0;
        }

        private void FillTypeStripComboBox(string ACategory, string ATypePriopity = null)
        {
            TypeStripComboBox.Items.Clear();
            foreach (string TypeName in MetaDataManager.Instance.GetObjectTypesByCategory(ACategory))
                TypeStripComboBox.Items.Add(TypeName);
            if (ATypePriopity != null)
                TypeStripComboBox.SelectedIndex = TypeStripComboBox.Items.IndexOf(ATypePriopity);
            else
                TypeStripComboBox.SelectedIndex = 0;
        }

        private void CategoryStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTypeStripComboBox((string)(CategoryStripComboBox.SelectedItem));
            if (isEditMode)
                FillObjectTreeView();
        }

        public EditorForm(string AObjectCategory, string AObjectType)
        {
            InitializeComponent();
            FillStripControls(AObjectCategory, AObjectType);
        }

        private void FillStripControls(string AObjectCategory, string AObjectType)
        {
            CategoryStripComboBox.Items.Clear();
            foreach (string obj in MetaDataManager.Instance.GetObjectTypesOfObjectCategory(AObjectCategory))
                CategoryStripComboBox.Items.Add(obj);
            if (AObjectType != "")
                CategoryStripComboBox.SelectedIndex = CategoryStripComboBox.Items.IndexOf(AObjectType);
        }

        private void DrawPage_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            DrawManager.ClearLinksFocus();
            DrawManager.ClearBlocksFocus();

            Point ptr = PointToClient(Cursor.Position);
            ptr.X -= DrawPage.Location.X;
            ptr.Y -= DrawPage.Location.Y;

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

        private void DrawPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown) && (SelectBlockIndex != -1))
            {
                Point Pnt = this.PointToClient(Cursor.Position);
                DrawManager.Blocks[SelectBlockIndex].Move(Pnt, ClickOffset);
            }

            DrawPage.Invalidate();
        }

        private void DrawPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
        }

        private void DrawPage_Paint(object sender, PaintEventArgs e)
        {
            DrawManager.DrawElements(e.Graphics);
        }

        private void EditorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                DrawManager.DeleteElements();
                DrawPage.Invalidate();
            }
        }
    }
}
