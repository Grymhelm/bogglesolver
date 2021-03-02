using BoggleSolver.Lib.BoggleDictionary;
using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleScorer
{
    /// <summary>
    /// A scorer that uses the formal scoring as defined by official Boggle rules
    /// 
    /// Num of Letters |  3  |  4  |  5  |  6  |  7  |  8+
    /// ---------------+-----+-----+-----+-----+-----+-----
    ///    Point Value |  1  |  1  |  2  |  3  |  5  |  11
    ///    
    /// Official rules: https://www.fgbradleys.com/rules/Boggle.pdf
    /// </summary>
    internal class StandardScorer : BaseScorer
    {
        internal StandardScorer(IBoggleDictionary boggleDictionary) : base (boggleDictionary) { }

        public override int ScoreWord(string word)
        {
            int wordLength = word.Length;
            //see summary for logic explanation

            //Considering how this might be doable through "score matrix" of some sort, but starting simple for now.
            if(!_boggleDictionary.IsWord(word) || wordLength <= 2)
            {
                return 0;
            }
            if (wordLength <= 4) //Because of previous condition, we know it is greater than 2 
            {
                return 1;
            }
            if (wordLength == 5)
            {
                return 2;
            }
            if (wordLength == 6)
            {
                return 3;
            }
            if (wordLength == 7)
            {
                return 5;
            }

            return 11;
        }
    }
}
