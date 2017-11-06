using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmI_Tp1
{
    public class ProcDados
    {
         List<int> time = new List<int>();
         List<string> events = new List<string>();
         List<string> keys = new List<string>();
         List<Tuple<int, string, string, string>> dados = new List<Tuple<int, string, string, string>>();
         Database db;
         int id_BackspaceCaracter;
        int idTop10;
      

        public void init(string utilizador, string file, Database db) {
            this.db = db;
            db.connect();
            db.insertDB("Insert into Utilizador (Nome) select* from (select'"+utilizador+ "') as tmp WHERE NOT EXISTS (SELECT Nome FROM Utilizador WHERE Nome = '"+utilizador+"') LIMIT 1; ");
            ler(file);
            constrHand();            
        }



        public void ler(string file) 
        {
            String line;
            System.IO.StreamReader File = new System.IO.StreamReader(@file);
            while ((line = File.ReadLine()) != null)
            {
                String[] substrings = line.Split(':');
                time.Add(Convert.ToInt32(substrings[0]));
                events.Add(substrings[1]);
                keys.Add(substrings[2]);
                
            }
            File.Close();
        }


        public void constrHand()
        {
            Hand h = new Hand();

            for (int i = 0; i < keys.Count - 1; i++)
            {

                int x = time[i + 1] - time[i];
                string fst;
                string snd;
                if (h.pertenceLeft(keys[i]))
                {
                    fst = "L";
                }
                else
                {
                    if (h.pertenceRight(keys[i]))
                    {
                        fst = "R";
                    }
                    else
                    {
                        fst = "S";
                    }
                }

                if (h.pertenceLeft(keys[i + 1]))
                {
                    snd = "L";
                }
                else
                {
                    if (h.pertenceRight(keys[i + 1]))
                    {
                        snd = "R";
                    }
                    else
                    {
                        snd = "S";
                    }
                }

                dados.Add(new Tuple<int, string, string, string>(x, keys[i], keys[i + 1], fst + "-" + snd));
                

            }
          
        }



        public void nBackSpacesCaracter()
        {
            int count = 0;
            foreach (string s in keys)
            {
                if (s.Equals("Anterior"))
                {
                    count++;
                }
            }
            
            db.insertDB("Insert into BackSpaceCaracter (Percentagem) values("+ (count / Convert.ToDouble(keys.Count)) * 100+");");
            id_BackspaceCaracter = db.getTableId("BackspaceCaracter");
        }

        public void top10Keystrokes()
        {
            Dictionary<string, int> top = new Dictionary<string, int>();
            StringBuilder sb = new StringBuilder();
            foreach (string s in keys)
            {
                if (top.ContainsKey(s))
                {
                    top[s] += 1;
                }
                else
                {
                    top.Add(s, 1);
                }

            }
            //int line = 1;
            
            foreach (var item in top.OrderByDescending(r => r.Value).Take(10))
            {
                double valor = item.Value / (Convert.ToDouble(keys.Count)) * 100;
                db.insertDB("Insert into Chars (Char, Percentagem,Top10) values("+item.Key+","+valor+","+ idTop10+");");
               /* float valor = item.Value / (Convert.ToSingle(keys.Count)) * 100;
                sb.Append(line.ToString("00"));
                sb.Append("-  ");
                sb.Append(item.Key);
                sb.Append(' ', 15 - item.Key.Length);
                sb.Append(valor.ToString()+"%");
                sb.AppendLine();
                line++;
                */
            }           
            //return sb.ToString();
        }

        public string DigraphAnalysis()
        {
            double soma = 0;
            double somaLL = 0;
            double somaLR = 0;
            double somaLS = 0;
            double somaRL = 0;
            double somaRR = 0;
            double somaRS = 0;
            double somaSL = 0;
            double somaSR = 0;
            double somaSS = 0;

            double countLL = 0;
            double countLR = 0;
            double countLS = 0;
            double countRL = 0;
            double countRR = 0;
            double countRS = 0;
            double countSL = 0;
            double countSR = 0;
            double countSS = 0;

            for (int i = 0; i < dados.Count; i++)
            {
                soma += dados[i].Item1;

                if (dados[i].Item4 == "L-L")
                {
                    somaLL += dados[i].Item1;
                    countLL++;
                }
                else if (dados[i].Item4 == "L-R")
                {
                    somaLR += dados[i].Item1;
                    countLR++;
                }
                else if (dados[i].Item4 == "L-S")
                {
                    somaLS += dados[i].Item1;
                    countLS++;
                }
                else if (dados[i].Item4 == "R-L")
                {
                    somaRL += dados[i].Item1;
                    countRL++;
                }
                else if (dados[i].Item4 == "R-R")
                {
                    somaRR += dados[i].Item1;
                    countRR++;
                }
                else if (dados[i].Item4 == "R-S")
                {
                    somaRS += dados[i].Item1;
                    countRS++;
                }
                else if (dados[i].Item4 == "S-L")
                {
                    somaSL += dados[i].Item1;
                    countSL++;
                }
                else if (dados[i].Item4 == "S-R")
                {
                    somaSR += dados[i].Item1;
                    countSR++;
                }
                else if (dados[i].Item4 == "S-S")
                {
                    somaSS += dados[i].Item1;
                    countSS++;
                }
            }

            double mediaAll = soma / dados.Count;
            double mediaLL = 0;
            if (countLL != 0)
                mediaLL = somaLL / countLL;
            double mediaLR = 0;
            if (countLR != 0)
                mediaLR = somaLR / countLR;
            double mediaLS = 0;
            if (countLS != 0)
                mediaLS = somaLS / countLS;
            double mediaRL = 0;
            if (countRL != 0)
                mediaRL = somaRL / countRL;
            double mediaRR = 0;
            if (countRR != 0)
                mediaRR = somaRR / countRR;
            double mediaRS = 0;
            if (countRS != 0)
                mediaRS = somaRS / countRS;
            double mediaSL = 0;
            if (countSL != 0)
                mediaSL = somaSL / countSL;
            double mediaSR = 0;
            if (countSR != 0)
                mediaSR = somaSR / countSR;
            double mediaSS = 0;
            if (countSS != 0)
                mediaSS = somaSS / countSS;



            double somaDiffDaMediaAll = 0;
            double somaDiffDaMediaLL = 0;
            double somaDiffDaMediaLR = 0;
            double somaDiffDaMediaLS = 0;
            double somaDiffDaMediaRL = 0;
            double somaDiffDaMediaRR = 0;
            double somaDiffDaMediaRS = 0;
            double somaDiffDaMediaSL = 0;
            double somaDiffDaMediaSR = 0;
            double somaDiffDaMediaSS = 0;


            for (int i = 0; i < dados.Count; i++)
            {
                somaDiffDaMediaAll += Math.Pow((dados[i].Item1 - mediaAll), 2);
                if (dados[i].Item4 == "L-L")
                {
                    somaDiffDaMediaLL += Math.Pow((dados[i].Item1 - mediaLL), 2);
                }
                else if (dados[i].Item4 == "L-R")
                {
                    somaDiffDaMediaLR += Math.Pow((dados[i].Item1 - mediaLR), 2);
                }
                else if (dados[i].Item4 == "L-S")
                {
                    somaDiffDaMediaLS += Math.Pow((dados[i].Item1 - mediaLS), 2);
                }
                else if (dados[i].Item4 == "R-L")
                {
                    somaDiffDaMediaRL += Math.Pow((dados[i].Item1 - mediaRL), 2);
                }
                else if (dados[i].Item4 == "R-R")
                {
                    somaDiffDaMediaRR += Math.Pow((dados[i].Item1 - mediaRR), 2);
                }
                else if (dados[i].Item4 == "R-S")
                {
                    somaDiffDaMediaRS += Math.Pow((dados[i].Item1 - mediaRS), 2);
                }
                else if (dados[i].Item4 == "S-L")
                {
                    somaDiffDaMediaSL += Math.Pow((dados[i].Item1 - mediaSL), 2);
                }
                else if (dados[i].Item4 == "S-R")
                {
                    somaDiffDaMediaSR += Math.Pow((dados[i].Item1 - mediaSR), 2);
                }
                else if (dados[i].Item4 == "S-S")
                {
                    somaDiffDaMediaSS += Math.Pow((dados[i].Item1 - mediaSS), 2);
                }
            }

            double StdDev = Math.Sqrt(somaDiffDaMediaAll / (dados.Count));
            double StdDevLL = Math.Sqrt(somaDiffDaMediaLL / (countLL));
            double StdDevLR = Math.Sqrt(somaDiffDaMediaLR / (countLR));
            double StdDevLS = Math.Sqrt(somaDiffDaMediaLS / (countLS));
            double StdDevRL = Math.Sqrt(somaDiffDaMediaRL / (countRL));
            double StdDevRR = Math.Sqrt(somaDiffDaMediaRR / (countRR));
            double StdDevRS = Math.Sqrt(somaDiffDaMediaRS / (countRS));
            double StdDevSL = Math.Sqrt(somaDiffDaMediaSL / (countSL));
            double StdDevSR = Math.Sqrt(somaDiffDaMediaSR / (countSR));
            double StdDevSS = Math.Sqrt(somaDiffDaMediaSS / (countSS));

            return "Mean of the writing time of all Key Events: " + mediaAll +
                     "\nStandard Deviation of the writing time of all Key Events:" + StdDev +
                     "\nMean of the writing time of LL key groupings:" + mediaLL +
                     "\nStandard Deviation of the writing time of LL key groupings:" + StdDevLL +
                     "\nMean of the writing time of LR key groupings:" + mediaLR +
                     "\nStandard Deviation of the writing time of LR key groupings:" + StdDevLR +
                     "\nMean of the writing time of LS key groupings:" + mediaLS +
                     "\nStandard Deviation of the writing time of LS key groupings:" + StdDevLS +
                     "\nMean of the writing time of RL key groupings:" + mediaRL +
                     "\nStandard Deviation of the writing time of RL key groupings:" + StdDevRL +
                     "\nMean of the writing time of RR key groupings:" + mediaRR +
                     "\nStandard Deviation of the writing time of RR key groupings:" + StdDevRR +
                     "\nMean of the writing time of RS key groupings:" + mediaRS +
                     "\nStandard Deviation of the writing time of RS key groupings:" + StdDevRS +
                     "\nMean of the writing time of SL key groupings:" + mediaSL +
                     "\nStandard Deviation of the writing time of SL key groupings:" + StdDevSL +
                     "\nMean of the writing time of SR key groupings:" + mediaSR +
                     "\nStandard Deviation of the writing time of SR key groupings:" + StdDevSR +
                     "\nMean of the writing time of SS key groupings:" + mediaSS +
                     "\nStandard Deviation of the writing time of SS key groupings:" + StdDevSS; ;
        }














        //Analise de palavras




 





    }
}
