using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace tryhard
{
    public enum FieldTypes { };

    public class CField
    {
        /* Fields  */

        private string FName;
        private string FCaption;
        private int FWidth;
        private int FTableTag;
        private FieldTypes FType;
        private CField FReference = null;

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

        public CField Reference
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
        private bool BIsRefFields = false;

        public List<CField> Fields = new List<CField>();
        public List<string> NameFields = new List<string>();

        public CTable (string ATableName, List<string> ANameFields)
        {
            Name = ATableName;
            Caption = ATableName;
            NameFields = ANameFields;
            FillDataTable();
        }

        /* Methods*/

        private void FillDataTable()
        {
            foreach (string field_name in NameFields)
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

        private bool isRefFields
        {
            get { return isRefFields; }
            set { isRefFields = value; }
        }
    }

    public class CMeta
    {
        /* Fields */

        public List<CTable> Tables = new List<CTable>();
        public List<string> TablesList = new List<string>();
        public DBConnection database = new DBConnection();

        /* Methods */

        public CMeta(string ADataBasePath)
        {
            if (database.Connect(ADataBasePath) == 1)
            {
                TablesList = database.GetListTables();
                foreach(string table_name in TablesList)
                {
                    CreateTable(table_name, database.GetListTableFields(table_name));
                }
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
            return;
        }

        public void DeleteTable(int ATableIndex)
        {
            Tables.RemoveAt(ATableIndex);
            return;
        }

        public List<string> GetListRecords(string ATableName, string AFieldName)
        {
            return database.GetListRecords(ATableName, AFieldName);
        }

        public List<string> GetListFieldOfTableName(string ATableName)
        {
            for (int i = 0; i < Tables.Count; i++)
            {
                if (Tables[i].Name == ATableName)
                {
                    return Tables[i].NameFields;
                }
            }
            return new List<string>();
        }
    }
}   