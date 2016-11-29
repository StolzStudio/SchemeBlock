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
            PointLocation    = new Point[5];
            PointLocation[0] = new Point(BlockBodyWidth / 2 - BlockPointSize / 2, 0);
            PointLocation[1] = new Point(BlockBodyWidth - BlockPointSize, BlockBodyHeight / 2 - BlockPointSize / 2);
            PointLocation[2] = new Point(BlockBodyWidth / 2 - BlockPointSize / 2, BlockBodyHeight - BlockPointSize);
            PointLocation[3] = new Point(0, BlockBodyHeight / 2 - BlockPointSize / 2);
            PointLocation[4] = new Point(BlockBodyWidth / 2, BlockBodyHeight / 2);
            this.InitializeComponent(ABlockClass, "TPC-100", APosition);
        }

        private void InitializeComponent(string ABlockClass, string ABlockModel, Point APosition)
        {
            /* BlockBody */

            this.BlockBody = new Panel();
            this.BlockBody.BackColor  =     Color.Beige;
            this.BlockBody.Location   = new Point(APosition.X, APosition.Y);
            this.BlockBody.Size       = new Size(BlockBodyWidth, BlockBodyHeight);
            this.BlockBody.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockBody.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockBody.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Click     += new EventHandler(this.SchemeBodyClick);
            this.Form.DrawingPanel.Controls.Add(BlockBody);
            this.SetFocus();

            /* BlockClassLabel */

            this.BlockClassLabel          = new Label();
            this.BlockClassLabel.Location = new Point(0, 0);
            this.BlockClassLabel.Text     = ABlockClass;
            this.BlockBody.Controls.Add(this.BlockClassLabel);

            /* BlockModelLabel */

            this.BlockModelLabel          = new Label();
            this.BlockModelLabel.Location = new Point(0, 0);
            this.BlockModelLabel.Text     = ABlockModel;
            this.BlockBody.Controls.Add(this.BlockModelLabel);
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
        }

        private void SchemeBodyClick(object sender, EventArgs e)
        {
            if (Form.isBlockPointClick)
            {
                Panel Pnl              = sender as Panel;
                SchemeLink SL          = new SchemeLink(Form.InputSchemeIndex, Form.InputSchemePointIndex, this.Index, 4);
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
