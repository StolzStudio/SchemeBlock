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
        //
        //Consts
        //
        public  const int BlockBodyWidth  = 80;
        public  const int BlockBodyHeight = 80;
        private const int BlockPointSize  = 6;
        //
        //Fields
        //
        private MainForm Form;

        private bool isMouseDown = false;
        private bool isCtrlDown  = false;
        public  bool isFocus     = false;
        public  int  Index       = 0;

        public Point PointLocation;
        public Point ClickOffset;

        public Panel   BlockBody;
        public Label   BlockClassLabel;
        public Label   BlockModelLabel;
        public Panel[] BlockPoints;
        //
        //Properties
        //
        public string BlockClass { get; set; }
        public string BlockId { get; set; }
        public int Count { get; set; }
        //
        //Methods
        //
        public SchemeBlock(int AIndex, string ABlockClass, string ABlockId, Point APosition, MainForm AForm, int ACount = 1)
        {
            Form       = AForm;
            Index      = AIndex;
            BlockClass = ABlockClass;
            BlockId    = ABlockId;
            Count = ACount;

            PointLocation = new Point(BlockBodyWidth / 2, BlockBodyHeight / 2);

            this.InitializeComponent(ABlockClass, "test", APosition);
        }

        private void InitializeComponent(string ABlockClass, string ABlockModel, Point APosition)
        {
            //
            //BlockBody
            //
            this.BlockBody            = new Panel();
            this.BlockBody.BackColor  =     Color.FromArgb(244, 188, 66);
            this.BlockBody.Location   = new Point(APosition.X, APosition.Y);
            this.BlockBody.Size       = new Size(BlockBodyWidth, BlockBodyHeight);
            this.BlockBody.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockBody.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockBody.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            this.BlockBody.Paint     += new PaintEventHandler(this.SchemeBodyRedraw);

            if (Form.SchemeManagerNumber == 0)
            {
                this.Form.MainPage.Controls.Add(BlockBody);
            }
            else if (Form.SchemeManagerNumber == 1)
            {
             //   this.Form.ObjectPage.Controls.Add(BlockBody);
            }
            this.SetFocus();
            //
            //BlockClassLabel
            //
            this.BlockClassLabel            = new Label();
            this.BlockClassLabel.Location   = new Point(5, 5);
            this.BlockClassLabel.Width      = BlockBodyWidth - 10;
            this.BlockClassLabel.ForeColor  = Color.Black;
            this.BlockClassLabel.Text       = ABlockClass;
            this.BlockClassLabel.TextAlign  = ContentAlignment.MiddleCenter;
            this.BlockClassLabel.MouseDown += new MouseEventHandler(this.SchemeBodyMouseDown);
            this.BlockClassLabel.MouseMove += new MouseEventHandler(this.SchemeBodyMouseMove);
            this.BlockClassLabel.MouseUp   += new MouseEventHandler(this.SchemeBodyMouseUp);
            //
            //BlockModelLabel
            //
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
            //
            //add controls
            //
            this.BlockBody.Controls.Add(this.BlockClassLabel);
            this.BlockBody.Controls.Add(this.BlockModelLabel);
        }
        //
        //work with draw
        //
        private void SchemeBodyRedraw(object sender, PaintEventArgs e)
        {
            if (this.isFocus) { this.SetFocus(); }
        }
        //
        //work with focus
        //
        public void SetFocus()
        {
            this.isFocus = true;

            Form.SchemeManager[Form.SchemeManagerNumber].isHaveSelectedBlock = true;
            Form.SchemeManager[Form.SchemeManagerNumber].SelectedBlockIndex  = this.Index;

            Graphics g = this.BlockBody.CreateGraphics();

            Pen BlackPen = new Pen(Color.Black, 4);

            g.DrawRectangle(BlackPen, 
                            0,
                            0,
                            BlockBodyWidth, 
                            BlockBodyHeight);

            BlockBody.BringToFront();
            //Form.DeleteBlockButton.Visible = true;
        }

        public void ClearFocus()
        {
            Form.SchemeManager[Form.SchemeManagerNumber].isHaveSelectedBlock = false;
            //Form.DeleteBlockButton.Visible = false;
            this.isFocus = false;

            this.BlockBody.Invalidate();
        }

        private void CheckFocus()
        {
            if (!isFocus)
            {
                foreach (int Key in Form.SchemeManager[Form.SchemeManagerNumber].Blocks.Keys)
                {
                    Form.SchemeManager[Form.SchemeManagerNumber].Blocks[Key].ClearFocus();
                }
                this.SetFocus();
            }
        }
        //
        //work with mouse
        //
        private void SchemeBodyMouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            Point Pnt   = this.PointNormalize(Form.PointToClient(Cursor.Position));
            ClickOffset = new Point(Pnt.X - this.BlockBody.Location.X, 
                                    Pnt.Y - this.BlockBody.Location.Y);

            if (Control.ModifierKeys == Keys.Control) { isCtrlDown = true;  }
                                                 else { isCtrlDown = false; }

            if ((Form.SchemeManager[Form.SchemeManagerNumber].isHaveSelectedBlock) && 
                (Form.SchemeManager[Form.SchemeManagerNumber].SelectedBlockIndex != this.Index) && isCtrlDown)
            {
                Panel Pnl = sender as Panel;
                if (!Form.SchemeManager[Form.SchemeManagerNumber].CheckLink(Form.SchemeManager[Form.SchemeManagerNumber].SelectedBlockIndex, this.Index))
                {
                    Form.SchemeManager[Form.SchemeManagerNumber].isHaveSelectedBlock = false;
                    isCtrlDown                             = false;

                    Form.SchemeManager[Form.SchemeManagerNumber].ClearLinksFocus();
                    Form.SchemeManager[Form.SchemeManagerNumber].AddSchemeLink(new SchemeLink(Form.SchemeManager[Form.SchemeManagerNumber].SelectedBlockIndex, this.Index));
                }
            }
            else
            {
                Form.SchemeManager[Form.SchemeManagerNumber].isHaveSelectedBlock = true;
                Form.SchemeManager[Form.SchemeManagerNumber].SelectedBlockIndex  = this.Index;
            }

            //Form.SetComboBoxes(this.BlockClass, this.BlockId);
            Form.SchemeManager[Form.SchemeManagerNumber].ClearLinksFocus();
            CheckFocus();
        }

        private void SchemeBodyMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point Pnt = this.PointNormalize(Form.PointToClient(Cursor.Position));

                Pnt.X -= this.ClickOffset.X;
                Pnt.Y -= this.ClickOffset.Y;
                
                this.BlockBody.Location = Pnt;

                this.BlockBody.Invalidate();
            }

            Form.MainPage.Invalidate();
        }

        private void SchemeBodyMouseUp(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control) { isCtrlDown = true;  }
                                                 else { isCtrlDown = false; }
            isMouseDown = false;

            Form.MainPage.Invalidate();
        }

        private bool isPointValidate(Point Pnt)
        {
            if (Pnt.X + this.BlockBody.Width <= Form.MainPage.Width &&
                Pnt.Y + this.BlockBody.Height <= Form.MainPage.Height)
            {
                return true;
            }
            return false;
        }

        private Point PointNormalize(Point Pnt)
        {
            return new Point(Pnt.X - Form.DrawingPanelOffset.X, Pnt.Y - Form.DrawingPanelOffset.Y);
        }

    }
}
