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

        public WelcomeForm()
        {
            isUserGoFuther = false;

            InitializeComponent();
        }

        private void AddComplexButton_Click(object sender, EventArgs e)
        {
            UserGoFuther();

        }

        private void AddDeviceButton_Click(object sender, EventArgs e)
        {
            UserGoFuther();
        }

        private void UserGoFuther()
        {
            isUserGoFuther = true;
            this.Close();
        }

        private void WelcomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isUserGoFuther)
            {
                Application.Exit();
            }
        }
    }
}
