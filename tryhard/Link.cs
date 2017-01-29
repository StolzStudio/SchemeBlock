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
        public int FirstBlockIndex { get; set; }
        public int SecondBlockIndex { get; set; }
        public string ParameterName { get; set; }
        public bool isFocus { get; set; }
        //
        //Fields
        //
        private Point[] Points;
        private LineType FirstLine;
        private Color ArrowColor;
        private Color SelectArrowColor;
        //
        //Methods
        //
        public Link(int AFirstBlockIndex, int ASecondBlockIndex)
        {
            Points = new Point[4];
            ArrowColor = Color.DarkSlateBlue;
            SelectArrowColor = Color.IndianRed;
            FirstBlockIndex = AFirstBlockIndex;
            SecondBlockIndex = ASecondBlockIndex;
            isFocus = false;
        }
        //
        //take point location
        //
        public Point GetFirstBlockPointLocation(Dictionary<int, Block> aBlocks)
        {
            Block Result = aBlocks[FirstBlockIndex];
            return new Point(Result.Location.X + (Block.BlockWidth / 2),
                             Result.Location.Y + (Block.BlockHeight / 2));
        }

        public Point GetSecondBlockPointLocation(Dictionary<int, Block> aBlocks)
        {
            Block Result = aBlocks[SecondBlockIndex];
            return new Point(Result.Location.X + (Block.BlockWidth / 2),
                             Result.Location.Y + (Block.BlockHeight / 2));
        }

        public bool CheckDeletedLink(int AIndex)
        {
            if ((AIndex == FirstBlockIndex) || (AIndex == SecondBlockIndex)) { return true; }
            else { return false; }
        }
        //
        //draw link
        //
        public void Draw(Dictionary<int, Block> aBlocks, Graphics g)
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

            Points[0] = GetFirstBlockPointLocation(aBlocks);
            Points[2] = GetSecondBlockPointLocation(aBlocks);

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

            int Width = 40;

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
        public void TrySetFocus(Point Coord)
        {
            if (FirstLine == LineType.LTHorizontal)
            {
                CheckFocus(Points[0].X, Points[0].Y, Points[1].X, Coord.X, Coord.Y);
                CheckFocus(Points[1].Y, Points[1].X, Points[2].Y, Coord.Y, Coord.X);
            }
            else
            {
                CheckFocus(Points[0].Y, Points[0].X, Points[1].Y, Coord.Y, Coord.X);
                CheckFocus(Points[1].X, Points[1].Y, Points[2].X, Coord.X, Coord.Y);
            }

        }

        public void CheckFocus(int Pnt0, int Pnt0_1, int Pnt1, int Pnt2, int Pnt2_1)
        {
            int SelectOffset = 7;

            int min;
            int max;

            if (Pnt0 <= Pnt1) { min = Pnt0; max = Pnt1; }
            else { min = Pnt1; max = Pnt0; }

            if ((min - SelectOffset <= Pnt2) && (Pnt2 <= max + SelectOffset))
            {
                if ((Pnt0_1 - SelectOffset <= Pnt2_1) && (Pnt2_1 <= Pnt0_1 + SelectOffset))
                {
                    this.isFocus = true;
                }
            }
        }
    }
}
