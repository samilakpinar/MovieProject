using Business.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MovieManager : IMovieService
    {
        public async Task<string> CreateToken()
        {
            var url = $"https://api.themoviedb.org/3/authentication/token/new?api_key=a87921328b55114d690b35cec33d3aae";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateSession(CreateSession token)
        {
            var url = $"https://api.themoviedb.org/3/authentication/session/new?api_key=a87921328b55114d690b35cec33d3aae";
            var httpClient = new HttpClient();
            var json = System.Text.Json.JsonSerializer.Serialize(token);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAllPopulerMovies(int page)
        {
            var url = $"https://api.themoviedb.org/3/movie/popular?api_key=a87921328b55114d690b35cec33d3aae&language=en-US&page={page}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetMovieById(string movie_id)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movie_id}?api_key=a87921328b55114d690b35cec33d3aae";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}/account_states?api_key=a87921328b55114d690b35cec33d3aae&session_id={sessionId}&guest_session_id={guestId}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RateMovie(RateMovie rateMovie)
        {
            if(rateMovie.value <= 0 || rateMovie.value > 10)
            {
                return "Geçersiz Puan Değeri";
            }

            var url = $"https://api.themoviedb.org/3/movie/{rateMovie.MovieId}/rating?api_key=a87921328b55114d690b35cec33d3aae&guest_session_id={rateMovie.GuestId}&session_id={rateMovie.SessionId}";
            var httpClient = new HttpClient();
            var json = System.Text.Json.JsonSerializer.Serialize(rateMovie);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
