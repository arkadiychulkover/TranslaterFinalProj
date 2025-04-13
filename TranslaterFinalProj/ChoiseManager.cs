using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TranslaterFinalProj
{
    internal class ChoiseManager
    {
        Translater Translate;
        Menu Men;
        FileManager FileManage;
        Dictionary<string, Dictionary<string, string[]>> AllDickt;
        Dictionary<string, string[]> CurrentDickt;

        public ChoiseManager(Dictionary<string, string[]> dc) 
        {
            FileManage = new FileManager();
            Men = new Menu();
            AllDickt = FileManage.LoadDictionary();
        }

        public void ManageChoise(Dictionary<string, int> choise) 
        {
            if (choise.ContainsKey("Translate"))
            {
                switch (choise["Translate"]) 
                {
                    // Translate from English
                    case 1:
                        string[] translatedWords;
                        string word;
                        AllDickt.TryGetValue("English", out CurrentDickt);
                        Translate = new Translater(CurrentDickt);
                        ShowWordsToTranslate("English");
                        Console.WriteLine("Enter the word to translate:");
                        word = Console.ReadLine().ToLower();
                        translatedWords = Translate.TranslateWord(word);
                        if (translatedWords != null)
                        {
                            Console.WriteLine("Translated words: " + string.Join(", ", translatedWords));
                            Console.WriteLine("Do you want to export the word? (yes/no)");
                            string answer = Console.ReadLine().ToLower();
                            if (answer.ToLower() == "yes")
                            {
                                ExportWord(word, translatedWords);
                            }
                            else
                            {
                                Console.WriteLine("Export canceled.");
                            }
                        }
                        break;
                    // Translate from Ukraine
                    case 2:
                        string[] translatedWords1;
                        string word1;
                        AllDickt.TryGetValue("Ukraine", out CurrentDickt);
                        Translate = new Translater(CurrentDickt);
                        ShowWordsToTranslate("Ukraine");
                        Console.WriteLine("Enter the word to translate:");
                        word1 = Console.ReadLine().ToLower();
                        translatedWords1 = Translate.TranslateWord(word1);
                        if (translatedWords1 != null)
                        {
                            Console.WriteLine("Translated words: " + string.Join(", ", translatedWords1));
                            Console.WriteLine("Do you want to export the word? (yes/no)");
                            string answer = Console.ReadLine().ToLower();
                            if (answer.ToLower() == "yes")
                            {
                                ExportWord(word1, translatedWords1);
                            }
                            else
                            {
                                Console.WriteLine("Export canceled.");
                            }
                        }
                        break;
                    // Translate from other language
                    case 3:
                        string[] translatedWords2;
                        string word2;
                        Console.WriteLine("Choose the language to translate to:");
                        string Lan = Console.ReadLine();
                        AllDickt.TryGetValue(Lan, out CurrentDickt);
                        Translate = new Translater(CurrentDickt);
                        ShowWordsToTranslate(Lan);
                        Console.WriteLine("Enter the word to translate:");
                        word2 = Console.ReadLine().ToLower();
                        translatedWords2 = Translate.TranslateWord(word2);
                        if (translatedWords2 != null)
                        {
                            Console.WriteLine("Translated words: " + string.Join(", ", translatedWords2));
                            Console.WriteLine("Do you want to export the word? (yes/no)");
                            string answer = Console.ReadLine().ToLower();
                            if (answer.ToLower() == "yes")
                            {
                                ExportWord(word2, translatedWords2);
                            }
                            else
                            {
                                Console.WriteLine("Export canceled.");
                            }
                        }
                        break;
                    // Exit
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number.");
                        break;
                }
            }
            else if (choise.ContainsKey("Settings"))
            {
                switch (choise["Settings"]) 
                {
                    // Create dictionary
                    case 1:
                        Dictionary<string, string[]> newDickt = new Dictionary<string, string[]>();
                        Console.WriteLine("Enter the language");
                        string lang = Console.ReadLine();

                        while (true) 
                        {
                            Console.WriteLine("Enter the word or exit");
                            string worde = Console.ReadLine().ToLower();

                            if (worde == "exit")
                            {
                                break;
                            }
                            string[] str = new string[10];
                            for (var i = 0; i > -1; i++) 
                            {
                                Console.WriteLine("Enter Translate");
                                str[i] = Console.ReadLine();
                                if (str[i] == "exit")
                                {
                                    str[i] = null;
                                    break;
                                }
                            }

                            string[] strings = str.Where(s => s != null).ToArray();
                            newDickt.Add(worde, strings);
                            Console.WriteLine("Do you want to add more words? (yes/no)");
                            string answer = Console.ReadLine();

                            if (answer.ToLower() != "yes")
                            {
                                break;
                            }
                        }

                        AllDickt.Add(lang, newDickt);
                        FileManage.SaveDictionaries(AllDickt);
                        break;
                    // Change word or translate
                    case 2:
                        Console.WriteLine("Do you wana change word or translate");
                        string word = Console.ReadLine();
                        Console.WriteLine("Enter the Language");
                        string lang1 = Console.ReadLine();

                        if (word == "word")
                        {
                            ShowWordsToTranslate(lang1);
                            Console.WriteLine("Enter the word");
                            string word1 = Console.ReadLine().ToLower();
                            Console.WriteLine("Enter the new word");
                            string newWord = Console.ReadLine().ToLower();
                            if (AllDickt[lang1].ContainsKey(word1))
                            {
                                string[] translations = AllDickt[lang1][word1];
                                AllDickt[lang1].Remove(word1);
                                AllDickt[lang1].Add(newWord, translations);
                                FileManage.SaveDictionaries(AllDickt);
                            }
                        }
                        else if (word == "translate")
                        {
                            ShowWordsToTranslate(lang1);
                            Console.WriteLine("Enter the word");
                            string word1 = Console.ReadLine().ToLower();
                            if (AllDickt[lang1].ContainsKey(word1))
                            {
                                string[] translations = new string[AllDickt[lang1][word1].Length];
                                for (int i = 0; i < AllDickt[lang1][word1].Length; i++)
                                {
                                    Console.WriteLine("Do you wana change (no - 1, yes - 2) " + AllDickt[lang1][word1][i]);
                                    string answer = Console.ReadLine();
                                    if (answer == "2")
                                    {
                                        Console.WriteLine("Enter the new translate");
                                        string newTranslate = Console.ReadLine();
                                        translations[i] = newTranslate;
                                    }
                                    else
                                        translations[i] = AllDickt[lang1][word1][i];
                                }
                                AllDickt[lang1].Remove(word1);
                                AllDickt[lang1].Add(word1, translations);
                                FileManage.SaveDictionaries(AllDickt);
                            }
                        }

                        break;
                    // Delete word or translate
                    case 3:
                        Console.WriteLine("Do you want to delete word or translate");
                        string word2 = Console.ReadLine();
                        Console.WriteLine("Enter the Language");
                        string lang2 = Console.ReadLine();
                        ShowWordsToTranslate(lang2);
                        if (word2 == "word")
                        {
                            Console.WriteLine("Enter the word");
                            string word1 = Console.ReadLine();
                            if (AllDickt[lang2].ContainsKey(word1))
                            {
                                AllDickt[lang2].Remove(word1);
                                FileManage.SaveDictionaries(AllDickt);
                            }
                        }
                        else if (word2 == "translate")
                        {
                            Console.WriteLine("Enter the word");
                            string word1 = Console.ReadLine();
                            Console.WriteLine("some - 1 translation or all - 2");
                            string choice = Console.ReadLine();
                            if (AllDickt[lang2].ContainsKey(word1))
                            {
                                string[] temp = AllDickt[lang2][word2];
                                if (temp.Length == 1) 
                                {
                                    AllDickt[lang2].Remove(word1);
                                    FileManage.SaveDictionaries(AllDickt);
                                }
                                else if (choice == "1")
                                {
                                    Console.WriteLine("Enter the number of translation");
                                    int num = Convert.ToInt32(Console.ReadLine());
                                    string[] newTemp = new string[temp.Length - 1];
                                    for (int i = 0; i < temp.Length; i++)
                                    {
                                        if (i != num)
                                        {
                                            newTemp[i] = AllDickt[lang2][word1][i];
                                        }
                                    }
                                    AllDickt[lang2].Remove(word1);
                                    AllDickt[lang2].Add(word1, newTemp);
                                }
                                else if (choice == "2")
                                {
                                    string[] newTemp = new string[temp.Length - 1];
                                    for (int i = 0; i < temp.Length; i++)
                                    {
                                        newTemp[i] = temp[i];
                                    }
                                    AllDickt[lang2].Remove(word1);
                                    AllDickt[lang2].Add(word1, newTemp);
                                }
                                FileManage.SaveDictionaries(AllDickt);
                            }
                        }
                        break;
                    // Add word or translate
                    case 4:
                        Dictionary<string, string[]> newDickt1 = new Dictionary<string, string[]>();
                        Console.WriteLine("Enter the language");
                        string lang3 = Console.ReadLine();
                        Console.WriteLine("Add word or translate");
                        string cho = Console.ReadLine();
                        if (cho == "word")
                        {
                            while(true)
{
                                Console.WriteLine("Enter the word or exit");
                                string worde = Console.ReadLine().ToLower();

                                if (worde == "exit")
                                {
                                    break;
                                }
                                string[] str = new string[10];
                                for (var i = 0; i > 10; i++)
                                {
                                    Console.WriteLine("Enter Translate");
                                    str[i] = Console.ReadLine();
                                    if (str[i] == "exit")
                                    {
                                        str[i] = null;
                                        break;
                                    }
                                }

                                string[] strings = str.Where(s => s != null).ToArray();
                                newDickt1.Add(worde, strings);
                                break;
                            }
                        }
                        else if (cho == "translate")
                        {
                            ShowWordsToTranslate(lang3);
                            Console.WriteLine("Enter the word");
                            string word1 = Console.ReadLine().ToLower();
                            if (AllDickt[lang3].ContainsKey(word1))
                            {
                                Console.WriteLine("Enter the number of translation");
                                string[] translations = new string[Convert.ToInt32(Console.ReadLine())];
                                for (int i = 0; i < AllDickt[lang3][word1].Length; i++)
                                {
                                    Console.WriteLine("Enter the new translate");
                                    string newTranslate = Console.ReadLine();
                                    translations[i] = newTranslate;
                                }
                                string[] res = new string[AllDickt[lang3][word1].Length + translations.Length];

                                for (int i = 0; i < AllDickt[lang3][word1].Length; i++)
                                {
                                    res[i] = AllDickt[lang3][word1][i];
                                }
                                for (int i = 0; i < translations.Length; i++)
                                {
                                    res[i + AllDickt[lang3][word1].Length] = translations[i];
                                }

                                AllDickt[lang3][word1] = res;
                                FileManage.SaveDictionaries(AllDickt);
                            }
                        }
                        break;
                    // Back to main menu
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number.");
                        break;
                }
            }
            else if (choise.ContainsKey("Exit"))
            {
                Environment.Exit(0);
            }
        }
        public void ExportWord(string word, string[] translatedWords)
        {
            Dictionary<string, string[]> WordRes = new Dictionary<string, string[]>();
            WordRes.Add(word, translatedWords);
            FileManage.ExportWord(WordRes);
            Console.WriteLine("Word exported successfully.");
        }

        public void ShowLanguages()
        {
            Console.WriteLine("Available languages:");
            foreach (var lang in AllDickt.Keys)
            {
                Console.WriteLine(lang);
            }
        }

        public void ShowWordsToTranslate(string lang)
        {
            Console.WriteLine("Available words in " + lang + ":");
            foreach (var word in AllDickt[lang].Keys)
            {
                Console.WriteLine(word);
            }
        }
    }
}
