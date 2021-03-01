using BoggleSolver.Lib.BoggleDictionary;
using BoggleSolver.Lib.BoggleScorer;
using BoggleSolver.Lib.DictionaryReader;

namespace BoggleSolver.Lib
{
    public enum DictionaryType
    {
        Invalid,
        CompactMemory
    }

    public enum ScorerType
    {
        Invalid,
        Simple,
    }

    public struct GameConfiguration
    {
        public DictionaryType DictionaryType { get; set; }
        public ScorerType ScorerType { get; set; }
        public int MinimumWordLength { get; set; }
    }

    public class BoggleGame
    {
        public IBoggleDictionary GameDictionary { get; }
        public IBoggleScorer BoggleScorer { get; }

        public BoggleGame(IBoggleDictionary boggleDictionary, IBoggleScorer scorer)
        {
            GameDictionary = boggleDictionary;
            BoggleScorer = scorer;
        }
    }

    /// <summary>
    /// Currently, many members of this class are internal; they could be made public, but this is currently built as a "library" of functionality
    /// where most content should be opaque to calling clients...  (This class would still potentially be useful in the case members were public, of course.)
    /// </summary>
    public static class BoggleGameFactory
    {
        public static BoggleGame CreateBoggleGame(IDictionaryReader dictionaryReader, GameConfiguration gameConfiguration)
        {
            if (gameConfiguration.MinimumWordLength <= 0)
            {
                throw new System.InvalidOperationException($"Minimum word length of {gameConfiguration.MinimumWordLength} is not supported; please choose a positive, non-zero value.");
            }

            IBoggleDictionary boggleDictionary = BuildDictionary(dictionaryReader, gameConfiguration.DictionaryType, gameConfiguration.MinimumWordLength);
            IBoggleScorer scorer = BuildScorer(boggleDictionary, gameConfiguration.ScorerType);

            return CreateCustomBoggleGame(boggleDictionary, scorer);
        }

        public static BoggleGame CreateBoggleGameWithCustomDictionary(IBoggleDictionary customDictionary, ScorerType scorerType = ScorerType.Simple)
        {
            if (customDictionary == null)
            {
                throw new System.InvalidOperationException($"Invalid dictionary was provided. Please provide a non-null value.");
            }

            IBoggleScorer scorer = BuildScorer(customDictionary, scorerType);

            return CreateCustomBoggleGame(customDictionary, scorer);
        }

        public static BoggleGame CreateCustomBoggleGame(IBoggleDictionary customDictionary, IBoggleScorer customScorer)
        {
            return new BoggleGame(customDictionary, customScorer);
        }


        private static IBoggleDictionary BuildDictionary(IDictionaryReader dictionaryReader, DictionaryType dictionaryType, int minimumWordLength)
        {
            switch (dictionaryType)
            {
                default:
                case DictionaryType.CompactMemory:
                    return new TrieBasedBoggleDictionary(dictionaryReader, minimumWordLength);
            }
        }

        private static IBoggleScorer BuildScorer(IBoggleDictionary boggleDictionary, ScorerType scorerType)
        {
            switch (scorerType)
            {
                default:
                case ScorerType.Simple:
                    return new SimpleScorer(boggleDictionary);
            }
        }
    }
}
