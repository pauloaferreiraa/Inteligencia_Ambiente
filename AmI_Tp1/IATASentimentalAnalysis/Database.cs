﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace IATASentimentalAnalysis
{
    public class Database
    {
        //OLAAAAA
        private MySqlConnection connection;
        private string database;
        private MySqlCommand command;
        public Database(string server, string database, string uid, string password)
        {
            this.database = database;
            connection = new MySqlConnection("SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password);
        }

        
        public MySqlDataReader getResultsDB(string query)
        {
            MySqlDataReader reader = null;
            try
            {
                command = new MySqlCommand(query,connection);
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
            }
            return reader;
        }

        public int getTableId(string table)
        {
            string column_name;
            if (table.Equals("BackspaceCaracter") || table.Equals("BackspacePalavra"))
            {
                column_name = "idBackspace";
            }
            else
            {
                column_name = "id" + table;
            }
             
            string query = "select " + column_name + " from " + table + " order by " + column_name + " desc limit 1";
            int id = 0;
            MySqlDataReader reader = null;
            try
            {
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return id;
        }

        public bool connect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Close connection
        public bool close()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
