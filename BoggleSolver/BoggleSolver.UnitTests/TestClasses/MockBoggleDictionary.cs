using BoggleSolver.Lib.BoggleDictionary;
using System.Collections.Generic;

namespace BoggleSolver.UnitTests.TestClasses
{
    public class MockBoggleDictionary : IBoggleDictionary
    {
        private List<string> _validWords;
        public MockBoggleDictionary(List<string> validWords)
        {
            _validWords = validWords;
        }

        public IReadOnlyList<string> GetFullWordList()
        {
            return _validWords;
        }

        public bool IsWord(string word)
        {
            return _validWords.Contains(word);
        }
    }
}
