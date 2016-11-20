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
        public int InputSchemePointIndex;
        public int OutputSchemeIndex;
        public int OutputSchemePointIndex;

        public SchemeLink(int AInputSchemeIndex,  int AInputSchemePointIndex,
                          int AOutputSchemeIndex, int AOutputSchemePointIndex)
        {
            InputSchemeIndex       = AInputSchemeIndex;
            InputSchemePointIndex  = AInputSchemePointIndex;
            OutputSchemeIndex      = AOutputSchemeIndex;
            OutputSchemePointIndex = AOutputSchemePointIndex;
        }

        public Point GetInputSchemePointLocation(MainForm AForm)
        {
            SchemeBlock Block = AForm.Blocks[InputSchemeIndex];
            return new Point(Block.PointLocation[InputSchemePointIndex].X + Block.BlockBody.Location.X,
                             Block.PointLocation[InputSchemePointIndex].Y + Block.BlockBody.Location.Y);
        }

        public Point GetOutputSchemePointLocation(MainForm AForm)
        {
            SchemeBlock Block = AForm.Blocks[OutputSchemeIndex];
            return new Point(Block.PointLocation[OutputSchemePointIndex].X + Block.BlockBody.Location.X,
                             Block.PointLocation[OutputSchemePointIndex].Y + Block.BlockBody.Location.Y);
        }
    }
}
