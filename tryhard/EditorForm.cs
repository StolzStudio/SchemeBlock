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
    public struct ObjectNameParam
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int Id { get; set; }

        public ObjectNameParam(string aCategory, string aTypeName, string aModelName, int aId)
        {
            Category = aCategory;
            Type = aTypeName;
            Model = aModelName;
            Id = aId;
        }
    }

    public partial class EditorForm : Form
    {
        public bool isEditMode { get; set; } = false;
        public bool isEditObject { get; set; } = false;
        public ObjectNameParam EditObject = new ObjectNameParam(); 
        public Manager DrawManager;
        private int SelectBlockIndex;
        private CountManager CalcManager;
        private bool isMouseDown { get; set; }
        public Point ClickOffset { get; set; }
        private bool isNextStep;
        private List<string> ProgressStep = new List<string>() { "Start", "First", "Second", "Third" };
        private int ProgressVal = 0;
        private int MarginInLinkPanel = 12;

        public EditorForm()
        {
            InitializeComponent();
            FillCategoryStripComboBox();
            FillObjectTreeView();
            SetDefaultSetting();
        }

        public EditorForm(string AObjectCategory, string AObjectType, int AId)
        {
            InitializeComponent();
            FillStripControls(AObjectCategory, AObjectType);
            FillObjectTreeView();
            SetDefaultSetting();
            SelectTreeViewNode(AObjectType, AId);
        }

        public void SetDefaultSetting()
        {
            DrawManager = new Manager(this.DrawPage);
            isMouseDown = false;
            isNextStep = false;
            SetMode(false);
            GoNextButton.Enabled = false;
            WorkPanel.Visible = false;
            DrawPage.BringToFront();
        }

        private void SelectTreeViewNode(string AObjectType, int AId)
        {
            foreach (TreeNode ObjectTypeNode in ObjectsTreeView.Nodes)
                if (MetaDataManager.Instance.Dictionary[ObjectTypeNode.Text] == AObjectType)
                    foreach (TreeNode Node in ObjectTypeNode.Nodes)
                    {
                        string nodeName = MetaDataManager.Instance.GetBaseObjectOfId(AObjectType, AId).Name;
                        if (Node.Text == nodeName)
                        {
                            ObjectsTreeView.SelectedNode = Node;
                            return;
                        }
                    }
        }

        public void UpdateViewControls()
        {
            if (isEditMode)
            {
                FillCategoryStripComboBox((string)CategoryStripComboBox.SelectedItem);
            }
            else
                FillObjectTreeView();
        }

        public void FillObjectTreeView()
        {
            ObjectsTreeView.Nodes.Clear();
            IEnumerable<string> Categories;
            if (isEditMode)
            {
                string needed_category = "";
                switch ((string)(CategoryStripComboBox.SelectedItem))
                {
                    case "Оборудование": needed_category = "Detail"; break;
                    case "Комплекс": needed_category = "Equipment"; break;
                }
                Categories = MetaDataManager.Instance.ObjectCategories.Where(t => t == needed_category);
            }
            else
                Categories = MetaDataManager.Instance.ObjectCategories.Where(t => t != "Detail");
            foreach (string CategoryName in Categories)
            {
                foreach (string TypeName in MetaDataManager.Instance.GetObjectTypesByCategory(CategoryName))
                {
                    TreeNode node = new TreeNode(MetaDataManager.Instance.Dictionary[TypeName]);
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
                CategoryStripComboBox.Items.Add(MetaDataManager.Instance.Dictionary[CategoryName]);
            if (ACategoryPriopity != null)
                CategoryStripComboBox.SelectedIndex = CategoryStripComboBox.Items.IndexOf(
                                                            MetaDataManager.Instance.Dictionary[ACategoryPriopity]);
            else
                CategoryStripComboBox.SelectedIndex = 0;
        }

        private void FillTypeStripComboBox(string ACategory, string ATypePriopity = null)
        {
            TypeStripComboBox.Items.Clear();
            foreach (string TypeName in MetaDataManager.Instance.GetObjectTypesByCategory(ACategory))
                TypeStripComboBox.Items.Add(MetaDataManager.Instance.Dictionary[TypeName]);
            if (ATypePriopity != null)
                TypeStripComboBox.SelectedIndex = TypeStripComboBox.Items.IndexOf(MetaDataManager.Instance.Dictionary[ATypePriopity]);
            else
                TypeStripComboBox.SelectedIndex = 0;
        }

        private void CategoryStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTypeStripComboBox(MetaDataManager.Instance.Dictionary[(string)(CategoryStripComboBox.SelectedItem)]);
            if (isEditMode)
            {
                FillObjectTreeView();
                DrawManager.DeleteAllElements();
                DrawPage.Invalidate();
            }
        }

        private void FillStripControls(string AObjectCategory, string AObjectType)
        {
            FillCategoryStripComboBox(AObjectCategory);
        }

        private void SelectTreeNode()
        {
            int i = DrawManager.SelectedBlockIndex;
            foreach (TreeNode node in ObjectsTreeView.Nodes)
            {
                if (DrawManager.Blocks[i].ClassText == MetaDataManager.Instance.Dictionary[node.Text])
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

        private void EditForm_Closing(object sender, FormClosingEventArgs e)
        {
            FormsManager.Instance.DeleteEditForm(this);
        }

        private void DrawPage_MouseDown(object sender, MouseEventArgs e)
        {
            if (isEditMode)
            {
                isMouseDown = true;

                DrawManager.ClearLinksFocus();
                DrawManager.ClearBlocksFocus();

                Point ptr = PointToClient(Cursor.Position);
                ptr.X = ptr.X - DrawPage.Location.X - ToolStrip.Location.X;
                ptr.Y = ptr.Y - DrawPage.Location.Y - ToolStrip.Location.Y;

                ClickOffset = ptr;
                Console.WriteLine("Click offset at mousedown: " + ptr.X + " " + ptr.Y);

                if (Control.ModifierKeys == Keys.Control)
                {
                    DrawManager.TrySetFocusInBlocks(ptr);

                    if ((DrawManager.SelectedBlockIndex == -1) && (ObjectsTreeView.SelectedNode != null))
                    {
                        ptr.X -= Block.BlockWidth / 2;
                        ptr.Y -= Block.BlockHeight / 2;
                        ClickOffset = new Point(Block.BlockWidth / 2, Block.BlockHeight / 2);
                        string parentNodeText = ObjectsTreeView.SelectedNode.Parent.Text;
                        DrawManager.AddBlock(ptr, MetaDataManager.Instance.Dictionary[parentNodeText],
                                             ObjectsTreeView.SelectedNode.Text, (int)ObjectsTreeView.SelectedNode.Tag);
                        this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                    }
                    else if ((this.SelectBlockIndex != -1) && (this.SelectBlockIndex != DrawManager.SelectedBlockIndex))
                    {
                        string category = "";
                        if ((string)(CategoryStripComboBox.SelectedItem) == "Комплекс")
                            category = "Equipment";
                        else
                            category = "Detail";
                        if ((!DrawManager.CheckLink(this.SelectBlockIndex, DrawManager.SelectedBlockIndex)) && 
                            (MetaDataManager.Instance.isPossibleLink(category, 
                                                                     DrawManager.Blocks[this.SelectBlockIndex].ClassText,
                                                                     DrawManager.Blocks[DrawManager.SelectedBlockIndex].ClassText)))
                        {
                            AddLink();
                            DrawManager.ClearBlocksFocus();
                            this.SelectBlockIndex = -1;
                            DrawManager.Links[DrawManager.Links.Count - 1].isFocus = true;
                            ShowLinkPanel();
                        }
                        this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                        if (this.SelectBlockIndex != -1)
                        {
                            ClickOffset = new Point(ptr.X - DrawManager.Blocks[SelectBlockIndex].Location.X,
                                                    ptr.Y - DrawManager.Blocks[SelectBlockIndex].Location.Y);
                        }
                    }
                }
                else
                {
                    if (DrawManager.TrySetFocusInBlocks(ptr))
                    {
                        ShowPropertiesPanel();
                    } else if (DrawManager.TrySetFocusInLinks(ptr))
                    {
                        ShowLinkPanel();
                    }
                    else
                    {
                        ObjectsTreeView.SelectedNode = ObjectsTreeView.Nodes[0].Nodes[0];
                        ShowPropertiesPanel();
                    }
                    if (DrawManager.SelectedBlockIndex != -1)
                    {
                        this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                    }
                    if (this.SelectBlockIndex != -1)
                    {
                        ClickOffset = new Point(ptr.X - DrawManager.Blocks[SelectBlockIndex].Location.X,
                                                ptr.Y - DrawManager.Blocks[SelectBlockIndex].Location.Y);
                    }
                }
                if (this.SelectBlockIndex != -1)
                    SelectTreeNode();
            }
        }

        private void AddLink()
        {
            DrawManager.ClearLinksFocus();
            List<string> LinkableParameters =
                MetaDataManager.Instance.GetLinkableParameters(DrawManager.Blocks[this.SelectBlockIndex].ClassText,
                                                               DrawManager.Blocks[DrawManager.SelectedBlockIndex].ClassText);
            BaseObject baseObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[DrawManager.SelectedBlockIndex].ClassText,
                                                                               DrawManager.Blocks[DrawManager.SelectedBlockIndex].Id);

            Link newLink = new Link(this.SelectBlockIndex, DrawManager.SelectedBlockIndex, LinkableParameters[0],
                                    Convert.ToInt32(baseObject.GetType().GetProperty(LinkableParameters[0] + "Input").GetValue(baseObject)));
            DrawManager.AddLink(newLink);
            FillLinkPanel(newLink);
        }

        private void FillLinkPanel(Link ALink)
        {
            List<string> LinkableParameters =
                MetaDataManager.Instance.GetLinkableParameters(DrawManager.Blocks[ALink.FirstBlockIndex].ClassText,
                                                               DrawManager.Blocks[ALink.SecondBlockIndex].ClassText);
            LinkInfoPanel.Controls.Clear();
            LinkInfoPanel.Tag = -1;
            BaseObject secondObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[ALink.SecondBlockIndex].ClassText,
                                                                     DrawManager.Blocks[ALink.SecondBlockIndex].Id);
            for (int i = 0; i < LinkableParameters.Count; i++)
            {
                RadioButton radioBtn = new System.Windows.Forms.RadioButton();
                radioBtn.AutoSize = true;
                radioBtn.Location = new System.Drawing.Point(MarginInLinkPanel, MarginInLinkPanel + i * (17 + MarginInLinkPanel));
                radioBtn.Name = "radioButton" + i;
                radioBtn.Size = new System.Drawing.Size(85, 17);
                radioBtn.TabIndex = i;
                radioBtn.TabStop = true;
                radioBtn.Text = LinkableParameters[i];
                radioBtn.Tag = i;
                radioBtn.UseVisualStyleBackColor = true;
                if (ALink.LinkParameter == LinkableParameters[i])
                {
                    radioBtn.Checked = true;
                    LinkInfoPanel.Tag = i;
                }
                radioBtn.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
                LinkInfoPanel.Controls.Add(radioBtn);

                NumericUpDown numericalUpDown = new System.Windows.Forms.NumericUpDown();
                numericalUpDown.Location = new System.Drawing.Point(164, MarginInLinkPanel + i * (17 + MarginInLinkPanel));
                numericalUpDown.Maximum = new decimal(new int[] {
                Convert.ToInt32(secondObject.GetType().GetProperty(LinkableParameters[i] + "Input").GetValue(secondObject)),
                0,
                0,
                0});
                numericalUpDown.Minimum = new decimal(new int[] {
                0,
                0,
                0,
                0});
                numericalUpDown.Name = "numericUpDown" + i;
                numericalUpDown.Size = new System.Drawing.Size(67, 20);
                numericalUpDown.TabIndex = 2;
                numericalUpDown.Tag = i;
                numericalUpDown.Value = new decimal(new int[] {
                1,
                0,
                0,
                0});
                numericalUpDown.ValueChanged += new System.EventHandler(numericalUpDown_ValueChanged);
                LinkInfoPanel.Controls.Add(numericalUpDown);
            }
            (LinkInfoPanel.Controls[1] as NumericUpDown).Value = Convert.ToDecimal(ALink.LinkParameterValue);
        }

        private void numericalUpDown_ValueChanged(Object sender, EventArgs e)
        {
            UpdateLinkParameter((int)(sender as NumericUpDown).Tag);
        }

        private void radioButton_CheckedChanged(Object sender, EventArgs e)
        {
            UpdateLinkParameter((int)(sender as RadioButton).Tag);
        }

        private void UpdateLinkParameter(int AParameterIndex)
        {
            DrawManager.UpdateFocusedLink((LinkInfoPanel.Controls[AParameterIndex * 2] as RadioButton).Text,
                                           Decimal.ToInt32((LinkInfoPanel.Controls[AParameterIndex * 2 + 1] as NumericUpDown).Value));
        }

        private void ShowLinkPanel()
        {
            Link selectedLink = DrawManager.GetFocusedLink();
            FillLinkPanel(selectedLink);
            LinkInfoPanel.BringToFront();
        }

        private void ShowPropertiesPanel()
        {
            PropertiesGridView.BringToFront();
        }

        private void DrawPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown) && (SelectBlockIndex != -1))
            {
                Console.WriteLine("Click offset at mousemove: " + ClickOffset.X + " " + ClickOffset.Y);
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
                    EditObjectButton.Enabled = true;
                    isEditObject = false;
                    GoNextButton.Enabled = false;
                    (sender as Button).Enabled = false;
                    DrawManager.DeleteAllElements();
                }
            }
            isNextStep = false;
            WorkPanel.Visible = false;
               
            FillObjectTreeView();
            GoNextButton.Text = "next";
            DrawPage.Invalidate();
        }

        private void FillFieldComboBox()
        {
            CalcManager.SetFieldObjects();

            foreach (var el in CalcManager.FieldObjects)
            {
                FieldComboBox.Items.Add(el.Name);
            }
        }

        private void FillCombinationDataGrid()
        {
            //List<DataGridViewColumn> Columns = CalcManager.GiveCombinationColumns();
            //foreach (var c in Columns)
            //{
            //    CombinationDataGridView.Columns.Add(c);
            //}
        }

        private void GoNextButton_Click(object sender, EventArgs e)
        {
            if (!isNextStep)
            {
                WorkPanel.Visible = true;
                WorkPanel.BringToFront();
                (sender as Button).BringToFront();
                GoBackButton.BringToFront();
                GoBackButton.Enabled = true;
                GoNextButton.Text = "save";
                //CountDataGridView.Visible = false;
                isNextStep = true;

                CalcManager = new CountManager(ref DrawManager.Blocks, ref DrawManager.Links, TypeStripComboBox.SelectedItem.ToString(), this);

                FillFieldComboBox();
                FillCombinationDataGrid();
                
                
            }
            else
            {
                foreach (DataGridViewRow row in PropteryDataGridView.Rows)
                {
                    foreach (BaseObject obj in MetaDataManager.Instance.Objects[EditObject.Type].Where(ob => ob.Id == EditObject.Id))
                    {
                        if (obj.GetType().GetProperty((string)row.Cells[0].Value).PropertyType == typeof(System.Int32))
                            obj.GetType().GetProperty((string)row.Cells[0].Value).SetValue(obj, Convert.ToInt32(row.Cells[1].Value));
                        else if (obj.GetType().GetProperty((string)row.Cells[0].Value).PropertyType == typeof(System.Int64))
                            obj.GetType().GetProperty((string)row.Cells[0].Value).SetValue(obj, Convert.ToInt64(row.Cells[1].Value));
                        else
                            obj.GetType().GetProperty((string)row.Cells[0].Value).SetValue(obj, row.Cells[1].Value);
                    }
                }
                MetaDataManager.Instance.FillObjectStructure(EditObject.Type, EditObject.Id,
                                                             DrawManager.Links, DrawManager.Blocks);
                GoBackButton.PerformClick();
                GoBackButton.PerformClick();
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
                        PropertiesGridView.Rows.Add(MetaDataManager.Instance.Dictionary[APropertyName], 
                                                    obj.GetType().GetProperty(APropertyName).GetValue(obj));
                }
            ShowPropertiesPanel();
        }

        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ObjectsTreeView.SelectedNode.Nodes.Count != 0)
                if (ObjectsTreeView.SelectedNode.Parent == null)
                    ObjectsTreeView.SelectedNode = ObjectsTreeView.SelectedNode.Nodes[0];
            string parentText = ObjectsTreeView.SelectedNode.Parent.Text;
            FillPropertiesGridView(MetaDataManager.Instance.GetCateroryNameByType(MetaDataManager.Instance.Dictionary[parentText]),
                                   MetaDataManager.Instance.Dictionary[parentText], (int)ObjectsTreeView.SelectedNode.Tag);
            if (!isEditMode)
                DrawManager.LoadStructureOfObject(MetaDataManager.Instance.Dictionary[parentText], (int)ObjectsTreeView.SelectedNode.Tag);
        }

        private void ShowPropertiesGridView()
        {
            PropertiesGridView.BringToFront();
        }

        private void EditObjectButton_Click(object sender, EventArgs e)
        {
            isEditObject = true;
            EditObject.Category = MetaDataManager.Instance.GetCateroryNameByType(ObjectsTreeView.SelectedNode.Parent.Text);
            EditObject.Type = ObjectsTreeView.SelectedNode.Parent.Text;
            EditObject.Model = ObjectsTreeView.SelectedNode.Text;
            EditObject.Id = (int)ObjectsTreeView.SelectedNode.Tag;
            FillCategoryStripComboBox(EditObject.Category);
            SetMode(true);
            FillObjectTreeView();
            GoNextButton.Enabled = true;
            GoBackButton.Enabled = true;
            (sender as Button).Enabled = false;
            AddNewObjectButton.Enabled = false;

            TypeStripComboBox.SelectedIndex = TypeStripComboBox.Items.IndexOf(EditObject.Type);
            ToolStrip.Enabled = false;
        }

        private void FieldDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //CalcManager.SelectedField = FieldDataGridView.SelectedRows[1].ToString();
        }

        private void FieldComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcManager.MakeCalculate(DrawManager.Blocks, FieldComboBox.Text);
        }
    }
}
