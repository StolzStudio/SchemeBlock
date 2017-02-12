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
        public const int BlockWidth  = 88;
        public const int BlockHeight = 88;
        //
        //Properties
        //
        public string ClassText { get; set; }
        public string ModelText { get; set; }
        public int    Count     { get; set; }
        public int    Index     { get; set; }
        public int    Id        { get; set; }
        //
        //Fields
        //
        public Point Location;
        public bool isFocus = false;
        private Point TextLocation;

        private int   TextOffsetX = 0;
        private int   TextOffsetY = 25;
        private Point PageOffset;
        //
        //Constructor
        //
        public Block(int aIndex, string aClass, string aModel, int aId, Point aLocation, Point aPageOffset)
        {
            Index      = aIndex;
            ClassText  = aClass;
            ModelText  = aModel;
            Location   = aLocation;
            PageOffset = aPageOffset;
            Count      = 0;
            Id         = aId;
            SetFocus();
            SetTextLocation();
        }

        public Block(StructuralObject AStructuralObject, Point aPageOffset)
        {
            Id         = AStructuralObject.Id;
            Index      = AStructuralObject.Index;
            ClassText  = AStructuralObject.Type;
            ModelText  = MetaDataManager.Instance.GetBaseObjectOfId(ClassText, Id).Name;
            Location   = AStructuralObject.Coordinates;
            PageOffset = aPageOffset;
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
                Location.X = Pnt.X;   
            }
            if ((0 + offset <= Pnt.Y) && ((Pnt.Y + BlockHeight) <= (aPageSize.Y - offset)))
            {
                Location.Y = Pnt.Y;
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

            DrawText(g);
        }

       private void DrawText(Graphics g)
       {
            SetTextLocation();

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font font = new Font("Microsoft YaHei", 6);
            SolidBrush brush = new SolidBrush(Color.Black);
            string text = MetaDataManager.Instance.Dictionary[ClassText];
            RectangleF rectClass = new Rectangle(TextLocation.X, TextLocation.Y - 5, BlockWidth, 20);
            g.DrawString(text, font, brush, rectClass, stringFormat);

            font = new Font("Microsoft YaHei", 7);
            brush = new SolidBrush(Color.DimGray);
            text = ModelText;
            RectangleF rectModel = new Rectangle(TextLocation.X, TextLocation.Y + 20, BlockWidth - 1, 20);
            g.DrawString(text, font, brush, rectModel, stringFormat);
        }

    }
}
