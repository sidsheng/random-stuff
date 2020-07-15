using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Conversion
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Conversion()
        {

        }

        public void RunTests()
        {
            ConvertToArray();
            ConvertToList();
            ConvertToDictionary();
            ConvertToType();
        }

        private void ConvertToArray()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            var sortedDoubles = from d in doubles
                orderby d descending
                select d;
            
            var doublesArray = sortedDoubles.ToArray();

            Console.WriteLine("Every other double from highest to lowest:");
            for (int d = 0; d < doublesArray.Length; d += 2)
            {
                Console.WriteLine(doublesArray[d]);
            }
        }

        private void ConvertToList()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            var sortedWords = from w in words
                orderby w
                select w;
            var wordList = sortedWords.ToList();

            Console.WriteLine("The sorted word list:");
            foreach (var w in wordList)
            {
                Console.WriteLine(w);
            }
        }

        private void ConvertToDictionary()
        {
            var scoreRecords = new[] { new {Name = "Alice", Score = 50},
                                new {Name = "Bob"  , Score = 40},
                                new {Name = "Cathy", Score = 45}
                            };
            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);

            Console.WriteLine(scoreRecordsDict.GetType());
            Console.WriteLine("Bob's score: {0}", scoreRecordsDict["Bob"]);
            foreach (var p in scoreRecordsDict)
            {
                Console.WriteLine($"{p.Key}: {p.Value}");
            }
        }

        private void ConvertToType()
        {
            object[] numbers = { null, 1.0, "two", 3, "four", 5, "six", 7.0 };
            var doubles = numbers.OfType<double>();

            Console.WriteLine("Numbers stored as doubles:");
            foreach (var d in doubles)
            {
                Console.WriteLine(d);
            }
        }
    }
}