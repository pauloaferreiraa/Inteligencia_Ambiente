using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmI_Tp1
{
    public partial class AnalisePalavras : Form
    {
        Form1 f1;
        ProcDados pD;
        public AnalisePalavras(Form1 f1, ProcDados pD)
        {
            this.pD = pD;
            this.f1 = f1;
            InitializeComponent();
            string ds = pD.AnalisePalavras();
        }

        private void AnalisePalavras_Load(object sender, EventArgs e)
        {

        }
    }
}
