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
    class Block
    {
        //
        //Consts
        //
        public const int BlockWidth  = 80;
        public const int BlockHeight = 80; 
        //
        //Fields
        //
        public bool isFocus = false;
        public int  Index   = 0;

        private string ClassText;
        private string ModelText;
        public  Point  Location;
        private Point  TextLocation;

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

        public void Move(Point aLocation)
        {
            Point Pnt = this.PointNormalize(aLocation);

            //Pnt.X -= this.ClickOffset.X;
            //Pnt.Y -= this.ClickOffset.Y;

            this.Location = Pnt;
            SetTextLocation();
        }

        private Point PointNormalize(Point Pnt)
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

            DrawText(g, new Font("Microsoft YaHei", 12), new SolidBrush(Color.Black), ClassText);
            Location.Y += 15;
            DrawText(g, new Font("Microsoft YaHei", 10), new SolidBrush(Color.DimGray), ModelText);
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
