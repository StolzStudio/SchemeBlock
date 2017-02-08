using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class CalculationDownStructure
    {
        private static CalculationDownStructure instance;
        private CalculationDownStructure() { }

        public static CalculationDownStructure Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CalculationDownStructure();
                }
                return instance;
            }
        }        

    }
}
