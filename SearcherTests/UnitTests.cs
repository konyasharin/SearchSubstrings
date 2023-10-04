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

        /*
        TEST_SEARCH_MANY_SYMBOL = [
            ('', 'abc', False, 'first', 1, None),
            ('', 'abc', True, 'first', 1, None),
            ('', 'abc', False, 'last', 1, None),
            ('', 'abc', True, 'last', 1, None),

            ('a', 'abc', False, 'first', 1, None),
            ('a', 'abc', True, 'first', 1, None),
            ('a', 'abc', False, 'last', 1, None),
            ('a', 'abc', True, 'last', 1, None),

            ('abc', 'abc', False, 'first', 1, (0, )),
            ('abc', 'abc', True, 'first', 1, (0, )),
            ('abc', 'abc', False, 'last', 1, (0, )),
            ('abc', 'abc', True, 'last', 1, (0, )),

            ('abcabc', 'abc', False, 'first', 2, (0, 3)),
            ('abcabc', 'abc', True, 'first', 2, (0, 3)),
            ('abcabc', 'abc', False, 'last', 2, (3, 0)),
            ('abcabc', 'abc', True, 'last', 2, (3, 0)),

            ('aabcbccaabc', 'abc', False, 'first', 2, (1, 8)),
            ('aabcbccaabc', 'abc', True, 'first', 2, (1, 8)),
            ('aAbCbccaabc', 'AbC', False, 'first', 2, (1, 8)),
            ('aabcbccaAbC', 'AbC', True, 'first', 1, (8, )),
            ('aabcbccaabc', 'abc', False, 'last', 2, (8, 1)),
            ('aabcbccaabc', 'abc', True, 'last', 2, (8, 1)),
            ('aAbCbccaabc', 'AbC', False, 'last', 2, (8, 1)),
            ('aabcbccaAbC', 'AbC', True, 'last', 1, (8, )),

            ('abcabc', 'abc', False, 'first', 1, (0, )),
            ('abcabcabc', 'abc', True, 'first', 2, (0, 3)),
            ('abcabc', 'abc', False, 'last', 9, (3, 0)),
            ('abcabc', 'abc', True, 'last', 2, (3, 0)),
        ]
        */

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

        public void TestSearchOneSymbol(string str, string[] substrings, bool sensitivity, string method, int count, int[] indexes)
        {
            List<int> listIndexes = new List<int>(indexes);
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>> { { substrings[0], listIndexes } };
            searcherController = new(char.ConvertToUtf32("A".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
            Assert.IsTrue(EqualIndexes(result[substrings[0]], searcherController.Search(str, substrings, sensitivity, method, count)[substrings[0]]));
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