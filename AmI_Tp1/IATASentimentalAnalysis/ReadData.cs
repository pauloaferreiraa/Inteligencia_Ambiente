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
                    return backspaceCaracterS(utilizador).Select(item => Convert.ToDouble(item)).ToArray();
                case "Palavra backSpace":
                    return getArray(backspacePalavras(utilizador));
                case "Média da Latência de palavra":
                    return getArray(latenciaPal(utilizador,"Media"));
                case "desvio da Latência de palavra":
                    return getArray(latenciaPal(utilizador, "Desvio_Padrao")) ;
                case "média digraph":
                    return getArray(writingTimeMediaDesvio(utilizador,"Media"));
                case "desvio digraph":
                    return getArray(writingTimeMediaDesvio(utilizador, "Desvio_Padrao")) ;
                case "Positivo":
                    return getArray(getEmocao(utilizador,"Positivo"));
                case "Negativo":
                    return getArray(getEmocao(utilizador, "Negativo"));
                case "Anger":
                    return getArray(getEmocao(utilizador, "Anger"));
                case "Anticipation":
                    return getArray(getEmocao(utilizador, "Antecipation"));
                case "Disgust":
                    return getArray(getEmocao(utilizador, "Disgust"));
                case "Fear":
                    return getArray(getEmocao(utilizador, "Fear"));
                case "Joy":
                    return getArray(getEmocao(utilizador, "Joy"));
                case "Sadness":
                    return getArray(getEmocao(utilizador, "Sadness"));
                case "Surprise":
                    return getArray(getEmocao(utilizador, "Surprise"));
                case "Trust":
                    return getArray(getEmocao(utilizador, "Trust"));
                default:
                    return null;
            }
        }



        public double[] getArray(List<double> val) {
            double[] res = new double[val.Count];
            for (int i = 0; i < val.Count; i++) {
                res[i] = val[i];
            }
            return res;
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
