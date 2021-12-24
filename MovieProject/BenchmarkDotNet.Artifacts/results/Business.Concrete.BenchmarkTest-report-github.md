``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.18363.1621 (1909/November2019Update/19H2)
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|                Method |     Mean |   Error |   StdDev | Allocated |
|---------------------- |---------:|--------:|---------:|----------:|
|   CalculateForVersion | 257.0 μs | 5.10 μs | 14.22 μs |      88 B |
| CalculateWhileVersion | 260.8 μs | 5.20 μs |  9.89 μs |      88 B |
