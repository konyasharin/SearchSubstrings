using SearchSubstrings.BL.Controller;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace SearcherTests
{
    /*
    TEST_SEARCH_ONE_SYMBOL = [
        ('', 'a', False, 'first', 1, None),
        ('', 'a', True, 'first', 1, None),
        ('', 'a', False, 'last', 1, None),
        ('', 'a', True, 'last', 1, None),

        ('a', 'a', False, 'first', 1, (0, )),
        ('a', 'a', True, 'first', 1, (0, )),
        ('a', 'a', False, 'last', 1, (0, )),
        ('a', 'a', True, 'last', 1, (0, )),

        ('aaa', 'a', False, 'first', 3, (0, 1, 2)),
        ('aaa', 'a', True, 'first', 3, (0, 1, 2)),
        ('aaa', 'a', False, 'last', 3, (2, 1, 0)),
        ('aaa', 'a', True, 'last', 3, (2, 1, 0)),

        ('bca', 'c', False, 'first', 1, (1, )),
        ('bca', 'c', True, 'first', 1, (1, )),
        ('bca', 'c', False, 'last', 1, (1, )),
        ('bca', 'c', True, 'last', 1, (1, )),

        ('aaa', 'a', False, 'first', 2, (0, 1)),
        ('aaa', 'a', True, 'first', 1, (0, )),
        ('aaa', 'a', False, 'last', 2, (2, 1)),
        ('aaa', 'a', True, 'last', 10, (2, 1, 0)),
    ]
    */
    [TestClass]
    public class UnitTests
    {
        SearcherController searcherController;
        [DataTestMethod]
        [DataRow("", new string[1] { "a" }, false, "first", 1, new Dictionary<string, List<int>>{{"", new List<int>}})]
        //[DataRow("", new string[1] { "a" }, true, "first", 1, null)]
        //[DataRow("", new string[1] { "a" }, false, "last", 1, null)]
        //[DataRow("", new string[1] { "a" }, true, "last", 1, null)]
        public void TestMethod1(string str, string[] substrings, bool sensitivity, string method, int count, Dictionary<string, List<int>> result)
        {
            searcherController = new(char.ConvertToUtf32("A".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
            Assert.AreEqual(searcherController.Search(str, substrings, sensitivity, method, count), result);
        }
    }
}