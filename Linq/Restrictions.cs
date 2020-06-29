using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
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
            ExamineSequence();
            FilterBasedOnPosition();
        }

        private void RestrictionOperators()
        {
            int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
            var lowNumbers =
                from num in numbers
                where num < 5
                select num;

            // non query language format
            var lowNumbers1 = numbers.
                Where(num => num < 5).
                Select(num => num);
                                
            Console.WriteLine("Numbers < 5:");

            foreach (var x in lowNumbers)
            {
                Console.WriteLine(x);
            }
        }

        private void FilterElements()
        {
            List<Product> products = GetProductList();

            var soldOutProducts =
                from prod in products
                where prod.UnitsInStock == 0
                select prod;
            
            var soldOutProducts1 = products.
                Where(prod => prod.UnitsInStock == 0).
                Select(prod => prod);

            Console.WriteLine("Sold out products:");

            foreach (var product in soldOutProducts)
            {
                Console.WriteLine(product.ProductName);
            }

            var expensiveInStockProducts =
                from prod in products
                where prod.UnitsInStock > 0 && prod.UnitPrice > 3
                select prod;

            var expensiveInStockProducts1 = products.
                Where(prod => prod.UnitsInStock > 0 && prod.UnitPrice > 3).
                Select(prod => prod);

            Console.WriteLine("In stock expensive products:");

            foreach (var product in expensiveInStockProducts)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private void ExamineSequence()
        {
            List<Customer> customers = GetCustomerList();
            var waCustomers = from cust in customers
                where cust.Region == "WA"
                select cust;

            Console.WriteLine("Customers from Washington and their orders:");
            foreach (var customer in waCustomers)
            {
                Console.WriteLine($"Customer {customer.CustomerID}: {customer.CompanyName}");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"  Order {order.OrderID}: {order.OrderDate}");
                }
            }
        }

        private void FilterBasedOnPosition()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            Console.WriteLine("Short digits:");
            foreach (var d in shortDigits)
            {
                Console.WriteLine($"The word {d} is shorter than its value.");
            }
        }
    }
}
