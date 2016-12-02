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
        /* Consts */

        private int  BlockBodyWidth    = 80;
        private int  BlockBodyHeight   = 80;
        private int  BlockPointSize    = 6;
        private bool isMouseDown       = false;

        private MainForm Form;
        private int      Index;
        private bool     isFocus;

        public bool       isAddSchemeLink = false;
        public Point      PointLocation;
        public SchemeLink AddedSchemeLink;

        /* Controls */

        public DrawPanel BlockBody;
        public Label     BlockClassLabel;
        public Label     BlockModelLabel;

        /* DB fields */

        private string block_class;
        private string block_id;

        public string BlockClass
        {
            get { return block_class; }
            set { block_class = value; }
        }

        public string BlockId
        {
            get { return block_id; }
            set { block_id = value; }
        }


        public SchemeBlock(int AIndex, string ABlockClass, string ABlockId, Point APosition, MainForm AForm)
        {
            Form  = AForm;
            Index = AIndex;
            BlockClass = ABlockClass;
            BlockId = ABlockId;
            PointLocation = new Point(BlockBodyWidth / 2, BlockBodyHeight / 2);
            this.InitializeComponent(ABlockClass, (string)Form.ModelCB.SelectedItem, APosition);
        }

        private void InitializeComponent(string ABlockClass, string ABlockModel, Point APosition)
        {
            /* BlockBody */

            this.BlockBody            = new DrawPanel();
            this.BlockBody.BackColor  =     Color.FromArgb(244, 188, 66);
            this.BlockBody.Location   = new Point(APosition.X, APosition.Y);
            this.BlockBody.Size       = new Size(BlockBodyWidth, BlockBodyHeight);
            this.BlockBody.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockBody.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockBody.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.Form.DrawingPanel.Controls.Add(BlockBody);
            this.SetFocus();

            /* BlockClassLabel */

            this.BlockClassLabel            = new Label();
            this.BlockClassLabel.Location   = new Point(5, 5);
            this.BlockClassLabel.Width      = BlockBodyWidth - 10;
            this.BlockClassLabel.ForeColor  = Color.Black;
            this.BlockClassLabel.Text       = ABlockClass;
            this.BlockClassLabel.TextAlign  = ContentAlignment.MiddleCenter;
            this.BlockClassLabel.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockClassLabel.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockClassLabel.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Controls.Add(this.BlockClassLabel);

            /* BlockModelLabel */

            this.BlockModelLabel            = new Label();
            this.BlockModelLabel.Location   = new Point(5, 30);
            this.BlockModelLabel.Width      = BlockBodyWidth - 10;
            this.BlockModelLabel.ForeColor  = Color.FromArgb(128, 128, 128);
            this.BlockModelLabel.Text       = ABlockModel;
            this.BlockModelLabel.TextAlign  = ContentAlignment.MiddleCenter;
            this.BlockModelLabel.Font       = new Font(BlockModelLabel.Font.Name, 6, BlockModelLabel.Font.Style);
            this.BlockModelLabel.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockModelLabel.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockModelLabel.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Controls.Add(this.BlockModelLabel);
        }

        public void SetFocus()
        {
            this.isFocus             = true;
            Form.isHaveSelectedBlock = true;
            Form.SelectedBlockIndex    = this.Index;
            this.BlockBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        public void ClearFocus()
        {
            this.isFocus             = false;
            Form.isHaveSelectedBlock = false;
            this.BlockBody.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void SchemeBodyMouseDown(object sender, MouseEventArgs e)
        {
            if (this.isFocus)
            {
                isMouseDown = true;
            }

            if ((Form.isHaveSelectedBlock) && (Form.SelectedBlockIndex != this.Index))
            {
                Panel Pnl = sender as Panel;
                if (!Form.CheckLink(Form.SelectedBlockIndex, this.Index))
                {
                    SchemeLink SL = new SchemeLink(Form.SelectedBlockIndex, this.Index);
                    Form.isHaveSelectedBlock = false;
                    Form.AddSchemeLink(SL);
                }
            }
            else
            {
                Form.SelectedBlockIndex = this.Index;
            }

            CheckFocus();
        }

        private void CheckFocus()
        {
            if (!isFocus)
            {
                for (int i = 0; i < Form.Blocks.Length; i++)
                {
                    Form.Blocks[i].ClearFocus();
                }
                this.SetFocus();
            }
        }

        private void SchemeBodyMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point Ptr = Form.PointToClient(Cursor.Position);

                Ptr.X -= Form.DrawingPanelOffset.X + BlockBodyWidth  / 2;
                Ptr.Y -= Form.DrawingPanelOffset.Y + BlockBodyHeight / 2;

                this.BlockBody.Location = Ptr;
                this.BlockBody.Invalidate();
            }
            Form.DrawingPanel.Invalidate();
        }

        private void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Form.DrawingPanel.Invalidate();
        }

    }
}
