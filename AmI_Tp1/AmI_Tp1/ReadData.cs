using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmI_Tp1
{
    public class ReadData
    {
        private string utilizador;
        private Database db;

        public ReadData(string utilizador,Database db)
        {
            this.utilizador = utilizador;
            this.db = db;
        }

        //id da data mais recente
        public int getIdData()
        {
            int id = 0;

            string query = "select idData from data order by Data desc limit 1;";

            MySqlDataReader reader = null;
            try
            {
                reader = db.getResultsDB(query);
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
            Console.WriteLine(id);
            return id;
        }

        public string readTop10()
        {
            StringBuilder sb = new StringBuilder();

                

            return sb.ToString();
        }
    }
}
