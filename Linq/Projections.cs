using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Projections
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Projections()
        {

        }

        public void RunTests()
        {
            //SelectSimple();
            //SelectSingle();
            //Transform();
            //SelectAnonymousAndTuples();
            SelectSubset();
        }

        private void SelectSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numsPlusOne = from n in numbers
                select n + 1;

            var numsPlusOne1 = numbers.Select(n => n + 1);

            Console.WriteLine("Numbers + 1:");
            foreach (int i in numsPlusOne)
            {
                Console.WriteLine(i);
            }
        }

        private void SelectSingle()
        {
            List<Product> products = GetProductList();

            var productNames = from p in products
                select p.ProductName;

            var productNames1 = products.Select(p => p.ProductName);

            Console.WriteLine("Product Names:");
            foreach (string name in productNames)
            {
                Console.WriteLine(name);
            }
        }

        private void Transform()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var textNumbers = from n in numbers
                select strings[n];

            var textNumbers1 = numbers.Select(n => strings[n]);

            Console.WriteLine("Number strings:");
            foreach (var s in textNumbers)
            {
                Console.WriteLine(s);
            }
        }

        private void SelectAnonymousAndTuples()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            
            // anonymous
            var upperLowerWords = from w in words
                select new { Upper = w.ToUpper(), Lower = w.ToLower() };

            var upperLowerWords1 = words.
                Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });

            foreach (var w in upperLowerWords)
            {
                Console.WriteLine($"Upper: {w.Upper}, lower: {w.Lower}");
            }

            var digitOddEvens = from n in numbers
                select new { Digit = strings[n], Even = (n % 2 == 0)};
            
            var digitOddEvens1 = numbers
                .Select(n => new { Digit = strings[n], Even = (n % 2 == 0)});

            foreach (var d in digitOddEvens)
            {
                Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
            }

            // tuples
            var upperLowerWordsTuples = from w in words
                select (Upper: w.ToUpper(), Lower: w.ToLower());

            var upperLowerWordsTuples1 = words.
                Select(w => (Upper: w.ToUpper(), Lower: w.ToLower()));

            foreach (var w in upperLowerWordsTuples1)
            {
                Console.WriteLine($"Upper: {w.Upper}, lower: {w.Lower}");
            }

            var digitOddEvensTuples = from n in numbers
                select (Digit: strings[n], Even: n % 2 == 0);

            var digitOddEvensTuples1 = numbers.
                Select(n => (Digit: strings[n], Even: n % 2 == 0));

            foreach (var d in digitOddEvensTuples)
            {
                Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
            }
        }

        private void SelectSubset()
        {
            List<Product> products = GetProductList();
            var productInfos = from p in products
                select (p.ProductName, p.Category, Price: p.UnitPrice);
            
            Console.WriteLine("Product Info:");
            foreach (var productInfo in productInfos)
            {
                Console.WriteLine($"{productInfo.ProductName} is in the category {productInfo.Category} and costs {productInfo.Price} per unit.");
            }
        }
    }
}