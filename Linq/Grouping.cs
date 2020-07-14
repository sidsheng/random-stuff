using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    public class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => getCanonicalString(x) == getCanonicalString(y);

        public int GetHashCode(string obj) => getCanonicalString(obj).GetHashCode();

        private string getCanonicalString(string word)
        {
            char[] wordChars = word.ToCharArray();
            Array.Sort<char>(wordChars);
            return new string(wordChars);
        }
    }

    public class Grouping
    {
        public List<Product> GetProductList() => Products.ProductList;

        public List<Customer> GetCustomerList() => Customers.CustomerList;

        public Grouping()
        {

        }

        public void RunTests()
        {
            GroupIntoBuckets();
            GroupByProperty();
            GroupByKey();
            NestedGrouping();
            GroupCustomComparer();
            GroupCustomComparerNested();
        }

        private void GroupIntoBuckets()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numberGroups = from n in numbers
                group n by n % 5 into g
                select (Remainder: g.Key, Numbers: g);

            foreach (var g in numberGroups)
            {
                Console.WriteLine($"Numbers with a remainder of {g.Remainder} when divided by 5:");
                foreach (var n in g.Numbers)
                {
                    Console.WriteLine(n);
                }
            }
        }

        private void GroupByProperty()
        {
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
            var wordGroups = from w in words
                group w by w[0] into g
                select (FirstLetter: g.Key, Words: g);

            foreach (var g in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", g.FirstLetter);
                foreach (var w in g.Words)
                {
                    Console.WriteLine(w);
                }
            }
        }

        private void GroupByKey()
        {
            List<Product> products = GetProductList();
            var orderGroups = from p in products
                group p by p.Category into g
                select (Category: g.Key, Products: g);

            foreach (var orderGroup in orderGroups)
            {
                Console.WriteLine($"Products in {orderGroup.Category} category:");
                foreach (var product in orderGroup.Products)
                {
                    Console.WriteLine($"\t{product}");
                }
            }
        }

        private void NestedGrouping()
        {
            List<Customer> customers = GetCustomerList();
            var customerOrderGroups = from c in customers
                select
                (
                c.CompanyName,
                YearGroups: from o in c.Orders
                    group o by o.OrderDate.Year into yg
                    select
                    (
                    Year: yg.Key,
                    MonthGroups: from o in yg
                        group o by o.OrderDate.Month into mg
                        select (Month: mg.Key, Orders: mg)
                    )
                );

            foreach (var ordersByCustomer in customerOrderGroups)
            {
                Console.WriteLine($"Customer Name: {ordersByCustomer.CompanyName}");
                foreach (var ordersByYear in ordersByCustomer.YearGroups)
                {
                    Console.WriteLine($"\tYear: {ordersByYear.Year}");
                    foreach (var ordersByMonth in ordersByYear.MonthGroups)
                    {
                        Console.WriteLine($"\t\tMonth: {ordersByMonth.Month}");
                        foreach (var order in ordersByMonth.Orders)
                        {
                            Console.WriteLine($"\t\t\tOrder: {order}");
                        }
                    }
                }
            }
        }

        private void GroupCustomComparer()
        {
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var orderGroups = anagrams.GroupBy(w => w.Trim(), new AnagramEqualityComparer());

            foreach (var set in orderGroups)
            {
                // The key would be the first item in the set
                foreach (var word in set)
                {
                    Console.WriteLine(word);
                }
                Console.WriteLine("...");
            }
        }

        private void GroupCustomComparerNested()
        {
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var orderGroups = anagrams.GroupBy(
                w => w.Trim(),
                a => a.ToUpper(),
                new AnagramEqualityComparer()
            );

            foreach (var set in orderGroups)
            {
                Console.WriteLine(set.GetType());
                Console.WriteLine(set.Key);
                foreach (var word in set)
                {
                    Console.WriteLine($"\t{word}");
                }
            }
        }
    }
}