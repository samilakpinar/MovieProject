using Business.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Business.Models;
using Business.Responses;
using Microsoft.Extensions.Options;

namespace Business.Concrete
{
    public class MovieManager : IMovieService
    {
        HttpClient httpClient = new HttpClient();
        private readonly AppSettings _appSettings;


        public MovieManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
                
        public async Task<List<Movie>> GetAllPopulerMovies(int page)
        {
            
            var url = $"{_appSettings.Url}movie/popular?api_key={_appSettings.ApiKey}&language=en-US&page={page}";
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<PopularMoviesResponse>(jsonAsString);
            return movies.Results;
        }

        public async Task<Movie> GetMovieById(string movie_id)
        {
            var url = $"{_appSettings.Url}movie/{movie_id}?api_key={_appSettings.ApiKey}";
            var response = await httpClient.GetAsync(url);
            var jsonAsString = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(jsonAsString);
            return movie;
        }

        public async Task<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {
            var url = $"{_appSettings.Url}movie/{movieId}/account_states?api_key={_appSettings.ApiKey}&session_id={sessionId}&guest_session_id={guestId}";
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RateMovie(RateMovie rateMovie)
        {
            if(rateMovie.value <= 0 || rateMovie.value > 10)
            {
                return "Geçersiz Puan Değeri";
            }

            var url = $"{_appSettings.Url}movie/{rateMovie.MovieId}/rating?api_key={_appSettings.ApiKey}&guest_session_id={rateMovie.GuestId}&session_id={rateMovie.SessionId}";
            var json = System.Text.Json.JsonSerializer.Serialize(rateMovie);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
