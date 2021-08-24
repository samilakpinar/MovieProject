using Business.Abstract;
using Business.Models;
using Business.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        /// <summary>
        /// Get populer movie
        /// </summary>
        /// <param name="page"></param>
        /// <returns>List Movie</returns>
        [HttpGet("get-populer-movie")]
        public async Task<BaseResponse<List<Movie>>> GetPopulerMovie(int page)
        {
            BaseResponse<List<Movie>> movieList = new BaseResponse<List<Movie>>();
            movieList.Data = await _movieService.GetAllPopulerMovies(page);
            movieList.ErrorMessages = null;

            return  movieList;
        }


        /// <summary>
        /// Get Movie By Id
        /// </summary>
        /// <param name="movie_id"></param>
        /// <returns>BaseResponse Movie</returns>
        [HttpGet("get-movie-by-id")]
        public async Task<BaseResponse<Movie>>  GetMovieById(string movie_id)
        {
            BaseResponse<Movie> response = new BaseResponse<Movie>();
            response.Data = await _movieService.GetMovieById(movie_id);
            response.ErrorMessages = null;
            return response;
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
