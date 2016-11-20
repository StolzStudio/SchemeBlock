﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace tryhard
{
    public class SchemeBlock
    {
        //{consts}
        private int  BlockBodyWidth    = 120;
        private int  BlockBodyHeight   = 60;
        private int  BlockPointSize    = 6;
        private bool isMouseDown       = false;

        private MainForm Form;
        private int      Index;
        private bool     isFocus;

        public bool       isAddSchemeLink = false;
        public Point[]    PointLocation;
        public SchemeLink AddedSchemeLink;
        //{end}

        //{controls}
        public Panel   BlockBody;
        public Label   NameLabel;
        public Panel[] BlockPoints;
        //{end}

        public SchemeBlock(int AIndex, string AName, Point APosition, MainForm AForm)
        {
            Form  = AForm;
            Index = AIndex;
            PointLocation    = new Point[4];
            PointLocation[0] = new Point(BlockBodyWidth / 2 - BlockPointSize / 2, 0);
            PointLocation[1] = new Point(BlockBodyWidth - BlockPointSize, BlockBodyHeight / 2 - BlockPointSize / 2);
            PointLocation[2] = new Point(BlockBodyWidth / 2 - BlockPointSize / 2, BlockBodyHeight - BlockPointSize);
            PointLocation[3] = new Point(0, BlockBodyHeight / 2 - BlockPointSize / 2);

            this.InitializeComponent(AName, APosition);
        }

        private void InitializeComponent(string AName, Point APosition)
        {
            //{BlockBody}
            this.BlockBody = new Panel();

            this.BlockBody.BackColor  =     Color.Beige;
            this.BlockBody.Location   = new Point(APosition.X, APosition.Y);
            this.BlockBody.Size       = new Size(BlockBodyWidth, BlockBodyHeight);
            this.BlockBody.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockBody.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockBody.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Click     += new EventHandler(this.SchemeBodyClick);
            this.Form.DrawingPanel.Controls.Add(BlockBody);
            //{end}

            this.SetFocus();

            //{NameLabel}
            this.NameLabel          = new Label();
            this.NameLabel.Location = new Point(0, 0);
            this.NameLabel.Text     = AName;
            this.BlockBody.Controls.Add(this.NameLabel);
            //{end}
        }
        
        public void SetFocus()
        {
            this.isFocus     = true;
            this.BlockPoints = new Panel[4];

            for (int i = 0; i < 4; i++)
            {
                this.BlockPoints[i]           = new Panel();
                this.BlockPoints[i].Location  = PointLocation[i];
                this.BlockPoints[i].Size      = new Size(BlockPointSize, BlockPointSize);
                this.BlockPoints[i].TabIndex  = i;
                this.BlockPoints[i].BackColor = Color.Black;
                this.BlockPoints[i].Click    += new EventHandler(this.BlockPointClick);
                this.BlockBody.Controls.Add(this.BlockPoints[i]);
            }
        }

        public void ClearFocus()
        {
            this.isFocus = false;

            if (BlockBody.Controls.Contains(BlockPoints[0]))
            {
                for (int i = 0; i < 4; i++)
                {
                    this.BlockPoints[i].Click -= new EventHandler(this.BlockPointClick);
                    this.BlockBody.Controls.Remove(BlockPoints[i]);
                    BlockPoints[i].Dispose();
                }
            }
        }

        private void SchemeBodyMouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        private void SchemeBodyMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point Ptr = Form.PointToClient(Cursor.Position);

                Ptr.X -= Form.DrawingPanelOffset.X + BlockBodyWidth  / 2;
                Ptr.Y -= Form.DrawingPanelOffset.Y + BlockBodyHeight / 2;

                this.BlockBody.Location = Ptr;
                Form.DrawingPanel.Invalidate();
            }
        }

        private void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void BlockPointClick(object sender, EventArgs e)
        {
            Panel Pnl = sender as Panel;
            if (!Form.isBlockPointClick)
            {
                Form.InputSchemeIndex      = this.Index;
                Form.InputSchemePointIndex = Pnl.TabIndex;
                Form.isBlockPointClick     = true;
            }
            else
            {
                SchemeLink SL = new SchemeLink(Form.InputSchemeIndex, Form.InputSchemePointIndex, this.Index, Pnl.TabIndex);
                Form.AddSchemeLink(SL);
                Form.isBlockPointClick = false;
            }
            Form.label2.Text = Form.InputSchemePointIndex.ToString();
        }

        private void SchemeBodyClick(object sender, EventArgs e)
        {
            if (Form.isBlockPointClick)
            {

            }
        }
    }
}
