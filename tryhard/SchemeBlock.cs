using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Input;

namespace tryhard
{
    public class SchemeBlock
    {
        /* Consts */

        public const int  BlockBodyWidth    = 80;
        public const int  BlockBodyHeight   = 80;
        private int  BlockPointSize    = 6;
        private bool isMouseDown       = false;
        private bool isCtrlDown        = false;

        /* Fields */

        private MainForm Form;
        public bool     isFocus;
        public  int      Index;
        public  Point    PointLocation;
        public Point ClickOffset;

        public Panel   BlockBody;
        public Label   BlockClassLabel;
        public Label   BlockModelLabel;
        public Panel[] BlockPoints;

        /* Properties */

        public string BlockClass { get; set; }
        public string BlockId { get; set; }

        /* Methods */

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

            this.BlockBody            = new Panel();
            this.BlockBody.BackColor  =     Color.FromArgb(244, 188, 66);
            this.BlockBody.Location   = new Point(APosition.X, APosition.Y);
            this.BlockBody.Size       = new Size(BlockBodyWidth, BlockBodyHeight);
            this.BlockBody.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockBody.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockBody.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Paint     += new PaintEventHandler(this.SchemeBodyRedraw);
            this.Form.DrawingPanel.Controls.Add(BlockBody);
            this.SetFocus();

            /* BlockClassLabel */

            this.BlockClassLabel            = new Label();
            this.BlockClassLabel.Location   = new Point(5, 5);
            this.BlockClassLabel.Width      = BlockBodyWidth - 10;
            this.BlockClassLabel.ForeColor  = Color.Black;
            this.BlockClassLabel.Text       = CMeta.DictionaryName[ABlockClass];
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

        private void SchemeBodyRedraw(object sender, PaintEventArgs e)
        {
            if (this.isFocus)
            {
                this.SetFocus();
            }
        }

        public void SetFocus()
        {
            this.isFocus             = true;
            Form.Manager.isHaveSelectedBlock = true;
            Form.Manager.SelectedBlockIndex  = this.Index;
            Graphics g        = this.BlockBody.CreateGraphics();
            Pen      BlackPen = new Pen(Color.Black, 4);
            g.DrawRectangle(BlackPen, 
                            0,
                            0,
                            BlockBodyWidth, 
                            BlockBodyHeight);
            Form.DeleteBlockButton.Visible = true;
        }

        public void ClearFocus()
        {
            this.isFocus                   = false;
            Form.Manager.isHaveSelectedBlock = false;
            Form.DeleteBlockButton.Visible = false;
            this.BlockBody.Invalidate();
        }

        private void SchemeBodyMouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            Point Ptr = this.PointNormalize(Form.PointToClient(Cursor.Position));
            ClickOffset = new Point(Ptr.X - this.BlockBody.Location.X, Ptr.Y - this.BlockBody.Location.Y);

            if (Control.ModifierKeys == Keys.Control)
            {
                isCtrlDown = true;
            }
            else
            {
                isCtrlDown = false;
            }

            if ((Form.Manager.isHaveSelectedBlock) && (Form.Manager.SelectedBlockIndex != this.Index) && isCtrlDown)
            {
                Panel Pnl = sender as Panel;
                if (!Form.Manager.CheckLink(Form.Manager.SelectedBlockIndex, this.Index) && 
                     Form.Meta.isPossibleLink(Form.Manager.Blocks[Form.Manager.SelectedBlockIndex].BlockClass, this.BlockClass))
                {
                    Form.Manager.isHaveSelectedBlock = false;
                    isCtrlDown = false;
                    Form.Manager.ClearLinksFocus();
                    Form.Manager.AddSchemeLink(new SchemeLink(Form.Manager.SelectedBlockIndex, this.Index));
                }
            }
            else
            {
                Form.Manager.isHaveSelectedBlock = true;
                Form.Manager.SelectedBlockIndex = this.Index;
            }

            Form.SetComboBoxes(this.BlockClass, this.BlockId);

            CheckFocus();
        }

        private void CheckFocus()
        {
            if (!isFocus)
            {
                foreach (int Key in Form.Manager.Blocks.Keys)
                {
                    Form.Manager.Blocks[Key].ClearFocus();
                }
                this.SetFocus();
            }
        }

        private void SchemeBodyMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point Ptr = this.PointNormalize(Form.PointToClient(Cursor.Position));

                Ptr.X -= this.ClickOffset.X;
                Ptr.Y -= this.ClickOffset.Y;
                
                this.BlockBody.Location = Ptr;
                this.BlockBody.Invalidate();
            }
            Form.DrawingPanel.Invalidate();
        }

        private void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                isCtrlDown = true;
            }
            else
            {
                isCtrlDown = false;
            }
            isMouseDown = false;
            Form.DrawingPanel.Invalidate();
        }

        private Point PointNormalize(Point Ptr)
        {
            return new Point(Ptr.X  - Form.DrawingPanelOffset.X, Ptr.Y - Form.DrawingPanelOffset.Y);
        }

    }
}
