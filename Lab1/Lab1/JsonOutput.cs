using Newtonsoft.Json;
using System.IO;

namespace Lab1
{
    class JsonOutput
    {
        public static void SaveStudentsGroup(Group group, string name)
        {
            string json = JsonConvert.SerializeObject(group);
            string fullPath = ".\\json\\" + name + ".json";

            try
            {
                File.WriteAllText(fullPath, json);
            }
            catch
            {
                System.Console.WriteLine("Неверное имя или путь к файлу.");
            }
        }
    }
}
