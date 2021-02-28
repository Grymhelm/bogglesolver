using System.Collections.Generic;

namespace BoggleSolver.Lib.DictionaryReader
{
    public interface IDictionaryReader
    {
        bool TryGetNextWord(out string nextWord);
    }
}


