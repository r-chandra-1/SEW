using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SEWTest
{
    class Program
    {

        private static List<string> _words = new List<string>();

        static void Main(string[] args)
        {
            LoadData();

            var largestWord = "";
            var nextLargestWord = "";
            var totalCombinationWords = 0;
            var wordsProcessed = 0;

            foreach (var word in _words)
            {

                wordsProcessed++;

                var probableWord = "";
                var matched = 0; //should atleast be 2 matches to be considered a combination word

                //no way to split, so make a guess by going through each char association
                var chars = word.ToCharArray();

                foreach (var c in chars)
                {
                    probableWord += c;

                    if (_words.Contains(probableWord))
                    {
                        probableWord = ""; //reset if you found a small word inside the big word
                        matched++;
                    }
                }

                //we are good if no chars are left to be matched and we found atleast 2 matches
                if (string.IsNullOrEmpty(probableWord) && matched > 1)
                {
                    totalCombinationWords++;
                    if (word.Length > largestWord.Length)
                    {
                        nextLargestWord = largestWord;
                        largestWord = word;
                    }
                    else if (word.Length > nextLargestWord.Length)
                    {

                        nextLargestWord = word;
                    }

                }
                else //try going in reverse order  
                {
                    probableWord = "";
                    matched = 0;

                    for (var i = chars.Length - 1; i >= 0; i--)
                    {
                        probableWord += chars[i];


                        //Console.WriteLine($"probableWord {probableWord}");
                        var reversed = new string(probableWord.ToCharArray().Reverse().ToArray());

                        if (_words.Contains(reversed))
                        {
                            //Console.WriteLine($"resetting {reversed}");
                            probableWord = ""; //reset
                            matched++;
                        }
                    }

                    if (string.IsNullOrEmpty(probableWord) && matched > 1)
                    {
                        totalCombinationWords++;
                        if (word.Length > largestWord.Length)
                        {
                            nextLargestWord = largestWord;
                            largestWord = word;
                        }
                        else if (word.Length > nextLargestWord.Length)
                        {

                            nextLargestWord = word;
                        }

                    }
                }

                Console.Clear();
                Console.WriteLine($"Words that can be constructed: {totalCombinationWords}/ processed: {wordsProcessed}/ total: {_words.Count}");


            }

            Console.WriteLine($"largestWord {largestWord}, nextLargestWord {nextLargestWord}");

            Console.ReadLine();
        }




        private static void LoadData()
        {

            //_words.Add("cat");
            //_words.Add("catratcat");
            //_words.Add("cats");
            //_words.Add("rat");
            //_words.Add("catrat");


            //_words.Add("rat");
            //_words.Add("cat");
            //_words.Add("catbatrat");
            //_words.Add("catbat");

            //         _words.Add("cat");
            //_words.Add("cats");
            //_words.Add("catsdogcats");
            //_words.Add("catxdogcatsrat");
            //_words.Add("dog");
            //_words.Add("dogcatsdog");
            //_words.Add("hippopotamuses");
            //_words.Add("rat");
            //_words.Add("ratcatdogcat");


            var words = System.IO.File.ReadAllLines("wordlist.txt");
            _words = words.ToList();


        }
    }
}
