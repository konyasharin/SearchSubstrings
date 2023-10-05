using SearchSubstrings.BL.Controller;
using Microsoft.Win32;

namespace SearchSubstring.CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SearcherController searcherController = new(char.ConvertToUtf32("A".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
            Highlight(searcherController);
        }

        public static void Highlight(SearcherController searcherController)
        {
            Dictionary<string, List<int>> dictionary =
                searcherController.Search("aAbCbccaabc", new String[2] { "abc", "bc" }, caseSensitivity: false, method: "last", count: 1);

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
    }
}