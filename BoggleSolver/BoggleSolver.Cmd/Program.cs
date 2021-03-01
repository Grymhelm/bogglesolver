
using BoggleSolver.Lib;
using BoggleSolver.Lib.DictionaryReader;
using System;
using System.Text;

namespace BoggleSolver.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            int defaultRows = 4;
            int defaultCols = 4;

            //Note: this was borrowed from https://github.com/dwyl/english-words/blob/master/words_alpha.txt
            string defaultFileName = "FullDictionary.txt";

            Console.Write("Enter the number of rows: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int rows) || rows < 1)
            {
                Console.WriteLine($"***INVALID ENTRY: {input} - Setting rows = {defaultRows}");
                rows = defaultRows;
            }

            Console.Write("Enter the number of columns: ");
            input = Console.ReadLine();

            if (!int.TryParse(input, out int columns) || columns < 1)
            {
                Console.WriteLine($"***INVALID ENTRY: {input} - Setting rows = {defaultCols}");
                columns = defaultCols;
            }

            Console.Write("Enter the file path to your dictionary (leave blank to use default): ");
            input = Console.ReadLine();


            CharacterDelimitedStringReader reader;
            if (!string.IsNullOrWhiteSpace(input))
            {
                string fileName = input;

                Console.Write("What character did you use for separating words? ");
                input = Console.ReadLine();

                if (!char.TryParse(input, out char delimiter))
                {
                    Console.WriteLine($"***INVALID ENTRY: {input} - Setting delimiter = '\n'");
                    reader = new CharacterDelimitedStringReader('\n');
                }
                else
                {
                    reader = new CharacterDelimitedStringReader(delimiter);
                }

                if (!reader.TryReadFromTextFile(fileName))
                {
                    Console.WriteLine($"***UNABLE TO READ FILE - Falling back to default dictionary");
                    reader = new CharacterDelimitedStringReader('\n');
                    reader.TryReadFromTextFile(defaultFileName);
                }
            }
            else
            {
                reader = new CharacterDelimitedStringReader('\n');
                reader.TryReadFromTextFile(defaultFileName);
            }

            Console.WriteLine(" ----  SETUP COMPLETE!  Prepare to play!! ----");

            GameConfiguration gameConfiguration = new GameConfiguration
            {
                DictionaryType = DictionaryType.CompactMemory,
                ScorerType = ScorerType.Simple,
                MinimumWordLength = 3 //can extract this as a custom settable property later
            };

            var boggleGame = BoggleGameFactory.CreateBoggleGame(reader, gameConfiguration);

            var gameBoard = GenerateGameBoard(rows, columns);

            Console.WriteLine("\n\n" + GetBoardAsText(gameBoard));

            Console.Write("\nEnter your answers separated by a comma: ");
            input = Console.ReadLine();

            var wordList = input.Split(',');

            var scoringWords = boggleGame.BoggleScorer.ScoreWordList(wordList);

            Console.WriteLine(" ---- CORRECT ANSWERS ----");

            int totalScore = 0;
            foreach (var scoringWord in scoringWords)
            {
                Console.WriteLine($" + {scoringWord.Points} pts | {scoringWord.Word}");
                totalScore += scoringWord.Points;
            }
            Console.WriteLine($" = TOTAL POINTS: {totalScore}");


            Console.WriteLine("\n\n PRESS ENTER TO SEE ALL POSSIBLE ANSWERS!! ");
            Console.ReadLine();

            var allAnswers = boggleGame.BoggleScorer.GetAllAnswers(gameBoard);

            foreach (var answer in allAnswers)
            {
                Console.Write($"  {answer.Word}  ");
            }

            System.Console.WriteLine("\n\n Press ENTER to quit... ");
            System.Console.ReadLine();
        }

        private static char[][] GenerateGameBoard(int rows, int cols)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char[][] gameBoard = new char[rows][];
            Random rng = new Random();

            for (int row = 0; row < rows; row++)
            {
                char[] newRow = new char[cols];
                for (int col = 0; col < cols; col++)
                {
                    newRow[col] = GetRandomCharacter(alphabet, rng);
                }
                gameBoard[row] = newRow;
            }

            return gameBoard;
        }

        private static char GetRandomCharacter(string text, Random rng)
        {
            int index = rng.Next(text.Length);
            return text[index];
        }

        private static string GetBoardAsText(char[][] board)
        {
            StringBuilder boardAsText = new StringBuilder();
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board[row].Length; col++)
                {
                    boardAsText.Append($"  {board[row][col]}  ");
                }
                boardAsText.Append("\n\n");
            }

            return boardAsText.ToString();
        }
    }
}
