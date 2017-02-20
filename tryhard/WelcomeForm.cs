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
    public partial class WelcomeForm : Form
    {
        public bool isUserGoFuther { get; set; }

        private ProjectConfig Config;

        public WelcomeForm(ref ProjectConfig aConfig)
        {
            Config         = aConfig;
            isUserGoFuther = false;

            InitializeComponent();

            MetaDataManager.Instance.Initialize("../Databases/");
            //FormsManager.Instance.Initialize(this);

            LoadPogectsListItems();
        }

        private void WelcomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           // Application.Exit();
        }

        private void LoadPogectsListItems()
        {
            List<string> ProjectsList = new List<string>();
            //обращение к мете, чтобы она дала список проектов
            foreach(string Element in ProjectsList)
            {
                ProjectsListBox.Items.Add(Element);
            }
        }

        private void UserGoFuther()
        {
            isUserGoFuther = true;
            Config.isUserGoFuther = true;
            this.Close();
        }
        //
        //work with OpenAnotherProjectLabel
        //
        private void OpenAnotherProjectLabel_MouseEnter(object sender, EventArgs e)
        {
            OpenAnotherProjectLabel.ForeColor = Color.Orange;
        }

        private void OpenAnotherProjectLabel_MouseLeave(object sender, EventArgs e)
        {
            OpenAnotherProjectLabel.ForeColor = Color.Black;
        }
        //
        //work with CreateNewProjectPanel
        //
        private void CreateNewProjectPanel_MouseEnter(object sender, EventArgs e)
        {
            MainDescriptionLabel.ForeColor = Color.Orange;
            DescriptionLabel.ForeColor     = Color.Orange;
        }

        private void CreateNewProjectPanel_MouseLeave(object sender, EventArgs e)
        {
            MainDescriptionLabel.ForeColor = Color.Black;
            DescriptionLabel.ForeColor     = Color.Black;
        }

        private void CreateNewProjectPanel_Click(object sender, EventArgs e)
        {
            UserGoFuther();
        }

        private void OpenAnotherProjectLabel_Click(object sender, EventArgs e)
        {
            //загрузка файла из сторонней папки и открытие его
        }

        private void ProjectsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
           
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.Orange);

            e.DrawBackground();   
            e.Graphics.DrawString(ProjectsListBox.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();

        }
    }
}
