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

        public List<string> GetListTables ()
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table';";
            reader = cmd.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader["name"].ToString());
            }
            return list;
        }

        public List<string> GetListRecords(string ATableName, string AFieldName)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT " + AFieldName + " FROM " + ATableName + ";";
            reader = cmd.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader["name"].ToString());
            }
            return list;
        }

        public List<string> GetListTableRows(string ATableName)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "pragma table_info(" + ATableName + ");";
            reader = cmd.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader["name"].ToString());
            }
            return list;
        }
    }
}

