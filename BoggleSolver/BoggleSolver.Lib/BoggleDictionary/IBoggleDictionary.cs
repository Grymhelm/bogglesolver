using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleDictionary
{
    public interface IBoggleDictionary
    {
        IReadOnlyList<string> GetFullWordList();
        bool IsWord(string word);
    }
}
