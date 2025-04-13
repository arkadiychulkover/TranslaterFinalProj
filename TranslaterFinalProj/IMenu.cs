using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslaterFinalProj
{
    internal interface IMenu
    {
        public Dictionary<string, int> StartMenu();
        public int Settings();
        public int ChooseTransLanguage();
    }
}
