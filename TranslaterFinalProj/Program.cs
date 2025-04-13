using System;
using System.Collections.Generic;

namespace TranslaterFinalProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChoiseManager manager = new ChoiseManager(new Dictionary<string, string[]>());

            while (true)
            {
                try
                {
                    Menu menu = new Menu();
                    Dictionary<string, int> choice = menu.StartMenu();
                    manager.ManageChoise(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
