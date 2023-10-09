using SearchSubstrings.BL.Model;

namespace SearchSubstrings.BL.Controller
{
    /// <summary>
    /// Контроллер для модели Searcher(здесь и содержится
    /// алгоритм Бойера-Мура-Хорспула(для нашего случая она усовершенствована для
    /// возможности поиска множества подстрок и поиска с конца)).
    /// </summary>
    public class SearcherController
    {
        /// <summary>
        /// Свойство Searcher(есть геттер).
        /// </summary>
        public Searcher Searcher { get; }
        /// <summary>
        /// Конструктор данного класса.
        /// </summary>
        /// <param name="startUnicodeIndex">Индекс первого символа алфавита в юникод.</param>
        /// <param name="endUnicodeIndex">Индекс последнего символа алфавита в юникод.</param>
        /// <exception cref="ArgumentNullException">если searcher null, то вызывается данная ошибка.</exception>
        public SearcherController(int startUnicodeIndex, int endUnicodeIndex)
        {
            Searcher searcher = new(startUnicodeIndex, endUnicodeIndex, "", "");
            Searcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
        }

        /// <summary>
        /// Алгоритм Бойера-Мура-Хорспула для поиска подстрок в строке.
        /// </summary>
        /// <param name="currentString">Текущая строка, в которой проводится поиск.</param>
        /// <param name="substrings">Подстроки, которые мы будем искать.</param>
        /// <param name="caseSensitivity">Чувствительность к регистру.</param>
        /// <param name="method">Поиск с начала(first) или с конца(last).</param>
        /// <param name="count">Количество первых вхождений.</param>
        /// <returns>Словарь с парами "подстрока" - "массив индексов"(с которых начинается данная подстрока).</returns>
        public Dictionary<string, List<int>> Search(string currentString, string[] substrings, bool caseSensitivity = false, string method = "first", int? count = null)
        {
            Dictionary<string, List<int>> answers = new Dictionary<string, List<int>>();
            string currentStringOld = currentString;
            // проходимся по всем подстрокам и выполняем для каждой из них наш алгоритм
            // (нельзя в нашем алгоритме искать сразу все подстроки, только по очереди)
            for (int j = 0; j < substrings.Length; j++)
            {
                string currentSubstring = substrings[j];
                string currentSubstringOld = currentSubstring;
                // если не учитываем регистр, то строку и подстроки ставим в нижний регистр
                if (!caseSensitivity)
                {
                    Searcher.CurrentSubstring = currentSubstringOld;
                    Searcher.CurrentString = currentStringOld;
                    currentSubstring = currentSubstring.ToLower();
                    currentString = currentString.ToLower();
                }
                else
                {
                    Searcher.CurrentSubstring = currentSubstring;
                    Searcher.CurrentString = currentString;
                }

                int i = currentSubstring.Length;
                if (method == "last")
                {
                    i = currentString.Length - currentSubstring.Length;
                }
                answers.Add(currentSubstringOld, new List<int>());
                // алгоритм поиска с начала строки
                while (i <= currentString.Length && method == "first" && count != 0)
                {
                    if (currentString.Substring(i - currentSubstring.Length, currentSubstring.Length) == currentSubstring)
                    {
                        answers[currentSubstringOld].Add(i - currentSubstring.Length);
                    }
                    i += Searcher.AlphabetTable[char.ConvertToUtf32(currentString[i - 1].ToString(), 0) - Searcher.StartUnicodeIndex];
                }
                // алгоритм поиска с конца строки
                while (i >= 0 && method == "last" && count != 0)
                {
                    if (currentString.Substring(i, currentSubstring.Length) ==
                        currentSubstring)
                    {
                        answers[currentSubstringOld].Add(i);
                    }
                    i -= Searcher.AlphabetTable[char.ConvertToUtf32(currentString[i].ToString(), 0) - Searcher.StartUnicodeIndex];
                }
            }

            /*
            Если у нас есть ограничение на количество вхождений, то применяем следующий алгоритм для отбрасывания части найденных подстрок:
            Например: у нас count = 2, а был получен словарь { "a": [1, 3], "b": [2] }, чтобы получить словарь { "a": [1], "b": [2] } мы
            берем все массивы индексов из словаря и объединяем их в один массив и сортируем по возрастанию (если method = "first") или
            по убыванию (если method = "last"). В нашем случаем method = "first", поэтому получится массив [1, 2, 3], а дальше мы идем
            через for по нему до индекса = count и добавляем в новый словарь элементы из старого словаря, искав в нем первые 2 элемента
            нашего массива.
            */
            if (count > 0)
            {
                List<int> cutAnswers = new List<int>();
                Dictionary<string, List<int>> newAnswersDict = new Dictionary<string, List<int>>();
                foreach (string key in answers.Keys)
                {
                    newAnswersDict[key] = new List<int>();
                    for (int i = 0; i < answers[key].Count; i++)
                    {
                        cutAnswers.Add(answers[key][i]);
                    }
                }
                cutAnswers.Sort();
                if (method == "last")
                {
                    cutAnswers.Reverse();
                }
                while (cutAnswers.Count > count)
                {
                    cutAnswers.RemoveAt(cutAnswers.Count - 1);
                }

                for (int i = 0; i < cutAnswers.Count; i++)
                {
                    foreach (string key in answers.Keys)
                    {
                        if (answers[key].Contains(cutAnswers[i]))
                        {
                            newAnswersDict[key].Add(cutAnswers[i]);
                            break;
                        }
                    }
                }

                answers = newAnswersDict;
            }
            return answers;
        }
    }
}
