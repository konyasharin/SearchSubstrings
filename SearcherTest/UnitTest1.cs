namespace SearcherTest
{
    [TestFixture]
    public class Tests
    {
        [TestCase('', new string[1] { 'a' }, false, 'first', 1, null)]
        public void Test1(string str, string[] substrs, bool sensitivity, string method, int count, Dictionary<string, List<int>> result)
        {
            Assert.Pass();
        }
    }
}