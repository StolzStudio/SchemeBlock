using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

        public DBConnection Database = new DBConnection();
        public List<CTable> Tables = new List<CTable>();
        public List<string> TablesList = new List<string>();
        public static Dictionary<string, string> DictionaryName = new Dictionary<string, string>();
        public Dictionary<string, Dictionary<string, bool>> EquipmentMatchingTable = new Dictionary<string, Dictionary<string, bool>>();

        /* Consts */

        private const string DictionaryNamesPath = "../Databases/dictionary.txt";
        private const string RIdentificator = "id";
        private const string DictSeparator = "%";
        private const string InputIdentificator = "input";
        private const string OutputIdentificator = "output";
        private const string EquipmentMatchingTableName = "equipment_matching";
        public List<string> ObjectTablesList = new List<string>() { "pipe", "pump", "bolt" };
        public List<string> EquipmentTablesList = new List<string>() { "dk", "dks", "field_parameters", "fu",
                                                                       "nnpv", "rpv", "rr", "rtn", "ukpg",
                                                                       "ukppv", "upn"};

        /* Methods */

        public CMeta(string ADataBasePath)
        {
            if (Database.Connect(ADataBasePath) == 1)
            {
                FillDictionaryNames(DictionaryNamesPath);
                TablesList = Database.GetListTables();
                foreach(string TableName in TablesList)
                    CreateTable(TableName, Database.GetListTableRows(TableName));
                EquipmentMatchingTable = Database.GetMatchingDict(EquipmentMatchingTableName);
                CheckReferensesInTables();
                CheckInputOutputParameters();
            }
        }

        public void CreateTable(string ATableName, List<string> ANameFields)
        {
            List<string> IdList = new List<string>();
            IdList = Database.GetListRecordsId(ATableName);
            Tables.Add(new CTable(ATableName, DictionaryName[ATableName], IdList, ANameFields));
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

        public List<string> GetListRecordsWithId(string ATableName, string AFieldName)
        {
            return Database.GetListRecordsWithId(ATableName, AFieldName);
        }

        public List<string> GetFieldData(string ATableName, string AFieldId)
        {
            return Database.GetFieldData(ATableName, AFieldId);
        }

        public List<string> GetListFieldOfTableName(string ATableName)
        {
            foreach (CTable Table in Tables)
            {
                if (Table.Name == ATableName)
                {
                    return Table.FieldsList;
                }
            }
            return new List<string>();
        }

        public void CheckReferensesInTables()
        {
            foreach (CTable Table in Tables)
            {
                foreach (CField Field in Table.Fields)
                {
                    string name_referense_table = isReferenseField(Field.Name);
                    if (name_referense_table != null)
                    {
                        Field.Reference = new CReferenseTableInfo(name_referense_table, RIdentificator);
                        Table.isReferensed = true;
                    }
                }
            }
        }

        public void CheckInputOutputParameters()
        {
            foreach (CTable Table in Tables)
            {
                foreach (CField Field in Table.Fields)
                {
                    if (Field.Name.Contains(InputIdentificator))
                    {
                        Table.InputParameters.Add(Field.Name.Substring(0, Field.Name.Length - InputIdentificator.Length - 1));
                    }
                    else if (Field.Name.Contains(OutputIdentificator))
                    {
                        Table.OutputParameters.Add(Field.Name.Substring(0, Field.Name.Length - OutputIdentificator.Length - 1));
                    }
                }
            }
        }

        public bool isPossibleLink(string AFirstTable, string ASecondTable)
        {   
            return EquipmentMatchingTable[AFirstTable][ASecondTable];
        }

        public string GetCommonParameterForLink(string AFirstTable, string ASecondTable)
        {
            CTable FirstTable = GetTableOfName(AFirstTable);
            CTable SecondTable = GetTableOfName(ASecondTable);
            return FirstTable.GetCommonParameterForLink(SecondTable);
        }

        public CTable GetTableOfName(string ATableName)
        {
            foreach (CTable Table in Tables)
            {
                if (Table.Name == ATableName)
                {
                    return Table;
                }
            }
            return null;
        }

        public string isReferenseField(string AFieldName)
        {
            int field_len = AFieldName.Length;
            if (field_len <= 2)
            {
                return null;
            }
            if (AFieldName.Substring(field_len - 2) == RIdentificator)
            {
                return AFieldName.Substring(0, field_len - 3);
            }
            return null;
        }

        public string GetStringValueOfParameter(string ATableName, string AFieldId, string AParameter)
        {
            return Database.GetStringValueOfParameter(ATableName, AFieldId, AParameter);
        }

        public int GetIntValueOfParameter(string ATableName, string AFieldId, string AParameter)
        {
            return Database.GetIntValueOfParameter(ATableName, AFieldId, AParameter);
        }

        public void FillDictionaryNames(string AFileName)
        {
            string str = null;
            using (StreamReader input = new StreamReader(@AFileName))
            {
                while ((str = input.ReadLine()) != null)
                {
                    int i = str.IndexOf(DictSeparator);
                    string name1 = str.Substring(0, i);
                    string name2 = str.Substring(i + 1);
                    DictionaryName.Add(name1, name2);
                    DictionaryName.Add(name2, name1);
                }
            }
        }
    }
}   