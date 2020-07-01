using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    class PetOwner
{
    public string Name { get; set; }
    public List<string> Pets { get; set; }
}

    public class Projections
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Projections()
        {

        }

        public void RunTests()
        {
            SelectSimple();
            SelectSingle();
            Transform();
            SelectAnonymousAndTuples();
            SelectSubset();
            SelectWithIndex();
            SelectFromMultiple();
            SelectFromRelated();
            CompoundSelectMultipleWhere();
            CompoundSelectWithIndex();
        }

        private void SelectSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numsPlusOne = from n in numbers
                select n + 1;

            var numsPlusOne1 = numbers.Select(n => n + 1);

            Console.WriteLine("Numbers + 1:");
            foreach (int i in numsPlusOne)
            {
                Console.WriteLine(i);
            }
        }

        private void SelectSingle()
        {
            List<Product> products = GetProductList();

            var productNames = from p in products
                select p.ProductName;

            var productNames1 = products.Select(p => p.ProductName);

            Console.WriteLine("Product Names:");
            foreach (string name in productNames)
            {
                Console.WriteLine(name);
            }
        }

        private void Transform()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var textNumbers = from n in numbers
                select strings[n];

            var textNumbers1 = numbers.Select(n => strings[n]);

            Console.WriteLine("Number strings:");
            foreach (var s in textNumbers)
            {
                Console.WriteLine(s);
            }
        }

        private void SelectAnonymousAndTuples()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            
            // anonymous
            var upperLowerWords = from w in words
                select new { Upper = w.ToUpper(), Lower = w.ToLower() };

            var upperLowerWords1 = words.
                Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });

            foreach (var w in upperLowerWords)
            {
                Console.WriteLine($"Upper: {w.Upper}, lower: {w.Lower}");
            }

            var digitOddEvens = from n in numbers
                select new { Digit = strings[n], Even = (n % 2 == 0)};
            
            var digitOddEvens1 = numbers
                .Select(n => new { Digit = strings[n], Even = (n % 2 == 0)});

            foreach (var d in digitOddEvens)
            {
                Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
            }

            // tuples
            var upperLowerWordsTuples = from w in words
                select (Upper: w.ToUpper(), Lower: w.ToLower());

            var upperLowerWordsTuples1 = words.
                Select(w => (Upper: w.ToUpper(), Lower: w.ToLower()));

            foreach (var w in upperLowerWordsTuples1)
            {
                Console.WriteLine($"Upper: {w.Upper}, lower: {w.Lower}");
            }

            var digitOddEvensTuples = from n in numbers
                select (Digit: strings[n], Even: n % 2 == 0);

            var digitOddEvensTuples1 = numbers.
                Select(n => (Digit: strings[n], Even: n % 2 == 0));

            foreach (var d in digitOddEvensTuples)
            {
                Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
            }
        }

        private void SelectSubset()
        {
            List<Product> products = GetProductList();
            var productInfos = from p in products
                select (p.ProductName, p.Category, Price: p.UnitPrice);
            
            Console.WriteLine("Product Info:");
            foreach (var productInfo in productInfos)
            {
                Console.WriteLine($"{productInfo.ProductName} is in the category {productInfo.Category} and costs {productInfo.Price} per unit.");
            }
        }

        private void SelectWithIndex()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var numsInPlace = numbers.
                Select((num, index) => (Num: num, InPlace: (num == index)));

            Console.WriteLine("Number: In place?");
            foreach (var n in numsInPlace)
            {
                Console.WriteLine($"{n.Num}: {n.InPlace}");
            }

            var lowNums = from n in numbers
                where n < 5
                select digits[n];

            Console.WriteLine("Numbers < 5:");
            foreach (var n in lowNums)
            {
                Console.WriteLine(n);
            }
        }

        private void SelectFromMultiple()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs = from a in numbersA
                from b in numbersB
                where a < b
                select (a, b);

            var pairs1 = numbersA.SelectMany(a => numbersB, (a, b) => new {a, b}).
                Where(ab => (ab.a < ab.b)).
                Select(ab => (ab.a, ab.b));

            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs)
            {
                Console.WriteLine($"{pair.a} is less than {pair.b}");
            }
        }

        private void SelectFromRelated()
        {
            List<Customer> customers = GetCustomerList();
            var ordersTotal =
                from c in customers
                from o in c.Orders
                where o.Total < 500.00M
                select (c.CustomerID, o.OrderID, o.Total);

            var ordersTotal1 = customers.
                SelectMany(c => c.Orders, (c, o) => (c, o)).
                Where(co => (co.o.Total < 500.00M)).
                Select(co => (co.c.CustomerID, co.o.OrderID, co.o.Total));

            foreach (var order in ordersTotal)
            {
                Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}, Total value: {order.Total}");
            }

            var ordersDate =
                from c in customers
                from o in c.Orders
                where o.OrderDate >= new DateTime(1998, 1, 1)
                select (c.CustomerID, o.OrderID, o.OrderDate);

            var ordersDate1 = customers.
                SelectMany(c => c.Orders, (c, o) => (c, o)).
                Where(co => co.o.OrderDate >= new DateTime(1998, 1, 1)).
                Select(co => (co.c.CustomerID, co.o.OrderID, co.o.OrderDate));

            foreach (var order in ordersDate)
            {
                Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}, Total date: {order.OrderDate.ToShortDateString()}");
            }
        }

        private void CompoundSelectMultipleWhere()
        {
            List<Customer> customers = GetCustomerList();
            DateTime cutoffDate = new DateTime(1997, 1, 1);
            var orders =
                from c in customers
                where c.Region == "WA"
                from o in c.Orders
                where o.OrderDate >= cutoffDate
                select (c.CustomerID, o.OrderID);
            
            var orders1 = customers.Where(c => c.Region == "WA").
                SelectMany(c => c.Orders, (c, o) => (c, o)).
                Where(co => co.o.OrderDate >= cutoffDate).
                Select(co => (co.c.CustomerID, co.o.OrderID));

            foreach (var order in orders)
            {
                Console.WriteLine($"Customer: {order.CustomerID}, Order: {order.OrderID}");
            }
        }

        public void CompoundSelectWithIndex()
        {
            List<Customer> customers = GetCustomerList();
            var customerOrders =
                customers.SelectMany(
                    (cust, custIndex) =>
                    cust.Orders.Select(o => "Customer #" + (custIndex + 1) +
                    " has an order with OrderID " + o.OrderID));

            foreach (var order in customerOrders)
            {
                Console.WriteLine(order);
            }
        }
    }
}