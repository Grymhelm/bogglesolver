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
    /// Scores words based on whatever parameters are provided
    /// </summary>
    public interface IBoggleScorer
    {
        /// <summary>
        /// Provides a score for a given word
        /// </summary>
        /// <param name="word">the word to attempt to score</param>
        /// <returns>The score value, or 0 if invalid</returns>
        int ScoreWord(string word);

        /// <summary>
        /// Scores a full collection of words
        /// </summary>
        /// <param name="wordList">any enumerable collection of strings to score</param>
        /// <returns>A named structure of word and its value</returns>
        IEnumerable<ScoringWord> ScoreWordList(IEnumerable<string> wordList);

        /// <summary>
        /// Provides the caller with all possible answers according to the scorer logic
        /// </summary>
        /// <param name="gameboard">A multidimensional array representing the game board</param>
        /// <returns>A collection of all possible scoring words and their value</returns>
        IEnumerable<ScoringWord> GetAllAnswers(char[][] gameboard);
    }
}
