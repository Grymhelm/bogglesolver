using BoggleSolver.Lib.DictionaryReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BoggleSolver.UnitTests
{
    [TestClass]
    public class DictionaryReaderTests
    {
        [TestMethod]
        public void CharacterDelimitedReader_SingleWord()
        {
            string word = "word";

            CharacterDelimitedStringReader reader = new CharacterDelimitedStringReader(',', word);

            Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
            Assert.AreEqual(word, nextWord);
        }

        [TestMethod]
        public void CharacterDelimitedReader_RawString()
        {
            List<string> testWords = new List<string> { "word1", "word2", "word3", "word4" };
            char delimiter = ',';

            CharacterDelimitedStringReader reader = new CharacterDelimitedStringReader(delimiter, string.Join(delimiter,testWords));

            //order should match input
            foreach(var word in testWords)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }
        }

        [TestMethod]
        public void CharacterDelimitedReader_TryReadFromTextFile_SingleWord()
        {
            string textFileName = "testfile.txt";
            string word = "word";

            //Necessary as test may only run partially, leaving old file behind
            if (File.Exists(textFileName))
            {
                File.Delete(textFileName);
            }

            using(StreamWriter writer = new StreamWriter(textFileName))
            {
                writer.Write(word);
            }

            CharacterDelimitedStringReader reader = new CharacterDelimitedStringReader(',');

            Assert.IsTrue(reader.TryReadFromTextFile(textFileName));
            Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
            Assert.AreEqual(word, nextWord);

            File.Delete(textFileName);
        }

        [TestMethod]
        public void CharacterDelimitedReader_TryReadFromTextFile_SingleLine()
        {
            string textFileName = "testfile.txt";
            List<string> testWords = new List<string> { "word1", "word2", "word3", "word4" };
            char delimiter = ',';

            //Necessary as test may only run partially, leaving old file behind
            if (File.Exists(textFileName))
            {
                File.Delete(textFileName);
            }

            using (StreamWriter writer = new StreamWriter(textFileName))
            {
                writer.Write(string.Join(delimiter, testWords));
            }

            CharacterDelimitedStringReader reader = new CharacterDelimitedStringReader(delimiter);
            Assert.IsTrue(reader.TryReadFromTextFile(textFileName));

            //order should match input
            foreach (var word in testWords)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }
        }

        [TestMethod]
        public void CharacterDelimitedReader_TryReadFromTextFile_MultipleLines_WindowsLineFeed()
        {
            string textFileName = "testfile.txt";
            List<string> testWordsLine1 = new List<string> { "word1", "word2", "word3", "word4" };
            List<string> testWordsLine2 = new List<string> { "word5", "word6", "word7", "word8" };
            List<string> testWordsLine3 = new List<string> { "word9", "word10" };
            char delimiter = ',';

            //Necessary as test may only run partially, leaving old file behind
            if (File.Exists(textFileName))
            {
                File.Delete(textFileName);
            }

            using (StreamWriter writer = new StreamWriter(textFileName))
            {
                writer.Write(string.Join(delimiter, testWordsLine1) + delimiter);
                writer.Write("\r\n" + string.Join(delimiter, testWordsLine2) + delimiter);
                writer.Write("\r\n" + string.Join(delimiter, testWordsLine3));
            }

            CharacterDelimitedStringReader reader = new CharacterDelimitedStringReader(delimiter);
            Assert.IsTrue(reader.TryReadFromTextFile(textFileName));

            //order should match input
            foreach (var word in testWordsLine1)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }

            foreach (var word in testWordsLine2)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }

            foreach (var word in testWordsLine3)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }
        }

        [TestMethod]
        public void CharacterDelimitedReader_TryReadFromTextFile_MultipleLines_UnixLineFeed()
        {
            string textFileName = "testfile.txt";
            List<string> testWordsLine1 = new List<string> { "word1", "word2", "word3", "word4" };
            List<string> testWordsLine2 = new List<string> { "word5", "word6", "word7", "word8" };
            List<string> testWordsLine3 = new List<string> { "word9", "word10" };
            char delimiter = ',';

            //Necessary as test may only run partially, leaving old file behind
            if (File.Exists(textFileName))
            {
                File.Delete(textFileName);
            }

            using (StreamWriter writer = new StreamWriter(textFileName))
            {
                writer.Write(string.Join(delimiter, testWordsLine1) + delimiter);
                writer.Write("\n" + string.Join(delimiter, testWordsLine2) + delimiter);
                writer.Write("\n" + string.Join(delimiter, testWordsLine3));
            }

            CharacterDelimitedStringReader reader = new CharacterDelimitedStringReader(delimiter);
            Assert.IsTrue(reader.TryReadFromTextFile(textFileName));

            //order should match input
            foreach (var word in testWordsLine1)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }

            foreach (var word in testWordsLine2)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }

            foreach (var word in testWordsLine3)
            {
                Assert.IsTrue(reader.TryGetNextWord(out string nextWord));
                Assert.AreEqual(word, nextWord);
            }
        }
    }
}
