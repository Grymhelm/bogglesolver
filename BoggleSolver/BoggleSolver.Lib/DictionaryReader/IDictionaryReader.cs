namespace BoggleSolver.Lib.DictionaryReader
{
    /// <summary>
    /// A reader is a fairly simple class which reports one word at a time from whatever source it uses
    /// They (currently) do not manage sanitization of text, only really validating that data is provided
    /// and broken apart in the expected format
    /// 
    /// Currently forward only, but could be expanded to go both directions
    /// </summary>
    public interface IDictionaryReader
    {
        /// <summary>
        /// Attempts to get the next word
        /// </summary>
        /// <param name="nextWord">Set if another word exists in sequence</param>
        /// <returns><c>true</c> if next word exists, <c>false</c> otherwise</returns>
        bool TryGetNextWord(out string nextWord);
    }
}


