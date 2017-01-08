using System;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace tryhard
{
    public class DBConnection
    {
        /* Field */

        private List<string> UnvisibleTables = new List<string>()
        {
            "equipment_matching", "oil_quality", "tables_of_equipments", "tables_of_modules"
        };
        private SQLiteConnection connection = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader reader = null;

        /* Methods */

        public DBConnection()
        {

        }

        ~DBConnection ()
        {
            connection.Dispose();
        }

        public int Connect(string ADataBasePath)
        {
            string args = "Data Source=" + ADataBasePath + "; Version=3;";
            connection = new SQLiteConnection(args);
            try
            {
                connection.Open();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            return 1;
        }

        public void Disconnect()
        {
            connection.Close();
        }

        private void ReaderLoad(string ACommandText)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = ACommandText;
            reader = cmd.ExecuteReader();
        }

        public List<string> GetListTables ()
        {
            ReaderLoad("SELECT name FROM sqlite_master WHERE type = 'table';");
            List<string> list_tables = new List<string>();
            while (reader.Read())
            {
                string table_name = reader["name"].ToString();
                if (!UnvisibleTables.Contains(table_name))
                    list_tables.Add(table_name);
            }
            return list_tables;
        }

        public Dictionary<string, Dictionary<string, bool>> GetMatchingDict(string ATableName)
        {
            List<string> cols_name = GetListTableRows(ATableName);
            if (cols_name.Contains("name")) cols_name.Remove("name");
            ReaderLoad("SELECT * FROM " + ATableName + ";");
            Dictionary<string, Dictionary<string, bool>> result_dict = new Dictionary<string, Dictionary<string, bool>>();
            while (reader.Read())
            {
                Dictionary<string, bool> temp_dict = new Dictionary<string, bool>();
                for (int i = 0; i < cols_name.Count; i++)
                    temp_dict.Add(cols_name[i], (bool)reader[cols_name[i]]);
                result_dict.Add(reader["name"].ToString(), temp_dict);
            }
            return result_dict;
        }

        public List<string> GetListRecordsWithId(string ATableName, string AFieldName)
        {
            ReaderLoad("SELECT id, " + AFieldName + " FROM " + ATableName + ";");
            List<string> list_records = new List<string>();
            while (reader.Read())
            {
                list_records.Add(reader["id"].ToString());
                list_records.Add(reader[AFieldName].ToString());
            }
            return list_records;
        }

        public List<string> GetListRecordsId(string ATableName)
        {
            ReaderLoad("SELECT id FROM " + ATableName + ";");
            List<string> list_id= new List<string>();
            while (reader.Read())
            {
                list_id.Add(reader["id"].ToString());
            }
            return list_id;
        }

        public List<string> GetListTableRows(string ATableName)
        {
            ReaderLoad("pragma table_info(" + ATableName + ");");
            List<string> list_rows = new List<string>();
            while (reader.Read())
            {
                list_rows.Add(reader["name"].ToString());
            }
            return list_rows;
        }

        public List<string> GetFieldData(string ATableName, string AFieldId)
        {
            ReaderLoad("SELECT * FROM " + ATableName + " WHERE id = " + AFieldId);
            List<string> list_data = new List<string>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list_data.Add(reader[i].ToString());
                }
            }
            return list_data;
        }

        public string GetStringValueOfParameter(string ATableName, string AFieldId, string AParameter)
        {
            ReaderLoad("SELECT " + AParameter + " FROM " + ATableName + " WHERE id = " + AFieldId);
            string Value = null;
            while (reader.Read())
            {
                Value = reader[AParameter].ToString();
            }
            return Value;
        }

        public int GetIntValueOfParameter(string ATableName, string AFieldId, string AParameter)
        {
            ReaderLoad("SELECT " + AParameter + " FROM " + ATableName + " WHERE id = " + AFieldId);
            int Value = 0;
            while (reader.Read())
            {   
                Value = Convert.ToInt32(reader[AParameter]);
            }
            return Value;
        }
    }
}

