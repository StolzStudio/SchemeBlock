using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace tryhard
{
    public enum LineType { LTHorizontal, LTVertical };

    public class Link
    {
        //
        //Properties
        //
        public int    FirstBlockIndex    { get; set; }
        public int    SecondBlockIndex   { get; set; }
        public string LinkParameter      { get; set; } = "";
        public int    LinkParameterValue { get; set; }
        public bool   isFocus            { get; set; }
        //
        //Fields
        //
        private Point[]  Points;
        private LineType FirstLine;
        private Color    ArrowColor;
        private Color    SelectArrowColor;
        //
        //Methods
        //
        public Link(int firstBlockIndex, int secondBlockIndex, string linkParameter, int linkParameterValue)
        {
            Points             = new Point[4];
            isFocus            = false;
            ArrowColor         = Color.DarkSlateBlue;
            LinkParameter      = linkParameter;
            FirstBlockIndex    = firstBlockIndex;
            SelectArrowColor   = Color.IndianRed;
            SecondBlockIndex   = secondBlockIndex;
            LinkParameterValue = linkParameterValue;
        }

        public Link(LinkStructuralObject link)
        {
            Points           = new Point[4];
            isFocus          = false;
            ArrowColor       = Color.DarkSlateBlue;
            LinkParameter    = link.LinkParameter;
            FirstBlockIndex  = link.FirstBlockIndex;
            SelectArrowColor = Color.IndianRed;
            SecondBlockIndex = link.SecondBlockIndex;
        }
        //
        //take point location
        //
        public Point GetFirstBlockPointLocation(Dictionary<int, Block> blocks)
        {
            Block Result = blocks[FirstBlockIndex];
            return new Point(Result.Location.X + (Block.BlockWidth / 2),
                             Result.Location.Y + (Block.BlockHeight / 2));
        }

        public Point GetSecondBlockPointLocation(Dictionary<int, Block> blocks)
        {
            Block Result = blocks[SecondBlockIndex];
            return new Point(Result.Location.X + (Block.BlockWidth / 2),
                             Result.Location.Y + (Block.BlockHeight / 2));
        }

        public bool CheckDeletedLink(int index)
        {
            if ((index == FirstBlockIndex) || (index == SecondBlockIndex)) return true;
            return false;
        }
        //
        //draw link
        //
        public void Draw(Dictionary<int, Block> blocks, Graphics g)
        {
            Pen pen = new Pen(this.ArrowColor);

            if (this.isFocus)
            {
                pen.Width = 2.0F;
                pen.Color = this.SelectArrowColor;
            }
            else
            {
                pen.Width = 1.5F;
            }

            Points[0] = GetFirstBlockPointLocation(blocks);
            Points[2] = GetSecondBlockPointLocation(blocks);

            if (Math.Abs(Math.Abs(Points[0].X) - Math.Abs(Points[2].X)) <
                Math.Abs(Math.Abs(Points[0].Y) - Math.Abs(Points[2].Y)))
            {
                Points[1] = new Point(Points[2].X, Points[0].Y);
            }
            else
            {
                Points[1] = new Point(Points[0].X, Points[2].Y);
            }

            for (int i = 0; i < 2; i++)
            {
                g.DrawLine(pen, Points[i], Points[i + 1]);
            }

            DrawPointer(g, pen);
        }

        private void DrawPointer(Graphics g, Pen p)
        {
            Point[] Ptr = new Point[3];
            Brush b = new SolidBrush(p.Color);

            int Width = 44;

            if (Points[1].X == Points[2].X)
            {
                FirstLine = LineType.LTHorizontal;
                if (Points[1].Y > Points[2].Y)
                {
                    Ptr[0] = new Point(Points[2].X, Points[2].Y + Width);
                    Ptr[1] = new Point(Points[2].X + 5, Points[2].Y + Width + 10);
                    Ptr[2] = new Point(Points[2].X - 5, Points[2].Y + Width + 10);
                }
                else
                {
                    Ptr[0] = new Point(Points[2].X, Points[2].Y - Width);
                    Ptr[1] = new Point(Points[2].X + 5, Points[2].Y - Width - 10);
                    Ptr[2] = new Point(Points[2].X - 5, Points[2].Y - Width - 10);
                }
            }

            if (Points[1].Y == Points[2].Y)
            {
                FirstLine = LineType.LTVertical;
                if (Points[1].X > Points[2].X)
                {
                    Ptr[0] = new Point(Points[2].X + Width, Points[1].Y);
                    Ptr[1] = new Point(Points[2].X + Width + 10, Points[1].Y + 5);
                    Ptr[2] = new Point(Points[2].X + Width + 10, Points[1].Y - 5);
                }
                else
                {
                    Ptr[0] = new Point(Points[2].X - Width, Points[1].Y);
                    Ptr[1] = new Point(Points[2].X - Width - 10, Points[1].Y + 5);
                    Ptr[2] = new Point(Points[2].X - Width - 10, Points[1].Y - 5);
                }
            }

            g.DrawPolygon(p, Ptr);
            g.FillPolygon(b, Ptr);
        }
        //
        //work with focus
        //
        public void TrySetFocus(Point coord)
        {
            if (FirstLine == LineType.LTHorizontal)
            {
                CheckFocus(Points[0].X, Points[0].Y, Points[1].X, coord.X, coord.Y);
                CheckFocus(Points[1].Y, Points[1].X, Points[2].Y, coord.Y, coord.X);
            }
            else
            {
                CheckFocus(Points[0].Y, Points[0].X, Points[1].Y, coord.Y, coord.X);
                CheckFocus(Points[1].X, Points[1].Y, Points[2].X, coord.X, coord.Y);
            }

        }

        public void CheckFocus(int pnt0, int pnt0_1, int pnt1, int pnt2, int pnt2_1)
        {
            int SelectOffset = 7;

            int min;
            int max;

            if (pnt0 <= pnt1) { min = pnt0; max = pnt1; }
            else { min = pnt1; max = pnt0; }

            if ((min - SelectOffset <= pnt2) && (pnt2 <= max + SelectOffset))
            {
                if ((pnt0_1 - SelectOffset <= pnt2_1) && (pnt2_1 <= pnt0_1 + SelectOffset))
                {
                    this.isFocus = true;
                }
            }
        }
    }
}
