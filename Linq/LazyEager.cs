using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class LazyEager
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public LazyEager()
        {

        }

        public void RunTests()
        {
            Lazy();
            Eager();
            Reuse();
        }

        private void Lazy()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q = from n in numbers
                select ++i;

            // Note, the local variable 'i' is not incremented
            // until each element is evaluated (as a side-effect):
            foreach (var v in q)
            {
                Console.WriteLine($"v = {v}, i = {i}");
            }
        }

        private void Eager()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q = (from n in numbers
                select ++i).ToList();

            // Note, the local variable 'i' is not incremented
            // until each element is evaluated (as a side-effect):
            foreach (var v in q)
            {
                Console.WriteLine($"v = {v}, i = {i}");
            }
        }

        private void Reuse()
        {
            // Deferred execution lets us define a query once
            // and then reuse it later after data changes.

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var lowNumbers = from n in numbers
                where n <= 3
                select n;
            
            Console.WriteLine("First run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                Console.WriteLine(n);
            }

            for (int i = 0; i < 10; i++)
            {
                numbers[i] = -numbers[i];
            }

            // During this second run, the same query object,
            // lowNumbers, will be iterating over the new state
            // of numbers[], producing different results:
            Console.WriteLine("Second run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                Console.WriteLine(n);
            }
        }
    }
}