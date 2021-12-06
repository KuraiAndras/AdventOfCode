using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Day6;
using System.Numerics;

_ = BenchmarkRunner.Run<Bench>();

[MemoryDiagnoser]
public class Bench
{
    private List<int> _numbers = new List<int>();

    [GlobalSetup]
    public void Setup() => _numbers = File
        .ReadAllText("Data1.txt")
        .Split(',')
        .Select(int.Parse)
        .ToList();

    [Benchmark]
    public BigInteger Simulate() => LanternfishSimulation.Simulate(_numbers, 256, 9, 7);
}
