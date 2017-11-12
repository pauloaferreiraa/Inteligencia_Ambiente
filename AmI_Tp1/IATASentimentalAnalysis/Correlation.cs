using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IATASentimentalAnalysis
{
    public partial class Correlation : Form
    {
        List<string> colunas = new List<string>(new[] {" ","Keystroke Backspace", "Palavra backSpace","Média da Latência de palavra", "desvio da Latência de palavra",
                                                        "média digraph","desvio digraph","Positivo","Negativo","Anger","Anticipation","Disgust","Fear","Joy",
                                                        "Sadness","Surprise","Trust" });
        Database db;
        string utilizador;
        public Correlation(string utilizador,Database db)
        {
            this.utilizador = utilizador;
            this.db = db;
            InitializeComponent();
            showCorrelations();
        }
        private void showCorrelations()
        {
            dataGridView1.ColumnCount = colunas.Count;
            for (int i= 0;i<colunas.Count;i++)
            {
                dataGridView1.Columns[i].Name = colunas[i];
            }

            for (int i = 1; i < colunas.Count; i++) {
                
                string[] row = new string[] { colunas[i], "Product 1", "1000" };
                dataGridView1.Rows.Add(row);
            }
            
        }

    }
}
