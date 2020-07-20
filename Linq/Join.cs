using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Join
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Join()
        {

        }

        public void RunTests()
        {
            CrossJoin();
            GroupJoin();
            CrossGroupJoin();
            LeftOuterJoin();
        }

        private void CrossJoin()
        {
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();
            var q = from c in categories
                join p in products on c equals p.Category
                select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
        }

        private void GroupJoin()
        {
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();
            var q = from c in categories
                join p in products on c equals p.Category into ps
                select (Category: c, Products: ps);

            foreach (var v in q)
            {
                Console.WriteLine(v.Category + ":");
                foreach (var p in v.Products)
                {
                    Console.WriteLine("   " + p.ProductName);
                }
            }
        }

        private void CrossGroupJoin()
        {
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps
                    select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
        }

        private void LeftOuterJoin()
        {
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps.DefaultIfEmpty()
                    select (Category: c, ProductName: p == null ? "(No products)" : p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine($"{v.ProductName}: {v.Category}");
            }
        }
    }
}