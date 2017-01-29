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
        public bool isEditMode { get; set; } = false;

        public EditorForm()
        {
            InitializeComponent();
            FillCategoryStripComboBox();
            FillObjectTreeView();
        }

        public EditorForm(string AObjectCategory, string AObjectType)
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
                FillObjectTreeView();
        }

        private void FillStripControls(string AObjectCategory, string AObjectType)
        {
            CategoryStripComboBox.Items.Clear();
            foreach (string obj in MetaDataManager.Instance.GetObjectTypesOfObjectCategory(AObjectCategory))
                CategoryStripComboBox.Items.Add(obj);
            if (AObjectType != "")
                CategoryStripComboBox.SelectedIndex = CategoryStripComboBox.Items.IndexOf(AObjectType);
        }
    }
}
