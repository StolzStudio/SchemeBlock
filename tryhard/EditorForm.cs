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
        public EditorForm()
        {
            InitializeComponent();
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
    }
}
