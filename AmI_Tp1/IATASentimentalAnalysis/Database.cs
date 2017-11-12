using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace IATASentimentalAnalysis
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

        

        public void insertEmocoes(string Positivo, string Negativo, 
            string Anger, string Antecipation, string Disgust, string Fear, string Joy, string Sadness, string Surprise,
            string Trust)
        {
            try
            {

                string query_insert = "INSERT INTO emocoes " +
                                      "(Positivo, Negativo, Anger, Antecipation, Disgust, Fear, Joy, Sadness, Surprise, Trust) " +
                                      "VALUES(" + Positivo + "," + Negativo + "," + Anger + "," + Antecipation +
                                      "," + Disgust + "," + Fear + "," + Joy + "," + Sadness + "," + Surprise + "," +
                                      Trust + ");";
                command = new MySqlCommand(query_insert, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //Console.WriteLine("ENTROU AQUI NA QUERY: " + query);
                Console.WriteLine(e.Message);
            }
        }

        public void updateData(string utilizador, string data)
        {
            int id = getTableId("Emocoes");

            string query_update = "update data set Emocoes_idEmocoes = " + id + " where Utilizador = '" + utilizador +
                                  "' && Data = str_to_date('" + data + "','%d/%m/%Y %H:%i:%s');";

            try
            {
                command = new MySqlCommand(query_update, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool checkUserFile(string utilizador, string date) //verifica se utilizador e ficheiro existem
        {
            string query = "select exists(select * from data where Utilizador = '" + utilizador +
                           "' && Data = str_to_date('" + date + "','%d/%m/%Y %H:%i:%s'));";
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

        public bool checkUser(string utilizador) //verifica se utilizador existe
        {
            string query = "select exists(select * from data where Utilizador = '" + utilizador +"');";
            Console.WriteLine(query);
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
            
            string query = "select " + column_name + " from " + table + " order by " + column_name + " desc limit 1;";
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
