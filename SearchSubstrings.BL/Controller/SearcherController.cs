using SearchSubstrings.BL.Model;

namespace SearchSubstrings.BL.Controller
{
    public class SearcherController
    {
        public Searcher Searcher { get; }
        public SearcherController(int startUnicodeIndex, int endUnicodeIndex)
        {
            Searcher searcher = new(startUnicodeIndex, endUnicodeIndex, "", "");
            Searcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
        }

        public Dictionary<string, List<int>> Search(string currentString, string[] substrings, bool caseSensitivity = false, string method = "first", int? count = null)
        {
            Dictionary<string, List<int>> answers = new Dictionary<string, List<int>>();
            int countOfFound = 0;
            string currentStringOld = currentString;

            for (int j = 0; j < substrings.Length; j++)
            {
                string currentSubstring = substrings[j];
                string currentSubstringOld = currentSubstring;
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

                while (i <= currentString.Length && method == "first" && count != 0)
                {
                    if (currentString.Substring(i - currentSubstring.Length, currentSubstring.Length) == currentSubstring)
                    {
                        answers[currentSubstringOld].Add(i - currentSubstring.Length);
                        countOfFound += 1;

                        if (countOfFound >= count)
                        {
                            break;
                        }
                    }
                    i += Searcher.AlphabetTable[char.ConvertToUtf32(currentString[i - 1].ToString(), 0) - Searcher.StartUnicodeIndex];
                }

                while (i >= 0 && method == "last" && count != 0)
                {
                    if (currentString.Substring(i, currentSubstring.Length) ==
                        currentSubstring)
                    {
                        answers[currentSubstringOld].Add(i);
                        countOfFound += 1;

                        if (countOfFound >= count)
                        {
                            break;
                        }
                    }
                    i -= Searcher.AlphabetTable[char.ConvertToUtf32(currentString[i].ToString(), 0) - Searcher.StartUnicodeIndex];
                }
            }
            
            // Наш алгоритм может одновременно искать только одну подстроку, поэтому потом нужно убрать часть найденных подстрок
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
