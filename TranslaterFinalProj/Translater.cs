using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslaterFinalProj
{
    internal class Translater
    {
        Dictionary<string, string[]> Dickt;
        public Translater(Dictionary<string, string[]> dc)
        {
            Dickt = dc;
        }
        public string[] TranslateWord(string word)
        {
            string lowerWord = word.ToLower();
            if (Dickt.ContainsKey(lowerWord))
            {
                return Dickt[lowerWord];
            }
            else
            {
                Console.WriteLine("Word not found in dictionary.");
                return null;
            }
        }
    }
}
