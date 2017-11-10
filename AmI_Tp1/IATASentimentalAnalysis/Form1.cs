using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Iveonik.Stemmers;
using Syn.WordNet;

namespace IATASentimentalAnalysis
{
    public partial class Form1 : Form
    {
        public class Words
        {
            public string word;
            public string StemmedWord;
            public int count = 1;
            public int[] WPParagraph;
            public double TF_IDF = 0;

            public Words(string w, int nP, string stW)
            {
                word = w;
                WPParagraph = new int[nP];
                StemmedWord = stW;
            }
        }

        List<Words> words = new List<Words>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        int nP;
        bool stemm = false;

        private void button1_Click(object sender, EventArgs e)
        {
            words.Clear();
            IStemmer stemmer = new EnglishStemmer();
            Tokenizer TK = new Tokenizer();

            List<string> w = new List<string>();

            if (richTextBox1.Text != String.Empty)
            { 
                nP= richTextBox1.Text.Split(new string[] { Environment.NewLine, "\t", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length; // todo fazer uma estrutura melhor que guarde quantas vezes aparece uma palavra num paragrafo
                string tex = richTextBox1.Text.Replace("\n", " ");
                w = TK.Tokenize(Regex.Replace(tex, @"[^\w\s]", string.Empty));
                w.RemoveAll(item => item.Length == 0);
                if (StemmedcheckBox.Checked)
                {
                    stemm = true;
                }
            }
            else
            {
                return;
            }

            foreach (var wo in w)
            {
                if (words.Any(n => n.word == wo))
                {
                    words.Find(n => n.word == wo).count++;
                }
                else
                {
                    words.Add(new Words(wo, nP, stemmer.Stem(wo)));
                }
            }

            int N = 3; //numero de N-Grams

            Dictionary<string, int>[] ListasNGram = new Dictionary<string, int>[4];
            ListasNGram[0] = new Dictionary<string, int>();
            ListasNGram[1] = new Dictionary<string, int>();
            ListasNGram[2] = new Dictionary<string, int>();
            ListasNGram[3] = new Dictionary<string, int>();

            for (int y = 1; y <= N; y++)
            {
                for (int i = 0; i < (w.Count - y + 1); i++)
                {
                    String s = "";
                    int inicio = i;
                    int fim = i + y;
                    for (int j = inicio; j < fim; j++)
                    {
                        if (stemm)
                        {
                            s = s + " " + w[j];

                        }
                        else
                        {
                            s = s + " " + w[j];
                        }
                    }
                    int value;
                    if (!ListasNGram[y].TryGetValue(s, out value))
                    {
                        ListasNGram[y].Add(s, 1);
                    }
                    else
                    {
                        ListasNGram[y][s] = value + 1;
                    }
                }
            }
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            checkedListBox3.Items.Clear();

            for (int i = 1; i <= N; i++)
            {
                foreach (var LNG in ListasNGram[i].OrderByDescending(key => key.Value))
                {
                    switch (i)
                    {
                        case 1:
                            checkedListBox1.Items.Add(LNG.Key + " : " + LNG.Value, false);
                            break;
                        case 2:
                            checkedListBox2.Items.Add(LNG.Key + " : " + LNG.Value, false);
                            break;
                        case 3:
                            checkedListBox3.Items.Add(LNG.Key + " : " + LNG.Value, false);
                            break;
                    }
                }
            }
        }

        private void calculoTFIDF()
        {
            string[] texto;
            if (nP == 1)
            {
                texto = richTextBox1.Text.Split(new string[] { "." },
                    StringSplitOptions.RemoveEmptyEntries);
                nP = richTextBox1.Text.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Length;
            }
            else
            {
                texto = richTextBox1.Text.Split(new string[] { Environment.NewLine, "\t", "\n" },
                    StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var word in words)
            {
                int WinParagraph = 0;
                foreach (var parag in texto)
                {
                    if (parag.Contains(word.word))
                    {
                        WinParagraph++;
                    }
                }
                double TF = (double)word.count / words.Count;

                double IDF = Math.Log((double)nP / WinParagraph);

                word.TF_IDF = TF * IDF;
                var myControl = new Label();
                if (stemm)
                    myControl.Text = word.StemmedWord + " : " + word.TF_IDF;
                else
                {
                    myControl.Text = word.word + " : " + word.TF_IDF;
                }
                myControl.Dock = DockStyle.Top;

                panel1.Controls.Add(myControl);
            }
        }

        private void PolarityAnalysis()
        {
            string fileName = "NRC-Emotion-Lexicon-v0.92-InManyLanguages.xlsx";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            string con = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + path + "; Extended Properties = Excel 12.0;";
            DataTable Contents = new DataTable();
            using (OleDbDataAdapter adapter = new OleDbDataAdapter("Select * From [P1$]", con))
            {
                adapter.Fill(Contents);
            }

            float positive = 0,
                negative = 0,
                anger = 0,
                anticipation = 0,
                disgust = 0,
                fear = 0,
                joy = 0,
                sadness = 0,
                suprise = 0,
                trust = 0;

            int countWords = 0;


            // 41 a 50
            foreach (var w in words.OrderByDescending(key => key.TF_IDF))
            {
                DataRow[] dt = Contents.Select("[English Word] like '" + w.word + "'");

                if (dt.Length == 0)
                {
                    continue;
                }
                else
                {
                    positive += Convert.ToInt32(dt[0].ItemArray[41]);
                    negative += Convert.ToInt32(dt[0].ItemArray[42]);
                    anger += Convert.ToInt32(dt[0].ItemArray[43]);  
                    anticipation += Convert.ToInt32(dt[0].ItemArray[44]);
                    disgust += Convert.ToInt32(dt[0].ItemArray[45]);
                    fear += Convert.ToInt32(dt[0].ItemArray[46]);
                    joy += Convert.ToInt32(dt[0].ItemArray[47]);
                    sadness += Convert.ToInt32(dt[0].ItemArray[48]);
                    suprise += Convert.ToInt32(dt[0].ItemArray[49]);
                    trust += Convert.ToInt32(dt[0].ItemArray[50]);
                    countWords++;
                }
            }

            Console.WriteLine("Positive : "+positive/ countWords);
            Console.WriteLine("Negative : "+negative / countWords);
            Console.WriteLine("Anger : "+anger / countWords);
            Console.WriteLine("anticipation : " + anticipation / countWords);
            Console.WriteLine("disgust : " + disgust / countWords);
            Console.WriteLine("fear : " + fear / countWords);
            Console.WriteLine("joy : " + joy / countWords);
            Console.WriteLine("sadness : " + sadness / countWords);
            Console.WriteLine("suprise : " + suprise / countWords);
            Console.WriteLine("trust : " + trust / countWords);

            //set the chart-type to "Pie"
            chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
           //Add some datapoints so the series. in this case you can pass the values to this method
            chart1.Series["Series1"].Points[0].YValues[0] = positive / countWords;
            chart1.Series["Series1"].Points[1].YValues[0] = negative / countWords;

            //set the chart-type to "Pie"
            chart2.Series["Series1"].ChartType = SeriesChartType.Pie;
            chart2.Series["Series1"].Points[0].YValues[0] = anger / countWords;
            chart2.Series["Series1"].Points[1].YValues[0] = anticipation / countWords;
            chart2.Series["Series1"].Points[2].YValues[0] = disgust / countWords;
            chart2.Series["Series1"].Points[3].YValues[0] = fear / countWords;
            chart2.Series["Series1"].Points[4].YValues[0] = joy / countWords;
            chart2.Series["Series1"].Points[5].YValues[0] = sadness / countWords;
            chart2.Series["Series1"].Points[6].YValues[0] = suprise / countWords;
            chart2.Series["Series1"].Points[7].YValues[0] = trust / countWords;
        }


        List<string> Stopwords = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {

            foreach (var chkLB in checkedListBox1.CheckedItems)
            {
                string[] part = chkLB.ToString().Split(' ');
                Stopwords.Add(part[1]);
            }
            foreach (var chkLB in checkedListBox2.CheckedItems)
            {
                string[] part = chkLB.ToString().Split(' ');
                Stopwords.Add(part[1] + " " + part[2]);
            }
            foreach (var chkLB in checkedListBox3.CheckedItems)
            {
                string[] part = chkLB.ToString().Split(' ');
                Stopwords.Add(part[1] + " " + part[2] + " " +part[3]);
            }

            foreach (var sw in Stopwords)
            {
                words.RemoveAll(x => x.word == sw);
            }

            calculoTFIDF();
            PolarityAnalysis();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
