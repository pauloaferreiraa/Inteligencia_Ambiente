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
    public partial class AnaliseDigraph : Form
    {
        ReadData rd;
        string utilizador;

        public AnaliseDigraph(ReadData rd,string utilizador)
        {
            this.rd = rd;
            this.utilizador = utilizador;
            
            InitializeComponent();
            //digraphTB.Text = rd.
            string st = rd.writingTime(utilizador);
            string[] split = st.Split(' ');
            string media = split[0];
            string dp = split[1];
            writingTimeMediaTB.Text = media;
            writingTimeDPTB.Text = dp;
        }
        
    }
}
