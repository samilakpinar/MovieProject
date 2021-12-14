using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllPopulerMovies(int page);
        Task<Movie> GetMovieById(int movie_id);
        Task<string> GetRateMovie(int movieId, string sessionId, string guestId);
        Task<string> RateMovie(RateMovie rateMovie);
        Task<List<Movie>> GetUpcomingMovie(int page);
        Task<List<MovieVideo>> GetMovieVideoById(int movieId);
    }
}
