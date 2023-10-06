using SearchSubstrings.BL.Controller;

namespace SearcherTests
{
    /// <summary>
    /// ёнит-тесты.
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        /// <summary>
        ///  онтроллер дл€ Searcher.
        /// </summary>
        SearcherController? _searcherController;

        /// <summary>
        /// ¬ыполн€ет тесты с поиском одной подстроки в строке.
        /// </summary>
        /// <param name="str">—трока, в которой происходит поиск.</param>
        /// <param name="substrings">ѕодстроки, которые нужно найти.</param>
        /// <param name="sensitivity">„увствительность к регистру.</param>
        /// <param name="method">ѕоиск с начала(first) или с конца(last).</param>
        /// <param name="count"> оличество первых вхождений.</param>
        /// <param name="indexes">»ндексы (места, с которых начинаетс€ подстрока), которые должны содержатьс€ в ответе.</param>
        [DataTestMethod]

        #region Tests 1

        [DataRow("", new[] { "a" }, false, "first", 1, new int[0])] // у нас возвращаетс€ пустой массив индексов
        [DataRow("", new[] { "a" }, true, "first", 1, new int[0])]
        [DataRow("", new[] { "a" }, false, "last", 1, new int[0])]
        [DataRow("", new[] { "a" }, true, "last", 1, new int[0])]

        [DataRow("a", new[] { "a" }, false, "first", 1, new[] { 0 })]
        [DataRow("a", new[] { "a" }, true, "first", 1, new[] { 0 })]
        [DataRow("a", new[] { "a" }, false, "last", 1, new[] { 0 })]
        [DataRow("a", new[] { "a" }, true, "last", 1, new[] { 0 })]

        [DataRow("aaa", new[] { "a" }, false, "first", 3, new[] { 0, 1, 2 })]
        [DataRow("aaa", new[] { "a" }, true, "first", 3, new[] { 0, 1, 2 })]
        [DataRow("aaa", new[] { "a" }, false, "last", 3, new[] { 2, 1, 0 })]
        [DataRow("aaa", new[] { "a" }, true, "last", 3, new[] { 2, 1, 0 })]

        [DataRow("bca", new[] { "c" }, false, "first", 1, new[] { 1 })]
        [DataRow("bca", new[] { "c" }, true, "first", 1, new[] { 1 })]
        [DataRow("bca", new[] { "c" }, false, "last", 1, new[] { 1 })]
        [DataRow("bca", new[] { "c" }, true, "last", 1, new[] { 1 })]

        [DataRow("aaa", new[] { "a" }, false, "first", 2, new[] { 0, 1 })]
        [DataRow("aaa", new[] { "a" }, true, "first", 1, new[] { 0 })]
        [DataRow("aaa", new[] { "a" }, false, "last", 2, new[] { 2, 1 })]
        [DataRow("aaa", new[] { "a" }, true, "last", 10, new[] { 2, 1, 0 })]

        [DataRow("", new[] { "abc" }, false, "first", 1, new int[0])]
        [DataRow("", new[] { "abc" }, true, "first", 1, new int[0])]
        [DataRow("", new[] { "abc" }, false, "last", 1, new int[0])]
        [DataRow("", new[] { "abc" }, true, "last", 1, new int[0])]

        [DataRow("a", new[] { "abc" }, false, "first", 1, new int[0])]
        [DataRow("a", new[] { "abc" }, true, "first", 1, new int[0])]
        [DataRow("a", new[] { "abc" }, false, "last", 1, new int[0])]
        [DataRow("a", new[] { "abc" }, true, "last", 1, new int[0])]

        [DataRow("abc", new[] { "abc" }, false, "first", 1, new[] { 0 })]
        [DataRow("abc", new[] { "abc" }, true, "first", 1, new[] { 0 })]
        [DataRow("abc", new[] { "abc" }, false, "last", 1, new[] { 0 })]
        [DataRow("abc", new[] { "abc" }, true, "last", 1, new[] { 0 })]

        [DataRow("abcabc", new[] { "abc" }, false, "first", 2, new[] { 0, 3 })]
        [DataRow("abcabc", new[] { "abc" }, true, "first", 2, new[] { 0, 3 })]
        [DataRow("abcabc", new[] { "abc" }, false, "last", 2, new[] { 3, 0 })]
        [DataRow("abcabc", new[] { "abc" }, true, "last", 2, new[] { 3, 0 })]

        [DataRow("aabcbccaabc", new[] { "abc" }, false, "first", 2, new[] { 1, 8 })]
        [DataRow("aabcbccaabc", new[] { "abc" }, true, "first", 2, new[] { 1, 8 })]
        [DataRow("aAbCbccaabc", new[] { "AbC" }, false, "first", 2, new[] { 1, 8 })]
        [DataRow("aabcbccaAbC", new[] { "AbC" }, true, "first", 1, new[] { 8 })]
        [DataRow("aabcbccaabc", new[] { "abc" }, false, "last", 2, new[] { 8, 1 })]
        [DataRow("aabcbccaabc", new[] { "abc" }, true, "last", 2, new[] { 8, 1 })]
        [DataRow("aAbCbccaabc", new[] { "AbC" }, false, "last", 2, new[] { 8, 1 })]
        [DataRow("aabcbccaAbC", new[] { "AbC" }, true, "last", 1, new[] { 8 })]

        [DataRow("abcabc", new[] { "abc" }, false, "first", 1, new[] { 0 })]
        [DataRow("abcabcabc", new[] { "abc" }, true, "first", 2, new[] { 0, 3 })]
        [DataRow("abcabc", new[] { "abc" }, false, "last", 9, new[] { 3, 0 })]
        [DataRow("abcabc", new[] { "abc" }, true, "last", 2, new[] { 3, 0 })]

        #endregion

        public void TestSearchOneSubstr(string str, string[] substrings, bool sensitivity, string method, int count, int[] indexes)
        {
            List<int> listIndexes = new List<int>(indexes);
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>> { { substrings[0], listIndexes } };
            _searcherController = new(char.ConvertToUtf32("A", 0), char.ConvertToUtf32("z", 0));
            Assert.IsTrue(EqualIndexes(result[substrings[0]], _searcherController.Search(str, substrings, sensitivity, method, count)[substrings[0]]));
        }

        /// <summary>
        /// ¬ыполн€ет тесты с поиском двух подстрок в строке.
        /// </summary>
        /// <param name="str">—трока, в которой происходит поиск.</param>
        /// <param name="substrings">ѕодстроки, которые нужно найти.</param>
        /// <param name="sensitivity">„увствительность к регистру.</param>
        /// <param name="method">ѕоиск с начала(first) или с конца(last).</param>
        /// <param name="count"> оличество первых вхождений.</param>
        /// <param name="indexes1">»ндексы (места, с которых начинаетс€ подстрока), которые должны содержатьс€ в ответе дл€ первой подстроки.</param>
        /// <param name="indexes2">»ндексы (места, с которых начинаетс€ подстрока), которые должны содержатьс€ в ответе дл€ второй подстроки.</param>
        [DataTestMethod]

        #region Tests 2

        [DataRow("", new[] { "abc", "a" }, false, "first", 1, new int[0], new int[0])]
        [DataRow("", new[] { "abc", "a" }, true, "first", 1, new int[0], new int[0])]
        [DataRow("", new[] { "abc", "a" }, false, "last", 1, new int[0], new int[0])]
        [DataRow("", new[] { "abc", "a" }, true, "last", 1, new int[0], new int[0])]

        [DataRow("a", new[] { "abc", "a" }, false, "first", 1, new int[0], new[] { 0 })]
        [DataRow("a", new[] { "abc", "a" }, true, "first", 1, new int[0], new[] { 0 })]
        [DataRow("a", new[] { "abc", "a" }, false, "last", 1, new int[0], new[] { 0 })]
        [DataRow("a", new[] { "abc", "a" }, true, "last", 1, new int[0], new[] { 0 })]

        [DataRow("ababbababa", new[] { "aba", "bba" }, false, "first", 4, new[] { 0, 5, 7 }, new[] { 3 })]
        [DataRow("ababbababa", new[] { "aba", "bba" }, true, "first", 4, new[] { 0, 5, 7 }, new[] { 3 })]
        [DataRow("ababbababa", new[] { "aba", "bba" }, false, "last", 4, new[] { 7, 5, 0 }, new[] { 3 })]
        [DataRow("ababbababa", new[] { "aba", "bba" }, true, "last", 4, new[] { 7, 5, 0 }, new[] { 3 })]

        [DataRow("ababbababa", new[] { "aba", "bba" }, false, "first", 3, new[] { 0, 5 }, new[] { 3 })]
        [DataRow("ababbababa", new[] { "aba", "bba" }, true, "first", 2, new[] { 0 }, new[] { 3 })]
        [DataRow("ababbababa", new[] { "aba", "bba" }, false, "last", 1, new[] { 7 }, new int[0])]
        [DataRow("ababbababa", new[] { "aba", "bba" }, true, "last", 10, new[] { 7, 5, 0 }, new[] { 3 })]

        #endregion

        public void TestSearchTwoSubstr(string str, string[] substrings, bool sensitivity, string method, int count,
            int[] indexes1, int[] indexes2)
        {
            List<int> listIndexes1 = new List<int>(indexes1);
            List<int> listIndexes2 = new List<int>(indexes2);
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>> { { substrings[0], listIndexes1 }, { substrings[1], listIndexes2 } };
            _searcherController = new(char.ConvertToUtf32("A", 0), char.ConvertToUtf32("z", 0));
            Assert.IsTrue(EqualIndexes(result[substrings[0]], _searcherController.Search(str, substrings, sensitivity, method, count)[substrings[0]]));
            Assert.IsTrue(EqualIndexes(result[substrings[1]], _searcherController.Search(str, substrings, sensitivity, method, count)[substrings[1]]));
        }

        /// <summary>
        /// —равнение индексов в двух массивах на равенство.
        /// </summary>
        /// <param name="indexes1">ѕервый массив индексов.</param>
        /// <param name="indexes2">¬торой массив индексов.</param>
        /// <returns>true - если индексы в двух массивах равны, иначе - false.</returns>
        private bool EqualIndexes(List<int> indexes1, List<int> indexes2)
        {
            bool result = true;
            if (indexes1.Count != indexes2.Count)
            {
                return false;
            }

            for (int i = 0; i < indexes1.Count; i++)
            {
                if (indexes1[i] != indexes2[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}