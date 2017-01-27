using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProjectConfig Config = new ProjectConfig();
            Application.Run(new WelcomeForm(Config));
            if (Config.isUserGoFuther)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
