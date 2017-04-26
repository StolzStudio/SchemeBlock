using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Drawing;

namespace tryhard
{
    public static class ProgramState
    {
        public static int  currentProjectId { get; set; } = -1;
        public static bool isSelectedProject { get; set; } = false;
        public static bool isExit { get; set; } = false;

        public static void DefaultState()
        {
            currentProjectId  = -1;
            isSelectedProject = false;
            isExit            = false;
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
        public Dictionary<int, Project>   Projects;
        public Dictionary<string, string> Dictionary;
        public Dictionary<string, List<BaseObject>>     Objects;
        public Dictionary<string, List<MetaObjectInfo>> ObjectsInfo;

        public void Initialize(string ADir)
        {
            ObjFilesDir = ADir;
            Dictionary = new Dictionary<string, string>();
            InitializeDictionary(ADir + "dictionary.json");
            DeserializeProjects(ADir + "projects.json");
            Objects = new Dictionary<string, List<BaseObject>>();
            ObjectsInfo = JsonConvert.DeserializeObject<Dictionary<string, List<MetaObjectInfo>>>(GetJson(ADir + "objectsinfo.json"));
            foreach (string Category in ObjectsInfo.Keys)
                foreach (MetaObjectInfo ObjectType in ObjectsInfo[Category])
                    Objects.Add(ObjectType.Name, DeserializeMetaObjects(ObjectType.Name));
        }

        private void InitializeDictionary(string path)
        {
            string json = MetaDataManager.Instance.GetJson(path);
            Dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public void DeserializeProjects(string path)
        {
            string json = MetaDataManager.Instance.GetJson(path);
            Projects = JsonConvert.DeserializeObject<Dictionary<int, Project>>(json);
        }

        public void PushProject(Project project)
        {
            if (Projects.Keys.Contains(project.Id))
                Projects[project.Id] = project;
            else
                Projects.Add(project.Id, project);
        }

        public void SerializeProjects()
        {
            string json = JsonConvert.SerializeObject(Projects, Formatting.Indented);
            StreamWriter sw = new StreamWriter(ObjFilesDir + "projects" + ObjFileFormat);
            sw.Write(json);
            sw.Close();
        }

        public int GetFreeProjectId()
        {
            int max = 0;
            foreach (int key in Projects.Keys)
                if (key > max) max = key;
            return max + 1;
        }


        public List<BaseObject> GetProjectsIdName()
        {
            return Projects.Keys.Select(key => new BaseObject(Projects[key].Id, Projects[key].Name)).ToList();
        }

        public IEnumerable<string> ObjectCategories
        {
            get { return ObjectsInfo.Keys.Where(k => k != "InfoClasses"); }
        }

        public string GetCateroryNameByType(string type)
        {
            foreach (string ACategory in ObjectsInfo.Keys)
            {
                foreach (MetaObjectInfo AObjectInfo in ObjectsInfo[ACategory].Where(obj => obj.Name == type))
                {
                    return ACategory;
                }
            }
            return "Equipment";
        }

        public List<string> GetObjectTypesOfObjectCategory(string objectCategory)
        {
            List<string> result = new List<string>();
            foreach (MetaObjectInfo obj in ObjectsInfo[objectCategory])
                result.Add(obj.Name);
            return result;
        }

        public IEnumerable<BaseObject> GetObjectsInfoByType(string objectsType)
        {
            return Objects[objectsType].Select(k => new BaseObject(k.Id, k.Name));
        }

        public BaseObject GetObject(string type, int id)
        {
            return Objects[type].Where(obj => obj.Id == id).ToList()[0];
        }

        public IEnumerable<BaseObject> GetObjectsInfoByTypeAndEstimatedFieldId(string objectsType, int estimatedFieldId)
        {
            IEnumerable<Complex> objects = (Objects[objectsType].Select(k => k as Complex)).Where(k => k.EstimatedFieldId == estimatedFieldId);
            return objects.Select(k => new BaseObject(k.Id, k.Name));
        }

        public bool isPossibleLink(string category, string firstObjectType, string secondObjectType)
        {
            foreach (MetaObjectInfo FirstObjectInfo in this.ObjectsInfo[category].Where(obj=>obj.Name == firstObjectType))
                return FirstObjectInfo.PossibleLink.Contains(secondObjectType);
            return false;
        }

        private void PrintAllProperties(object _object)
        {
            foreach (var Property in _object.GetType().GetProperties())
            {
                Console.WriteLine(Property.Name + " " + Property.GetValue(_object));
            }
        }

        public IEnumerable<string> GetObjectTypesByCategory(string category)
        {
            return ObjectsInfo[category].Select(k => k.Name);
        }

        public List<int> GetIdCortageByType(string type)
        {
            return Objects[type].Select(obj => obj.Id).ToList();
        }

        public void FillObjectStructure(List<Link> links, Dictionary<int, Block> blocks, ref ObjectsStructure objectStructure)
        {
            List<StructuralObject> objects = new List<StructuralObject>();
            List<LinkStructuralObject> _links = new List<LinkStructuralObject>();
            foreach (int Key in blocks.Keys)
            {
                StructuralObject obj = new StructuralObject();
                obj.Id = blocks[Key].Id;
                obj.Index = blocks[Key].Index;
                obj.Type = blocks[Key].ClassText;
                obj.Coordinates = blocks[Key].Location;
                objects.Add(obj);
            }
            objectStructure.Objects = objects;
            foreach (Link link in links)
            {
                LinkStructuralObject _link = new LinkStructuralObject();
                _link.FirstBlockIndex = link.FirstBlockIndex;
                _link.SecondBlockIndex = link.SecondBlockIndex;
                _link.LinkParameter = link.LinkParameter;
                _links.Add(_link);
            }
            objectStructure.Links = _links;
        }

        public void PushObjectStructure(string type, int id, ObjectsStructure objectStructure)
        {
            foreach (BaseObject Object in Objects[type].Where(obj => obj.Id == id))
                Object.GetType().GetProperty("Structure").SetValue(Object, objectStructure);
        }

        public List<string> GetLinkableParameters(string firstType, string secondType)
        {
            List<string> parametersFirstType = GetParametersByParamenterType(GetCateroryNameByType(firstType), firstType, "Output");
            List<string> parametersSecondType = GetParametersByParamenterType(GetCateroryNameByType(secondType), secondType, "Input");
            return parametersSecondType;
        }

        public List<string> GetParametersByParamenterType(string objectCategory, string objectType, string paramenterType)
        {
            List<string> parameters = new List<string>();
            foreach (MetaObjectInfo ObjectInfo in ObjectsInfo[objectCategory].Where(obj => obj.Name == objectType))
                foreach (string Property in ObjectInfo.Properties)
                {
                    int matchPos = 0;
                    if ((matchPos = Property.IndexOf(paramenterType)) != -1)
                        parameters.Add(Property.Substring(0, Property.Length - paramenterType.Length));
                }
            return parameters;
        }

        public void FillDrawingObjectStructure(string type, int id, ref List<Link> links, 
                                               ref Dictionary<int, Block> blocks, Point pageOffset)
        {
            ObjectsStructure ObjectStructure = new ObjectsStructure();
            foreach (BaseObject Object in MetaDataManager.Instance.Objects[type].Where(obj => obj.Id == id))
                ObjectStructure = (ObjectsStructure)Object.GetType().GetProperty("Structure").GetValue(Object);
            foreach (LinkStructuralObject link in ObjectStructure.Links)
                links.Add(new Link(link));
            foreach (StructuralObject structuralObject in ObjectStructure.Objects)
                blocks.Add(structuralObject.Index, new Block(structuralObject, pageOffset));
        }

        public void FillDrawingObjectProjectStructure(int id, ref List<Link> links, ref Dictionary<int, Block> blocks, Point pageOffset)
        {
            foreach (LinkStructuralObject link in Projects[id].Structure.Links)
                links.Add(new Link(link));
            foreach (StructuralObject structuralObject in Projects[id].Structure.Objects)
                blocks.Add(structuralObject.Index, new Block(structuralObject, pageOffset));
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
        
        public BaseObject GetBaseObjectOfId(string type, int id)
        {
            object resultObject = new object();
            foreach (BaseObject Object in Objects[type].Where(obj => obj.Id == id))
                resultObject = Object;
            return (BaseObject)resultObject;
        }

        private List<BaseObject> DeserializeMetaObjects(string objectName)
        {
            List<BaseObject> result = new List<BaseObject>();
            string ObjectPath = ObjFilesDir + objectName + ObjFileFormat;
            string json = GetJson(ObjectPath);
            switch (objectName)
            {
                case "integrated_complex": result.AddRange(JsonConvert.DeserializeObject<List<IntegratedComplex>>(json)); break;
                case "processing_complex": result.AddRange(JsonConvert.DeserializeObject<List<ProcessingComplex>>(json)); break;
                case "field_parameters": result.AddRange(JsonConvert.DeserializeObject<List<FieldParameters>>(json)); break;
                case "mining_complex": result.AddRange(JsonConvert.DeserializeObject<List<MiningComplex>>(json)); break;
                case "oil_quality": result.AddRange(JsonConvert.DeserializeObject<List<OilQuality>>(json)); break;
                case "ukppv": result.AddRange(JsonConvert.DeserializeObject<List<Ukppv>>(json)); break;
                case "pump": result.AddRange(JsonConvert.DeserializeObject<List<Pump>>(json)); break;
                case "pipe": result.AddRange(JsonConvert.DeserializeObject<List<Pipe>>(json)); break;
                case "ukpg": result.AddRange(JsonConvert.DeserializeObject<List<Ukpg>>(json)); break;
                case "nnpv": result.AddRange(JsonConvert.DeserializeObject<List<Nnpv>>(json)); break;
                case "sov": result.AddRange(JsonConvert.DeserializeObject<List<Sov>>(json)); break;
                case "ngs": result.AddRange(JsonConvert.DeserializeObject<List<Ngs>>(json)); break;
                case "upn": result.AddRange(JsonConvert.DeserializeObject<List<Upn>>(json)); break;
                case "dks": result.AddRange(JsonConvert.DeserializeObject<List<Dks>>(json)); break;
                case "dk": result.AddRange(JsonConvert.DeserializeObject<List<Dk>>(json)); break;
                case "bg": result.AddRange(JsonConvert.DeserializeObject<List<Bg>>(json)); break;
                case "bu": result.AddRange(JsonConvert.DeserializeObject<List<Bu>>(json)); break;
                case "fu": result.AddRange(JsonConvert.DeserializeObject<List<Fu>>(json)); break;
                case "jk": result.AddRange(JsonConvert.DeserializeObject<List<Jk>>(json)); break;
                case "ek": result.AddRange(JsonConvert.DeserializeObject<List<Ek>>(json)); break;
            }
            return result;
        }

        public string GiveTypeName(string objectType)
        {
            switch (objectType)
            {
                case "integrated_complex": return "IntegratedComplex"; break;
                case "processing_complex": return "ProcessingComplex"; break;
                case "field_parameters": return "FieldParameters"; break;
                case "mining_complex": return "MiningComplex"; break;
                case "oil_quality": return "OilQuality"; break;
                case "ukppv": return "Ukppv"; break;
                case "ukpg": return "Ukpg"; break;
                case "pump": return "Pump"; break;
                case "pipe": return "Pipe"; break;
                case "nnpv": return "Nnpv"; break;
                case "sov": return "Sov"; break;
                case "ngs": return "Ngs"; break;
                case "upn": return "Upn"; break;
                case "dks": return "Dks"; break;
                case "dk": return "Dk"; break;
                case "jk": return "Jk"; break;
                case "ek": return "Ek"; break;
                case "fu": return "Fu"; break;
            }
            return null;
        }

        public List<string> GetListOfProjectNames()
        {
            return new List<string>();
        }

        public string GetJson(string path)
        {
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            sr.Close();
            return json;
        }
    }
}   