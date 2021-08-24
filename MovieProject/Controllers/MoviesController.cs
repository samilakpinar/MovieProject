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

        /// <summary>
        /// Get rate movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="sessionId"></param>
        /// <param name="guestId"></param>
        /// <returns></returns>
        [HttpGet("get-rate-movie")]
        public async Task<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {
            return await _movieService.GetRateMovie(movieId, sessionId, guestId);
        }

        
        /// <summary>
        /// User vote for the movie 
        /// </summary>
        /// <param name="rateMovie"></param>
        /// <returns>string rate movie</returns>
        [HttpPost("rate-movie")]
        public Task<string> RateMovie([FromBody] RateMovie rateMovie)
        {
            return _movieService.RateMovie(rateMovie);

        }

        
        //film adına göre filtreleme eklenmesi NOT: Tüm filmleri getirilecek sistem arkada tüm filmleri dolaşığ aranan filmi bulup kullanıcıya dönecektir.



    }
}
