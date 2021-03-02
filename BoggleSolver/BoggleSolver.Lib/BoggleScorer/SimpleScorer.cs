using BoggleSolver.Lib.BoggleDictionary;
using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleScorer
{
    /// <summary>
    /// In the simple scorer, the score of the word is equal to the word length
    /// </summary>
    internal class SimpleScorer : BaseScorer
    {
        internal SimpleScorer(IBoggleDictionary boggleDictionary) : base(boggleDictionary) { }

        public override int ScoreWord(string word)
        {
            return _boggleDictionary.IsWord(word) ? word.Length : 0;
        }
    }
}
