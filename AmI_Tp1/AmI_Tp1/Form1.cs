using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmI_Tp1
{
    public partial class Form1 : Form
    {
        private string User;
        private ReadData rd;
        Database db = new Database("localhost","mydb","root","SLpaulO25");
        public Form1()
        {
            db.connect();
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcDados pd = new ProcDados();
            string utilizador = textBoxUt.Text;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK) 
            {
                User = utilizador;
                rd = new ReadData(User,db);
                string file = openFileDialog1.FileName;
                pd.init(utilizador,file,db);
                MostraResult.Enabled = true;

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Analise de eventos Keystroke
            if (VerResult.Text.Equals("Análise de eventos Keystroke"))
            {
                AnaliseKeystroke aKStroke = new AnaliseKeystroke(rd,User);
                aKStroke.Show();
            }
            //Analise de eventos Digraph
            if (VerResult.Text.Equals("Análise de eventos Digraph"))
            {
                AnaliseDigraph aD = new AnaliseDigraph();
                aD.Show();
            }
            //Analise de eventos de Palavras
            if (VerResult.Text.Equals("Análise de eventos de Palavras"))
            {
                AnalisePalavras aP = new AnalisePalavras();
                aP.Show();
            }
        }

        

    }
}
