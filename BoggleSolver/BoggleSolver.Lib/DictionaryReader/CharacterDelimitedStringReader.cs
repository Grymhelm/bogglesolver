﻿using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BoggleSolver.Lib.DictionaryReader
{
    /// <summary>
    /// The simplest form of a dictionary file representation: a string or a text file with some character separating each value
    /// </summary>
    public class CharacterDelimitedStringReader : IDictionaryReader
    {
        private List<string> _words;
        private readonly char _delimiter;
        private int _currentPosition = 0;

        public CharacterDelimitedStringReader(char delimiter, string rawContentsString = null)
        {
            _delimiter = delimiter;

            InitializeWordListUsingDelimiter(rawContentsString);
        }

        public bool TryReadFromTextFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                StringBuilder contents = new StringBuilder(string.Empty);

                if (_delimiter != '\n' && _delimiter != '\r') //if it's not a new line delimiter
                {
                    while (!reader.EndOfStream)
                    {
                        //reading individual lines to prevent odd behaviors around line feeds being included in words
                        contents.Append(reader.ReadLine());
                    }
                }
                else //we can just read the whole file in at once
                {
                    contents = contents.Append(reader.ReadToEnd());
                }

                InitializeWordListUsingDelimiter(contents.ToString());
            }

            return true;
        }


        public bool TryGetNextWord(out string nextWord)
        {
            if (_words == null || _words.Count <= 0 || _currentPosition >= _words.Count)
            {
                nextWord = null;
                return false;
            }

            nextWord = _words[_currentPosition];
            _currentPosition++;

            return true;
        }

        private void InitializeWordListUsingDelimiter(string contents)
        {
            if (string.IsNullOrEmpty(contents) || _delimiter == '\0')
            {
                return;
            }

            string[] wordsFound = contents.Split(_delimiter);
            _words = new List<string>(wordsFound.Length);
            _currentPosition = 0;

            foreach (var word in wordsFound)
            {
                //We are not sanitizing much here; functionally, this assumes however you have broken up your contents is "correct"
                _words.Add(word.Trim());
            }
        }
    }
}
