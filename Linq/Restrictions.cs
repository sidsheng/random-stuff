using System;
using System.Collections.Generic;
using System.Linq;

namespace random_stuff
{
    public class Restrictions
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Restrictions()
        {

        }

        public void RunTests()
        {
            RestrictionOperators();
            FilterElements();
        }

        public void RestrictionOperators()
        {
            int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
            var lowNumbers = from num in numbers
                                where num < 5
                                select num;
            Console.WriteLine("Numbers < 5:");

            foreach (var x in lowNumbers)
            {
                Console.WriteLine(x);
            }
        }

        public void FilterElements()
        {
            List<Product> products = GetProductList();

            var soldOutProducts =
                from prod in products
                where prod.UnitsInStock == 0
                select prod;
            
            Console.WriteLine("Sold out products:");

            foreach (var product in soldOutProducts)
            {
                Console.WriteLine(product.ProductName);
            }

            var expensiveInStockProducts =
                from prod in products
                where prod.UnitsInStock > 0 && prod.UnitPrice > 3
                select prod;

            Console.WriteLine("In stock expensive products:");

            foreach (var product in expensiveInStockProducts)
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}
