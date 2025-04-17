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
                        Console.WriteLine("Choose the language to translate from:");
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
                                Console.WriteLine("Enter Translate or exit");
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
                                foreach (var it in AllDickt[lang1][word1])
                                {
                                    Console.WriteLine($"- {it}");
                                }
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
                        Console.WriteLine("word or translate");
                        string action = Console.ReadLine().ToLower();

                        Console.WriteLine("Language:");
                        string lang2 = Console.ReadLine();

                        if (!AllDickt.ContainsKey(lang2))
                        {
                            Console.WriteLine("No language.");
                            break;
                        }

                        Console.WriteLine("Word:");
                        string word2 = Console.ReadLine().ToLower();

                        if (!AllDickt[lang2].ContainsKey(word2))
                        {
                            Console.WriteLine("Word not found.");
                            break;
                        }

                        if (action == "word")
                        {
                            AllDickt[lang2].Remove(word2);
                            Console.WriteLine("Deleted");
                        }
                        else if (action == "translate")
                        {
                            string[] translations = AllDickt[lang2][word2];

                            if (translations.Length == 1)
                            {
                                Console.WriteLine("Its Last translatoin so you cant delete it");
                            }
                            else
                            {
                                Console.WriteLine("translations: ");
                                foreach (var t in translations)
                                    Console.WriteLine($"- {t}");

                                Console.WriteLine("Enter translation to delete: ");
                                string toDelete = Console.ReadLine();

                                string[] updated = translations.Where(t => t != toDelete).ToArray();

                                if (updated.Length == translations.Length)
                                {
                                    Console.WriteLine("Translation not found");
                                }
                                else
                                {
                                    AllDickt[lang2][word2] = updated;
                                    Console.WriteLine("Deleted");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice");
                        }

                        FileManage.SaveDictionaries(AllDickt);
                        break;
                    // Add word or translate
                    case 4:
                        Console.WriteLine("Enter Language:");
                        string lang3 = Console.ReadLine();

                        if (!AllDickt.ContainsKey(lang3))
                        {
                            AllDickt[lang3] = new Dictionary<string, string[]>();
                        }

                        Console.WriteLine("word or translate");
                        string choice = Console.ReadLine().ToLower();

                        if (choice == "word")
                        {
                            while (true)
                            {
                                Console.WriteLine("word or exit:");
                                string word4 = Console.ReadLine().ToLower();
                                if (AllDickt[lang3].ContainsKey(word4))
                                {
                                    Console.WriteLine("word already exists");
                                    continue;
                                }

                                if (word4 == "exit") break;

                                List<string> translations = new List<string>();
                                while (true)
                                {
                                    Console.WriteLine("Translate or exit:");
                                    string translate = Console.ReadLine();

                                    if (translate == "exit")
                                        break;
                                    
                                    translations.Add(translate);
                                }
                                AllDickt[lang3].Add(word4, translations.ToArray());
                                Console.WriteLine("Sdded");
                            }

                            FileManage.SaveDictionaries(AllDickt);
                        }
                        else if (choice == "translate")
                        {
                            ShowWordsToTranslate(lang3);

                            Console.WriteLine("word:");
                            string word4 = Console.ReadLine().ToLower();

                            if (AllDickt[lang3].ContainsKey(word4))
                            {
                                Console.WriteLine("translatoin count");
                                if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                                {
                                    List<string> newTranslations = new List<string>();
                                    for (int i = 0; i < count; i++)
                                    {
                                        Console.WriteLine("translation: ");
                                        newTranslations.Add(Console.ReadLine());
                                    }

                                    string[] existing = AllDickt[lang3][word4];
                                    int l = existing.Length + newTranslations.ToArray().Length;
                                    string[] newArr = new string[l];
                                    existing.CopyTo(newArr, 0);
                                    newTranslations.ToArray().CopyTo(newArr, existing.Length);
                                    AllDickt[lang3].Remove(word4);
                                    AllDickt[lang3].Add(word4, newArr);

                                    FileManage.SaveDictionaries(AllDickt);
                                    Console.WriteLine("Added");
                                }
                                else
                                {
                                    Console.WriteLine("incorect num");
                                }
                            }
                            else
                            {
                                Console.WriteLine("word not found");
                            }
                        }
                        else
                        {
                            Console.WriteLine("wrong choise.");
                        }
                        break;
                    // Back to main menu
                    case 5:
                        break;
                    // Invalid
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
