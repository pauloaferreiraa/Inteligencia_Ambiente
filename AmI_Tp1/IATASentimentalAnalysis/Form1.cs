using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
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

       // List<Words> words = new List<Words>();
        EmotionalClass EC = new EmotionalClass();
        string utilizador;
       // int nP;
        bool stemm = false;
        string text;
        string data;
        List<string> documentos= new List<string>();
        Database db;
        List<string> words = new List<string>();
        public Form1()
        {
            db = new Database("localhost", "mydb", "root", "");
            db.connect();
            InitializeComponent();
        }


      

        private void button1_Click(object sender, EventArgs e)
        {
            utilizador = "";
            IStemmer stemmer = new EnglishStemmer();
            Tokenizer TK = new Tokenizer();
            words.Clear();
           

            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                utilizador = textBox1.Text;
                Console.WriteLine(utilizador);
                if (!db.checkUser(utilizador))
                {
                    MessageBox.Show("Este utilizador não existe", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                text = Regex.Replace(text, @"[^\w\s]", "").ToLower().Replace("\r\n", " ");
                words = TK.Tokenize(text);
                words.RemoveAll(item => item.Length == 0);
                
            }
            else {
                if (!String.IsNullOrEmpty(richTextBox1.Text))
                {
                    documentos.Clear();
                    string tex = Regex.Replace(richTextBox1.Text, @"[^\w\s]", "").ToLower();
                    string[] paragrafos = tex.Split(new[] { '\r', '\n' });
                    documentos = paragrafos.ToList();
                    documentos.RemoveAt(0);

                    words = TK.Tokenize(paragrafos[0]);
                    words.RemoveAll(item => item.Length == 0);
                }
                else {
                    return;
                }

            }

            if (StemmedcheckBox.Checked)
            {
                stemm = true;
            }
            else
            {
                stemm = false;
            }

            int N = 3; //numero de N-Grams

            Dictionary<string, int>[] ListasNGram = new Dictionary<string, int>[4];
            ListasNGram[0] = new Dictionary<string, int>();
            ListasNGram[1] = new Dictionary<string, int>();
            ListasNGram[2] = new Dictionary<string, int>();
            ListasNGram[3] = new Dictionary<string, int>();

            for (int y = 1; y <= N; y++)
            {
                for (int i = 0; i < (words.Count - y + 1); i++)
                {
                    String s = "";
                    int inicio = i;
                    int fim = i + y;
                    for (int j = inicio; j < fim; j++)
                    {
                        if (stemm)
                        {
                            s = s + " " + stemmer.Stem(words[j]);

                        }
                        else
                        {
                            s = s + " " + words[j];
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


        private void calculoTFIDF(List<string> wordsLimpa)
        {
            IStemmer stemmer = new EnglishStemmer();
            Dictionary<string, double> TF = TermFreq(wordsLimpa);
            Dictionary<string, double> tf_idf = new Dictionary<string, double>();
            if (stemm) {
                foreach (string s in TF.Keys) {
                    tf_idf.Add(s,TF[s]*IDF(stemmer.Stem(s)));
                }
            } else {
                foreach (string s in TF.Keys)
                {
                    tf_idf.Add(s, TF[s] * IDF(s));
                }
            }
            listBox1.Items.Clear();
            foreach (var item in tf_idf.OrderByDescending(r => r.Value))
            {
                listBox1.Items.Add(item.Key + " : " + item.Value);
               
            }

        }


        // term frequency
        public Dictionary<string, double> TermFreq(List<string> palavras)
        {
            IStemmer stemmer = new EnglishStemmer();
            Dictionary<string, double> res = new Dictionary<string, double>();
            if (stemm)
            {
                foreach (string s in palavras)
                {
                    if (res.ContainsKey(stemmer.Stem(s)))
                    {
                        res[stemmer.Stem(s)]++;
                    }
                    else
                    {
                        res.Add(stemmer.Stem(s), 1);
                    }
                }
            }
            else {
                foreach (string s in palavras)
                {
                    if (res.ContainsKey(s))
                    {
                        res[s]++;
                    }
                    else
                    {
                        res.Add(s, 1);
                    }
                }
            }
            List<string> keys = res.Keys.ToList();
            foreach (string s in keys) {
                res[s] = res[s] /Convert.ToDouble(words.Count);
            }

            return res;
        }

        // Inverse Documnet Frequency
        public double IDF(string palavra)
        {
            IStemmer stemmer = new EnglishStemmer();
            Tokenizer TK = new Tokenizer();
            int count = 1;
            if (stemm) {
                foreach (string s in documentos)
                {
                    string str =stemmer.Stem(s);
                    if (TK.Tokenize(s).Contains(palavra))
                    {
                        count++;
                    }
                }
            }
            else
            {
                foreach (string s in documentos)
                {
                    if (TK.Tokenize(s).Contains(palavra))
                    {
                        count++;
                    }
                }
            }
            return Math.Log(documentos.Count + 1 / Convert.ToDouble(count));
        }







        private void PolarityAnalysis(List<string> wordsLimpa)
        {
           
            
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
            string str;

            // 41 a 50
            foreach (var w in wordsLimpa)
            {
               
               
                str = EC.procuraPal(w);
                string[] emocoes;
                if (!String.IsNullOrEmpty(str))
                {
                    emocoes = str.Split(';');
                    positive += Convert.ToInt32(emocoes[41]);
                    negative += Convert.ToInt32(emocoes[42]);
                    anger += Convert.ToInt32(emocoes[43]);
                    anticipation += Convert.ToInt32(emocoes[44]);
                    disgust += Convert.ToInt32(emocoes[45]);
                    fear += Convert.ToInt32(emocoes[46]);
                    joy += Convert.ToInt32(emocoes[47]);
                    sadness += Convert.ToInt32(emocoes[48]);
                    suprise += Convert.ToInt32(emocoes[49]);
                    trust += Convert.ToInt32(emocoes[50]);
                    countWords++;
                }
            }
            
            db.insertEmocoes(utilizador,data.Replace('.',':').Replace('-','/'), (positive / countWords) * 100,
                (negative / countWords) * 100,
                (anger / countWords) * 100,
                (anticipation / countWords) * 100,
                (disgust / countWords) * 100,
                (fear / countWords) * 100,
                (joy / countWords) * 100,
                (sadness / countWords) * 100,
                (suprise / countWords) * 100,
                (trust / countWords) * 100);

            Console.WriteLine("Positive : "+(positive/ countWords)*100);
            Console.WriteLine("Negative : "+(negative / countWords)*100);
            Console.WriteLine("Anger : "+(anger / countWords)*100);
            Console.WriteLine("anticipation : " +( anticipation / countWords)*100);
            Console.WriteLine("disgust : " + (disgust / countWords)*100);
            Console.WriteLine("fear : " + (fear / countWords)*100);
            Console.WriteLine("joy : " + (joy / countWords)*100);
            Console.WriteLine("sadness : " +( sadness / countWords)*100);
            Console.WriteLine("suprise : " + (suprise / countWords)*100);
            Console.WriteLine("trust : " + (trust / countWords)*100);

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
            Stopwords.Clear();
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

            List<string> stpWordsRemovidas = removeStopWords();
            calculoTFIDF(stpWordsRemovidas);
            PolarityAnalysis(stpWordsRemovidas);
        }



        private List<string> removeStopWords() {
            List<string> limpaStopWords = new List<string>();
            StringBuilder sb = new StringBuilder();
            IStemmer stemmer = new EnglishStemmer();
            for (int i=0; i< words.Count; i++)
            {
                //Stopwords words 3-gram
                if (i + 2 < words.Count)
                {
                    sb.Clear();
                    if (stemm)
                    {
                        sb.Append(stemmer.Stem(words[i]));
                        sb.Append(' ');
                        sb.Append(stemmer.Stem(words[i + 1]));
                        sb.Append(' ');
                        sb.Append(stemmer.Stem(words[i + 2]));
                    }
                    else {
                        sb.Append(words[i]);
                        sb.Append(' ');
                        sb.Append(words[i + 1]);
                        sb.Append(' ');
                        sb.Append(words[i + 2]);
                    }

                    if (Stopwords.Contains(sb.ToString()))
                    {
                        i = i + 2;
                        continue;
                    }

                }
                //Stopwords words comp 2-gram
                if (i + 1 < words.Count)
                {
                    sb.Clear();
                    if (stemm)
                    {
                        sb.Append(stemmer.Stem(words[i]));
                        sb.Append(' ');
                        sb.Append(stemmer.Stem(words[i + 1]));
                      
                    }
                    else
                    {
                        sb.Append(words[i]);
                        sb.Append(' ');
                        sb.Append(words[i + 1]);

                    }
                    if (Stopwords.Contains(sb.ToString())){
                        i++;
                        continue;
                    }
                }
                // Stopwords 1-gram

                if (stemm)
                {
                    if (!Stopwords.Contains(stemmer.Stem(words[i]))) {
                        limpaStopWords.Add(words[i]);
                    } 
                }
                else {
                    if (!Stopwords.Contains(words[i])) {
                        limpaStopWords.Add(words[i]);
                    }
                }

            }
            return limpaStopWords;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog2.ShowDialog();
            documentos.Clear();

            if (dr == DialogResult.OK)
            {

                // Read the files
                foreach (String file in openFileDialog2.FileNames)
                {
                    string s = File.ReadAllText(@file);
                    s = Regex.Replace(s, @"[^\w\s]", "").ToLower().Replace("\r\n", " ");
                    documentos.Add(s);
                   
                }
            
                MessageBox.Show("Documentos Carregados");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                string[] files = file.Split('\\');
                data = files[files.Length - 1];
                data = data.Remove(data.Length-4);
                text = File.ReadAllText(file);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ut = textBox1.Text;
            if (!db.checkUserFile(ut,data.Replace('.',':').Replace('-','/')))
            {
                MessageBox.Show("Este utilizador não existe", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                Correlacao c = new Correlacao(ut, db);
                c.Show();
            }
        }
    }
}
