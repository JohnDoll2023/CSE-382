using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

/*
 * ReadWordsList ran in 0.0639 seconds
 * ReadWordsSortedSet ran in 0.7869 seconds
 * ReadWordsHashSet ran in 0.1250 seconds
 * 
 * The sortedSet took significantly longer than the other list or hashSet took to run. The List ran the quickest, and the hashSet
 * took twice as long as the List.
 * 
 * 
 * 
 * List contains 1000x ran in 3.7198 seconds
 * SortedSet contains 1000x ran in 0.0121 seconds
 * HashSet contains 1000x ran in 0.0002 seconds
 * 
 * The List took much much longer to find if the words were contained in its data structure than the sortedset and the hashset which makes sense
 * as the hashset would just look for a matching hashcode and the sortedset would be able to sort to find the correct string, while the list
 * has to iterate over every word (until a match is found) in order to find the word.
 * 
 * 
 * 
 * There are 972 3 letter words
 * List finding 3 letter words took 0.0018 seconds
 * SortedSet finding 3 letter words took 0.0111 seconds
 * HashSet finding 3 letter words took 0.0022 seconds
 * 
 * The sortedSet took much longer than the list and hashset to count the 3 letter words
 */

namespace cs
{
    public class IO
    {
        public static void IOMain(string[] args)
        {
            using (StreamWriter output = new StreamWriter("sorted.txt"))
            using (StreamReader input = new StreamReader("items.csv"))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    string[] toks = line.Split(',');
                    int[] values = new int[toks.Length];
                    for (int i = 0; i < toks.Length; i++)
                        values[i] = Int32.Parse(toks[i]);
                    Array.Sort(values);
                    foreach (int value in values)
                    {
                        output.Write(value + " ");
                    }
                    output.WriteLine();
                }
            }

            // reading words into data structure
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> list = ReadWordsList("words.txt");
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            stopwatch.Restart();

            stopwatch.Start();
            SortedSet<string> sortedSet = ReadWordsSortedSet("words.txt");
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            stopwatch.Restart();

            stopwatch.Start();
            HashSet<string> hashSet = ReadWordsHashSet("words.txt");
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            stopwatch.Restart();

            string[] words = { "COMPUTERS", "ZYMOTIC", "AARDVARK", "WORD", "DATABASIC" };

            // checking whether or not words are contained in the data set
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                foreach (string word in words)
                {
                    list.Contains(word);
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            stopwatch.Restart();

            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                foreach (string word in words)
                {
                    sortedSet.Contains(word);
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            stopwatch.Restart();

            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                foreach (string word in words)
                {
                    hashSet.Contains(word);
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            stopwatch.Restart();

            // count 3 letter words
            int count = 0;
            stopwatch.Start();
            foreach (string word in list)
            {
                if (word.Length == 3)
                {
                    count++;
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            Console.WriteLine(count);
            stopwatch.Restart();


            count = 0;
            stopwatch.Start();
            foreach (string word in sortedSet)
            {
                if (word.Length == 3)
                {
                    count++;
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            Console.WriteLine(count);
            stopwatch.Restart();

            count = 0;
            stopwatch.Start();
            foreach (string word in hashSet)
            {
                if (word.Length == 3)
                {
                    count++;
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine(ts);
            Console.WriteLine(count);
            stopwatch.Restart();

        }

        // complete
        public static List<string> ReadWordsList(string fileName)
        {
            List<string> result = new List<string>();
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    string[] toks = line.Split(',');
                    for (int i = 0; i < toks.Length; i++)
                        result.Add(toks[i]);
                }
            }
            return result;
        }


        // complete
        public static SortedSet<string> ReadWordsSortedSet(string fileName)
        {
            SortedSet<string> result = new SortedSet<string>();
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    string[] toks = line.Split(',');
                    for (int i = 0; i < toks.Length; i++)
                        result.Add(toks[i]);
                }
            }
            return result;
        }

        // complete
        public static HashSet<string> ReadWordsHashSet(string fileName)
        {
            HashSet<string> result = new HashSet<string>();
            using (StreamReader input = new StreamReader(fileName))
            {
                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    string[] toks = line.Split(',');
                    for (int i = 0; i < toks.Length; i++)
                        result.Add(toks[i]);
                }
            }
            return result;
        }
    }
}
