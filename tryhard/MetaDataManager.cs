using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Drawing;

namespace tryhard
{
    public static class UserState
    {
        public static int currentProjectId { get; set; } = -1;
        public static bool isSelectedProject { get; set; } = false;
        public static bool isExit { get; set; } = false;

        public static void DefaultState()
        {
            currentProjectId = -1;
            isSelectedProject = false;
            isExit = false;
        }
    }

    public class IdNameInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IdNameInfo(int AId, string AName)
        {
            Id = AId;
            Name = AName;
        }
    }

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

        private string ObjFilesDir;
        private string ObjFileFormat = ".json";
        public Dictionary<string, string> Dictionary;
        public Dictionary<string, List<BaseObject>> Objects;
        public Dictionary<string, List<MetaObjectInfo>> ObjectsInfo;

        public void Initialize(string ADir)
        {
            ObjFilesDir = ADir;
            Dictionary = new Dictionary<string, string>();
            InitializeDictionary(ADir + "dictionary.json");
            Objects = new Dictionary<string, List<BaseObject>>();
            ObjectsInfo = JsonConvert.DeserializeObject<Dictionary<string, List<MetaObjectInfo>>>(GetJson(ADir + "objectsinfo.json"));
            foreach (string Category in ObjectsInfo.Keys)
            {
                foreach (MetaObjectInfo ObjectType in ObjectsInfo[Category])
                {
                    Objects.Add(ObjectType.Name, DeserializeMetaObjects(ObjectType.Name));
                }
            }
        }

        private void InitializeDictionary(string APath)
        {
            string json = MetaDataManager.Instance.GetJson(APath);
            Dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public IEnumerable<string> ObjectCategories
        {
            get { return ObjectsInfo.Keys.Where(k => k != "InfoClasses"); }
        }

        public string GetCateroryNameByType(string AType)
        {
            foreach (string ACategory in ObjectsInfo.Keys)
            {
                foreach (MetaObjectInfo AObjectInfo in ObjectsInfo[ACategory].Where(obj => obj.Name == AType))
                {
                    return ACategory;
                }
            }
            return "Equipment";
        }

        public List<string> GetObjectTypesOfObjectCategory(string AObjectCategory)
        {
            List<string> result = new List<string>();
            foreach (MetaObjectInfo obj in ObjectsInfo[AObjectCategory])
                result.Add(obj.Name);
            return result;
        }

        public IEnumerable<IdNameInfo> GetObjectsInfoByType(string AObjectsType)
        {
            return Objects[AObjectsType].Select(k => new IdNameInfo(k.Id, k.Name));
        }

        public IEnumerable<IdNameInfo> GetObjectsInfoByTypeAndEstimatedFieldId(string AObjectsType, int _estimatedFieldId)
        {
            IEnumerable<Complex> objects = (Objects[AObjectsType].Select(k => k as Complex)).Where(k => k.EstimatedFieldId == _estimatedFieldId);
            return objects.Select(k => new IdNameInfo(k.Id, k.Name));
        }

        public bool isPossibleLink(string ACategory, string AFirstObjectType, string ASecondObjectType)
        {
            foreach (MetaObjectInfo FirstObjectInfo in this.ObjectsInfo[ACategory].Where(obj=>obj.Name == AFirstObjectType))
                return FirstObjectInfo.PossibleLink.Contains(ASecondObjectType);
            return false;
        }

        private void PrintAllProperties(object AObject)
        {
            foreach (var Property in AObject.GetType().GetProperties())
            {
                Console.WriteLine(Property.Name + " " + Property.GetValue(AObject));
            }
        }

        public IEnumerable<string> GetObjectTypesByCategory(string ACategory)
        {
            return ObjectsInfo[ACategory].Select(k => k.Name);
        }

        public List<int> GetIdCortageByType(string AType)
        {
            return Objects[AType].Select(obj => obj.Id).ToList();
        }

        public void FillObjectStructure(string AType, int AId, List<Link> ALinks, Dictionary<int, Block> ABlocks)
        {
            ObjectsStructure structure = new ObjectsStructure();
            List<StructuralObject> objects = new List<StructuralObject>();
            List<LinkStructuralObject> links = new List<LinkStructuralObject>();
            foreach (int Key in ABlocks.Keys)
            {
                StructuralObject obj = new StructuralObject();
                obj.Id = ABlocks[Key].Id;
                obj.Index = ABlocks[Key].Index;
                obj.Type = ABlocks[Key].ClassText;
                obj.Coordinates = ABlocks[Key].Location;
                objects.Add(obj);
            }
            structure.Objects = objects;
            foreach (Link link in ALinks)
            {
                LinkStructuralObject _link = new LinkStructuralObject();
                _link.FirstBlockIndex = link.FirstBlockIndex;
                _link.SecondBlockIndex = link.SecondBlockIndex;
                _link.LinkParameter = link.LinkParameter;
                links.Add(_link);
            }
            structure.Links = links;
            foreach (BaseObject Object in Objects[AType].Where(obj => obj.Id == AId))
                Object.GetType().GetProperty("Structure").SetValue(Object, structure);
        }

        public List<string> GetLinkableParameters(string AFirstType, string ASecondType)
        {
            List<string> parametersFirstType = GetParametersByParamenterType(GetCateroryNameByType(AFirstType), AFirstType, "Output");
            List<string> parametersSecondType = GetParametersByParamenterType(GetCateroryNameByType(ASecondType), ASecondType, "Input");
            return parametersSecondType;
        }

        public List<string> GetParametersByParamenterType(string AObjectCategory, string AObjectType, string AParamenterType)
        {
            List<string> parameters = new List<string>();
            foreach (MetaObjectInfo ObjectInfo in ObjectsInfo[AObjectCategory].Where(obj => obj.Name == AObjectType))
                foreach (string Property in ObjectInfo.Properties)
                {
                    int matchPos = 0;
                    if ((matchPos = Property.IndexOf(AParamenterType)) != -1)
                        parameters.Add(Property.Substring(0, Property.Length - AParamenterType.Length));
                }
            return parameters;
        }

        public void FillDrawingObjectStructure(string AType, int AId, 
                                               ref List<Link> ALinks, ref Dictionary<int, Block> ABlocks, Point aPageOffset)
        {
            ObjectsStructure ObjectStructure = new ObjectsStructure();
            foreach (BaseObject Object in MetaDataManager.Instance.Objects[AType].Where(obj => obj.Id == AId))
                ObjectStructure = (ObjectsStructure)Object.GetType().GetProperty("Structure").GetValue(Object);
            foreach (LinkStructuralObject link in ObjectStructure.Links)
                ALinks.Add(new Link(link));
            foreach (StructuralObject structuralObject in ObjectStructure.Objects)
                ABlocks.Add(structuralObject.Index, new Block(structuralObject, aPageOffset));
        }

        public void SerializeMetaObjects()
        {
            foreach (string ObjectName in Objects.Keys)
            {
                string json = JsonConvert.SerializeObject(MetaDataManager.Instance.Objects[ObjectName], Formatting.Indented);
                StreamWriter sw = new StreamWriter(ObjFilesDir + ObjectName + ObjFileFormat);
                sw.Write(json);
                sw.Close();
            }
        }
        
        public BaseObject GetBaseObjectOfId(string AType, int AId)
        {
            object resultObject = new object();
            foreach (BaseObject Object in Objects[AType].Where(obj => obj.Id == AId))
                resultObject = Object;
            return (BaseObject)resultObject;
        }

        private List<BaseObject> DeserializeMetaObjects(string AObjectName)
        {
            Dictionary<string, System.Type> t = new Dictionary<string, System.Type>() { { "bolt", typeof(Bolt) } };

            List<BaseObject> result = new List<BaseObject>();
            string ObjectPath = ObjFilesDir + AObjectName+ ObjFileFormat;
            string json = GetJson(ObjectPath);
            switch (AObjectName)
            {
                case "integrated_complex": result.AddRange(JsonConvert.DeserializeObject<List<IntegratedComplex>>(json)); break;
                case "processing_complex": result.AddRange(JsonConvert.DeserializeObject<List<ProcessingComplex>>(json)); break;
                case "field_parameters": result.AddRange(JsonConvert.DeserializeObject<List<FieldParameters>>(json)); break;
                case "mining_complex": result.AddRange(JsonConvert.DeserializeObject<List<MiningComplex>>(json)); break;
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

        public List<string> GetListOfProjectNames()
        {
            return new List<string>();
        }

        public string GetJson(string APath)
        {
            StreamReader sr = new StreamReader(APath);
            string json = sr.ReadToEnd();
            sr.Close();
            return json;
        }
    }
}   