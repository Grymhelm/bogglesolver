using System.Collections.Generic;

namespace BoggleSolver.Lib.DictionaryReader
{
    public interface IDictionaryReader
    {
        bool TryGetNextWord(out string nextWord);
    }

    internal class SimpleTestDictionary : IDictionaryReader
    {
        private List<string> _simpleDictionary = new List<string> { "able", "ability", "cat", "catcher", "dog" };
        private int currentPosition = 0;
        public bool TryGetNextWord(out string nextWord)
        {
            if (currentPosition < _simpleDictionary.Count)
            {
                nextWord = _simpleDictionary[currentPosition];
                currentPosition++;
                return true;
            }

            nextWord = string.Empty;
            return false;
        }
    }
}


