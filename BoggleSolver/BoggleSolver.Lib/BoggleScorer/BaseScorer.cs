using BoggleSolver.Lib.BoggleDictionary;
using System;
using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleScorer
{
    abstract internal class BaseScorer : IBoggleScorer
    {
        protected IBoggleDictionary _boggleDictionary;

        internal BaseScorer(IBoggleDictionary boggleDictionary)
        {
            _boggleDictionary = boggleDictionary;
        }

        public IReadOnlyList<ScoringWord> GetAllAnswers(char[][] gameboard)
        {
            var answers = BoardSolver.TestAllCombinations(gameboard, _boggleDictionary);

            return ScoreWordList(answers);
        }

        public abstract int ScoreWord(string word);

        public virtual IReadOnlyList<ScoringWord> ScoreWordList(IEnumerable<string> wordList)
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
