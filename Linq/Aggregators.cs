using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Aggregators
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Aggregators()
        {

        }

        public void RunTests()
        {
            DistinctCount();
            MatchingCount();
            QueryCount();
            GroupCount();
            SumNumbers();
            SumSequence();
            SumGroup();
            MinNumbers();
            MinProjection();
            MinGroup();
            MinMatching();
            AggregateProduct();
            AggregateSeed();
        }

        private void DistinctCount()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };
            int uniqueFactors = factorsOf300.Distinct().Count();

            Console.WriteLine(uniqueFactors);
        }

        private void MatchingCount()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);

            Console.WriteLine(oddNumbers);
        }

        private void QueryCount()
        {
            List<Customer> customers = GetCustomerList();
            var orderCounts = from c in customers
                select (c.CustomerID, OrderCount: c.Orders.Count());

            foreach (var customer in orderCounts)
            {
                Console.WriteLine($"ID: {customer.CustomerID}, count: {customer.OrderCount}");
            }
        }

        private void GroupCount()
        {
            List<Product> products = GetProductList();
            var categoryCounts = from p in products
                group p by p.Category into g
                select (Category: g.Key, ProductCount: g.Count());

            foreach (var c in categoryCounts)
            {
                Console.WriteLine($"Category: {c.Category}: Product count: {c.ProductCount}");
            }
        }

        private void SumNumbers()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            double numSum = numbers.Sum();
            
            Console.WriteLine(numSum);
        }

        private void SumSequence()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            double totalChars = words.Sum(w => w.Length);

            Console.WriteLine($"There are a total of {totalChars} characters in these words.");
        }

        private void SumGroup()
        {
            List<Product> products = GetProductList();
            var categories = from p in products
                group p by p.Category into g
                select (Category: g.Key, TotalUnitsInStock: g.Sum(p => p.UnitsInStock));

            foreach (var pair in categories)
            {
                Console.WriteLine($"Category: {pair.Category}, Units in stock: {pair.TotalUnitsInStock}");
            }
        }

        private void MinNumbers()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int min = numbers.Min();
            int max = numbers.Max();
            double avg = numbers.Average();

            Console.WriteLine(min);
        }

        private void MinProjection()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            int shortestWord = words.Min(w => w.Length);
            int longestWord = words.Max(w => w.Length);
            double averageLen = words.Average(w => w.Length);

            Console.WriteLine(shortestWord);
        }

        private void MinGroup()
        {
            List<Product> products = GetProductList();
            var categories = from p in products
                group p by p.Category into g
                select (Category: g.Key, CheapestPrice: g.Min(p => p.UnitPrice));

            var categories1 = from p in products
                group p by p.Category into g
                select (Category: g.Key, MostExpensivePrice: g.Max(p => p.UnitPrice));

            var categories2 = from p in products
                group p by p.Category into g
                select (Category: g.Key, AveragePrice: g.Average(p => p.UnitPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category}, Lowest price: {c.CheapestPrice}");
            }
        }

        private void MinMatching()
        {
            List<Product> products = GetProductList();
            var categories = from p in products
                group p by p.Category into g
                let minPrice = g.Min(p => p.UnitPrice)
                select (Category: g.Key, CheapestProducts: g.Where(p => p.UnitPrice == minPrice));

            var categories1 = from p in products
                group p by p.Category into g
                let maxPrice = g.Max(p => p.UnitPrice)
                select (Category: g.Key, MostExpensiveProducts: g.Where(p => p.UnitPrice == maxPrice));

            foreach (var c in categories)
            {
                Console.WriteLine($"Category: {c.Category}");
                foreach (var p in c.CheapestProducts)
                {
                    Console.WriteLine($"\tProduct: {p}");
                }
            }
        }

        private void AggregateProduct()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            var product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Console.WriteLine($"Total product of all numbers: {product}");
        }

        private void AggregateSeed()
        {
            double startBalance = 100.0;
            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };
            var endBalance =
                attemptedWithdrawals.Aggregate(startBalance, 
                    (startBalance, nextWithdrawal) =>
                        ((nextWithdrawal < startBalance) ? (startBalance - nextWithdrawal) : startBalance));

            Console.WriteLine($"Ending balance: {endBalance}");
        }
    }
}