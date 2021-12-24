``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.18363.1621 (1909/November2019Update/19H2)
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.301
  [Host] : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|              Method | page | Mean | Error |
|-------------------- |----- |-----:|------:|
| GetAllPopulerMovies |    3 |   NA |    NA |

Benchmarks with issues:
  MovieManager.GetAllPopulerMovies: DefaultJob [page=3]
