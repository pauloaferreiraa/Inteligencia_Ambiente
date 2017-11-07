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

        public ReadData(string utilizador,Database db) //UPTODATE
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
            
            return id;
        }

        public string readTop10KeyStrokes(string utilizador)
        {
            StringBuilder sb = new StringBuilder();
            int idDataRecente = getIdData();

            string query = "select Caracter, Percentagem from data " +
                "inner join utilizador on (data.Utilizador = utilizador.Nome) inner join top10 on (data.idData = top10.IdData) " +
                "inner join chars on  (top10.idTop10 = chars.Top10)" +
                "where utilizador = '" + utilizador + "' && data.idData = "
                 + idDataRecente + " order by Percentagem desc;";
            MySqlDataReader reader = db.getResultsDB(query);
            int line = 1;
            while (reader.Read())
            {
                string caracter = reader.GetString(0);
                string perc = reader.GetString(1);
                sb.Append(line.ToString("00"));
                sb.Append("-  ");
                
                sb.Append(caracter);
                sb.Append(' ', 15 - caracter.Length);
                sb.Append(perc).Append("%");
                sb.AppendLine();
                line++;
            }
            reader.Close();
            return sb.ToString();
        }

        public string backspaceCaracter(string utilizador)
        {
            int idData = getIdData();
            string valor = "";
            string query = "select Percentagem from " +
                "data inner join utilizador on(data.Utilizador = utilizador.Nome) " +
                "inner join backspacecaracter on(idBackspace = data.Backspace_idBackspace) " +
                "where utilizador = + '" + utilizador + "' && data.idData = " + idData + ";";
            MySqlDataReader reader = db.getResultsDB(query);
            while (reader.Read())
            {
                valor = reader.GetString(0);
            }
            reader.Close();
            return valor;
        }
        
        public string writingTime(string utilizador)
        {
            int idData = getIdData();
            string query = "select Media,Desvio_Padrao from writingtime " +
                "inner join data on(data.WritingTime_idWritingTime = writingtime.idWritingTime) " +
                "inner join utilizador on(data.Utilizador = Nome) " +
                "where utilizador = '" + utilizador+ "' && data.idData = "+ idData + ";";
            MySqlDataReader reader = db.getResultsDB(query);
            string valor = "";
            while (reader.Read())
            {
                valor = reader.GetString(0) + " " + reader.GetString(1);
            }
            reader.Close();
            return valor;
        }
    }
}
