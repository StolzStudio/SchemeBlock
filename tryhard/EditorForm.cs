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
            FillObjectTreeView();
        }

        public void FillObjectTreeView()
        {
            ObjectsTreeView.Nodes.Clear();
            if (isEditMode)
            {

            } else
            {
                foreach (string CategoryName in MetaDataManager.Instance.ObjectCategories.Where(t => t != "Complex"))
                {
                    TreeNode node = new TreeNode(CategoryName);
                    foreach (string TypeName in MetaDataManager.Instance.GetObjectTypesByCategory(CategoryName))
                        node.Nodes.Add(new TreeNode(TypeName));
                    ObjectsTreeView.Nodes.Add(node);
                }
            }
        }

        public void FillStripControls()
        {

        }
    }
}
