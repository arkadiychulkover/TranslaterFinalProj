using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslaterFinalProj
{
    internal class Menu : IMenu
    {
        public Dictionary<string, int> StartMenu()
        {
            Console.WriteLine("1. Translate");
            Console.WriteLine("2. Settings");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option (format: 1): ");
            string input = Console.ReadLine();
            int choice;
            Dictionary<string, int> dc = new Dictionary<string, int>();
            string punct;
            int ch;
            while (true)
            {
                switch (input)
                {
                    case "1":
                        punct = "Translate";
                        ch = ChooseTransLanguage();
                        dc.Add(punct, ch);
                        return dc;
                    case "2":
                        punct = "Settings";
                        ch = Settings();
                        dc.Add(punct, ch);
                        return dc;
                    case "3":
                        punct = "Exit";
                        ch = 0;
                        dc.Add(punct, ch);
                        return dc;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number.");
                        input = Console.ReadLine();
                        break;
                }
            }
        }
        public int Settings()
        {
            Console.WriteLine("Settings");
            Console.WriteLine("1. Create dictionary");
            Console.WriteLine("2. Change Word");
            Console.WriteLine("3. Delete Word");
            Console.WriteLine("4. Add Word");
            Console.WriteLine("5. Back to Main manu");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();
            int choice;
            while (true)
            {
                switch (input)
                {
                    case "1":
                        return 1;
                        break;
                    case "2":
                        return 2;
                        break;
                    case "3":
                        return 3;
                        break;
                    case "4":
                        return 4;
                        break;
                    case "5":
                        return 5;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number.");
                        input = Console.ReadLine();
                        break;
                }
            }
        }
        public int ChooseTransLanguage()
        {
            Console.WriteLine("Choose translation language");
            Console.WriteLine("1. English to Ukranian");
            Console.WriteLine("2. Ukraine to English");
            Console.WriteLine("3. Custom");
            Console.WriteLine("4. Back to main menu");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();
            int choice;
            while (true) 
            {
                switch (input)
                {
                    case "1":
                        return 1;
                        break;
                    case "2":
                        return 2;
                        break;
                    case "3":
                        return 3;
                        break;
                    case "4":
                        return 4;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number.");
                        input = Console.ReadLine();
                        break;
                }
            }
        }
    }
}
