using BenchmarkDotNet.Attributes;
using Business.Models;
using Business.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Business.Concrete
{
    [MemoryDiagnoser]
    public class MovieUrlPerformanceTest
    {
        private static HttpClient httpClient = new HttpClient();

        [Benchmark]
        public async void AllMovieUrlTest()
        {

            var url = $"https://api.themoviedb.org/3/movie/popular?api_key=a87921328b55114d690b35cec33d3aae&language=en-US&page=1";
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<PopularMoviesResponse>(jsonAsString);
            Console.WriteLine("All movie url tested ");


        }

        [Benchmark]
        public async void MovieByIdUrlTest()
        {

            var url = $"https://api.themoviedb.org/3/movie/550?api_key=a87921328b55114d690b35cec33d3aae";
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(jsonAsString);
            Console.WriteLine("Movie by id tested ");

        }

        [Benchmark]
        public async void MovieVideoByIdUrlTest()
        {
            var url = $"https://api.themoviedb.org/3/movie/550/videos?api_key=a87921328b55114d690b35cec33d3aae";
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<MovieVideoResponse>(jsonAsString);
            Console.WriteLine("Movie video by id tested ");
        }
    }
}
