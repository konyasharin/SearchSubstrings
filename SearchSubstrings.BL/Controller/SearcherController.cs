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

            if (method == "last")
            {
                currentString = new string(currentString.Reverse().ToArray());
            }

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
                answers.Add(currentSubstring, new List<int>());

                while (i <= currentString.Length)
                {
                    if (count == 0 || countOfFound >= count)
                    {
                        break;
                    }
                    if (currentString.Substring(i - currentSubstring.Length, currentSubstring.Length) == currentSubstring)
                    {
                        if (method == "last")
                        {
                            answers[currentSubstring].Add(currentString.Length - i);
                        }
                        else
                        {
                            answers[currentSubstring].Add(i - currentSubstring.Length);
                        }
                        countOfFound += 1;

                        if (countOfFound >= count)
                        {
                            break;
                        }
                    }
                    i += Searcher.AlphabetTable[char.ConvertToUtf32(currentString[i - 1].ToString(), 0) - Searcher.StartUnicodeIndex];
                }
            }

            if (method == "last")
            {
                Searcher.CurrentString = new string(Searcher.CurrentString.Reverse().ToArray());
            }
            return answers;
        }
    }
}
