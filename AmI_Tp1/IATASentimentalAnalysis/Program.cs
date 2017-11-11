using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syn.WordNet;


namespace IATASentimentalAnalysis
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.Run(new Form1());
            Console.ReadLine();
        }
    }
}
