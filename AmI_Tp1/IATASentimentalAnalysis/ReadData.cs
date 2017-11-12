using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IATASentimentalAnalysis
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

        public double[] getInfo(string utilizador, string valor) {
            switch (valor)
            {
                case "Keystroke Backspace":
                    return backspaceCaracterS(utilizador).ToArray();
                case "Palavra backSpace":
                    return backspacePalavras(utilizador).ToArray();
                case "Média da Latência de palavra":
                    return null;
                case "desvio da Latência de palavra":
                    return null;
                case "média digraph":
                    return null;
                case "desvio digraph":
                    return null;
                case "Positivo":
                    return null;
                case "Negativo":
                    return null;
                case "Anger":
                    return null;
                case "Anticipation":
                    return null;
                case "Disgust":
                    return null;
                case "Fear":
                    return null;
                case "Joy":
                    return null;
                case "Sadness":
                    return null;
                case "Surprise":
                    return null;
                case "Trust":
                    return null;
                default:
                    return null;
            }
        }

        public List<double> backspaceCaracterS(string utilizador)
        {

            List < double> valor = new List<double>();
            string query = "select Percentagem from " +
                "data inner join utilizador on(data.Utilizador = utilizador.Nome) " +
                "inner join backspacecaracter on(idBackspace = data.Backspace_idBackspace) " +
                "where utilizador ='" + utilizador +  "';";
            MySqlDataReader reader = db.getResultsDB(query);
            while (reader.Read())
            {
                valor.Add(reader.GetDouble(0));
            }
            reader.Close();
            return valor;
        }
        

        public string writingTime(string utilizador)
        {
            
            string query = "select Media,Desvio_Padrao from writingtime " +
                "inner join data on(data.WritingTime_idWritingTime = writingtime.idWritingTime) " +
                "inner join utilizador on(data.Utilizador = Nome) " +
                "where utilizador = '" + utilizador+ "';";
            MySqlDataReader reader = db.getResultsDB(query);
            string valor = "";
            while (reader.Read())
            {
                valor = reader.GetString(0) + " " + reader.GetString(1);
            }
            reader.Close();
            return valor;
        }

        public string groupAnalysis(string utilizador)
        {
           
            string query = "select HandGroup,Media,Desvio_Padrao from groupanalysis " +
                "inner join data on(groupanalysis.Data_idData = data.idData) " +
                "inner join utilizador on(data.Utilizador = Nome) " +
                "where utilizador = '" + utilizador + "';";

            MySqlDataReader reader = db.getResultsDB(query);
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                sb.Append(reader.GetString(0)).Append(' ',10).Append(reader.GetString(1)).Append(' ',16- reader.GetString(1).Length).
                    Append(reader.GetString(2)).AppendLine();
            }
            reader.Close();
            return sb.ToString();
        }


        public List<double> backspacePalavras(string utilizador)
        {
          
            string query = "select Percentagem from " +
                "data inner join utilizador on (data.Utilizador = utilizador.Nome) " +
                "inner join backspacepalavra on (idBackspace = data.Backspace_idBackspace) " +
                "where utilizador = '" + utilizador + "';";
            MySqlDataReader reader = db.getResultsDB(query);
            List<double> valor = new List<double>();
            while (reader.Read())
            {
                valor.Add(reader.GetDouble(0));
            }
            reader.Close();
            return valor;
        }

        public string backspaceCorrigidas(string utilizador)
        {
           
            string query = "select Tamanho,Percentagem from backspacescorrigidas " +
                "inner join data on (backspacescorrigidas.Data_idData = data.idData) " +
                "inner join utilizador on (data.Utilizador = Nome) " +
                "where utilizador = '" + utilizador + "' order by Tamanho;";
            MySqlDataReader reader = db.getResultsDB(query);
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                int x = Int32.Parse(reader.GetString(0));
                sb.Append(x.ToString("00")).Append(' ',10).Append(reader.GetString(1)).Append(" ").AppendLine("%");
            }
            reader.Close();
            return sb.ToString();
        }

        public string latenciaPal(string utilizador)
        {
          
            string query = "select Media,Desvio_Padrao from " +
                "data inner join utilizador on (data.Utilizador = utilizador.Nome) " +
                "inner join latenciapalavras on (idLatenciaPalavras = data.LatenciaPalavras_idLatenciaPalavras)" +
                "where utilizador = '" + utilizador + "';";
            MySqlDataReader reader = db.getResultsDB(query);
            string valor = "";
            while (reader.Read())
            {
                valor = reader.GetString(0) + " " + reader.GetString(1);
            }
            reader.Close();
            return valor;
        }

        public string latenciaTamanho(string utilizador)
        {
          
            string query = "select Tamanho,Media,Desvio_Padrao " +
                "from latenciatamanho inner join data on (latenciatamanho.Data_idData = data.idData) " +
                "inner join utilizador on (data.Utilizador = Nome) " +
                "where utilizador = '" + utilizador + "';";
            MySqlDataReader reader = db.getResultsDB(query);
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                int x = Int32.Parse(reader.GetString(0));
                sb.Append(x.ToString("00")).Append(' ',6).Append(reader.GetString(1)).Append(' ',16- reader.GetString(1).Length).
                    Append(reader.GetString(2)).AppendLine();
            }
            reader.Close();
            return sb.ToString();
        }
    }
}
