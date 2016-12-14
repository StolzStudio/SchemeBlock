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
        string table_name;
        string field_name;

        public CReferenseTableInfo(string ATableName, string AFieldName)
        {
            TableName = ATableName;
            FieldName = AFieldName;
        }

        public string TableName
        {
            get { return table_name; }
            set { table_name = value; }
        }

        public string FieldName
        {
            get { return field_name; }
            set { field_name = value; }
        }
    }

    public class CField
    {
        /* Fields  */

        private string FName;
        private string FCaption;
        private int FWidth;
        private int FTableTag;
        private FieldTypes FType;
        private CReferenseTableInfo FReference;

        /* Methods */

        public CField(string AName, string ACaption)
        {
            Name = AName;
            Caption = ACaption;
        }

        /* Properties */

        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        public string Caption
        {
            get { return FCaption; }
            set { FCaption = value; }
        }
        
        public int Width
        {
            get { return FWidth; }
            set { FWidth = value; }
        }

        public int TableTag
        {
            get { return FTableTag; }
            set { FTableTag = value; }
        }

        public FieldTypes Type
        {
            get { return FType; }
            set { FType = value; }
        }

        public CReferenseTableInfo Reference
        {
            get { return FReference; }
            set { FReference = value; }
        }
    }

    public class CTable
    {
        /* Fields */

        private string FName;
        private string FCaption;
        private bool FIsRefFields = false;

        public List<CField> Fields = new List<CField>();
        public List<string> FieldsList = new List<string>();
        public HashSet<string> InputParameters = new HashSet<string>();
        public HashSet<string> OutputParameters = new HashSet<string>();


        public CTable () { }

        public CTable (string ATableName, string ACaption, List<string> AFieldsList)
        {
            Name = ATableName;
            Caption = ACaption;
            FieldsList = AFieldsList;
            FillDataTable();
        }

        /* Methods*/

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

        /* Properties */

        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        public string Caption
        {
            get { return FCaption; }
            set { FCaption = value; }
        }

        public bool isReferensed
        {
            get { return FIsRefFields; }
            set { FIsRefFields = value; }
        }
    }

    public class CMeta
    {
        /* Fields */

        public List<CTable> Tables = new List<CTable>();
        public List<string> TablesList = new List<string>();
        public DBConnection Database = new DBConnection();
        public static Dictionary<string, string> DictionaryName = new Dictionary<string, string>();
        private string RIdentificator = "id";
        private string DictSeparator = "%";
        private string InputIdentificator = "input";
        private string OutputIdentificator = "output";

        /* Methods */

        public CMeta(string ADataBasePath)
        {
            if (Database.Connect(ADataBasePath) == 1)
            {
                FillDictionaryNames("../Databases/dictionary.txt");
                TablesList = Database.GetListTables();
                foreach(string TableName in TablesList)
                {
                    CreateTable(TableName, Database.GetListTableRows(TableName));
                }
                CheckReferensesInTables();
                CheckInputOutputParameters();
            }
        }

        public void CreateTable(string ATableName, List<string> ANameFields)
        {
            Tables.Add(new CTable(ATableName, DictionaryName[ATableName], ANameFields));
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
            CTable FirstTable = GetTableOfName(AFirstTable);
            CTable SecondTable = GetTableOfName(ASecondTable);        
            return FirstTable.isPossibleLinkWithTable(SecondTable);
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

        public string GetValueOfParameter(string ATableName, string AFieldId, string AParameter)
        {
            return Database.GetValueOfParameter(ATableName, AFieldId, AParameter);
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