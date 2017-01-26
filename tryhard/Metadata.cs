using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace tryhard
{
    public class CBaseObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


    public class CReferenseTableInfo
    {
        public CReferenseTableInfo(string ATableName, string AFieldName)
        {
            TableName = ATableName;
            FieldName = AFieldName;
        }

        public string TableName { get; set; }
        public string FieldName { get; set; }
    }

    public class CField
    {
        /* Properties */

        public string Name { get; set; }
        public int Width { get; set; }
        public int TableTag { get; set; }
        public CReferenseTableInfo Reference { get; set; }

        /* Methods */

        public CField(string AName, string ACaption)
        {
            Name = AName;
        }
    }

    public class CTable
    {
        /* Properties */

        public string Name { get; set; }
        public string Caption { get; set; }
        public bool isReferensed { get; set; }

        /* Fields */

        public List<CField> Fields = new List<CField>();
        public List<string> FieldsList = new List<string>();
        public List<string> IdList = new List<string>();
        public HashSet<string> InputParameters = new HashSet<string>();
        public HashSet<string> OutputParameters = new HashSet<string>();

        /* Methods */

        public CTable () { }

        public CTable (string ATableName, string ACaption, List<string> AIdList, List<string> AFieldsList)
        {
            Name = ATableName;
            Caption = ACaption;
            IdList = AIdList;
            FieldsList = AFieldsList;
            FillDataTable();
        }

        private void FillDataTable()
        {
            foreach (string field_name in FieldsList)
            {
                //Fields.Add(new CField(field_name, CMeta.DictionaryName[field_name]));
            }   
        }

        public bool isPossibleLinkWithTable(CTable ATable)
        {
            foreach (string Parameter in this.OutputParameters)
            {
                if (ATable.InputParameters.Contains(Parameter))
                {
                    return true;
                }
            }
            return false;
        }

        public string GetCommonParameterForLink(CTable ATable)
        {
            string parameter = null;
            foreach (string Parameter in this.OutputParameters)
            {
                if (ATable.InputParameters.Contains(Parameter))
                {
                    parameter = Parameter;
                    break;
                }
            }
            return parameter;
        }
    }

    public class CMeta
    {
        public CMeta(string APath)
        {
            
        }
    }
}   