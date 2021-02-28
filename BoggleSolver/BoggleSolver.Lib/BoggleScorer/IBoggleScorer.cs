using BoggleSolver.Lib.BoggleDictionary;
using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleScorer
{
    public struct ScoringWord
    {
        public int Points { get; }
        public string Word { get; }

        public ScoringWord(string word, int points)
        {
            Word = word;
            Points = points;
        }
    }

    /// <summary>
    /// Scores words based on whatever parameters are provided;
    /// </summary>
    public interface IBoggleScorer
    {
        int ScoreWord(string word);
        IEnumerable<ScoringWord> ScoreWordList(IEnumerable<string> wordList);
        IEnumerable<ScoringWord> GetAllAnswers(char[][] gameboard);
    }
}
