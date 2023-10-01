using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Search(string currentString, string currentSubstring, bool caseSensitivity = false, string method = "first", int? count = null)
        {
            if (!caseSensitivity)
            {
                Searcher.CurrentSubstring = currentSubstring.ToLower();
                Searcher.CurrentString = currentString.ToLower();
            }
            else
            {
                Searcher.CurrentSubstring = currentSubstring;
                Searcher.CurrentString = currentString;
            }
        }
    }
}
