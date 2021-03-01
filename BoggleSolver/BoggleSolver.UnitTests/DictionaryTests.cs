using BoggleSolver.Lib.BoggleDictionary;
using BoggleSolver.UnitTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BoggleSolver.UnitTests
{

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

            foreach (var word in simpleTestDictionary.WordList)
            {
                //We have to capitalize the values in the reader because it doesn't sanitize input (see interface notes)
                CollectionAssert.Contains((System.Collections.ICollection)fullWordList, word.ToUpperInvariant());
            }
        }

        [TestMethod]
        public void TrieDictionary_DictionaryWithAllValidWords_IsWordReturnsTrue()
        {
            var simpleTestDictionary = new SimpleTestDictionary();
            IBoggleDictionary boggleDictionary = new TrieBasedBoggleDictionary(simpleTestDictionary);

            foreach (var word in simpleTestDictionary.WordList)
            {
                Assert.IsTrue(boggleDictionary.IsWord(word), $"The word {word} came back as not a word.");
            }
        }

        [TestMethod]
        public void TrieDictionary_OnlyFourOrMoreLettersValid_IsWordReturnsTrue()
        {
            var simpleTestDictionary = new SimpleTestDictionary();
            var minimumWordLength = 4;
            IBoggleDictionary boggleDictionary = new TrieBasedBoggleDictionary(simpleTestDictionary, minimumWordLength);

            foreach (var word in simpleTestDictionary.WordList)
            {
                if (word.Length >= minimumWordLength)
                {
                    Assert.IsTrue(boggleDictionary.IsWord(word), $"The word {word} came back as not a word.");
                }
                else
                {
                    Assert.IsFalse(boggleDictionary.IsWord(word), $"The word {word} came back as word, but doesn't meet the min length of {minimumWordLength}.");
                }
            }
        }

        [TestMethod]
        public void TrieDictionary_DuplicateWords_AreIgnored()
        {
            var simpleTestDictionary = new SimpleTestDictionary(new List<string> { "dog", "dog", "doggy" });

            IBoggleDictionary boggleDictionary = new TrieBasedBoggleDictionary(simpleTestDictionary);

            var fullWordList = boggleDictionary.GetFullWordList();

            Assert.AreEqual(2, fullWordList.Count, "Wrong number of words in the dictionary constructed");

            //also make sure the correct words got added
            foreach (var word in simpleTestDictionary.WordList)
            {
                //We have to capitalize the values in the reader because it doesn't sanitize input (see interface notes)
                CollectionAssert.Contains((System.Collections.ICollection)fullWordList, word.ToUpperInvariant());
            }
        }
    }
}
