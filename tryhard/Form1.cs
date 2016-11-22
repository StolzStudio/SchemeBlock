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

        public int  InputSchemeIndex;
        public int  InputSchemePointIndex;
        public bool isBlockPointClick = false;

        public SchemeBlock[] Blocks;
        public  SchemeLink[]  Links;
        
        public MainForm()
        {
            Blocks = new SchemeBlock[0];
            Links  = new SchemeLink[0];
            InitializeComponent();
            DrawingPanelOffset = DrawingPanel.Location;
        }

        public void AddSchemeLink(SchemeLink ANewLink)
        {
            Array.Resize(ref Links, Links.Length + 1);
            Links[Links.Length - 1] = ANewLink;
            DrawingPanel.Invalidate();
        }

        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            Array.Resize(ref Blocks, Blocks.Length + 1);
            Point Pos = new Point(10, 10 * Blocks.Length + 60 * Blocks.Length);

            Blocks[Blocks.Length - 1] = new SchemeBlock(Blocks.Length - 1, 
                                                        "fu", 0, Pos, this);

            for (int i = 0; i < Blocks.Length; i++)
            {
               Blocks[i].ClearFocus();
            }

            Blocks[Blocks.Length - 1].SetFocus();

            //this.label1.Text = Blocks.Length.ToString();
        }
        
        private void TestDBButton_Click(object sender, EventArgs e)
        {
            
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Pen BlackPen = new Pen(Color.Black);

            if (Links.Length > 0)
            {
                for (int i = 0; i < Links.Length; i++)
                {
                    e.Graphics.DrawLine(BlackPen, Links[i].GetInputSchemePointLocation(this), Links[i].GetOutputSchemePointLocation(this));
                }
            }
        }
    }
}
