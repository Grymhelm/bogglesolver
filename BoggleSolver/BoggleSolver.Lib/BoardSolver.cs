using BoggleSolver.Lib.BoggleDictionary;
using System.Collections.Generic;
using System.Text;

namespace BoggleSolver.Lib
{
    public static class BoardSolver
    {
        public static List<string> TestAllCombinations(char[][] board, IBoggleDictionary validAnswers)
        {
            List<string> wordsFound = new List<string>();

            //we require at least a 1 x 1 board to test
            if (board.Length > 0 && board[0].Length > 0)
            {
                //Build visited matrix
                bool[][] visited = new bool[board.Length][];
                for (int index = 0; index < board.Length; index++)
                {
                    visited[index] = new bool[board[0].Length];
                }

                CheckBoardRecursive(wordsFound, validAnswers, board, visited, 0, 0, new StringBuilder());
            }

            return wordsFound;
        }

        // A recursive function to print all words present on boggle 
        private static void CheckBoardRecursive(
            List<string> wordList,
            IBoggleDictionary dictionary,
            char[][] board,
            bool[][] visited,
            int i,
            int j, 
            StringBuilder currentString)
        {
            // We have been to this cell, so mark it and append
            visited[i][j] = true;
            currentString.Append(board[i][j]);

            string str = currentString.ToString();
            if (dictionary.IsWord(str))
            {
                wordList.Add(str);
            }

            // Traverse neighbors
            for (int row = i - 1; row <= i + 1 && row < board.Length; row++)
                for (int col = j - 1; col <= j + 1 && col < board[0].Length; col++)
                    if (row >= 0 && col >= 0 && !visited[row][col])
                        CheckBoardRecursive(wordList, dictionary, board, visited, row, col, currentString);

            //Remove the character we just visited; could alternatively use a cached builder, but this allows us to reuse the same buffer
            currentString.Remove(currentString.Length - 1, 1);
            visited[i][j] = false;
        }
    }
}
