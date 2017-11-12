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
                    return latenciaPal(utilizador,"Media").ToArray();
                case "desvio da Latência de palavra":
                    return latenciaPal(utilizador, "Desvio_Padrao").ToArray(); ;
                case "média digraph":
                    return writingTimeMediaDesvio(utilizador,"Media").ToArray();
                case "desvio digraph":
                    return writingTimeMediaDesvio(utilizador, "Desvio_Padrao").ToArray(); ;
                case "Positivo":
                    return getEmocao(utilizador,"Positivo").ToArray();
                case "Negativo":
                    return getEmocao(utilizador, "Negativo").ToArray();
                case "Anger":
                    return getEmocao(utilizador, "Anger").ToArray();
                case "Anticipation":
                    return getEmocao(utilizador, "Antecipation").ToArray();
                case "Disgust":
                    return getEmocao(utilizador, "Disgust").ToArray();
                case "Fear":
                    return getEmocao(utilizador, "Fear").ToArray();
                case "Joy":
                    return getEmocao(utilizador, "Joy").ToArray();
                case "Sadness":
                    return getEmocao(utilizador, "Sadness").ToArray();
                case "Surprise":
                    return getEmocao(utilizador, "Surprise").ToArray();
                case "Trust":
                    return getEmocao(utilizador, "Trust").ToArray();
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
        

        public List<double> writingTimeMediaDesvio(string utilizador, string opcao)
        {
            
            string query = "select "+opcao+" from writingtime " +
                "inner join data on(data.WritingTime_idWritingTime = writingtime.idWritingTime) " +
                "inner join utilizador on(data.Utilizador = Nome) " +
                "where utilizador = '" + utilizador+ "';";
            MySqlDataReader reader = db.getResultsDB(query);
            List<double> valor = new List<double>();
            while (reader.Read())
            {
                valor.Add(reader.GetDouble(0));
            }
            reader.Close();
            return valor;
        }

        public List<double> getEmocao(string utilizador, string opcao)
        {

            string query = "select " + opcao + " from emocoes inner join data on (data.Emocoes_idEmocoes = idEmocoes) where Utilizador ='"+utilizador+"';";
            MySqlDataReader reader = db.getResultsDB(query);
            List<double> valor = new List<double>();
            while (reader.Read())
            {
                valor.Add(reader.GetDouble(0));
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

        public List<double> latenciaPal(string utilizador,string opcao)
        {
          
            string query = "select "+opcao+" from " +
                "data inner join utilizador on (data.Utilizador = utilizador.Nome) " +
                "inner join latenciapalavras on (idLatenciaPalavras = data.LatenciaPalavras_idLatenciaPalavras)" +
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
