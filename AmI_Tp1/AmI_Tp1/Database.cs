using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AmI_Tp1
{
    public class Database
    {
        private MySqlConnection connection;
        private string database;
        private MySqlCommand command;
        public Database(string server, string database, string uid, string password)
        {
            this.database = database;
            connection = new MySqlConnection("SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password);
        }

        public void insertDB(string query)
        {
            try
            {
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Console.WriteLine("ENTROU AQUI NA QUERY: " + query);
                Console.WriteLine(e.Message);
            }
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

        public bool checkUser(string utilizador, string date) //verifica se utilizador ja inseriu algum ficheiro com nome data
        {
            string query = "select exists(select * from data where Utilizador = '"+ utilizador + 
                "' && Data = str_to_date('"+ date + "','%d/%m/%Y %H:%i:%s'));";
            int x = 0;
            MySqlDataReader reader = null;
            try
            {
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    x = Convert.ToInt32(reader.GetString(0));
                    
                }
                reader.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            if (x == 1) return true;
            else return false;
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
