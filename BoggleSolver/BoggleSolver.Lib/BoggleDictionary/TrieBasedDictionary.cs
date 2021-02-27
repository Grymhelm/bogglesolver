using System.Collections.Generic;
using System.Text;
using BoggleSolver.Lib.DictionaryReader;

namespace BoggleSolver.Lib.BoggleDictionary
{
    /// <summary>
    /// This represents one possible representation of a dictionary: a trie (https://en.wikipedia.org/wiki/Trie)
    /// 
    /// Tries are super compact way of representing word data, especially when parts of words overlap.  For example, for the words "able" and "ability", 
    /// 'a' and 'b' are only represented once in memory; expanded for an entire language of words, this can reduce overall space trememdously. 
    /// 
    /// Additionally, tries (as a tree) have the potential for very fast lookup speeds; to keep readability, I left it fairly simple here for now, 
    /// but some examples of optimizations you might make here are 
    ///   1. searching the children from both the front and back
    ///   2. using multithreading to search multiple children simultaneously
    ///   
    /// </summary>
    internal class TrieBasedBoggleDictionary : IBoggleDictionary
    {
        private readonly int _minimumWordLength;
        private const int AlphabetSize = 26;

        //Assuming a full dictionary for the English Language here is probably the most common use case; if smaller dictionaries are expected, capacity should be removed.
        //For expanded, non-English content, it might be good to build out some language configuration concepts to better control this
        private Dictionary<char, TrieNode> _firstLetterTrieMapping = new Dictionary<char, TrieNode>(AlphabetSize);

        internal TrieBasedBoggleDictionary(IDictionaryReader reader, int minimumWordLength = 3)
        {
            _minimumWordLength = minimumWordLength;

            while (reader.TryGetNextWord(out string nextWord))
            {
                //Q: where is the responsibility to sanitize input, in the dictionary or the file reader?
                // building it into the dictionary as it seems it would own the "validness" of words
                if (!string.IsNullOrWhiteSpace(nextWord) && nextWord.Length >= minimumWordLength)
                {
                    InsertWord(nextWord);
                }
            }
        }

        public bool IsWord(string word)
        {
            // TODO: A lot of this logic is shared by the InsertWord()
            // will go back to see how to extract out shared logic meaningfully later...
            char firstLetter = word[0];
            if (_firstLetterTrieMapping.ContainsKey(firstLetter))
            {
                var currentTrieNode = _firstLetterTrieMapping[firstLetter];
                bool matchFound = false;

                int currentPositionInWord = 1;
                for (; currentPositionInWord < word.Length; currentPositionInWord++)
                {
                    matchFound = false;
                    var currentLetter = word[currentPositionInWord];


                    for (int continuationIndex = 0; continuationIndex < currentTrieNode.Continuations.Count; continuationIndex++)
                    {
                        var trieNode = currentTrieNode.Continuations[continuationIndex];
                        if (currentLetter == trieNode.Letter)
                        {
                            currentTrieNode = trieNode;
                            matchFound = true;
                            break;
                        }
                    }

                    if (!matchFound) { break; }
                }

                if (matchFound && currentTrieNode.IsWord)
                {
                    return true;
                }
            }

            return false;
        }

        public IList<string> GetFullWordList()
        {
            List<string> fullWordList = new List<string>();
            //Traverse the trie to construct the list

            foreach (var root in _firstLetterTrieMapping.Values)
            {
                StringBuilder builder = new System.Text.StringBuilder(_minimumWordLength);
                AddAllWordsFromNode(root, builder, fullWordList);
            }

            return fullWordList;
        }

        private void InsertWord(string word)
        {
            char firstLetter = word[0];

            if (_firstLetterTrieMapping.ContainsKey(firstLetter))
            {
                var currentTrieNode = _firstLetterTrieMapping[firstLetter];

                //find where we need to extend the trie
                int currentPositionInWord = 1;
                for (; currentPositionInWord < word.Length; currentPositionInWord++)
                {
                    var currentLetter = word[currentPositionInWord];

                    bool matchFound = false;
                    for (int continuationIndex = 0; continuationIndex < currentTrieNode.Continuations.Count; continuationIndex++)
                    {
                        var trieNode = currentTrieNode.Continuations[continuationIndex];
                        if (currentLetter == trieNode.Letter)
                        {
                            currentTrieNode = trieNode;
                            matchFound = true;
                            break;
                        }
                    }

                    if (!matchFound) { break; }
                }

                //We have matched as many letters as we can, so now we need to add the next set of letters
                FinishWordFromRoot(currentTrieNode, word, currentPositionInWord);
            }
            else //we are adding a brand new node
            {
                //A minor edge case where the root is also the complete word (unlikely in boggle, but maybe in some other system?)
                bool isASingleLetterWord = _minimumWordLength == 1 && word.Length == 1;

                var root = new TrieNode(firstLetter, isASingleLetterWord);
                _firstLetterTrieMapping.Add(root.Letter, root);

                FinishWordFromRoot(root, word, 1); //plus 1 because we already set the root and are extending from there
            }
        }

        private void FinishWordFromRoot(TrieNode root, string word, int startingPosition)
        {
            for (int position = startingPosition; position < word.Length; position++)
            {
                var nextTrieNode = new TrieNode(word[position], position == word.Length - 1); //Asking if we are at the end to determine if isWord
                root.Continuations.Add(nextTrieNode);

                //move forward in the chain by swapping ref values
                root = nextTrieNode;
            }
        }

        private void AddAllWordsFromNode(TrieNode node, StringBuilder currentWordPart, List<string> listToModify)
        {
            currentWordPart.Append(node.Letter);
            if (node.IsWord)
            {
                listToModify.Add(currentWordPart.ToString());
            }

            foreach (TrieNode child in node.Continuations)
            {
                AddAllWordsFromNode(child, currentWordPart, listToModify);
            }
        }
    }

    public class TrieNode
    {
        public char Letter { get; }
        public bool IsWord { get; } //represents the end of a complete word
        public List<TrieNode> Continuations { get; } = new List<TrieNode>();

        public TrieNode(char letter, bool isWord)
        {
            Letter = letter;
            IsWord = isWord;
        }
    }
}
