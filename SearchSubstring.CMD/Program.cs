using SearchSubstrings.BL.Controller;

namespace SearchSubstring.CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SearcherController searcherController = new(char.ConvertToUtf32("a".ToString(), 0), char.ConvertToUtf32("z".ToString(), 0));
            searcherController.Search("abcded", "edkk");
        }
    }
}