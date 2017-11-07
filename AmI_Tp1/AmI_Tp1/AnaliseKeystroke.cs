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
    public partial class AnaliseKeystroke : Form
    {

        ReadData rd;
        string utilizador;

        public AnaliseKeystroke(ReadData rd, string utilizador)
        {
            this.rd = rd;
            InitializeComponent();
            this.utilizador = utilizador;
            showTop10.Text = rd.readTop10KeyStrokes(utilizador);
            BackSpaceKey.Text = rd.backspaceCaracter(utilizador) + "%";
        }

        private void AnaliseKeystroke_Load(object sender, EventArgs e)
        {

        }
    }
}
