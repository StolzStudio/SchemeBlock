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
        public int    Id        { get; set; }
        public int    Count     { get; set; }
        public int    Index     { get; set; }
        public bool   isFocus   { get; set; }
        public string ClassText { get; set; }
        public string ModelText { get; set; }
        //
        //Fields
        //
        public  Point Location;
        private Point TextLocation;

        private int   TextOffsetX = 0;
        private int   TextOffsetY = 25;
        private Point PageOffset;
        //
        //Constructor
        //
        public Block(int _index, string _class, string _model, int _id, Point _location, Point _pageOffset)
        {
            Index      = _index;
            ClassText  = _class;
            ModelText  = _model;
            Location   = _location;
            PageOffset = _pageOffset;
            Count      = 1;
            Id         = _id;

            SetFocus();
            SetTextLocation();
        }

        public Block(Block block)
        {
            Index     = block.Index;
            Count     = block.Count;
            ClassText = block.ClassText;
            ModelText = block.ModelText;
            Id        = block.Id;
            Index     = block.Index;
            Count     = block.Count;
            Location  = block.Location;
            isFocus   = block.isFocus;
        }

        public Block(StructuralObject structuralObject, Point pageOffset)
        {
            Id         = structuralObject.Id;
            Index      = structuralObject.Index;
            ClassText  = structuralObject.Type;
            ModelText  = MetaDataManager.Instance.GetBaseObjectOfId(ClassText, Id).Name;
            Location   = structuralObject.Coordinates;
            PageOffset = pageOffset;
            Count      = 1;
        }
        //
        //Work with location
        //
        private void SetTextLocation()
        {
            TextLocation.X = Location.X + TextOffsetX;
            TextLocation.Y = Location.Y + TextOffsetY;
        }

        private void SetTextOffset(Graphics g, string text, Font font)
        {
            var size = g.MeasureString(text, font);
            TextOffsetX = (BlockWidth - (int)size.Width) / 2;
        }

        public void Move(Point location, Point clickOffset, Point pageSize)
        {
            Point Pnt = this.PointNormalize(location);
           
            Pnt.X -= clickOffset.X;
            Pnt.Y -= clickOffset.Y;

            int offset = 2;
            if ((0 + offset <= Pnt.X) && ((Pnt.X + BlockWidth) <= (pageSize.X - offset)))
            {
                Location.X = Pnt.X;   
            }
            if ((0 + offset <= Pnt.Y) && ((Pnt.Y + BlockHeight) <= (pageSize.Y - offset)))
            {
                Location.Y = Pnt.Y;
            }
            SetTextLocation();
        }

        public Point PointNormalize(Point point)
        {
            return new Point(point.X - PageOffset.X, point.Y - PageOffset.Y);
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

        public bool CheckFocus(Point mouseLocation)
        {
            if (((this.Location.X <= mouseLocation.X) && (mouseLocation.X <= this.Location.X + BlockWidth)) &&
                ((this.Location.Y <= mouseLocation.Y) && (mouseLocation.Y <= this.Location.Y + BlockHeight)))
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
