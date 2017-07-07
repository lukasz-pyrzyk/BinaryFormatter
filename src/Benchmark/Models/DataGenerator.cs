using System;
using System.Collections.Generic;

namespace Benchmark.Models
{
    public class DataGenerator
    {
        public static People CreatePeople()
        {
            var model = new People
            {
                Age = 50,
                Birthday = DateTime.Today.AddYears(-50),
                Name = "Knuth",
                Friends = new List<string>
                {
                   "Linus Torvalds",
                   "Bill Gates",
                    "Steve Jobs",
                }
            };

            return model;
        }
    }
}
