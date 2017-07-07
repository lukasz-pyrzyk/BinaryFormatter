using BenchmarkDotNet.Attributes;

namespace Benchmark.Types
{
    public class BoolBenchmark : BaseBenchmark<bool>
    {
	    [Benchmark]
        public override byte[] Serialize()
        {
            return Converter.Serialize(Model);
        }

	    [Benchmark]
        public override bool Deserialize()
        {
            return Converter.Deserialize<bool>(Serialized);
        }

        protected override bool CreateModel()
        {
            return true;
        }
    }
}
