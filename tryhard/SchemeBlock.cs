using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    class SchemeBlock
    {
        //{consts}
            private int  BlockBodyWidth   = 120;
            private int  BlockBodyHeight  = 60;
            private int  BlockPointSize   = 6;
            private bool isMouseDown      = false;

            private MainForm Form;
        //{end}

        //{controls}
            public System.Windows.Forms.Panel   BlockBody;
            public System.Windows.Forms.Label   NameLabel;
            public System.Windows.Forms.Panel[] BlockPoints;
        //{end}

        public SchemeBlock(string AName, System.Drawing.Point APosition, MainForm AForm)
        {
            Form = AForm;
            InitializeComponent(AName, APosition);
        }

        private void InitializeComponent(string AName, System.Drawing.Point APosition)
        {
            //{BlockBody}
                this.BlockBody = new System.Windows.Forms.Panel();

                this.BlockBody.BackColor  =     System.Drawing.Color.Beige;
                this.BlockBody.Location   = new System.Drawing.Point(APosition.X, APosition.Y);
                this.BlockBody.Size       = new System.Drawing.Size(BlockBodyWidth, BlockBodyHeight);
                this.BlockBody.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SchemeBodyMouseDown);
                this.BlockBody.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SchemeBodyMouseMove);
                this.BlockBody.MouseUp   += new System.Windows.Forms.MouseEventHandler(this.SchemeBodyMouseUp);
            //{end}

            //{BlockPoints}
                this.BlockPoints = new System.Windows.Forms.Panel[4];

                this.BlockPoints[0]          = new System.Windows.Forms.Panel();
                this.BlockPoints[0].Location = new System.Drawing.Point(BlockBodyWidth / 2 - BlockPointSize / 2, 0);

                this.BlockPoints[1]          = new System.Windows.Forms.Panel();
                this.BlockPoints[1].Location = new System.Drawing.Point(BlockBodyWidth - BlockPointSize, BlockBodyHeight / 2 - BlockPointSize / 2);

                this.BlockPoints[2]          = new System.Windows.Forms.Panel();
                this.BlockPoints[2].Location = new System.Drawing.Point(BlockBodyWidth / 2 - BlockPointSize / 2, BlockBodyHeight - BlockPointSize);

                this.BlockPoints[3]          = new System.Windows.Forms.Panel();
                this.BlockPoints[3].Location = new System.Drawing.Point(0, BlockBodyHeight / 2 - BlockPointSize / 2);

                for (int i = 0; i < 4; i++)
                {
                    this.BlockPoints[i].Size = new System.Drawing.Size(BlockPointSize, BlockPointSize);
                    this.BlockPoints[i].BackColor = System.Drawing.Color.Black;
                    this.BlockBody.Controls.Add(this.BlockPoints[i]);
                }
            
            //{end}

            //{NameLabel}
                this.NameLabel = new System.Windows.Forms.Label();
                this.NameLabel.Location = new System.Drawing.Point(0, 0);
                this.NameLabel.Text     = AName;
                this.BlockBody.Controls.Add(this.NameLabel);
            //{end}
        }

        public void SchemeBodyMouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        public void SchemeBodyMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                System.Drawing.Point Ptr = Form.PointToClient(Cursor.Position);
                Ptr.X -= Form.DrawingPanelOffset.X + BlockBodyWidth  / 2;
                Ptr.Y -= Form.DrawingPanelOffset.Y + BlockBodyHeight / 2;
                this.BlockBody.Location = Ptr;
            }
        }

        public void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
}
