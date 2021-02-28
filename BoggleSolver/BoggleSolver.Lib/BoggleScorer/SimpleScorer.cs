using BoggleSolver.Lib.BoggleDictionary;
using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleScorer
{
    /// <summary>
    /// In the simple scorer, the score of the word is equal to the word length
    /// </summary>
    internal class SimpleScorer : IBoggleScorer
    {
        private IBoggleDictionary _boggleDictionary;

        internal SimpleScorer(IBoggleDictionary boggleDictionary)
        {
            _boggleDictionary = boggleDictionary;
        }

        public IEnumerable<ScoringWord> GetAllAnswers(char[][] gameboard)
        {
            var answers = BoardSolver.TestAllCombinations(gameboard, _boggleDictionary);

            return ScoreWordList(answers);
        }

        public int ScoreWord(string word)
        {
            return _boggleDictionary.IsWord(word) ? word.Length : 0;
        }

        public IEnumerable<ScoringWord> ScoreWordList(IEnumerable<string> wordList)
        {
            List<ScoringWord> scoredWords = new List<ScoringWord>();

            foreach (var word in wordList)
            {
                int score = ScoreWord(word);
                if (score > 0)
                {
                    scoredWords.Add(new ScoringWord(word, score));
                }
            }

            return scoredWords;
        }
    }
}
