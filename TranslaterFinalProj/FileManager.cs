using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace TranslaterFinalProj
{
    internal class FileManager
    {
        public void ExportWord(Dictionary<string, string[]> wordTr) 
        {
            string serializedData = JsonSerializer.Serialize(wordTr);
            string filePath = "ExportedWord.json";
            File.AppendAllText(filePath, serializedData);
        }
        public Dictionary<string, Dictionary<string, string[]>> LoadDictionary()
        {
            string jsonString = File.ReadAllText("Dicktionarys.json");
            Dictionary<string, Dictionary<string, string[]>> dictionary = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string[]>>>(jsonString);
            return dictionary;
        }
        public void SaveDictionaries(Dictionary<string, Dictionary<string, string[]>> dictionary)
        {
            string jsonString = JsonSerializer.Serialize(dictionary);
            File.WriteAllText("Dicktionarys.json", jsonString);
        }
    }
}
