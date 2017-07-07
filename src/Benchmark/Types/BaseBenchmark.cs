using Benchmark.Configs;
using BenchmarkDotNet.Attributes;
using BinaryFormatter;

namespace Benchmark.Types
{
    [Config(typeof(CustomConfig))]
    public abstract class BaseBenchmark<T>
    {
        protected BinaryConverter Converter { get; } = new BinaryConverter();
        protected T Model { get; private set; }
        protected byte[] Serialized { get; private set; }

        [GlobalSetup]
        public void Setup()
        {
            Model = CreateModel();
            Serialized = Converter.Serialize(Model);
        }

        public abstract byte[] Serialize();

        public abstract T Deserialize();

        protected abstract T CreateModel();
    }
}
