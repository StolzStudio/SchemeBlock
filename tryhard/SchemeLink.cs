using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    public class SchemeLink
    {
        public int InputSchemeIndex;
        public int InputSchemePointIndex;
        public int OutputSchemeIndex;
        public int OutputSchemePointIndex;

        public SchemeLink(int AInputSchemeIndex, int AInputSchemePointIndex,
                          int AOutputSchemeIndex, int AOutputSchemePointIndex)
        {
            InputSchemeIndex       = AInputSchemeIndex;
            InputSchemePointIndex  = AInputSchemePointIndex;
            OutputSchemeIndex      = AOutputSchemeIndex;
            OutputSchemePointIndex = AOutputSchemePointIndex;
        }

        public System.Drawing.Point GetInputSchemePointLocation(MainForm AForm)
        {
            return AForm.Blocks[InputSchemeIndex].BlockPoints[InputSchemePointIndex].Location; 
        }

        public System.Drawing.Point GetOutputSchemePointLocation(MainForm AForm)
        {
            return AForm.Blocks[OutputSchemeIndex].BlockPoints[OutputSchemePointIndex].Location;
        }
    }
}
