using System;
using System.Collections.Generic;
using System.Text;

namespace BoggleSolver.Lib.BoggleDictionary
{
    public interface IBoggleDictionary
    {
        IList<string> GetFullWordList();
        bool IsWord(string word);
    }
}
