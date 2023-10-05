using SearchSubstrings.BL.Controller;

namespace SearcherTests
{
    [TestClass]
    public class UnitTests
    {
        SearcherController? searcherController;

        [DataTestMethod]

        [DataRow("", new string[1] { "a" }, false, "first", 1, new int[0])] // у нас возвращается пустой массив индексов
        [DataRow("", new string[1] { "a" }, true, "first", 1, new int[0])]
        [DataRow("", new string[1] { "a" }, false, "last", 1, new int[0])]
        [DataRow("", new string[1] { "a" }, true, "last", 1, new int[0])]

        [DataRow("a", new string[1] { "a" }, false, "first", 1, new int[1] { 0 })]
        [DataRow("a", new string[1] { "a" }, true, "first", 1, new int[1] { 0 })]
        [DataRow("a", new string[1] { "a" }, false, "last", 1, new int[1] { 0 })]
        [DataRow("a", new string[1] { "a" }, true, "last", 1, new int[1] { 0 })]

        [DataRow("aaa", new string[1] { "a" }, false, "first", 3, new int[3] { 0, 1, 2 })]
        [DataRow("aaa", new string[1] { "a" }, true, "first", 3, new int[3] { 0, 1, 2 })]
        [DataRow("aaa", new string[1] { "a" }, false, "last", 3, new int[3] { 2, 1, 0 })]
        [DataRow("aaa", new string[1] { "a" }, true, "last", 3, new int[3] { 2, 1, 0 })]

        [DataRow("bca", new string[1] { "c" }, false, "first", 1, new int[1] { 1 })]
        [DataRow("bca", new string[1] { "c" }, true, "first", 1, new int[1] { 1 })]
        [DataRow("bca", new string[1] { "c" }, false, "last", 1, new int[1] { 1 })]
        [DataRow("bca", new string[1] { "c" }, true, "last", 1, new int[1] { 1 })]

        [DataRow("aaa", new string[1] { "a" }, false, "first", 2, new int[2] { 0, 1 })]
        [DataRow("aaa", new string[1] { "a" }, true, "first", 1, new int[1] { 0 })]
        [DataRow("aaa", new string[1] { "a" }, false, "last", 2, new int[2] { 2, 1 })]
        [DataRow("aaa", new string[1] { "a" }, true, "last", 10, new int[3] { 2, 1, 0 })]

        [DataRow("", new string[1] { "abc" }, false, "first", 1, new int[0])]
        [DataRow("", new string[1] { "abc" }, true, "first", 1, new int[0])]
        [DataRow("", new string[1] { "abc" }, false, "last", 1, new int[0])]
        [DataRow("", new string[1] { "abc" }, true, "last", 1, new int[0])]

        [DataRow("a", new string[1] { "abc" }, false, "first", 1, new int[0])]
        [DataRow("a", new string[1] { "abc" }, true, "first", 1, new int[0])]
        [DataRow("a", new string[1] { "abc" }, false, "last", 1, new int[0])]
        [DataRow("a", new string[1] { "abc" }, true, "last", 1, new int[0])]

        [DataRow("abc", new string[1] { "abc" }, false, "first", 1, new int[1]{ 0 })]
        [DataRow("abc", new string[1] { "abc" }, true, "first", 1, new int[1] { 0 })]
        [DataRow("abc", new string[1] { "abc" }, false, "last", 1, new int[1] { 0 })]
        [DataRow("abc", new string[1] { "abc" }, true, "last", 1, new int[1] { 0 })]

        [DataRow("abcabc", new string[1] { "abc" }, false, "first", 2, new int[2] { 0, 3 })]
        [DataRow("abcabc", new string[1] { "abc" }, true, "first", 2, new int[2] { 0, 3 })]
        [DataRow("abcabc", new string[1] { "abc" }, false, "last", 2, new int[2] { 3, 0 })]
        [DataRow("abcabc", new string[1] { "abc" }, true, "last", 2, new int[2] { 3, 0 })]

        [DataRow("aabcbccaabc", new string[1] { "abc" }, false, "first", 2, new int[2] { 1, 8 })]
        [DataRow("aabcbccaabc", new string[1] { "abc" }, true, "first", 2, new int[2] { 1, 8 })]
        [DataRow("aAbCbccaabc", new string[1] { "AbC" }, false, "first", 2, new int[2] { 1, 8 })]
        [DataRow("aabcbccaAbC", new string[1] { "AbC" }, true, "first", 1, new int[1] { 8 })]
        [DataRow("aabcbccaabc", new string[1] { "abc" }, false, "last", 2, new int[2] { 8, 1 })]
        [DataRow("aabcbccaabc", new string[1] { "abc" }, true, "last", 2, new int[2] { 8, 1 })]
        [DataRow("aAbCbccaabc", new string[1] { "AbC" }, false, "last", 2, new int[2] { 8, 1 })]
        [DataRow("aabcbccaAbC", new string[1] { "AbC" }, true, "last", 1, new int[1] { 8 })]

        [DataRow("abcabc", new string[1] { "abc" }, false, "first", 1, new int[1] { 0 })]
        [DataRow("abcabcabc", new string[1] { "abc" }, true, "first", 2, new int[2] { 0, 3 })]
        [DataRow("abcabc", new string[1] { "abc" }, false, "last", 9, new int[2] { 3, 0 })]
        [DataRow("abcabc", new string[1] { "abc" }, true, "last", 2, new int[2] { 3, 0 })]

        /*
        TEST_SEARCH_FEW_SUBSTR = [
            ('', ('abc', 'a'), False, 'first', 1, None),
            ('', ('abc', 'a'), True, 'first', 1, None),
            ('', ('abc', 'a'), False, 'last', 1, None),
            ('', ('abc', 'a'), True, 'last', 1, None),

            ('a', ('abc', 'a'), False, 'first', 1, {'abc': None, 'a': (0, )}),
            ('a', ('abc', 'a'), True, 'first', 1, {'abc': None, 'a': (0, )}),
            ('a', ('abc', 'a'), False, 'last', 1, { 'abc': None, 'a': (0, )}),
            ('a', ('abc', 'a'), True, 'last', 1, { 'abc': None, 'a': (0, )}),

            ('ababbababa', ('aba', 'bba'), False, 'first', 4, { 'aba': (0, 5, 7), 'bba': (3, )}),
            ('ababbababa', ('aba', 'bba'), True, 'first', 4, { 'aba': (0, 5, 7), 'bba': (3, )}),
            ('ababbababa', ('aba', 'bba'), False, 'last', 4, { 'aba': (7, 5, 0), 'bba': (3, )}),
            ('ababbababa', ('aba', 'bba'), True, 'last', 4, { 'aba': (7, 5, 0), 'bba': (3, )}),

            ('ababbababa', ('aba', 'bba'), False, 'first', 3, { 'aba': (0, 5), 'bba': (3, )}),
            ('ababbababa', ('aba', 'bba'), True, 'first', 2, { 'aba': (0, ), 'bba': (3, )}),
            ('ababbababa', ('aba', 'bba'), False, 'last', 1, { 'aba': (7, ), 'bba': None}),
            ('ababbababa', ('aba', 'bba'), True, 'last', 10, { 'aba': (7, 5, 0), 'bba': (3, )}),
        ]
        */


        public void TestSearchOneSubstr(string str, string[] substrings, bool sensitivity, string method, int count, int[] indexes)
        {
            List<int> listIndexes = new List<int>(indexes);
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>> { { substrings[0], listIndexes } };
            searcherController = new(char.ConvertToUtf32("A".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
            Assert.IsTrue(EqualIndexes(result[substrings[0]], searcherController.Search(str, substrings, sensitivity, method, count)[substrings[0]]));
        }

        [DataTestMethod]

        [DataRow("", new string[2] { "abc", "a" }, false, "first", 1, new int[0], new int[0])]
        [DataRow("", new string[2] { "abc", "a" }, true, "first", 1, new int[0], new int[0])]
        [DataRow("", new string[2] { "abc", "a" }, false, "last", 1, new int[0], new int[0])]
        [DataRow("", new string[2] { "abc", "a" }, true, "last", 1, new int[0], new int[0])]

        [DataRow("a", new string[2] { "abc", "a" }, false, "first", 1, new int[0], new int[1] { 0 })]
        [DataRow("a", new string[2] { "abc", "a" }, true, "first", 1, new int[0], new int[1] { 0 })]
        [DataRow("a", new string[2] { "abc", "a" }, false, "last", 1, new int[0], new int[1] { 0 })]
        [DataRow("a", new string[2] { "abc", "a" }, true, "last", 1, new int[0], new int[1] { 0 })]

        [DataRow("ababbababa", new string[2] { "aba", "bba" }, false, "first", 4, new int[3] { 0, 5, 7 }, new int[1] { 3 })]
        [DataRow("ababbababa", new string[2] { "aba", "bba" }, true, "first", 4, new int[3] { 0, 5, 7 }, new int[1] { 3 })]
        [DataRow("ababbababa", new string[2] { "aba", "bba" }, false, "last", 4, new int[3] { 7, 5, 0 }, new int[1] { 3 })]
        [DataRow("ababbababa", new string[2] { "aba", "bba" }, true, "last", 4, new int[3] { 7, 5, 0 }, new int[1] { 3 })]

        [DataRow("ababbababa", new string[2] { "aba", "bba" }, false, "first", 3, new int[2] { 0, 5 }, new int[1] { 3 })]
        [DataRow("ababbababa", new string[2] { "aba", "bba" }, true, "first", 2, new int[1] { 0 }, new int[1] { 3 })]
        [DataRow("ababbababa", new string[2] { "aba", "bba" }, false, "last", 1, new int[1] { 7 }, new int[0])]
        [DataRow("ababbababa", new string[2] { "aba", "bba" }, true, "last", 10, new int[3] { 7, 5, 0 }, new int[1] { 3 })]

        public void TestSearchTwoSubstr(string str, string[] substrings, bool sensitivity, string method, int count,
            int[] indexes1, int[] indexes2)
        {
            List<int> listIndexes1 = new List<int>(indexes1);
            List<int> listIndexes2 = new List<int>(indexes2);
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>> { { substrings[0], listIndexes1 }, { substrings[1], listIndexes2 } };
            searcherController = new(char.ConvertToUtf32("A".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
            Assert.IsTrue(EqualIndexes(result[substrings[0]], searcherController.Search(str, substrings, sensitivity, method, count)[substrings[0]]));
            Assert.IsTrue(EqualIndexes(result[substrings[1]], searcherController.Search(str, substrings, sensitivity, method, count)[substrings[1]]));
        }

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