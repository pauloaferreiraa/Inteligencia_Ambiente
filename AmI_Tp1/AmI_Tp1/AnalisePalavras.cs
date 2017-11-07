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
        ReadData rd;
        string utilizador;
        public AnalisePalavras(ReadData rd, string utilizador)
        {
            this.rd = rd;
            this.utilizador = utilizador;
            InitializeComponent();
            showTopWords10TB.Text = rd.top10Words(utilizador);
            backspaceCorrigidasTB.Text = rd.backspaceCorrigidas(utilizador);
            BackSpaceKeyTB.Text = rd.backspacePalavras(utilizador);
            string med_dp = rd.latenciaPal(utilizador);
            string[] spl = med_dp.Split(' ');
            latenciaPalMediaTB.Text = spl[0];
            latenciaPalDPTB.Text = spl[1];
            latenciaTamTB.Text = rd.latenciaTamanho(utilizador);
        }
    }
}
