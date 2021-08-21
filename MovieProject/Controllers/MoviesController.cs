using Business.Abstract;
using Business.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Route("api/v1/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [HttpGet("get-populer-movie")]
        public Task<List<Movie>> GetPopulerMovie(int page)
        {
            return _movieService.GetAllPopulerMovies(page);
        }

        [HttpGet("get-movie-by-id")]
        public Task<Movie> GetMovieById(string movie_id)
        {
            return _movieService.GetMovieById(movie_id);
        }

        [HttpGet("get-rate-movie")]
        public Task<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {
            return _movieService.GetRateMovie(movieId, sessionId, guestId);
        }

        //Business alanında iş kodu yazılması gerekli çünkü verebileceği puan 1 ile 10 arasıdır. 1 ile 10 arası değil ise request atılmayacak.
        //Not ekleme olayı buarada yapılmalı, şuan puan ekleme oluyor
        [HttpPost("rate-movie")]
        public Task<string> RateMovie([FromBody] RateMovie rateMovie)
        {
            return _movieService.RateMovie(rateMovie);
        }

        
        //film adına göre filtreleme eklenmesi NOT: Tüm filmleri getirilecek sistem arkada tüm filmleri dolaşığ aranan filmi bulup kullanıcıya dönecektir.



    }
}
