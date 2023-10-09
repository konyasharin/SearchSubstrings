namespace SearchSubstrings.BL.Model
{
    /// <summary>
    /// Модель для SearcherContoller.
    /// </summary>
    public class Searcher
    {
        #region Variables

        /// <summary>
        /// Таблица алфавита, который задействуется в поиске(массив с индексами).
        /// </summary>
        public int[] AlphabetTable;
        /// <summary>
        /// Текущая строка, в которой производится поиск.
        /// </summary>
        public string CurrentString { get; set; }
        /// <summary>
        /// Текущая подстрока которую ищем.
        /// </summary>
        private string _currentSubstring = null!;
        /// <summary>
        /// Индекс юникод-символа, с которого начинается наш алфавит.
        /// </summary>
        public int StartUnicodeIndex;

        #endregion
        #region CurrentSubstring set and get
        
        /// <summary>
        /// Свойство CurrentSubstring, где мы объявляем геттеры и сеттеры для _currentSubstring.
        /// </summary>
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
            }
        }
        #endregion
        /// <summary>
        /// Конструктор для данного класса.
        /// </summary>
        /// <param name="startUnicodeIndex">Индекс первого символа алфавита в юникод.</param>
        /// <param name="endUnicodeIndex">Индекс последнего символа алфавита в юникод.</param>
        /// <param name="currentString">Текущая строка в которой происходит поиск.</param>
        /// <param name="currentSubstring">Текущая подстрока которую ищем.</param>
        public Searcher(int startUnicodeIndex, int endUnicodeIndex, string currentString, string currentSubstring)
        {
            AlphabetTable = new int[endUnicodeIndex - startUnicodeIndex + 1];
            StartUnicodeIndex = startUnicodeIndex;
            CurrentString = currentString;
            CurrentSubstring = currentSubstring;
        }
    }
}
