using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace tryhard
{
    class CalcBlock
    {
        public int FFirstIndex = 0;
        public int FSecondIndex = 0;
        public List<string> FFirstDBParametrs = new List<string>();
        public List<string> FSecondDBParametrs = new List<string>();

        public CalcBlock(int AFirstIndex, List<string> AFirstDBParametrs,
                         int ASecondIndex, List<string> ASecondDBParametrs)
        {

        }
        
    }
}