using SearchSubstrings.BL.Controller;

namespace SearchSubstring.CMD
{
    /// <summary>
    /// CMD приложение.
    /// </summary>
    internal class Program
    {
        #region Variables

        /// <summary>
        /// чувствительность к регистру.
        /// </summary>
        private static bool _caseSensitivity;
        /// <summary>
        /// Поиск с начала строки(first) или с конца(last).
        /// </summary>
        private static string? _method;
        /// <summary>
        /// Количество первых вхождений.
        /// </summary>
        private static int? _count;
        /// <summary>
        /// Флаг активности программы.
        /// </summary>
        private static bool _programIsActive = true;
        /// <summary>
        /// Контроллер для Searcher, также здесь мы говорим, что будем использовать английский алфавит.
        /// </summary>
        private static readonly SearcherController SearcherController = new(char.ConvertToUtf32("A", 0), char.ConvertToUtf32("z", 0));

        #endregion
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Внимание! Вы можете использовать в качестве символов строк и подстрок только символы английского алфавита(верхний и нижний регистры)!");
            while (Program._programIsActive)
            {
                Console.WriteLine("\nВведите одну из следующих команд (или END):\n" +
                                  "1 - Поиск подстроки в файле(первые 10 строк) и вывод с выделением\n" +
                                  "2 - Поиск подстроки в файле(первые 10 строк) и вывод без выделения\n" +
                                  "3 - Поиск подстроки в строке с выделением\n" +
                                  "4 - Поиск подстроки в строке без выделения");
                string? command = Console.ReadLine();
                string? currentString;
                switch (command)
                {
                    case "1": 
                        currentString = OpenFile();
                        if (currentString == null)
                        {
                            break;
                        }
                        GetStartParamsAndHighlight(currentString);
                        break;
                    case "2":
                        currentString = OpenFile();
                        if (currentString == null)
                        {
                            break;
                        }
                        GetStartParamsAndHighlight(currentString, isHighlight: false);
                        break;
                    case "3":
                        Console.WriteLine("Введите строку, в которой будет проводиться поиск: ");
                        currentString = Console.ReadLine();
                        GetStartParamsAndHighlight(currentString!);
                        break;
                    case "4":
                        Console.WriteLine("Введите строку, в которой будет проводиться поиск: ");
                        currentString = Console.ReadLine();
                        GetStartParamsAndHighlight(currentString!, isHighlight: false);
                        break;
                    case "END":
                        Program._programIsActive = false;
                        Console.WriteLine("Работа программы успешно завершена!");
                        break;
                    default:
                        Console.WriteLine("Вы ввели неверную команду!");
                        break;
                }
            }
        }

        /// <summary>
        /// Получает команды от пользователя(+возможен запуск выделения цветом).
        /// </summary>
        /// <param name="currentString">Текущая строка, в которой будет происходить поиск.</param>
        /// <param name="isHighlight">Показывает, нужно ли выделение цветом.</param>
        public static void GetStartParamsAndHighlight(string currentString, bool isHighlight = true)
        {
            if (currentString == "")
            {
                Console.WriteLine("Ошибка! Пустая строка!");
                return;
            }
            if (!IsEnglishString(currentString))
            {
                Console.WriteLine("Ошибка! Вы ввели символы не из английского алфавита!");
                return;
            }
            string[]? currentSubstrings = GetSubstrings();
            if (currentSubstrings == null)
            {
                return;
            }
            GetFlags();
            if (isHighlight)
            {
                Highlight(currentString, currentSubstrings);
            }
            else
            {
                OutputIndexes(currentString, currentSubstrings);
            }
            
        }

        /// <summary>
        /// Выделяет подстроки в консоли цветом.
        /// В языке C# нельзя просто выделить цветом символы по индексам, поэтому мы идем по всей строке
        /// до конца, и смотрим, есть ли позиция данного символа в словаре с найденными позициями искомых подстрок.
        /// Если есть, то мы переключаем цвет и выводим символы подстроки, дальше сбрасываем цвет.
        /// </summary>
        /// <param name="currentString">Строка, в которой будет происходить выделение</param>
        /// <param name="currentSubstrings"></param>
        public static void Highlight(string currentString, string[] currentSubstrings)
        {
            Dictionary<string, List<int>> dictionary =
                Program.SearcherController.Search(currentString, currentSubstrings, caseSensitivity: Program._caseSensitivity, method: Program._method!, count: Program._count);

            int i = 0;
            int indexEndOfPaint = 0;
            string checkSubstring = "";
            while (i < SearcherController.Searcher.CurrentString.Length)
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
                        Console.Write(SearcherController.Searcher.CurrentString[i]);
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
                            Console.Write(SearcherController.Searcher.CurrentString[j]);
                            indexEndOfPaint++;
                        }
                    }
                    i += 1;
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Вывод найденных позиций подстрок без выделения цветом.
        /// </summary>
        /// <param name="currentString">Текущая строка в которой происходил поиск.</param>
        /// <param name="currentSubstrings">Подстроки, которые мы искали.</param>
        public static void OutputIndexes(string currentString, string[] currentSubstrings)
        {
            Dictionary<string, List<int>> dictionary =
                Program.SearcherController.Search(currentString, currentSubstrings, caseSensitivity: Program._caseSensitivity, method: Program._method!, count: Program._count);
            foreach (var elem in dictionary)
            {
                Console.Write($"{elem.Key}: (");
                foreach (var elemList in elem.Value)
                {
                    Console.Write($"{elemList}, ");
                }
                Console.WriteLine(")\n");
            }
        }

        /// <summary>
        /// Открытие файла со строкой, в которой мы будем производит поиск.
        /// </summary>
        /// <returns>Первые 10 строк txt - файла в виде строки.</returns>
        public static string? OpenFile()
        {
            string str = "";
            Console.WriteLine("Введите полный путь до файла(.txt): ");
            string filePath = Console.ReadLine()!;
            if (filePath.Substring(filePath.Length - 4, 4) != ".txt")
            {
                Console.WriteLine("Ошибка! Вы открываете не .txt файл!");
                return null;
            }

            try
            {
                var lines = File.ReadLines(filePath).Take(10);
                foreach (var line in lines)
                {
                    str = string.Concat(str, line);
                }
            }
            catch
            {
                Console.WriteLine("Ошибка при открытии файла!");
                return null;
            }
            return str;
        }

        /// <summary>
        /// Получение подстрок от пользователя.
        /// </summary>
        /// <returns>Массив полученных подстрок (или null если ошибка).</returns>
        public static string[]? GetSubstrings()
        {
            Console.WriteLine("Введите количество подстрок, которое вы хотите искать: ");
            int countSubstrings;
            string[] currentSubstrings;
            try
            {
                countSubstrings = int.Parse(Console.ReadLine()!);
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверное число");
                return null;
            }

            if (countSubstrings <= 0)
            {
                Console.WriteLine("Ошибка! Неверное количество подстрок(должно быть больше нуля!)");
                return null;
            }
            currentSubstrings = new string[countSubstrings];
            for (int i = 0; i < countSubstrings; i++)
            {
                Console.WriteLine(string.Concat("Введите подстроку #", i + 1, ": "));
                currentSubstrings[i] = Console.ReadLine()!;
                if (!IsEnglishString(currentSubstrings[i]) || currentSubstrings[i] == "")
                {
                    Console.WriteLine("Вы ввели символ не из английского алфавита!");
                    return null;
                }
            }
            return currentSubstrings;
        }

        /// <summary>
        /// Получение команд (флагов) от пользователя.
        /// </summary>
        public static void GetFlags()
        {
            string? command;
            Console.WriteLine("Выберите одну из команд: \n" +
                              "1 - Включить чувствительность к регистру\n" +
                              "2 - Отключить чувствительность к регистру");
            command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    Program._caseSensitivity = true;
                    break;
                case "2":
                    Program._caseSensitivity = false;
                    break;
                default:
                    Console.WriteLine("Вы ввели неверную команду!");
                    GetFlags();
                    return;
            }

            Console.WriteLine("Выберите одну из команд: \n" +
                              "1 - Поиск с начала строки\n" +
                              "2 - Поиск с конца строки");
            command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    Program._method = "first";
                    break;
                case "2":
                    Program._method = "last";
                    break;
                default:
                    Console.WriteLine("Вы ввели неверную команду!");
                    GetFlags();
                    return;
            }

            Console.WriteLine("Введите количество первых вхождений(или 0, если хотите не ограничивать): ");
            try
            {
                Program._count = int.Parse(Console.ReadLine()!);
                if (Program._count < 0)
                {
                    Console.WriteLine("Количество первых вхождений должно быть больше нуля(или введите 0)!");
                    GetFlags();
                    return;
                }

                if (Program._count == 0)
                {
                    Program._count = null;
                }
            }
            catch
            {
                Console.WriteLine("Неверный формат ввода!");
                GetFlags();
                return;
            }
        }

        /// <summary>
        /// Проверяет, строка состоит только из символов английского алфавита или нет.
        /// </summary>
        /// <param name="str">Строка, которую проверяем на наличие символов не из английского алфавита.</param>
        /// <returns>true - строка состоит только из символов английского алфавита, иначе - false.</returns>
        public static bool IsEnglishString(string str)
        {
            bool isEnglish = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.ConvertToUtf32(str[i].ToString(), 0) < char.ConvertToUtf32("A", 0) ||
                    char.ConvertToUtf32(str[i].ToString(), 0) > char.ConvertToUtf32("z", 0))
                {
                    isEnglish = false; 
                    break;
                }
            }
            return isEnglish;
        }
    }
}
