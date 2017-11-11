using System;
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

        

        public void insertEmocoes(string utilizador, string data, double Positivo, double Negativo, 
            double Anger, double Antecipation, double Disgust, double Fear, double Joy, double Sadness, double Surprise, 
            double Trust)
        {
            string query_insert = "INSERT INTO emocoes " +
                                  "(idEmocoes, Positivo, Negativo, Anger, Antecipation, Disgust, Fear, Joy, Sadness, Surprise, Trust) " +
                                  "VALUES(1,@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10);" +
                                  "update data set Emocoes_idEmocoes = 1 where Utilizador = '@val11' && Data = '@val12';";
            try
            {
                command = new MySqlCommand(query_insert, connection);
                command.Parameters.AddWithValue("@val1", Positivo);
                command.Parameters.AddWithValue("@val2", Negativo);
                command.Parameters.AddWithValue("@val3", Anger);
                command.Parameters.AddWithValue("@val4", Antecipation);
                command.Parameters.AddWithValue("@val5", Disgust);
                command.Parameters.AddWithValue("@val6", Fear);
                command.Parameters.AddWithValue("@val7", Joy);
                command.Parameters.AddWithValue("@val8", Sadness);
                command.Parameters.AddWithValue("@val9", Surprise);
                command.Parameters.AddWithValue("@val10", Trust);
                command.Parameters.AddWithValue("@val11", utilizador);
                command.Parameters.AddWithValue("@val12", data);
                

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
