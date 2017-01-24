using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tryhard
{
    public enum FieldTypes { };

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
        public string Caption { get; set; }
        public int Width { get; set; }
        public int TableTag { get; set; }
        public FieldTypes Type { get; set; }
        public CReferenseTableInfo Reference { get; set; }

        /* Methods */

        public CField(string AName, string ACaption)
        {
            Name = AName;
            Caption = ACaption;
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
                Fields.Add(new CField(field_name, CMeta.DictionaryName[field_name]));
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

        /* Fields */

        public List<CTable> Tables = new List<CTable>();
        public List<string> TablesList = new List<string>();
        public static Dictionary<string, string> DictionaryName = new Dictionary<string, string>();
        public Dictionary<string, Dictionary<string, bool>> EquipmentMatchingTable = new Dictionary<string, Dictionary<string, bool>>();

        /* Methods */

        public CMeta(string ADataBasePath)
        {

        }

        public void CreateTable(string ATableName, List<string> ANameFields)
        {

        }

        public void DeleteTable(string ATableName)
        {
            for (int i = 0; i < Tables.Count; i++)
            {
                if (ATableName == Tables[i].Name)
                {
                    Tables.RemoveAt(i);
                    return;
                }
            }
        }

        public void DeleteTable(int ATableIndex)
        {
            Tables.RemoveAt(ATableIndex);
        }
    }
}   