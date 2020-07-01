using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class Partitions
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Partitions()
        {

        }

        public void RunTests()
        {
            Take();
            NestedTake();
            Skip();
            NestedSkip();
            TakeWhile();
            TakeWhileIndexed();
            SkipWhile();
            SkipWhileIndexed();
        }

        private void Take()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var first3 = numbers.Take(3);

            Console.WriteLine("First 3 numbers:");
            foreach (var n in first3)
            {
                Console.WriteLine(n);
            }
        }

        private void NestedTake()
        {
            List<Customer> customers = GetCustomerList();
            var first3WAOrders = (
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select (cust.CustomerID, order.OrderID, order.OrderDate))
            .Take(3);

            Console.WriteLine("First 3 orders in WA:");
            foreach (var order in first3WAOrders)
            {
                Console.WriteLine(order);
            }
        }

        private void Skip()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var allButFirst4 = numbers.Skip(4);

            Console.WriteLine("All but first 4 numbers:");
            foreach (var n in allButFirst4)
            {
                Console.WriteLine(n);
            }
        }

        public void NestedSkip()
        {
            List<Customer> customers = GetCustomerList();
            var waOrders = from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select (cust.CustomerID, order.OrderID, order.OrderDate);

            var allButFirst2Orders = waOrders.Skip(2);

            Console.WriteLine("All but first 2 orders in WA:");
            foreach (var order in allButFirst2Orders)
            {
                Console.WriteLine(order);
            }
        }

        private void TakeWhile()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);

            Console.WriteLine("First numbers less than 6:");
            foreach (var num in firstNumbersLessThan6)
            {
                Console.WriteLine(num);
            }
        }

        private void TakeWhileIndexed()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var firstSmallNumbers =
                numbers.TakeWhile((n, index) => n >= index);

            Console.WriteLine("First numbers not less than their position:");
            foreach (var num in firstSmallNumbers)
            {
                Console.WriteLine(num);
            }
        }

        private void SkipWhile()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var allButFirst3Numbers = numbers.SkipWhile(n => n % 3 != 0);

            Console.WriteLine("All elements starting from first element divisible by 3:");
            foreach (var n in allButFirst3Numbers)
            {
                Console.WriteLine(n);
            }
        }

        private void SkipWhileIndexed()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var laterNumbers = numbers.SkipWhile((n, index) => n >= index);

            Console.WriteLine("All elements starting from first element less than its position:");
            foreach (var n in laterNumbers)
            {
                Console.WriteLine(n);
            }
        }
    }
}