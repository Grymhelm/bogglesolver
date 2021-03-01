using BoggleSolver.Lib.BoggleScorer;
using BoggleSolver.UnitTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BoggleSolver.UnitTests
{
    [TestClass]
    public class ScorerTests
    {
        [TestMethod]
        public void SimpleScorer_ScoreWord_WordsProduceCorrectScores()
        {
            var mockDictionary = new MockBoggleDictionary(new List<string> { "a", "ab", "abc", "abcd" });
            var scorer = new SimpleScorer(mockDictionary);

            foreach (var word in mockDictionary.GetFullWordList())
            {
                Assert.AreEqual(word.Length, scorer.ScoreWord(word));
            }
        }

        [TestMethod]
        public void SimpleScorer_ScoreWord_DoesntScoreInvalidWords()
        {
            var mockDictionary = new MockBoggleDictionary(new List<string> { "a", "ab", "abc", "abcd" });
            var scorer = new SimpleScorer(mockDictionary);

            Assert.AreEqual(0, scorer.ScoreWord("abab"));
            Assert.AreEqual(0, scorer.ScoreWord("aaaa"));
        }

        [TestMethod]
        public void SimpleScorer_ScoreWordList_WordListScoreIsSameAsIndividualTotal()
        {
            var mockDictionary = new MockBoggleDictionary(new List<string> { "a", "ab", "abc", "abcd" });
            var scorer = new SimpleScorer(mockDictionary);

            int expectedScore = 0;
            var validWordList = mockDictionary.GetFullWordList();
            foreach (var word in validWordList)
            {
                expectedScore += word.Length;
            }

            var allScoringWords = scorer.ScoreWordList(new List<string> { "a", "ab", "abc", "abcd", "dddd", "aaaa" });

            int actualScore = 0;
            foreach (var scoringWord in allScoringWords)
            {
                Assert.AreEqual(scoringWord.Word.Length, scoringWord.Points);
                actualScore += scoringWord.Points;
            }

            Assert.AreEqual(expectedScore, actualScore);
        }
    }
}
