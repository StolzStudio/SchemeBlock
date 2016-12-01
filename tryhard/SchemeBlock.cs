using System;
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

        private int  BlockBodyWidth    = 60;
        private int  BlockBodyHeight   = 60;
        private int  BlockPointSize    = 6;
        private bool isMouseDown       = false;

        private MainForm Form;
        private int      Index;
        private bool     isFocus;

        public bool       isAddSchemeLink = false;
        public Point      PointLocation;
        public SchemeLink AddedSchemeLink;

        /* Controls */

        public Panel   BlockBody;
        public Label   BlockClassLabel;
        public Label   BlockModelLabel;
        public Panel[] BlockPoints;

        /* DB fields */

        private string block_class;
        private int block_id;

        public string BlockClass
        {
            get { return block_class; }
            set { block_class = value; }
        }

        public int BlockId
        {
            get { return block_id; }
            set { block_id = value; }
        }


        public SchemeBlock(int AIndex, string ABlockClass, int ABlockId, Point APosition, MainForm AForm)
        {
            Form  = AForm;
            Index = AIndex;
            BlockClass = ABlockClass;
            BlockId = ABlockId;
            PointLocation = new Point(BlockBodyWidth / 2, BlockBodyHeight / 2);
            this.InitializeComponent(ABlockClass, "TPC-100", APosition);
        }

        private void InitializeComponent(string ABlockClass, string ABlockModel, Point APosition)
        {
            /* BlockBody */

            this.BlockBody            = new Panel();
            this.BlockBody.BackColor  =     Color.FromArgb(27, 239, 253);
            this.BlockBody.Location   = new Point(APosition.X, APosition.Y);
            this.BlockBody.Size       = new Size(BlockBodyWidth, BlockBodyHeight);
            this.BlockBody.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockBody.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockBody.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Click     += new EventHandler(this.SchemeBodyClick);
            this.BlockBody.Paint     += new PaintEventHandler(this.SchemeBodyRedraw);
            this.Form.DrawingPanel.Controls.Add(BlockBody);
            this.SetFocus();

            /* BlockClassLabel */

            this.BlockClassLabel          = new Label();
            this.BlockClassLabel.Location = new Point(5, 5);
            this.BlockClassLabel.Width    = BlockBodyWidth - 10;
            this.BlockClassLabel.Text     = ABlockClass;
            this.BlockBody.Controls.Add(this.BlockClassLabel);

            /* BlockModelLabel */

            this.BlockModelLabel          = new Label();
            this.BlockModelLabel.Location = new Point(5, 5);
            this.BlockModelLabel.Width    = BlockBodyWidth - 10;
            this.BlockModelLabel.Text     = ABlockModel;
            this.BlockBody.Controls.Add(this.BlockModelLabel);
        }

        private void SchemeBodyRedraw(object sender, PaintEventArgs e)
        {
            if (this.isFocus)
            {
                this.SetFocus();
            }
        }

        public void SetFocus()
        {
            this.isFocus = true;
            Graphics g        = this.BlockBody.CreateGraphics();
            Point    Ptr      = new Point(this.BlockBody.Location.X + BlockBodyWidth, this.BlockBody.Location.Y + BlockBodyHeight);
            Pen      BlackPen = new Pen(Color.Black, 4);
            g.DrawRectangle(BlackPen, 
                            0,
                            0,
                            BlockBodyWidth, 
                            BlockBodyHeight);
        }

        public void ClearFocus()
        {
            this.isFocus = false;
            this.BlockBody.Invalidate();
        }

        private void SchemeBodyMouseDown(object sender, MouseEventArgs e)
        {
            if (this.isFocus)
            {
                isMouseDown = true;
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
        }

        private void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Form.DrawingPanel.Invalidate();
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
        }

        private void SchemeBodyClick(object sender, EventArgs e)
        {
            if (Form.isBlockPointClick)
            {
                Panel Pnl              = sender as Panel;
                SchemeLink SL          = new SchemeLink(Form.InputSchemeIndex, Form.InputSchemePointIndex);
                Form.isBlockPointClick = false;
                Form.AddSchemeLink(SL);
            }
            else
            {
                CheckFocus();
            }
        }
    }
}
