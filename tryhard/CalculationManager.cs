using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace tryhard
{
    public class CalcBlock
    {
        /* Fields */

        public List<int> InputLinks  = new List<int>();
        public List<int> OutputLinks = new List<int>();

        /* Properties */

        public string BlockClass { get; set; }
        public string BlockId    { get; set; }
        public bool   isDone     { get; set; }
        public int    Index      { get; set; }
        public int    Count      { get; set; }


        public CalcBlock(int AIndex, string ABlockClass, string ABlockId)
        {
            Index = AIndex;
            BlockClass = ABlockClass;
            BlockId= ABlockId;
            Count = 1;
        }
        
    }
}