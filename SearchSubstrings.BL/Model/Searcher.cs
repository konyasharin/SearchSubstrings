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
        public int[] AlphabetTable;
        public string CurrentString { get; set; }
        private string _currentSubstring = null!;
        public int StartUnicodeIndex;
        private int _endUnicodeIndex;
        public string CurrentSubstring
        {
            get => _currentSubstring;

            set
            {
                _currentSubstring = value;

                for (int i = 0; i < AlphabetTable.Length; i++)
                {
                    AlphabetTable[i] = value.Length;
                }

                for (int j = 0; j < _currentSubstring.Length; j++)
                {
                    int index = char.ConvertToUtf32(_currentSubstring[j].ToString(), 0) - StartUnicodeIndex;
                    if (j == _currentSubstring.Length - 1) // последнему символу в таблице ставим 1 для того чтобы поиск продолжался после обнаружения первой подстроки
                    {
                        AlphabetTable[char.ConvertToUtf32(_currentSubstring[j].ToString(), 0) - StartUnicodeIndex] = 1;
                        break;
                    }
                    if (AlphabetTable[index] == _currentSubstring.Length)
                    {
                        AlphabetTable[index] = _currentSubstring.Length - j - 1;
                        continue;
                    }
                    AlphabetTable[index] = _currentSubstring.Length;
                    AlphabetTable[index] = _currentSubstring.Length - j - 1;
                }

                /*
                for (int i = 0; i < AlphabetTable.Length; i++)
                {
                    Console.WriteLine(AlphabetTable[i]);
                }
                */
            }
        }

        public Searcher(int startUnicodeIndex, int endUnicodeIndex, string currentString, string currentSubstring)
        {
            AlphabetTable = new int[endUnicodeIndex - startUnicodeIndex + 1];
            StartUnicodeIndex = startUnicodeIndex;
            _endUnicodeIndex = endUnicodeIndex;
            CurrentString = currentString;
            CurrentSubstring = currentSubstring;
        }
}
}
