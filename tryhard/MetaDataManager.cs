using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tryhard
{
    public class MetaDataManager
    {
        public Dictionary<string, List<BaseObject>> Objects;
        public Dictionary<string, List<string>> ObjectsInfo;
        public Dictionary<string, Type> ClassesTypes = new Dictionary<string, Type>() { { "Ukppv",  typeof(Ukppv) }, { "Bolt", typeof(Bolt) },
                                                                                        { "Pump",   typeof(Pump)  }, { "Pipe", typeof(Pipe) },
                                                                                        { "Ukpg",   typeof(Ukpg)  }, { "Nnpv", typeof(Nnpv) },
                                                                                        { "Rpv",    typeof(Rpv)   }, { "Rtn",  typeof(Rtn)  },
                                                                                        { "Upn",    typeof(Upn)   }, { "Dks",  typeof(Dks)  },
                                                                                        { "Dk",     typeof(Dk)    }, { "Rr",   typeof(Rr)   },
                                                                                                                     { "Fu",   typeof(Fu)   }};
        public MetaDataManager(string APath)
        {

        }
    }
}   