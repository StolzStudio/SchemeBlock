using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace tryhard
{
    public class SchemeLink
    {
        public  int     InputSchemeIndex;
        public  int     OutputSchemeIndex;
        private Point[] Points;

        public SchemeLink(int AInputSchemeIndex, int AOutputSchemeIndex)
        {
            Points            = new Point[4];
            InputSchemeIndex  = AInputSchemeIndex;
            OutputSchemeIndex = AOutputSchemeIndex;
        }

        public Point GetInputSchemePointLocation(MainForm AForm)
        {
            SchemeBlock Block = AForm.Blocks[InputSchemeIndex];
            return new Point(Block.PointLocation.X + Block.BlockBody.Location.X,
                             Block.PointLocation.Y + Block.BlockBody.Location.Y);
        }

        public Point GetOutputSchemePointLocation(MainForm AForm)
        {
            SchemeBlock Block = AForm.Blocks[OutputSchemeIndex];
            return new Point(Block.PointLocation.X + Block.BlockBody.Location.X,
                             Block.PointLocation.Y + Block.BlockBody.Location.Y);
        }

        public void Draw(MainForm AForm, PaintEventArgs e)
        {
            Pen pen   = new Pen(Color.DarkSlateBlue);
            pen.Width = 1.5F;
            Points[0] = GetInputSchemePointLocation(AForm);
            Points[3] = GetOutputSchemePointLocation(AForm);
            if (Math.Abs(Points[0].X - Points[3].X) <
                Math.Abs(Points[0].Y - Points[3].Y))
            {
                Points[1] = new Point(Points[0].X, GiveY());
                Points[2] = new Point(Points[3].X, GiveY());
            }
            else
            {
                Points[1] = new Point(GiveX(), Points[0].Y);
                Points[2] = new Point(GiveX(), Points[3].Y);
            }

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, Points[i], Points[i + 1]);
            }
        }

        private int GiveX()
        {
            if (Points[0].X > Points[3].X)
            {
                return Points[0].X / 2;
            }
            else
            {
                return Points[3].X / 2;
            }
        }

        private int GiveY()
        {
            if (Points[0].Y > Points[3].Y)
            {
                return Points[0].Y / 2;
            }
            else
            {
                return Points[3].Y / 2;
            }
        }
    }
}
