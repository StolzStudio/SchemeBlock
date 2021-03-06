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
        List<Dictionary<int, Block>> Combinations;
        List<Complex> Complexes;
        public Manager DrawManager;
        private int SelectBlockIndex;
        private CountManager CalcManager;
        private bool isMouseDown { get; set; }
        public Point ClickOffset { get; set; }
        private bool isNextStep;
        private List<string> ProgressStep = new List<string>() { "Start", "First", "Second", "Third" };
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
                    foreach (BaseObject ObjectIdNameInfo in MetaDataManager.Instance.GetObjectsInfoByType(TypeName))
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
            if (isEditMode)
            {
                FillTypeStripComboBox(MetaDataManager.Instance.Dictionary[(string)(CategoryStripComboBox.SelectedItem)]);
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
                ptr.X = ptr.X - DrawPage.Location.X;
                ptr.Y = ptr.Y - DrawPage.Location.Y;

                if (Control.ModifierKeys == Keys.Control)
                {
                    DrawManager.TrySetFocusInBlocks(ptr);

                    if ((DrawManager.SelectedBlockIndex == -1))
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
            BaseObject firstObject = MetaDataManager.Instance.GetBaseObjectOfId(DrawManager.Blocks[ALink.FirstBlockIndex].ClassText,
                                                                     DrawManager.Blocks[ALink.FirstBlockIndex].Id);
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
                radioBtn.Text = MetaDataManager.Instance.Dictionary[LinkableParameters[i]];
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
                1000000,
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
            DrawManager.UpdateFocusedLink(MetaDataManager.Instance.Dictionary[(LinkInfoPanel.Controls[AParameterIndex * 2] as RadioButton).Text],
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
                if (Control.ModifierKeys == Keys.Shift)
                {
                    DrawManager.DeleteElements();
                    DrawPage.Invalidate();
                }
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
            ToolStrip.Enabled = false;
            EditObjectButton.Enabled = true;

            FillObjectTreeView();
            GoNextButton.Text = "Далее";
            DrawPage.Invalidate();
        }

        private void FillFieldComboBox()
        {

            FieldComboBox.Items.Clear();
            foreach (BaseObject field in MetaDataManager.Instance.GetObjectsInfoByType("field_parameters"))
                FieldComboBox.Items.Add(field.Name);
            FieldComboBox.SelectedIndex = 0;
            FillFieldPropertyDataGrid(FieldComboBox.SelectedItem.ToString());
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
                GoNextButton.Text = "Сохранить";
                isNextStep = true;
                ToolStrip.Enabled = false;
                EditObjectButton.Enabled = false;

                if (DrawManager.Links.Count == 0)
                {
                    SetParamForm ObjectParamForm;
                    if (!isEditObject)
                    {
                        ObjectParamForm = new SetParamForm(CategoryStripComboBox.SelectedItem.ToString(),
                                                           TypeStripComboBox.SelectedItem.ToString());
                    }
                    else
                    {
                        ObjectParamForm = new SetParamForm(CategoryStripComboBox.SelectedItem.ToString(),
                                                           TypeStripComboBox.SelectedItem.ToString(),
                                                           EditObject.Id);
                    }

                    ObjectParamForm.Show();
                    GoBackButton.PerformClick();
                    GoBackButton.PerformClick();
                    return;
                }

                CalcManager = new CountManager(ref DrawManager.Blocks, 
                                               ref DrawManager.Links, 
                                               MetaDataManager.Instance.Dictionary[CategoryStripComboBox.SelectedItem.ToString()],
                                               MetaDataManager.Instance.Dictionary[TypeStripComboBox.SelectedItem.ToString()]
                                               );

                if (CalcManager.CheckCombination() != "ok")
                {
                    MessageBox.Show(CalcManager.CheckCombination());
                    GoBackButton.PerformClick();
                }
            
                FillFieldComboBox();

            }
            else
            {
                List<int> Indeces = GetListOfSelectedItems();

                if (Indeces.Count == 0)
                {
                    MessageBox.Show("Вы не выбрали ни одного комплекса для сохранения");
                    return;
                }

                foreach(var ind in Indeces)
                {
                    if (!CheckSelectedItemToName(ind))
                    {
                        MessageBox.Show("Укажите названия для всех выбранных комплексов.");
                        return;
                    }
                }

                List<int> Ids = MetaDataManager.Instance.GetIdCortageByType(MetaDataManager.Instance.Dictionary[TypeStripComboBox.SelectedItem.ToString()]);
                foreach (var ind in Indeces)
                {
                    ObjectsStructure structure = new ObjectsStructure();
                    MetaDataManager.Instance.FillObjectStructure(DrawManager.Links, Combinations[ind], ref structure);

                    if (Complexes[ind].Id == EditObject.Id)
                    {
                        MetaDataManager.Instance.Objects[MetaDataManager.Instance.Dictionary[TypeStripComboBox.SelectedItem.ToString()]][EditObject.Id] = Complexes[ind];
                    }
                    else
                    {
                        Complexes[ind].Id = Ids.Max() + 1;
                        MetaDataManager.Instance.Objects[MetaDataManager.Instance.Dictionary[TypeStripComboBox.SelectedItem.ToString()]].Add(Complexes[ind]);
                    }
                    MetaDataManager.Instance.PushObjectStructure(MetaDataManager.Instance.Dictionary[TypeStripComboBox.SelectedItem.ToString()], Complexes[ind].Id, structure);
                }
                GoBackButton.PerformClick();
                GoBackButton.PerformClick();
            }
        }

        private List<int> GetListOfSelectedItems()
        {
            List<int> Result = new List<int>();

            foreach(DataGridViewRow row in CombinationDataGridView.Rows)
            {
                if (row.Cells[0].Value.Equals(true))
                {
                    Result.Add(row.Index);
                }
            }
            return Result;
        }

        private bool CheckSelectedItemToName(int aIndex)
        {
            if ((CombinationDataGridView.Rows[aIndex].Cells[1].Value == null) || (CombinationDataGridView.Rows[aIndex].Cells[1].Value == ""))
            {
                return false;
            }
            return true;
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
            CategoryStripComboBox.SelectedIndex = 1;

            DrawManager.Blocks.Clear();
            DrawManager.Links.Clear();

            DrawPage.Invalidate();
        }

        private void FillPropertiesGridView(string ACategory, string AType, int AId)
        {
            PropertiesGridView.Rows.Clear();
            BaseObject base_object = MetaDataManager.Instance.GetObject(AType, AId);
            foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo[ACategory].Where(obj => obj.Name == AType))
                foreach (string APropertyName in AObjectInfo.Properties)
                {
                    var propertyValue = base_object.GetType().GetProperty(APropertyName).GetValue(base_object);
                    if (APropertyName == "FluidInput")
                    {
                        int oilId = 0;
                        BaseObject oilQuality = MetaDataManager.Instance.GetObject("oil_quality", Convert.ToInt32(oilId));
                        double oilPart = Convert.ToDouble(oilQuality.GetType().GetProperty("OilProportion").GetValue(oilQuality));
                        double wetGasPart = Convert.ToDouble(oilQuality.GetType().GetProperty("WetGasProportion").GetValue(oilQuality));
                        double waterPart = Convert.ToDouble(oilQuality.GetType().GetProperty("WaterProportion").GetValue(oilQuality));
                        PropertiesGridView.Rows.Add("Вход нефти, bopd", string.Format("{0:0.0}", Convert.ToDouble(propertyValue) * oilPart / 0.159));
                        PropertiesGridView.Rows.Add("Вход газа, mmscfd", string.Format("{0:0.0}", Convert.ToDouble(propertyValue) * wetGasPart * (1 - 0.0021) / 28252.14));
                        PropertiesGridView.Rows.Add("Вход конденсата, blpd", string.Format("{0:0.0}", Convert.ToDouble(propertyValue) * wetGasPart * 0.0021 / 0.159));
                        PropertiesGridView.Rows.Add("Вход воды, dwpd", string.Format("{0:0.0}", Convert.ToDouble(propertyValue) * waterPart / 0.159));
                    }
                    else
                    {
                        PropertiesGridView.Rows.Add(MetaDataManager.Instance.Dictionary[APropertyName], propertyValue);
                        if (APropertyName == "Cost")
                            PropertiesGridView.Rows[PropertiesGridView.Rows.Count - 1].Cells[1].Style.Format = "C3";
                    }
                }
            ShowPropertiesPanel();
        }

        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ObjectsTreeView.SelectedNode.Nodes.Count != 0)
                if (ObjectsTreeView.SelectedNode.Parent == null)
                {
                    ObjectsTreeView.SelectedNode = ObjectsTreeView.SelectedNode.Nodes[0];
                    return;
                }
            if (!isEditMode)
            {
                string CategoryName = MetaDataManager.Instance.GetCateroryNameByType(MetaDataManager.Instance.Dictionary[ObjectsTreeView.SelectedNode.Parent.Text]);
                CategoryStripComboBox.SelectedIndex = CategoryStripComboBox.Items.IndexOf(MetaDataManager.Instance.Dictionary[CategoryName]);
                FillTypeStripComboBox(CategoryName);
                TypeStripComboBox.SelectedIndex = TypeStripComboBox.Items.IndexOf(ObjectsTreeView.SelectedNode.Parent.Text);
            }
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
            EditObject.Category = MetaDataManager.Instance.GetCateroryNameByType(MetaDataManager.Instance.Dictionary[ObjectsTreeView.SelectedNode.Parent.Text]);
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
            FillFieldPropertyDataGrid(FieldComboBox.Text);
        }

        private void FillFieldPropertyDataGrid(string aFieldName)
        {
            FieldPropertyDataGridView.Rows.Clear();
            foreach (MetaObjectInfo AObjectInfo in MetaDataManager.Instance.ObjectsInfo["InfoClasses"].Where(obj => obj.Name == "field_parameters"))
                foreach (string APropertyName in AObjectInfo.Properties)
                {
                    IEnumerable<BaseObject> base_object = MetaDataManager.Instance.Objects["field_parameters"].Where(obj => obj.Id == GetIdOfObject("field_parameters", aFieldName));
                    foreach (BaseObject obj in base_object)
                        FieldPropertyDataGridView.Rows.Add(APropertyName, obj.GetType().GetProperty(APropertyName).GetValue(obj));
                }
        }

        private int GetIdOfObject(string aClass, string aModel)
        {
            List<int> Ids = MetaDataManager.Instance.GetIdCortageByType(aClass);

            foreach (var i in Ids)
            {
                if (MetaDataManager.Instance.GetBaseObjectOfId(aClass, i).Name == aModel)
                {
                    return i;
                }
            }
            return -1;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            CombinationDataGridView.Rows.Clear();
            CalcManager.SetFluidParam(FieldComboBox.Text);
            Combinations = CalcManager.CalculateBlocksCombinations(DrawManager.Blocks);
            Complexes = new List<Complex>();
            foreach (var Combination in Combinations)
            {
                Complex el = CalcManager.MakeCalculate(Combination, FieldComboBox.Text);
                Complexes.Add(el);
                CombinationDataGridView.Rows.Add(false, el.Name, el.Cost, el.Volume, el.Weight, el.PeopleDemand, el.ElectricityDemand);
            }
            if (CombinationDataGridView.Rows.Count != 0)
            {
                CombinationDataGridView.CurrentCell = CombinationDataGridView.Rows[CombinationDataGridView.CurrentRow.Index].Cells[0];
            }

            if (isEditObject)
            {
                for (int i = 0; i < Combinations.Count; i++)
                {
                    if (IsCombinationsEqual(DrawManager.Blocks, Combinations[i]))
                    {
                        CombinationDataGridView.Rows[i].Cells[0].Value = true;
                        CombinationDataGridView.Rows[i].Cells[1].Value = EditObject.Model;
                        CombinationDataGridView.CurrentCell = CombinationDataGridView.Rows[i].Cells[0];

                        Complexes[i].Id = EditObject.Id;
                    }
                }
            }
        }

        private bool IsCombinationsEqual(Dictionary<int, Block> aFirstCombination, Dictionary<int, Block> aSecondCombination)
        {
            foreach(var key in aFirstCombination.Keys)
            {
                if (aFirstCombination[key].Id != aSecondCombination[key].Id)
                {
                    return false;
                }
            }
            return true;
        }

        private void CombinationDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CombinationDataGridView.Rows.Count != 0)
            {
                FillSelectedItemDataGrid(Combinations[CombinationDataGridView.CurrentRow.Index]);
                FillSelectedComplexDataGrid(Complexes[CombinationDataGridView.CurrentRow.Index]);
            }   
        }

        private void FillSelectedItemDataGrid(Dictionary<int, Block> aCombination)
        {
            SelectedItemDataGridView.Rows.Clear();

            foreach (var key in aCombination.Keys)
            {
                SelectedItemDataGridView.Rows.Add(MetaDataManager.Instance.GetBaseObjectOfId(aCombination[key].ClassText, aCombination[key].Id).Name, aCombination[key].Count);
            }
        }

        private void FillSelectedComplexDataGrid(Complex aComplex)
        {
            SelectedComplexDataGridView.Rows.Clear();
            foreach (var obj in aComplex.GetType().GetProperties())
            {
                if ((obj.Name != "Structure")&&(obj.Name != "Id")&&(obj.Name != "Name")&&(obj.Name != "EstimatedFieldId")&&
                    (obj.Name != "Cost")&&(obj.Name != "Volume")&&(obj.Name != "Weight"))
                    SelectedComplexDataGridView.Rows.Add(MetaDataManager.Instance.Dictionary[obj.Name],
                                                    aComplex.GetType().GetProperty(obj.Name).GetValue(aComplex));
            } 
        }

        private void CombinationDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((isEditMode) && (Complexes != null))
            {
                if (CombinationDataGridView.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    Complexes[e.RowIndex].Name = CombinationDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            }
        }
    }
}
