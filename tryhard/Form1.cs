using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace tryhard
{
    public partial class MainForm : Form
    {
        public System.Drawing.Point DrawingPanelOffset;

        private SchemeBlock[] Blocks;
        
        public MainForm()
        {
            Blocks = new SchemeBlock[0];
            InitializeComponent();
            DrawingPanelOffset = DrawingPanel.Location;
        }

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            Array.Resize(ref Blocks, Blocks.Length + 1);
            System.Drawing.Point Pos = new System.Drawing.Point(10, 10 * Blocks.Length + 60 * Blocks.Length);

            Blocks[Blocks.Length - 1] = new SchemeBlock("Труба" + (Blocks.Length - 1).ToString(), Pos, this);
            this.DrawingPanel.Controls.Add(Blocks[Blocks.Length - 1].BlockBody);
            this.label1.Text = Blocks.Length.ToString();
        }

        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void TestDBButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
