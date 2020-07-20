using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Sequence
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Sequence()
        {

        }

        public void RunTests()
        {
            CompareSequences();
            ConcatSequences();
            ConcatSequences1();
            Zip();
        }

        private void CompareSequences()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };
            bool match = wordsA.SequenceEqual(wordsB);

            Console.WriteLine($"The sequences match: {match}");
        }

        private void ConcatSequences()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var allNumbers = numbersA.Concat(numbersB);

            Console.WriteLine("All numbers from both arrays:");
            foreach (var n in allNumbers)
            {
                Console.WriteLine(n);
            }
        }

        private void ConcatSequences1()
        {
            List<Customer> customers = GetCustomerList();
            List<Product> products = GetProductList();
            var customerNames = from c in customers
                select c.CompanyName;
            var productNames = from p in products
                select p.ProductName;

            var allNames = customerNames.Concat(productNames);

            Console.WriteLine("Customer and product names:");
            foreach (var n in allNames)
            {
                Console.WriteLine(n);
            }
        }

        private void Zip()
        {
            int[] vectorA = { 0, 2, 4, 5, 6 };
            int[] vectorB = { 1, 3, 5, 7, 8 };
            var dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();

            Console.WriteLine($"Dot product: {dotProduct}");
        }
    }
}