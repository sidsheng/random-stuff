using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Element
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Element()
        {

        }

        public void RunTests()
        {
            First();
            FirstMatching();
            FirstOrDefault();
            FirstMatchingOrDefault();
            FirstElementAtPosition();
            CreateRange();
            Repeat();
            Any();
            GroupByElementsMatchingCondition();
            AllElementsMatch();
            GroupByAllElementsMatchingCondition();
        }

        private void First()
        {
            List<Product> products = GetProductList();
            Product product12 = (from p in products
                where p.ProductID == 12
                select p).First();
            
            Console.WriteLine(product12);
        }

        private void FirstMatching()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string startsWithO = strings.First(s => s[0] == 'o');

            Console.WriteLine(startsWithO);
        }

        private void FirstOrDefault()
        {
            int[] numbers = {};
            int firstNumOrDefault = numbers.FirstOrDefault();

            Console.WriteLine(firstNumOrDefault);
        }

        private void FirstMatchingOrDefault()
        {
            List<Product> products = GetProductList();
            Product product789 = products.FirstOrDefault(p => p.ProductID == 789);

            Console.WriteLine($"Product 789 exists: {product789 != null}");
        }

        private void FirstElementAtPosition()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int fourthLowNum = (
                from n in numbers
                where n > 5
                select n
            ).ElementAt(1);

            Console.WriteLine($"2nd number > 5: {fourthLowNum}");
        }

        private void CreateRange()
        {
            var numbers = from n in Enumerable.Range(100, 50)
                select (Number: n, OddEven: n % 2 == 1 ? "odd" : "even");

            foreach (var n in numbers)
            {
                Console.WriteLine("The number {0} is {1}.", n.Number, n.OddEven);
            }
        }

        private void Repeat()
        {
            var numbers = Enumerable.Repeat(7, 10);
            foreach (var n in numbers)
            {
                Console.WriteLine(n);
            }
        }

        private void Any()
        {
            string[] words = { "believe", "relief", "receipt", "field" };
            bool iAfterE = words.Any(w => w.Contains("ei"));

            Console.WriteLine(iAfterE);
        }

        private void GroupByElementsMatchingCondition()
        {
            List<Product> products = GetProductList();
            var productGroups = from p in products
                group p by p.Category into g
                where g.Any(p => p.UnitsInStock == 0)
                select (Category: g.Key, Products: g);

            foreach (var group in productGroups)
            {
                Console.WriteLine(group.Category);
                foreach (var product in group.Products)
                {
                    Console.WriteLine($"\t{product}");
                }
            }
        }

        private void AllElementsMatch()
        {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };
            bool onlyOdd = numbers.All(n => n % 2 == 1);
            Console.WriteLine(onlyOdd);
        }

        private void GroupByAllElementsMatchingCondition()
        {
            List<Product> products = GetProductList();
            var productGroups = from p in products
                group p by p.Category into g
                where g.All(p => p.UnitsInStock > 0)
                select (Category: g.Key, Products: g);

            foreach (var group in productGroups)
            {
                Console.WriteLine(group.Category);
                foreach (var product in group.Products)
                {
                    Console.WriteLine($"\t{product}");
                }
            }
        }
    }
}