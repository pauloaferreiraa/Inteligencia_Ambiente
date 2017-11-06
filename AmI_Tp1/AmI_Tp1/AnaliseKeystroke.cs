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
        
        Form1 f1;
        ProcDados pD;
        public AnaliseKeystroke(Form1 f1,ProcDados pD)
        {
            this.pD = pD;
            this.f1 = f1;
            InitializeComponent();
           
            showTop10.Text = pD.top10Keystrokes();
            BackSpaceKey.Text = pD.nBackSpaces().ToString()+"%";
        }

       
    }
}
