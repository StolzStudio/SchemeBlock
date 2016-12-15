using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace tryhard
{
    public class CalcBlock
    {
        public List<int> InputLinks = new List<int>();
        public List<int> OutputLinks = new List<int>();

        public int Index = 0;
        public string BlockClass;
        public string BlockId;

        public bool isDone = false;

        public int Count = 1;


        public CalcBlock(int AIndex, string ABlockClass, string ABlockId)
        {
            Index = AIndex;
            BlockClass = ABlockClass;
            BlockId= ABlockId;
        }
        
    }
}