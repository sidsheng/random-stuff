using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Sets
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Sets()
        {

        }

        public void RunTests()
        {
            FindDistinct();
            FindDistinctCategories();
            SetUnion();
            SetUnionProductCompany();
        }

        private void FindDistinct()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };
            var uniqueFactors = factorsOf300.Distinct();

            Console.WriteLine("Prime factors of 300:");
            foreach (var f in uniqueFactors)
            {
                Console.WriteLine(f);
            }
        }

        private void FindDistinctCategories()
        {
            List<Product> products = GetProductList();
            var categoryNames = (from p in products
                select p.Category).Distinct();
            
            Console.WriteLine(categoryNames.GetType());
            Console.WriteLine("Category names:");
            foreach (var n in categoryNames)
            {
                Console.WriteLine(n);
            }
        }

        private void SetUnion()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var uniqueNumbers = numbersA.Union(numbersB);

            Console.WriteLine("Unique numbers from both arrays:");
            foreach (var n in uniqueNumbers)
            {
                Console.WriteLine(n);
            }
        }

        private void SetUnionProductCompany()
        {
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();

            var productFirstChars = from p in products
                select p.ProductName[0];
            var customerFirstChars = from c in customers
                select c.CompanyName[0];

            var uniqueFirstChars = productFirstChars.Union(customerFirstChars);
            Console.WriteLine("Unique first letters from Product names and Customer names:");
            foreach (var ch in uniqueFirstChars)
            {
                Console.WriteLine(ch);
            }
        }
    }
}