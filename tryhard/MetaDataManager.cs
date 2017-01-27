using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace tryhard
{
    public class MetaDataManager
    {
        private static MetaDataManager instance;
        private MetaDataManager() { }
        public static MetaDataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MetaDataManager();
                }
                return instance;
            }
        }

        public Dictionary<string, List<BaseObject>> Objects;
        public Dictionary<string, List<string>> ObjectsInfo;
        public Dictionary<string, Type> ClassesTypes = new Dictionary<string, Type>()
                              { { "Ukppv",  typeof(Ukppv) }, { "Bolt", typeof(Bolt) },
                                { "Pump",   typeof(Pump)  }, { "Pipe", typeof(Pipe) },
                                { "Ukpg",   typeof(Ukpg)  }, { "Nnpv", typeof(Nnpv) },
                                { "Rpv",    typeof(Rpv)   }, { "Rtn",  typeof(Rtn)  },
                                { "Upn",    typeof(Upn)   }, { "Dks",  typeof(Dks)  },
                                { "Dk",     typeof(Dk)    }, { "Rr",   typeof(Rr)   },
                                                                { "Fu",   typeof(Fu)   }};
        public void Initialize(string APath)
        {
            Dictionary<string, List<MetaObjectInfo>> ObjectsInfo = 
                JsonConvert.DeserializeObject<Dictionary<string, List<MetaObjectInfo>>>(GetJson(APath));
            foreach (string Key in ObjectsInfo.Keys)
            {
                foreach (MetaObjectInfo ObjInfo in ObjectsInfo[Key])
                {
                    //получать листы кастованные к базе
                }
            }
        }

        private string GetJson(string APath)
        {
            StreamReader sr = new StreamReader(APath);
            string json = sr.ReadToEnd();
            sr.Close();
            return json;
        }
    }
}   