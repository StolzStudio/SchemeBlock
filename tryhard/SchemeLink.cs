using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace tryhard
{
    public class SchemeLink
    {
        public int InputSchemeIndex;
        public int OutputSchemeIndex;

        public SchemeLink(int AInputSchemeIndex, int AOutputSchemeIndex)
        {
            InputSchemeIndex       = AInputSchemeIndex;
            OutputSchemeIndex      = AOutputSchemeIndex;
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
    }
}
