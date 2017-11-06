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
        
        ProcDados pD = new ProcDados();
        Database db = new Database("localhost","mydb","root","");
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string utilizador = textBoxUt.Text;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK) 
            {
                
                string file = openFileDialog1.FileName;
                pD.init(utilizador,file,db);
                MostraResult.Enabled = true;

            }


        }

        private void button2_Click(object sender, EventArgs e)
        { 
            //Analise de eventos Keystroke
            if (VerResult.Text.Equals("Análise de eventos Keystroke"))
            {
                AnaliseKeystroke aKStroke = new AnaliseKeystroke(this, pD);
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
