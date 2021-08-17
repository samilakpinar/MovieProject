using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieService
    {
        Task<string> CreateToken();
        Task<string> CreateSession(CreateSession token);
        Task<string> GetAllPopulerMovies(int page);
        Task<string> GetMovieById(string movie_id);
        Task<string> GetRateMovie(int movieId, string sessionId, string guestId);
        Task<string> RateMovie(RateMovie rateMovie);
    }
}
