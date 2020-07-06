using System;
using System.Collections.Generic;
using System.Linq;

using random_stuff.Linq.DataSources;

namespace random_stuff.Linq
{
    // Custom comparer for use with ordering operators
    public class CaseInsensitiveComparer : IComparer<string>
    {
        public int Compare(string x, string y) =>
            string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
    }

    public class CustomComparer
    {
        public void RunTests()
        {
            Ordering();
        }

        private void Ordering()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());

            foreach (var word in sortedWords)
            {
                Console.WriteLine(word);
            }

            sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

            foreach (var word in sortedWords)
            {
                Console.WriteLine(word);
            }
        }
    }
}