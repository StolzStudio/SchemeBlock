using System;
using System.Collections.Generic;
using System.Data.SQLite;

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

        public CField(string AName)
        {
            Name = AName;
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

        public CTable (string ATableName, List<string> AFieldsList)
        {
            Name = ATableName;
            Caption = ATableName;
            FieldsList = AFieldsList;
            FillDataTable();
        }

        /* Methods*/

        private void FillDataTable()
        {
            foreach (string field_name in FieldsList)
            {
                Fields.Add(new CField(field_name));
            }   
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
        public DBConnection database = new DBConnection();
        private string RIdentificator = "id";

        /* Methods */

        public CMeta(string ADataBasePath)
        {
            if (database.Connect(ADataBasePath) == 1)
            {
                TablesList = database.GetListTables();
                foreach(string table_name in TablesList)
                {
                    CreateTable(table_name, database.GetListTableRows(table_name));
                }
                CheckReferensesInTables();
            }
        }

        public void CreateTable(string ATableName, List<string> ANameFields)
        {
            Tables.Add(new CTable(ATableName, ANameFields));
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
            return database.GetListRecordsWithId(ATableName, AFieldName);
        }

        public List<string> GetFieldData(string ATableName, string AFieldId)
        {
            return database.GetFieldData(ATableName, AFieldId);
        }

        public List<string> GetListFieldOfTableName(string ATableName)
        {
            for (int i = 0; i < Tables.Count; i++)
            {
                if (Tables[i].Name == ATableName)
                {
                    return Tables[i].FieldsList;
                }
            }
            return new List<string>();
        }

        public void CheckReferensesInTables()
        {
            for (int i = 0; i < Tables.Count; i++)
            {
                for (int j = 0; j < Tables[i].Fields.Count; j++)
                {
                    string name_referense_table = isReferenseField(Tables[i].Fields[j].Name);
                    if (name_referense_table != null)
                    {
                        Tables[i].Fields[j].Reference = 
                            new CReferenseTableInfo(name_referense_table, RIdentificator);
                        Tables[i].isReferensed = true;
                    }
                }
            }
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
    }
}   