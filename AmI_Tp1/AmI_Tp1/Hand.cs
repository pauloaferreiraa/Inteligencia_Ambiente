using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmI_Tp1
{
    class Hand
    {


        List<string> right = new List<string>();
        List<string> left = new List<string>();
        List<string> limitadores = new List<string>();

        public Hand()
        {
            left.Add("Q"); left.Add("W"); left.Add("E"); left.Add("R"); left.Add("A"); left.Add("S"); left.Add("D");
            left.Add("Z"); left.Add("X"); left.Add("C"); left.Add("V"); left.Add("F"); left.Add("G"); left.Add("OemBackslash");

            right.Add("T"); right.Add("Y"); right.Add("U"); right.Add("I"); right.Add("O"); right.Add("P"); right.Add("H");
            right.Add("J"); right.Add("K"); right.Add("L"); right.Add("B"); right.Add("N"); right.Add("M"); right.Add("Enter");
            right.Add("Anterior"); right.Add("Oemtilde"); right.Add("Oemcomma"); right.Add("OemPeriod"); right.Add("OemMinus");
            right.Add("Oemplus"); right.Add("Oem7"); right.Add("OemOpenBrackets");

            limitadores.Add("Space"); limitadores.Add("Oemcomma"); limitadores.Add("OemPeriod"); limitadores.Add("Enter");
            limitadores.Add("OemOpenBrackets");// ponto de Interrogação
        }

        public bool pertenceRight(string s)
        {
            return right.Contains(s);
        }

        public bool pertenceLeft(string s)
        {
            return left.Contains(s);
        }

        public bool pertenceLimit(string s)
        {
            return limitadores.Contains(s);
        }

    }
}
