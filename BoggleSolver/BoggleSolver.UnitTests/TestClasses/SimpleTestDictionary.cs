using BoggleSolver.Lib.DictionaryReader;
using System.Collections.Generic;

namespace BoggleSolver.UnitTests.TestClasses
{
    internal class SimpleTestDictionary : IDictionaryReader
    {
        private List<string> _simpleDictionary = new List<string>();
        private int currentPosition = 0;

        public int WordCount => _simpleDictionary.Count;
        public IReadOnlyList<string> WordList => _simpleDictionary;

        public SimpleTestDictionary() : this(new List<string> { "able", "ability", "cat", "catcher", "dog" })
        {
        }

        public SimpleTestDictionary(List<string> wordList)
        {
            // copying so we don't modify the original collection, in case it is relevant
            foreach (var word in wordList)
            {
                _simpleDictionary.Add(word);
            }
        }

        public bool TryGetNextWord(out string nextWord)
        {
            if (currentPosition < _simpleDictionary.Count)
            {
                nextWord = _simpleDictionary[currentPosition];
                currentPosition++;
                return true;
            }

            nextWord = string.Empty;
            return false;
        }
    }
}
