using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.Statistics;
namespace IATASentimentalAnalysis
{
    public partial class Correlacao : Form
    {
        List<string> colunas = new List<string>(new[] {" ","Keystroke Backspace", "Palavra backSpace","Média da Latência de palavra", "desvio da Latência de palavra",
                                                        "média digraph","desvio digraph","Positivo","Negativo","Anger","Anticipation","Disgust","Fear","Joy",
                                                        "Sadness","Surprise","Trust" });
        Database db;
        ReadData rd;
        string utilizador;
        public Correlacao(string utilizador,Database db)
        {
            this.utilizador = utilizador;
            this.db = db;
            rd = new ReadData(utilizador,db);
            InitializeComponent();
            showCorrelations();
        }
        private void showCorrelations()
        {

            dataGridView1.ColumnCount = colunas.Count;
            //inserir primeira linha com o nome das metricas
            for (int i= 0;i<colunas.Count;i++)
            {
                dataGridView1.Columns[i].Name = colunas[i];
            }
            string[] row = new string[colunas.Count];

            //inserir restantes linhas 
            for (int i = 1; i < colunas.Count; i++) {

                double[] valor = rd.getInfo(utilizador,colunas[i]);
                row[0] = colunas[i];

                for (int j = 1; j < colunas.Count; j++) {
                    row[j] = Correlation.Pearson(valor, rd.getInfo(utilizador, colunas[j])).ToString();
                }

                dataGridView1.Rows.Add(row);
                
            }
            
        }

    }
}
