using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class SchemeLink
    {
        private int InputSchemeIndex;
        private int InputSchemePointIndex;
        private int OutputSchemeIndex;
        private int OutputSchemePointIndex;

        public SchemeLink(int AInputSchemeIndex, int AInputSchemePointIndex,
                          int AOutputSchemeIndex, int AOutputSchemePointIndex)
        {
            InputSchemeIndex       = AInputSchemeIndex;
            InputSchemePointIndex  = AInputSchemePointIndex;
            OutputSchemeIndex      = AOutputSchemeIndex;
            OutputSchemePointIndex = AOutputSchemePointIndex;
        }

        public System.Drawing.Point GetInputSchemePointLocation(SchemeBlock AInputBlock)
        {
            return AInputBlock.BlockBody.Location; ///
        }
    }
}
