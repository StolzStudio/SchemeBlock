using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryhard
{
    public class Block
    {
        //
        //Consts
        //
        public const int BlockWidth  = 80;
        public const int BlockHeight = 80;
        //
        //Properties
        //
        public string ClassText   { get; set; }
        public string ModelText   { get; set; }
        public int    Count       { get; set; }
        //
        //Fields
        //
        public bool isFocus = false;
        public int  Index   = 0; 

        public  Point Location;
        private Point TextLocation;
        public  Point PointLocation;

        private int   TextOffsetX = 0;
        private int   TextOffsetY = 25;
        private Point PageOffset;
        //
        //Constructor
        //
        public Block(int aIndex, string aClass, string aModel, Point aLocation, Point aPageOffset)
        {
            Index      = aIndex;
            ClassText  = aClass;
            ModelText  = aModel;
            Location   = aLocation;
            PageOffset = aPageOffset;

            SetFocus();

            SetTextLocation();
        }
        //
        //Work with location
        //
        private void SetTextLocation()
        {
            TextLocation.X = Location.X + TextOffsetX;
            TextLocation.Y = Location.Y + TextOffsetY;
        }

        private void SetTextOffset(Graphics g, string aText, Font aFont)
        {
            var size = g.MeasureString(aText, aFont);
            TextOffsetX = (BlockWidth - (int)size.Width) / 2;
        }

        public void Move(Point aLocation, Point aClickOffset, Point aPageSize)
        {
            Point Pnt = this.PointNormalize(aLocation);
           
            Pnt.X -= aClickOffset.X;
            Pnt.Y -= aClickOffset.Y;

            int offset = 2;
            if ((0 + offset <= Pnt.X) && ((Pnt.X + BlockWidth) <= (aPageSize.X - offset)))
            {
                this.Location.X = Pnt.X;   
            }
            if ((0 + offset <= Pnt.Y) && ((Pnt.Y + BlockHeight) <= (aPageSize.Y - offset)))
            {
                this.Location.Y = Pnt.Y;
            }
            SetTextLocation();
        }

        public Point PointNormalize(Point Pnt)
        {
            return new Point(Pnt.X - PageOffset.X, Pnt.Y - PageOffset.Y);
        }
        //
        //Work with focus
        //
        public void SetFocus()
        {
            this.isFocus = true;
        }

        public void ClearFocus()
        {
            this.isFocus = false;
        }

        public bool CheckFocus(Point aMouseLocation)
        {
            if (((this.Location.X <= aMouseLocation.X)&&(aMouseLocation.X <= this.Location.X + BlockWidth))&&
                ((this.Location.Y <= aMouseLocation.Y)&&(aMouseLocation.Y <= this.Location.Y + BlockHeight)))
            {
                SetFocus();
            }
            return this.isFocus;
        }
        //
        //Draw
        //
        public void Draw(Graphics g)
        {
            Pen FigurePen;
            if (this.isFocus) { FigurePen = new Pen(Color.Black, 4); }
                         else { FigurePen = new Pen(Color.Black, 1); }

            SolidBrush FigureBrush = new SolidBrush(Color.Orange);

            Rectangle Figure = new Rectangle(Location.X, Location.Y,
                                        BlockWidth, BlockHeight);


            g.DrawRectangle(FigurePen, Figure);
            g.FillRectangle(FigureBrush, Figure);

            DrawText(g, new Font("Microsoft YaHei", 6), new SolidBrush(Color.Black), ClassText);
            Location.Y += 15;
            DrawText(g, new Font("Microsoft YaHei", 7), new SolidBrush(Color.DimGray), ModelText);
            Location.Y -= 15;

        }

       private void DrawText(Graphics g, Font aFont, SolidBrush aBrush, string aText)
       {
            SetTextOffset(g, aText, aFont);
            SetTextLocation();
            g.DrawString(aText, aFont, aBrush, TextLocation);
       }

    }
}
