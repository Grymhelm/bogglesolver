using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BoggleSolver.Lib.DictionaryReader
{
    public class CharacterDelimitedStringReader : IDictionaryReader
    {
        private List<string> _words;
        private readonly char _delimiter;
        private int _currentPosition = 0;

        public CharacterDelimitedStringReader(char delimiter, string rawContentsString = null)
        {
            _delimiter = delimiter;

            SplitAndAddWordsToListFromString(rawContentsString);
        }

        public bool TryReadFromTextFile(string filePath)
        {
            if(!File.Exists(filePath))
            {
                return false;
            }

            using(StreamReader reader = new StreamReader(filePath))
            {
                StringBuilder contents = new StringBuilder(string.Empty);

                while(!reader.EndOfStream)
                {
                    //reading individual lines to prevent odd behaviors around line feeds being included in words
                    contents.Append(reader.ReadLine());
                }

                SplitAndAddWordsToListFromString(contents.ToString());
            }

            return true;
        }


        public bool TryGetNextWord(out string nextWord)
        {
            if(_words == null || _words.Count <= 0 || _currentPosition >= _words.Count)
            {
                nextWord = null;
                return false;
            }

            nextWord = _words[_currentPosition];
            _currentPosition++;

            return true;
        }

        private void SplitAndAddWordsToListFromString(string contents)
        {
            if(string.IsNullOrEmpty(contents) || _delimiter == '\0')
            {
                return;
            }

            string[] wordsFound = contents.Split(_delimiter);
            _words = new List<string>(wordsFound.Length);

            foreach (var word in wordsFound)
            {
                //We are not sanitizing here; functionally, this assumes however you have broken up your contents is "correct"
                _words.Add(word);
            }
        }
    }
}
