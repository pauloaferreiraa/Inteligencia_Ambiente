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

        public void insert(string query)
        {
            try
            {
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
