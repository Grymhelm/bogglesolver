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

    public class BoggleGameFactory
    {
        public BoggleGame CreateBoggleGame(IDictionaryReader dictionaryReader, GameConfiguration gameConfiguration)
        {
            if(gameConfiguration.MinimumWordLength <= 0)
            {
                throw new System.InvalidOperationException($"Minimum word length of {gameConfiguration.MinimumWordLength} is not supported; please choose a positive, non-zero value.");
            }

            IBoggleDictionary boggleDictionary = BuildDictionary(dictionaryReader, gameConfiguration.DictionaryType, gameConfiguration.MinimumWordLength);
            IBoggleScorer scorer = BuildScorer(boggleDictionary, gameConfiguration.ScorerType);

            return CreateCustomBoggleGame(boggleDictionary, scorer);
        }

        public BoggleGame CreateBoggleGameWithCustomDictionary(IBoggleDictionary customDictionary, ScorerType scorerType = ScorerType.Simple)
        {
            if (customDictionary == null)
            {
                throw new System.InvalidOperationException($"Invalid dictionary was provided. Please provide a non-null value.");
            }

            IBoggleScorer scorer = BuildScorer(customDictionary, scorerType);

            return CreateCustomBoggleGame(customDictionary, scorer);
        }

        public BoggleGame CreateCustomBoggleGame(IBoggleDictionary customDictionary, IBoggleScorer customScorer)
        {
            return new BoggleGame(customDictionary, customScorer);
        }


        private IBoggleDictionary BuildDictionary(IDictionaryReader dictionaryReader, DictionaryType dictionaryType, int minimumWordLength)
        {
            switch (dictionaryType)
            {
                default:
                case DictionaryType.CompactMemory:
                    return new TrieBasedBoggleDictionary(dictionaryReader, minimumWordLength);
            }
        }

        private IBoggleScorer BuildScorer(IBoggleDictionary boggleDictionary, ScorerType scorerType)
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
