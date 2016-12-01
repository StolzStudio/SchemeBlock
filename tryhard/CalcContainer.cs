using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    class CalcContainer
    {
        public int    Id;
        public string Class;
        public int    IdFather;
        public string ClassFather;

        public CalcContainer(int AId, string AClass, int AIdFather, string AClassFather)
        {
            Id          = AId;
            Class       = AClass;
            IdFather    = AIdFather;
            ClassFather = AClassFather;
        }
    }
}
