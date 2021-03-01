using System.Collections.Generic;

namespace BoggleSolver.Lib.BoggleDictionary
{
    /// <summary>
    /// Provides the functionality of a language dictionary, and defines what words are valid for the game
    /// </summary>
    public interface IBoggleDictionary
    {
        /// <summary>
        /// Provides all the words this dictionary believes are valid words
        /// </summary>
        /// <returns>A read-only collection of correct words</returns>
        IReadOnlyList<string> GetFullWordList();

        /// <summary>
        /// A function for checking if a given word is in the dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns><c>true</c> if the word is found, <c>false</c> otherwise</returns>
        bool IsWord(string word);
    }
}
