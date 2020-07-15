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
            SetIntersect();
            SetIntersectProductCategories();
            SetExcept();
            SetExceptProductCategories();
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

        private void SetIntersect()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var commonNumbers = numbersA.Intersect(numbersB);

            Console.WriteLine("Common numbers shared by both arrays:");
            foreach (var n in commonNumbers)
            {
                Console.WriteLine(n);
            }
        }

        private void SetIntersectProductCategories()
        {
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();

            var productFirstChars = from p in products
                select p.ProductName[0];
            var customerFirstchars = from c in customers
                select c.CompanyName[0];
            
            var commonFirstChars = productFirstChars.Intersect(customerFirstchars);

            Console.WriteLine("Common first letters from Product names and Customer names:");
            foreach (var ch in commonFirstChars)
            {
                Console.WriteLine(ch);
            }
        }

        private void SetExcept()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var aOnlyNumbers = numbersA.Except(numbersB);

            Console.WriteLine("Numbers in first array but not second array:");
            foreach (var n in aOnlyNumbers)
            {
                Console.WriteLine(n);
            }
        }

        private void SetExceptProductCategories()
        {
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();

            var productFirstChars = from p in products
                select p.ProductName[0];
            var customerFirstchars = from c in customers
                select c.CompanyName[0];
            
            var productOnlyFirstChars = productFirstChars.Except(customerFirstchars);

            Console.WriteLine("First letters from Product names, but not from Customer names:");
            foreach (var ch in productOnlyFirstChars)
            {
                Console.WriteLine(ch);
            }
        }
    }
}