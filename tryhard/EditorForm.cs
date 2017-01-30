﻿using System;
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
        public bool isEditMode { get; set; } = false;
        public Manager DrawManager;
        private int SelectBlockIndex;
        private bool isMouseDown { get; set; }
        public Point ClickOffset { get; set; }
        private bool isNextStep;
        private List<string> ProgressStep = new List<string>() { "Start", "First", "Second", "Third" };
        private int ProgressVal = 0;

        public EditorForm()
        {
            InitializeComponent();
            FillCategoryStripComboBox();
            FillObjectTreeView();

            DrawManager = new Manager(this.DrawPage);
            isMouseDown = false;
            isNextStep = false;
            SetMode(false);
            GoNextButton.Enabled = false;

            WorkPanel.Visible = false;
            DrawPage.BringToFront();
        }

        public EditorForm(string AObjectCategory, string AObjectType, string AId)
        {
            InitializeComponent();
            FillStripControls(AObjectCategory, AObjectType);
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
                Categories = MetaDataManager.Instance.ObjectCategories.Where(t => t != "Detail");
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
            ObjectsTreeView.SelectedNode = ObjectsTreeView.Nodes[0];
        }

        private void FillCategoryStripComboBox(string ACategoryPriopity = null)
        {
            CategoryStripComboBox.Items.Clear();
            foreach (string CategoryName in MetaDataManager.Instance.ObjectCategories.Where(t => t != "Detail"))
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
            {
                FillObjectTreeView();
                DrawManager.DeleteAllElements();
                DrawPage.Invalidate();
            }
        }

        private void FillStripControls(string AObjectCategory, string AObjectType)
        {
            CategoryStripComboBox.Items.Clear();
            foreach (string obj in MetaDataManager.Instance.GetObjectTypesOfObjectCategory(AObjectCategory))
                CategoryStripComboBox.Items.Add(obj);
            if (AObjectType != "")
                CategoryStripComboBox.SelectedIndex = CategoryStripComboBox.Items.IndexOf(AObjectType);
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

        private void SetProgressEnvironment(string AProgressStep)
        {
            if (ProgressVal > 3)
                ProgressVal = 0;
            switch (ProgressStep[ProgressVal])
            {
                case "Start":
                    DrawPage.Visible = true;
                    WorkPanel.Visible = false;
                    DrawPage.BringToFront();
                    GoBackButton.Enabled = false;
                    isNextStep = false;
                    GoNextButton.Text = "next";
                    break;
                case "First":
                    DrawPage.Visible = true;
                    WorkPanel.Visible = false;
                    DrawPage.BringToFront();
                    GoBackButton.Enabled = true;
                    GoNextButton.Enabled = true;
                    isNextStep = false;
                    GoNextButton.Text = "next";
                    break;
                case "Second":
                    DrawPage.Visible = false;
                    WorkPanel.Visible = true;
                    GoBackButton.Enabled = false;
                    GoNextButton.Enabled = true;
                    WorkPanel.BringToFront();
                    break;
                case "Third":
                    break;
            }
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

            if (isEditMode)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    DrawManager.TrySetFocusInBlocks(ptr);

                    if ((DrawManager.SelectedBlockIndex == -1)&&(ObjectsTreeView.SelectedNode != null))
                    {
                        ptr.X -= Block.BlockWidth / 2;
                        ptr.Y -= Block.BlockHeight / 2;
                        DrawManager.AddBlock(ptr, ObjectsTreeView.SelectedNode.Parent.Text, ObjectsTreeView.SelectedNode.Text);
                        this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                    }
                    else
                    {
                        if (this.SelectBlockIndex != DrawManager.SelectedBlockIndex)
                        {
                            string temp = "";
                            if ((string)(CategoryStripComboBox.SelectedItem) == "Complex")
                                temp = "Equipment";
                            else
                                temp = "Detail";
                            if (MetaDataManager.Instance.isPossibleLink(temp,
                                                                         DrawManager.Blocks[this.SelectBlockIndex].ClassText,
                                                                         DrawManager.Blocks[DrawManager.SelectedBlockIndex].ClassText))
                            {
                                DrawManager.ClearLinksFocus();
                                DrawManager.AddLink(new Link(this.SelectBlockIndex, DrawManager.SelectedBlockIndex));
                            }
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
                    SelectTreeNode();
                }
            }
        }

        private void DrawPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown) && (SelectBlockIndex != -1))
            {
                Point Pnt = this.PointToClient(Cursor.Position);
                DrawManager.Blocks[SelectBlockIndex].Move(Pnt, ClickOffset, new Point(DrawPage.Width, DrawPage.Height));
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

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                if (!isNextStep)
                {
                    SetMode(false);
                    AddNewObjectButton.Enabled = true;
                    GoNextButton.Enabled = false;
                    (sender as Button).Enabled = false;
                    DrawManager.DeleteAllElements();
                }
            }
            isNextStep = false;
            DrawPage.BringToFront();
            
            
            FillObjectTreeView();
            GoNextButton.Text = "next";
            DrawPage.Invalidate();
        }

        private void GoNextButton_Click(object sender, EventArgs e)
        {
            if (!isNextStep)
            {
                WorkPanel.Visible = true;
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

        private void SetMode(bool AEditMode)
        {
            isEditMode = AEditMode;
            if (isEditMode)
            {
                ToolStrip.Enabled = true;
            }
            else
            {
                ToolStrip.Enabled = false;
            }
        }

        private void AddNewObjectButton_Click(object sender, EventArgs e)
        {
            SetMode(true);
            FillObjectTreeView();
            GoNextButton.Enabled = true;
            GoBackButton.Enabled = true;
            (sender as Button).Enabled = false;
        }

        private void FillPropertiesGridView(string ACategory, string AType, int AId)
        {
            PropertiesGridView.Rows.Clear();
            foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[ACategory].Where(obj => obj.Name == AType))
                foreach (string APropertyName in AObjectInfo.Properties)
                {
                    IEnumerable<BaseObject> base_object = MetaDataManager.Instance.Objects[AType].Where(obj => obj.Id == AId);
                    foreach (BaseObject obj in base_object)
                        PropertiesGridView.Rows.Add(APropertyName, obj.GetType().GetProperty(APropertyName).GetValue(obj));
                }
        }

        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ObjectsTreeView.SelectedNode.Nodes.Count != 0)
                if (ObjectsTreeView.SelectedNode.Parent == null)
                    ObjectsTreeView.SelectedNode = ObjectsTreeView.SelectedNode.Nodes[0];
            FillPropertiesGridView(MetaDataManager.Instance.GetCateroryNameByType(ObjectsTreeView.SelectedNode.Parent.Text), 
                                           ObjectsTreeView.SelectedNode.Parent.Text, (int)ObjectsTreeView.SelectedNode.Tag);
        }
    }
}
