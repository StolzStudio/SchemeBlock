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

        private string ObjFilesDir = "../Databases/";
        private string ObjFileFormat = ".json";
        public Dictionary<string, List<BaseObject>> Objects;
        public Dictionary<string, List<MetaObjectInfo>> ObjectsInfo;
        public void Initialize(string APath)
        {
            Objects = new Dictionary<string, List<BaseObject>>();
            ObjectsInfo = JsonConvert.DeserializeObject<Dictionary<string, List<MetaObjectInfo>>>(GetJson(APath));
            foreach (string Key in ObjectsInfo.Keys)
            {
                foreach (MetaObjectInfo ObjInfo in ObjectsInfo[Key])
                {
                    Objects.Add(ObjInfo.Name, DeserializeMetaObjects(ObjInfo.Name));
                }
            }
        }

        public List<string> ObjectTypes
        {
            get
            {
                List<string> result = new List<string>();
                foreach (string Key in ObjectsInfo.Keys.Where(k => k != "InfoClasses"))
                    result.Add(Key);
                return result;
            }
        }

        public List<string> GetObjectTypesOfObjectCategory(string AObjectType)
        {
            List<string> result = new List<string>();
            foreach (MetaObjectInfo obj in ObjectsInfo[AObjectType])
                result.Add(obj.Name);
            return result;
        }

        private void PrintAllProperties(object AObject)
        {
            foreach (var Property in AObject.GetType().GetProperties())
            {
                Console.WriteLine(Property.Name + " " + Property.GetValue(AObject));
            }
        }

        private List<BaseObject> DeserializeMetaObjects(string AObjectName)
        {
            Dictionary<string, System.Type> t = new Dictionary<string, System.Type>() { { "bolt", typeof(Bolt) } };

            List<BaseObject> result = new List<BaseObject>();
            string ObjectPath = ObjFilesDir + AObjectName+ ObjFileFormat;
            string json = GetJson(ObjectPath);
            System.Type st = t["bolt"];
            switch (AObjectName)
            {
                case "field_parameters": result.AddRange(JsonConvert.DeserializeObject<List<FieldParameters>>(json)); break;
                case "oil_quality": result.AddRange(JsonConvert.DeserializeObject<List<OilQuality>>(json)); break;
                case "ukppv": result.AddRange(JsonConvert.DeserializeObject<List<Ukppv>>(json)); break;
                case "bolt": result.AddRange(JsonConvert.DeserializeObject<List<Bolt>>(json)); break;
                case "pump": result.AddRange(JsonConvert.DeserializeObject<List<Pump>>(json)); break;
                case "pipe": result.AddRange(JsonConvert.DeserializeObject<List<Pipe>>(json)); break;
                case "ukpg": result.AddRange(JsonConvert.DeserializeObject<List<Ukpg>>(json)); break;
                case "nnpv": result.AddRange(JsonConvert.DeserializeObject<List<Nnpv>>(json)); break;
                case "rpv": result.AddRange(JsonConvert.DeserializeObject<List<Rpv>>(json)); break;
                case "rtn": result.AddRange(JsonConvert.DeserializeObject<List<Rtn>>(json)); break;
                case "upn": result.AddRange(JsonConvert.DeserializeObject<List<Upn>>(json)); break;
                case "dks": result.AddRange(JsonConvert.DeserializeObject<List<Dks>>(json)); break;
                case "dk": result.AddRange(JsonConvert.DeserializeObject<List<Dk>>(json)); break;
                case "rr": result.AddRange(JsonConvert.DeserializeObject<List<Rr>>(json)); break;
                case "fu": result.AddRange(JsonConvert.DeserializeObject<List<Fu>>(json)); break;
            }
            return result;
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