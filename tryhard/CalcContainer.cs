using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class CalcContainer
    {
        public string FirstBlockId;
        public string FirstBlockClass;
        public string SecondBlockId;
        public string SecondBlockClass;
        public int Count;

        public CalcContainer(string AFirstBlockId, string AFirstBlockClass, 
                             string ASecondBlockId, string ASecondBlockClass)
        {
            FirstBlockId     = AFirstBlockId;
            FirstBlockClass  = AFirstBlockClass;
            SecondBlockId    = ASecondBlockId;
            SecondBlockClass = ASecondBlockClass;
            Count = 1;
        }
    }
}
