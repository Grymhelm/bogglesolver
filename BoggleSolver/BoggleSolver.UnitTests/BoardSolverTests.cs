using BoggleSolver.Lib;
using BoggleSolver.UnitTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BoggleSolver.UnitTests
{
    [TestClass]
    public class BoardSolverTests
    {

        [TestMethod]
        public void BoggleSolver_OrthagonalAnswers_FindsAllExpected()
        {
            //contains "cat" and "able"
            char[] row1 = new char[3] { 'c', 'a', 't' };
            char[] row2 = new char[3] { 'x', 'b', 'x' };
            char[] row3 = new char[3] { 'x', 'l', 'e' };

            char[][] board = new char[][] { row1, row2, row3 };

            var mockDictionary = new MockBoggleDictionary(new List<string> { "cat", "able" });

            var answers = BoardSolver.TestAllCombinations(board, mockDictionary);

            CollectionAssert.AreEquivalent((System.Collections.ICollection)answers, (System.Collections.ICollection) mockDictionary.GetFullWordList());
        }

        [TestMethod]
        public void BoggleSolver_DiagonalAnswers_FindsAllExpected()
        {
            //contains "cat" and "able"
            char[] row1 = new char[3] { 'c', 'b', 'x' };
            char[] row2 = new char[3] { 'l', 'a', 'x' };
            char[] row3 = new char[3] { 'x', 'e', 't' };

            char[][] board = new char[][] { row1, row2, row3 };

            var mockDictionary = new MockBoggleDictionary(new List<string> { "cat", "able" });

            var answers = BoardSolver.TestAllCombinations(board, mockDictionary);

            CollectionAssert.AreEquivalent((System.Collections.ICollection)answers, (System.Collections.ICollection)mockDictionary.GetFullWordList());
        }

        [TestMethod]
        public void BoggleSolver_DuplicateAnswers_AreIgnored()
        {
            //contains "cat" multiple times
            char[] row1 = new char[3] { 'c', 'a', 't' };
            char[] row2 = new char[3] { 'a', 'a', 't' };
            char[] row3 = new char[3] { 't', 'c', 't' };

            char[][] board = new char[][] { row1, row2, row3 };

            var mockDictionary = new MockBoggleDictionary(new List<string> { "cat"});

            var answers = BoardSolver.TestAllCombinations(board, mockDictionary);

            CollectionAssert.AreEquivalent((System.Collections.ICollection)answers, (System.Collections.ICollection)mockDictionary.GetFullWordList());
        }
    }
}
