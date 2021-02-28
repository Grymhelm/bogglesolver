using BoggleSolver.Lib.BoggleDictionary;
using BoggleSolver.Lib.DictionaryReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BoggleSolver.UnitTests
{
    internal class SimpleTestDictionary : IDictionaryReader
    {
        private List<string> _simpleDictionary;
        private int currentPosition = 0;

        public int WordCount => _simpleDictionary.Count;
        public IReadOnlyList<string> WordList => _simpleDictionary;

        public SimpleTestDictionary()
        {
            _simpleDictionary = new List<string> { "able", "ability", "cat", "catcher", "dog" };
        }

        public SimpleTestDictionary(List<string> wordList)
        {
            _simpleDictionary = wordList;
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

    [TestClass]
    public class DictionaryTests
    {
        [TestMethod]
        public void TrieDictionary_SimpleTestDictionary_WordsMatch()
        {
            var simpleTestDictionary = new SimpleTestDictionary();
            IBoggleDictionary boggleDictionary = new TrieBasedBoggleDictionary(simpleTestDictionary);

            var fullWordList = boggleDictionary.GetFullWordList();

            Assert.AreEqual(simpleTestDictionary.WordCount, fullWordList.Count, "Wrong number of words in the dictionary constructed");

            CollectionAssert.AreEquivalent((System.Collections.ICollection)simpleTestDictionary.WordList, (System.Collections.ICollection)fullWordList);
        }
    }
}
