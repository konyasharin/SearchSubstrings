using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SearchSubstrings.BL.Model
{
    public class Searcher
    {
        private readonly int[] _alphabetTable;
        public string CurrentString { get; set; }
        private string _currentSubstring = null!;
        private int _startUnicodeIndex;
        private int _endUnicodeIndex;
        public string CurrentSubstring
        {
            get => _currentSubstring;

            set
            {
                _currentSubstring = value;

                for (int i = 0; i < _alphabetTable.Length; i++)
                {
                    _alphabetTable[i] = value.Length;
                }

                for (int j = 0; j < _currentSubstring.Length; j++)
                {
                    int index = char.ConvertToUtf32(_currentSubstring[j].ToString(), 0) - _startUnicodeIndex;
                    if (_alphabetTable[index] == _currentSubstring.Length)
                    {
                        _alphabetTable[index] = _currentSubstring.Length - j - 1;
                        continue;
                    }
                    _alphabetTable[index] = _currentSubstring.Length;
                    _alphabetTable[index] = _currentSubstring.Length - j - 1;
                }

                for (int i = 0; i < _alphabetTable.Length; i++)
                {
                    Console.WriteLine(_alphabetTable[i]);                    
                }
            }
        }

        public Searcher(int startUnicodeIndex, int endUnicodeIndex, string currentString, string currentSubstring)
        {
            _alphabetTable = new int[endUnicodeIndex - startUnicodeIndex + 1];
            _startUnicodeIndex = startUnicodeIndex;
            _endUnicodeIndex = endUnicodeIndex;
            CurrentString = currentString;
            CurrentSubstring = currentSubstring;
        }
}
}
