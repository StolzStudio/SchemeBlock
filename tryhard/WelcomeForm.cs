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
        private List<BaseObject> ProjectsInfo;

        public WelcomeForm()
        {
            InitializeComponent();
            ProgramState.DefaultState();
            MetaDataManager.Instance.Initialize("../Databases/");
            LoadProjectsListItems();
        }

        private void WelcomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ProgramState.currentProjectId == -1 && !ProgramState.isSelectedProject)
                ProgramState.isExit = true;
        }

        private void LoadProjectsListItems()
        {
            ProjectsInfo = MetaDataManager.Instance.GetProjectsIdName();
            foreach(BaseObject project in ProjectsInfo)
                ProjectsListBox.Items.Add(project.Name);
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
            ProgramState.isSelectedProject = true;
            this.Close();
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

        private void ProjectsListBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int indexItem = (sender as ListBox).IndexFromPoint(new Point(e.X, e.Y));
            if (indexItem == -1) return;
            ProjectsListBox.SelectedIndex = indexItem;
            ProgramState.currentProjectId = ProjectsInfo[indexItem].Id;
            ProgramState.isSelectedProject = true;
            this.Close();
        }

        private void ProjectsListBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point pnt = new Point(e.X, e.Y);
            ProjectsListBox.SelectedIndex = (sender as ListBox).IndexFromPoint(pnt);
        }

        private void ProjectsListBox_MouseLeave(object sender, System.EventArgs e)
        {
            ProjectsListBox.ClearSelected();
        }
    }
}
