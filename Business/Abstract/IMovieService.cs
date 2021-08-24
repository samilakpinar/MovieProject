using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieService
    {
       
        Task<List<Movie>> GetAllPopulerMovies(int page);
        Task<Movie> GetMovieById(string movie_id);
        Task<string> GetRateMovie(int movieId, string sessionId, string guestId);
        Task<string> RateMovie(RateMovie rateMovie);
    }
}
