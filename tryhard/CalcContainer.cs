using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class CalcContainer
    {
        public string Id;
        public string Class;
        public string IdFather;
        public string ClassFather;
        public int Count;

        public CalcContainer(string AId, string AClass, string AIdFather, string AClassFather)
        {
            Id          = AId;
            Class       = AClass;
            IdFather    = AIdFather;
            ClassFather = AClassFather;
            Count = 0;
        }
    }
}
