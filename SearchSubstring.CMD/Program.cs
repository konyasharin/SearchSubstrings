using SearchSubstrings.BL.Controller;
using Microsoft.Win32;

namespace SearchSubstring.CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                SearcherController searcherController = new(char.ConvertToUtf32("A".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
                Console.WriteLine("Введите одну из следующих команд (или END):\n" +
                                  "1 - Поиск подстроки в файле и вывод с выделением\n" +
                                  "2 - Поиск подстроки в строке с выделением\n" +
                                  "3 - Поиск подстроки в строке без выделения");
                string? command = Console.ReadLine();
                switch (command)
                {
                    case "1": 
                        OpenFile();
                        break;
                    case "2":
                        Console.WriteLine("Введите строку, в которой будет проводиться поиск: ");
                        string? currentString = Console.ReadLine();
                        string[] currentSubstrings = getSubstrings();
                        if (currentString != null)
                        {
                            Highlight(searcherController, currentString, currentSubstrings);
                        }
                        break;
                }
            }
        }

        public static void Highlight(SearcherController searcherController, string currentString, string[] currentSubstrings)
        {
            Dictionary<string, List<int>> dictionary =
                searcherController.Search(currentString, currentSubstrings, caseSensitivity: false, method: "last", count: 1);

            int i = 0;
            int indexEndOfPaint = 0;
            string checkSubstring = "";
            while (i < searcherController.Searcher.CurrentString.Length)
            {
                bool flag = false;
                foreach (var key in dictionary.Keys)
                {
                    if (dictionary[key].Contains(i))
                    {
                        flag = true;
                        checkSubstring = key;
                        break;
                    }
                }

                if (!flag)
                {
                    if (i < indexEndOfPaint)
                    {
                        i++;
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(searcherController.Searcher.CurrentString[i]);
                        i++;
                        indexEndOfPaint = i;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    for (int j = i; j < i + checkSubstring.Length; j++)
                    {
                        if (indexEndOfPaint <= j)
                        {
                            Console.Write(searcherController.Searcher.CurrentString[j]);
                            indexEndOfPaint++;
                        }
                    }
                    i += 1;
                }
            }

            Console.ResetColor();
        }

        public static string OpenFile()
        {
            Console.WriteLine("Введите полный путь до файла(.txt): ");
            string filePath = Console.ReadLine();
            return "";
        }

        public static string[] getSubstrings()
        {
            Console.WriteLine("Введите количество подстрок, которое вы хотите искать: ");
            int countSubstrings = int.Parse(Console.ReadLine());
            string[] currentSubstrings = new string[countSubstrings];
            for (int i = 0; i < countSubstrings; i++)
            {
                Console.WriteLine(string.Concat("Введите подстроку #", i + 1, ": "));
                currentSubstrings[i] = Console.ReadLine();
            }
            return currentSubstrings;
        }

        public static Dictionary<string, string> getFlags()
        {
            Console.WriteLine("Выберите одну из команд: ");
            return new Dictionary<string, string>();
        }
    }
}