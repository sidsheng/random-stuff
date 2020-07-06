using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Ordering
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Ordering()
        {

        }

        public void RunTests()
        {
            OrderBy();
            OrderByDescending();
            OrderMultiple();
            ReverseSequence();
        }

        private void OrderBy()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            List<Product> products = GetProductList();

            var sortedWords = from word in words
                orderby word
                select word;

            var sortedWords1 = words.OrderBy(w => w);

            Console.WriteLine("The sorted list of words:");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }

            sortedWords = from word in words
                orderby word.Length
                select word;

            sortedWords1 = words.OrderBy(w => w.Length);

            Console.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }

            var sortedProducts = from prod in products
                orderby prod.ProductName
                select prod;

            var sortedProducts1 = products.OrderBy(p => p.ProductName);

            foreach (var product in sortedProducts)
            {
                Console.WriteLine(product);
            }
        }

        private void OrderByDescending()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            List<Product> products = GetProductList();

            var sortedDoubles = from d in doubles
                orderby d descending
                select d;

            var sortedDoubles1 = doubles.OrderByDescending(d => d);

            Console.WriteLine("The doubles from highest to lowest:");
            foreach (var d in sortedDoubles)
            {
                Console.WriteLine(d);
            }

            var sortedProducts = from prod in products
                orderby prod.UnitsInStock descending
                select prod;

            var sortedProducts1 = products.OrderByDescending(p => p.UnitsInStock);

            foreach (var product in sortedProducts)
            {
                Console.WriteLine(product);
            }
        }

        private void OrderMultiple()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            List<Product> products = GetProductList();

            var sortedDigits = from digit in digits
                orderby digit.Length, digit
                select digit;

            var sortedDigits1 = digits.OrderBy(d => d.Length).ThenBy(d => d);

            Console.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                Console.WriteLine(d);
            }

            var sortedProducts = from prod in products
                orderby prod.Category, prod.UnitPrice descending
                select prod;

            var sortedProducts1 = products.OrderBy(p => p.Category).
                ThenByDescending(p => p.UnitPrice);

            foreach (var product in sortedProducts)
            {
                Console.WriteLine(product);
            }
        }

        private void ReverseSequence()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            
            var reversedIDigits = (from digit in digits
                where digit[1] == 'i'
                select digit).Reverse();

            var reversedIDigits1 = digits.Where(d => d[1] == 'i').Reverse();

            Console.WriteLine("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits)
            {
                Console.WriteLine(d);
            }
        }
    }
}