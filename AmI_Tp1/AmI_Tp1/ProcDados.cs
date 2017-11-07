using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmI_Tp1
{
    public class ProcDados
    {
        string utilizador;
         List<int> time = new List<int>();
         List<string> events = new List<string>();
         List<string> keys = new List<string>();

        List<Tuple<int, string, string, string>> dados = new List<Tuple<int, string, string, string>>();
        // tempo , caracter1 , caracter2 , Par

        List<Tuple<int, string, bool, int>> palavras = new List<Tuple<int, string, bool, int>>();
        // delta time , palavra, Existencia de backSpace, comprimento


        

        Database db;
         int id_BackspaceCaracter;
        int id_BackSpacePalavra;
        int idTop10;
        int idData;
        int id_LatenciaPalavras;
        int idWrittingTime;





        public void init(string utilizador, string file, Database db) {
            this.db = db;
            this.utilizador = utilizador;
           
            db.insertDB("Insert into Utilizador (Nome) select* from (select '"+utilizador+ "') as tmp WHERE NOT EXISTS (SELECT Nome FROM Utilizador WHERE Nome = '"+utilizador+"') LIMIT 1; ");

            ler(file);
            constrHand();
            prePalavras();

            BackSpacesCaracter();
            BackspacePalavra();
            LatenciaPalavras();
            DigraphAnalysis();
            desvioMediaCompWords();
            
            Top10();
            top10Keystrokes();
            top10Palavras();
            BackspaceCorrigidas();
            
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



        public void BackSpacesCaracter()
        {
            int count = 0;
            foreach (string s in keys)
            {
                if (s.Equals("Anterior"))
                {
                    count++;
                }
            }
            double valor = (count / Convert.ToDouble(keys.Count)) * 100;
            string val = valor.ToString().Replace(',', '.');
            db.insertDB("Insert into BackSpaceCaracter (Percentagem) values("+ val +");");
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
            
            
            foreach (var item in top.OrderByDescending(r => r.Value).Take(10))
            {
                double valor = item.Value / (Convert.ToDouble(keys.Count)) * 100;
                string val = valor.ToString().Replace(',', '.');
                db.insertDB("Insert into Chars (Caracter, Percentagem,Top10) values('"+item.Key+"',"+val+","+ idTop10+");");
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
            
        }

        public void DigraphAnalysis()
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

            if (Double.IsNaN(StdDev)) { StdDev = 0; }
            if (Double.IsNaN(StdDevLL)) { StdDevLL = 0; }
            if (Double.IsNaN(StdDevLR)) { StdDevLR = 0; }
            if (Double.IsNaN(StdDevLS)) { StdDevLS = 0; }
            if (Double.IsNaN(StdDevRL)) { StdDevRL = 0; }
            if (Double.IsNaN(StdDevRR)) { StdDevRR = 0; }
            if (Double.IsNaN(StdDevRS)) { StdDevRS = 0; }
            if (Double.IsNaN(StdDevSL)) { StdDevSL = 0; }
            if (Double.IsNaN(StdDevSR)) { StdDevSR = 0; }
            if (Double.IsNaN(StdDevSS)) { StdDevSS = 0; }

            db.insertDB("Insert into WritingTime (Media,Desvio_Padrao) values("+mediaAll.ToString().Replace(',','.')+","+StdDev.ToString().Replace(',', '.') + ");");
            idWrittingTime = db.getTableId("WritingTime");
            Data();
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('LL',"+mediaLL.ToString().Replace(',','.')+
                ","+idData+","+StdDevLL.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('LR'," + mediaLR.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevLR.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('LS'," + mediaLS.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevLS.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('RL'," + mediaRL.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevRL.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('RR'," + mediaRR.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevRR.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('RS'," + mediaRS.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevRS.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('SL'," + mediaSL.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevSL.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('SR'," + mediaSR.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevSR.ToString().Replace(',', '.') + ");");
            db.insertDB("Insert into GroupAnalysis(HandGroup,Media,Data_idData,Desvio_Padrao) values('SS'," + mediaSS.ToString().Replace(',', '.') + 
                "," + idData + "," + StdDevSS.ToString().Replace(',', '.') + ");");
            /* return "Mean of the writing time of all Key Events: " + mediaAll +
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
                      "\nStandard Deviation of the writing time of SS key groupings:" + StdDevSS; ;*/
        }


        public void Top10()
        {
            db.insertDB("insert into Top10(idData) values(" + idData + ");");
            idTop10 = db.getTableId("Top10");
        }

        public void Data() {
            db.insertDB("insert into Data(Data, Utilizador, Backspace_idBackspace, WritingTime_idWritingTime, " +
                        "LatenciaPalavras_idLatenciaPalavras, BackspacePalavra_idBackspace)values(NOW(),'"+utilizador+"',"+id_BackspaceCaracter+","
                                                                                                 +idWrittingTime+","+id_LatenciaPalavras+","+id_BackSpacePalavra+");");
            idData = db.getTableId("Data");
        }









        //Analise de palavras


        public void prePalavras()
        {
            Hand h = new Hand();
            StringBuilder pal = new StringBuilder();
            bool backSp = false; // existencia de backSpace
            int timeI = 0; //tempo da key do inicio da palavra
            List<int> Newtime = new List<int>();
            List<string> Newkeys = new List<string>();

            int comp = 0;

            PreProcPalavras(Newkeys, Newtime);



            for (int i = 0; i < Newkeys.Count; i++)
            {
                if (h.pertenceLimit(Newkeys[i]))
                {
                    if (pal.Length > 0)
                    {
                        if (Newkeys[i - 1].Equals("True"))
                            palavras.Add(new Tuple<int, string, bool, int>(Newtime[i - 2] - timeI, pal.ToString(), backSp, comp));
                        else
                            palavras.Add(new Tuple<int, string, bool, int>(Newtime[i - 1] - timeI, pal.ToString(), backSp, comp));
                        pal.Clear();
                    }

                    backSp = false;
                    comp = 0;

                }
                else
                {
                    if (Newkeys[i].Equals("True"))
                    {
                        backSp = true;
                        // caso seje o ultimo True
                        if (i == Newkeys.Count - 1 && pal.Length > 0)
                        {

                            palavras.Add(new Tuple<int, string, bool, int>(Newtime[i - 1] - timeI, pal.ToString(), backSp, comp));
                        }

                    }
                    else
                    {
                        if (pal.Length == 0)
                            timeI = Newtime[i];
                        pal.Append(Newkeys[i]);
                        comp++;
                        if (i == Newkeys.Count - 1 && pal.Length > 0)
                        {
                            palavras.Add(new Tuple<int, string, bool, int>(Newtime[i] - timeI, pal.ToString(), backSp, comp));
                        }
                    }
                }

            }

        }


        public void PreProcPalavras(List<string> Newkeys, List<int> Newtime)
        {
            Hand h = new Hand();
            int i = 0;
            bool backsp = false;
            for (i = 0; i < keys.Count; i++)
            {

                if (keys[i].Equals("Anterior") && Newkeys.Count > 0)
                {
                    backsp = true;
                    if (Newkeys[Newkeys.Count - 1].Equals("True"))
                    {
                        Newkeys.RemoveAt(Newkeys.Count - 1);
                        Newkeys.RemoveAt(Newkeys.Count - 1);
                        Newtime.RemoveAt(Newtime.Count - 1);
                        Newtime.RemoveAt(Newtime.Count - 1);
                    }
                    else
                    {
                        Newkeys.RemoveAt(Newkeys.Count - 1);
                        Newtime.RemoveAt(Newtime.Count - 1);
                    }
                }
                else
                {
                    if (h.pertenceLimit(keys[i]) && Newkeys.Count > 0 && backsp)
                    {

                        Newkeys.Add("True");
                        Newtime.Add(0);
                        backsp = false;


                    }

                    Newkeys.Add(keys[i]);
                    Newtime.Add(time[i]);
                }

            }

            if (Newkeys.Count > 0 && backsp)
            {
                Newkeys.Add("True");
                Newtime.Add(0);
            }



        }


        public void top10Palavras()
        {
            Dictionary<string, int> top = new Dictionary<string, int>();
            foreach (Tuple<int, string, bool, int> t in palavras)
            {
                if (top.ContainsKey(t.Item2))
                {
                    top[t.Item2] += 1;
                }
                else
                {
                    top.Add(t.Item2, 1);
                }

            }
            foreach (var item in top.OrderByDescending(r => r.Value).Take(10))
            {
                double valor = item.Value / (Convert.ToDouble(palavras.Count)) * 100;
                string val = valor.ToString().Replace(',', '.');
                db.insertDB("Insert into Words (Word, Percentagem,Top10_idTop10) values('" + item.Key + "'," + val + "," + idTop10 + ");");

                // Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value / (Convert.ToDouble(palavras.Count)) * 100);
            }
        }

        //percentagem de palavras em que encontrou o evento BackSpace durante a sua escrita
        public void BackspacePalavra()
        {
            int count = 0;
            foreach (Tuple<int, string, bool, int> t in palavras)
            {
                if (t.Item3)
                {
                    count++;
                }
            }
            double valor = (count / Convert.ToDouble(palavras.Count)) * 100;
            string val = valor.ToString().Replace(',', '.');
            db.insertDB("Insert into BackspacePalavra (Percentagem) values(" + val + ");");
            id_BackSpacePalavra = db.getTableId("BackspacePalavra");
            
        }


        //Averiguar a percentagem de palavras com tamanho Y que foram corrigidas durante a sua escrita
        public void BackspaceCorrigidas()
        {
            Dictionary<int, int> PalavrasCorrigidas = new Dictionary<int, int>();
            Dictionary<int, double> PalvrasBackspaceTam = new Dictionary<int, double>();
            foreach (Tuple<int, string, bool, int> t in palavras)
            {
                //Numero de palavras corrigidas por comprimento
                if (PalavrasCorrigidas.ContainsKey(t.Item4)) {
                    if (t.Item3) {
                        PalavrasCorrigidas[t.Item4] += 1;
                    }
                }
                else
                {
                    if (t.Item3)
                    {
                        PalavrasCorrigidas.Add(t.Item4, 1);
                    }
                }


                //Numero de palavras de determinado comprimento
                if (PalvrasBackspaceTam.ContainsKey(t.Item4))
                {
                    PalvrasBackspaceTam[t.Item4] += 1;
                }
                else
                {
                    PalvrasBackspaceTam.Add(t.Item4, 1);
                }
            }

            var keys = new List<int>(PalavrasCorrigidas.Keys);

            foreach (int key in keys)
            {

                PalvrasBackspaceTam[key] = (PalavrasCorrigidas[key] / Convert.ToDouble(PalvrasBackspaceTam[key])) * 100;
                db.insertDB("Insert into BackspacesCorrigidas (Tamanho,Percentagem,Data_idData) values("+key+","+PalavrasCorrigidas[key].ToString().Replace(',','.')+","+idData+");");

            }

        }

        public void LatenciaPalavras() {
            double media = mediaWords();
            double desvio = desvioWords();

            db.insertDB("Insert into LatenciaPalavras (Media,Desvio_Padrao) values("+media.ToString().Replace(',', '.') + ","+desvio.ToString().Replace(',', '.') + ");");
            id_LatenciaPalavras = db.getTableId("LatenciaPalavras");

        }

        public double mediaWords()
        {
            double soma = 0;

            foreach (Tuple<int, string, bool, int> t in palavras)
            {
                soma += t.Item1;
            }

            return soma / palavras.Count;
        }

        public double desvioWords()
        {
            double media = mediaWords();
            double soma = 0;
            foreach (Tuple<int, string, bool, int> x in palavras)
            {

                soma += Math.Pow(x.Item1 - media, 2);

            }
            return Math.Sqrt(soma / (palavras.Count - 1));

        }






        public void mediaCompWord(Dictionary<int,double> mediaWordsTam)
        {
            Dictionary<int, int> n = new Dictionary<int, int>();
            foreach (Tuple<int, string, bool, int> x in palavras)
            {
                if (mediaWordsTam.ContainsKey(x.Item4))
                {
                    mediaWordsTam[x.Item4] += x.Item1;
                    n[x.Item4] += 1;
                }
                else
                {
                    mediaWordsTam.Add(x.Item4, x.Item1);
                    n.Add(x.Item4, 1);
                }

            }
            var keys = new List<int>(mediaWordsTam.Keys);

            foreach (int key in keys)
            {
                mediaWordsTam[key] = mediaWordsTam[key] / n[key];

            }

        }



        public void desvioMediaCompWords()
        {
            Dictionary<int, double> mediaWordsTam = new Dictionary<int, double>();
            Dictionary<int, double> desvioWords = new Dictionary<int, double>();
            Dictionary<int, int> n = new Dictionary<int, int>();

            mediaCompWord(mediaWordsTam);

            
            foreach (Tuple<int, string, bool, int> x in palavras)
            {
                if (desvioWords.ContainsKey(x.Item4))
                {
                    desvioWords[x.Item4] += Math.Pow(x.Item1 - mediaWordsTam[x.Item4], 2);
                    n[x.Item4] += 1;
                }
                else
                {
                    desvioWords.Add(x.Item4, Math.Pow(x.Item1 - mediaWordsTam[x.Item4], 2));
                    n.Add(x.Item4, 1);
                }

            }
            var keys = new List<int>(mediaWordsTam.Keys);

            foreach (int key in keys)
            {
                desvioWords[key] = Math.Sqrt(desvioWords[key] / (n[key] - 1));
                if (Double.IsNaN(desvioWords[key])) { desvioWords[key] = 0; }
                db.insertDB("insert into LatenciaTamanho(Tamanho,Media,Desvio_Padrao,Data_idData) values("+key+","+
                    mediaWordsTam[key].ToString().Replace(',', '.') + ","+desvioWords[key].ToString().Replace(',', '.') + ","+idData+");");
            }

        }





    }
}
