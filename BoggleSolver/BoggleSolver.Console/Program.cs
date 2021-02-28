using System;
using System.Collections.Generic;
using System.Text;

namespace BoggleSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dictionary = new TrieBasedBoggleDictionary(new SimpleTestDictionary());

            //var list = dictionary.GetFullWordList();

            //var result = dictionary.IsWord("abbot");
            //result = dictionary.IsWord("able");
            //result = dictionary.IsWord("ability");

            bool[][] visited = new bool[3][];
            for (int index = 0; index < 3; index++)
            {
                visited[index] = new bool[3];
            }
        }
    }

     
}
