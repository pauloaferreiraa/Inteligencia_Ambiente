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


        public void init(string utilizador, string file) {

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



        public float nBackSpaces()
        {
            int count = 0;
            foreach (string s in keys)
            {
                if (s.Equals("Anterior"))
                {
                    count++;
                }
            }
            return (count / Convert.ToSingle(keys.Count)) * 100;
        }

        public string top10Keystrokes()
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
            int line = 1;
            
            foreach (var item in top.OrderByDescending(r => r.Value).Take(10))
            {
                
                float valor = item.Value / (Convert.ToSingle(keys.Count)) * 100;
                sb.Append(line.ToString("00"));
                sb.Append("-  ");
                sb.Append(item.Key);
                sb.Append(' ', 15 - item.Key.Length);
                sb.Append(valor.ToString()+"%");
                sb.AppendLine();
                line++;

            }           
            return sb.ToString();
        }







    }
}
