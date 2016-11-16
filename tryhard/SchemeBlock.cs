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
            private int  BlockBodyWidth  = 120;
            private int  BlockBodyHeight = 60;
            private bool isMouseDown     = false;

            private MainForm Form;
        //{end}

        //{controls}
            public System.Windows.Forms.Panel BlockBody;
            public System.Windows.Forms.Label NameLabel;
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
                this.BlockBody.Location = Form.PointToClient(Cursor.Position);
            }
        }

        public void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
}
