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
using Newtonsoft.Json;

namespace tryhard
{
    public enum MainFormPageType { Main, ObjectType }
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
        private int MarginInLinkPanel = 12;

        /* Methods */

        public MainForm()
        {
            InitializeComponent();
            //FillObjectTreeView();
            FillFieldComboBox();

            DrawManager = new Manager(this.MainPage);
            CalcManager = new CalculationManager();

            DrawingPanelOffset.X = MainPage.Location.X;
            DrawingPanelOffset.Y = MainPage.Location.Y;

            isMouseDown = false;
            isNextStep = false;
            UpStructurePanel.SendToBack();
            MainPage.BringToFront();
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
                    ClickOffset = new Point(Block.BlockWidth / 2, Block.BlockHeight / 2);
                    string parentNodeText = ObjectsTreeView.SelectedNode.Parent.Text;
                    DrawManager.AddBlock(ptr, MetaDataManager.Instance.Dictionary[parentNodeText], ObjectsTreeView.SelectedNode.Text,
                                        (int)ObjectsTreeView.SelectedNode.Tag);
                    this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                }
                else if (this.SelectBlockIndex != -1)
                {
                    if ((this.SelectBlockIndex != DrawManager.SelectedBlockIndex) && 
                        !DrawManager.CheckLink(this.SelectBlockIndex, DrawManager.SelectedBlockIndex) &&
                        MetaDataManager.Instance.isPossibleLink("Complex", DrawManager.Blocks[this.SelectBlockIndex].ClassText,
                                                                           DrawManager.Blocks[DrawManager.SelectedBlockIndex].ClassText))
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
                } else
                {
                    ObjectsTreeView.SelectedNode = ObjectsTreeView.Nodes[0].Nodes[0];
                    ShowPropertiesPanel();
                }
                this.SelectBlockIndex = DrawManager.SelectedBlockIndex;
                if (this.SelectBlockIndex != -1)
                {
                    ClickOffset = new Point(ptr.X - DrawManager.Blocks[SelectBlockIndex].Location.X,
                                            ptr.Y - DrawManager.Blocks[SelectBlockIndex].Location.Y);
                }
            }
            if (this.SelectBlockIndex != -1)
                SelectTreeNode();
        }

        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.isMouseDown) && (SelectBlockIndex != -1))
            {
                Console.WriteLine(ClickOffset.X + " " + ClickOffset.Y);
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

        private void ShowLinkPanel()
        {
            Link selectedLink = DrawManager.GetFocusedLink();
            FillLinkPanel(selectedLink);
            LinkInfoPanel.BringToFront();
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
                Convert.ToInt32(secondObject.GetType().GetProperty(ALink.LinkParameter + "Input").GetValue(secondObject)),
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
                ALink.LinkParameterValue,
                0,
                0,
                0});
                numericalUpDown.ValueChanged += new System.EventHandler(numericalUpDown_ValueChanged);
                LinkInfoPanel.Controls.Add(numericalUpDown);
            }
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

        private void ShowPropertiesPanel()
        {
            PropertiesGridView.BringToFront();
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
                Block block = DrawManager.Blocks[this.SelectBlockIndex];
                FormsManager.Instance.AddEditForm(new EditorForm("Complex", block.ClassText, block.Id));
                FormsManager.Instance.EditForms.Last().Show();
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
            ObjectsTreeView.SelectedNode = ObjectsTreeView.Nodes[0].Nodes[0];
        }

        private void FillFieldComboBox()
        {
            FieldComboBox.Items.Clear();
            foreach (IdNameInfo field in MetaDataManager.Instance.GetObjectsIdNameInfoByType("field_parameters"))
                FieldComboBox.Items.Add(field.Name);
            FieldComboBox.SelectedIndex = 0;
        }

        private void FieldComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            FillObjectTreeView();
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

        private void FillPropertiesGridView(string ACategory, string AType, int AId)
        {
            PropertiesGridView.Rows.Clear();
            foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[ACategory].Where(obj=>obj.Name == AType))
                foreach (string APropertyName in AObjectInfo.Properties)
                {
                    IEnumerable<BaseObject> base_object = MetaDataManager.Instance.Objects[AType].Where(obj => obj.Id == AId);
                    foreach (BaseObject obj in base_object)
                    {
                        PropertiesGridView.Rows.Add(MetaDataManager.Instance.Dictionary[APropertyName], obj.GetType().GetProperty(APropertyName).GetValue(obj));
                        if (APropertyName == "Cost")
                            PropertiesGridView.Rows[PropertiesGridView.Rows.Count - 1].Cells[1].Style.Format = "C3"; 
                    }
                }
            ShowPropertiesPanel();
        }

        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ObjectsTreeView.SelectedNode.Parent == null)
                ObjectsTreeView.SelectedNode = ObjectsTreeView.SelectedNode.Nodes[0];
            FillPropertiesGridView("Complex", MetaDataManager.Instance.Dictionary[ObjectsTreeView.SelectedNode.Parent.Text], 
                                   (int)ObjectsTreeView.SelectedNode.Tag);
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            MetaDataManager.Instance.SerializeMetaObjects();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (DrawManager.Blocks.Count != 0)
                ActivateUpStructurePanel();
        }

        private void ActivateUpStructurePanel()
        {
            InitializeUpStructurePanel();
            UpStructurePanel.BringToFront();
        }
    }
}
