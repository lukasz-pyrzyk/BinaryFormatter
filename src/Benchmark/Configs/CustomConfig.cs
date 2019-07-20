using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;

namespace Benchmark.Configs
{
    public class CustomConfig : ManualConfig
    {
        public CustomConfig()
        {
            Add(MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);
        }
    }
}
