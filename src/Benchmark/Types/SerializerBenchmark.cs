using Benchmark.Models;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Types
{
    public class SerializerBenchmark : BaseBenchmark<People>
    {
        [Benchmark]
        public override byte[] Serialize()
        {
            return Converter.Serialize(Model);
        }

        [Benchmark]
        public override People Deserialize()
        {
            return Converter.Deserialize<People>(Serialized);
        }

        protected override People CreateModel()
        {
            return DataGenerator.CreatePeople();
        }
    }
}
