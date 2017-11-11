using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IATASentimentalAnalysis
{
    class EmotionalClass
    {
        
        List<string> strLines = File.ReadLines(Path.Combine(Environment.CurrentDirectory, @"Data\",@"NRC.csv")).Skip(1).ToList();

        public string procuraPal(string text)
        {

            foreach (var line in strLines.Skip(1))
            {
                if (line.Split(';')[0].Equals(text))
                    return line;
            }

            return null;
        }
    }
}
