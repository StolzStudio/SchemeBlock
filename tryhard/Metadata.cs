using System;
using System.Collections.Generic;
//using System.Data.SQLite;

namespace tryhard
{
    public enum FieldTypes { };

    class CField
    {
        /* Fields  */

        private string FName;
        private string FCaption;
        private int FWidth;
        private int FTableTag;
        private FieldTypes FType;
        private CField FReference = null;

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

    class CTable
    {
        /* Fields */

        private string FName;
        private string FCaption;
        private bool BIsRefFields = false;

        public List<CField> Fields;

        public CTable (string ATableName)
        {
            Name = ATableName;
            Caption = ATableName;
            FillDataTable(ATableName);
        }

        /* Methods*/

        private void FillDataTable(string ATableName)
        {

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

    class CMeta
    {
        /* Fields */

        public List<CTable> Tables = null;

        /* Methods */

        public void CreateTable(string ATableName)
        {
            Tables.Add(new CTable(ATableName));
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
    }
}   