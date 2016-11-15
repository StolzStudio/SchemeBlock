using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    struct Point
    {
        public int x;
        public int y;
        public Point(int Ax, int Ay)
        {
            x = Ax;
            y = Ay;
        }
    }

    class SchemeBlock
    {
        //{consts}
            private int BlockBodyWidth  = 120;
            private int BlockBodyHeight = 60;
        //{end}

        //{controls}
            public System.Windows.Forms.Panel BlockBody;
            public System.Windows.Forms.Label NameLabel;
        //{end}

        public SchemeBlock(string AName, Point APosition)
        {
            InitializeComponent(AName, APosition);
        }

        private void InitializeComponent(string AName, Point APosition)
        {
            //{BlockBody}
                this.BlockBody = new System.Windows.Forms.Panel();

                this.BlockBody.BackColor =     System.Drawing.Color.Beige;
                this.BlockBody.Location  = new System.Drawing.Point(APosition.x, APosition.y);
                this.BlockBody.Size      = new System.Drawing.Size(BlockBodyWidth, BlockBodyHeight);
            //{end}

            //{NameLabel}
                this.NameLabel = new System.Windows.Forms.Label();

                this.NameLabel.Location = new System.Drawing.Point(0, 0);
                this.NameLabel.Text     = AName;

                this.BlockBody.Controls.Add(this.NameLabel);
            //{end}
        }
    }
}
