using System;
using System.Collections.Generic;

namespace Benchmark.Models
{
    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public List<string> Friends { get; set; }
    }
}