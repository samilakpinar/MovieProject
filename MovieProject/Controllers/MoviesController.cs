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

            var logger = NLog.LogManager.GetCurrentClassLogger();

            var movieLists = await _movieService.GetAllPopulerMovies(page);

            if (movieLists == null)
            {

                logger.Info("Movie list didn't send");
                movieList.Data = null ;
                movieList.ErrorMessages = "Movie list didn't send";

                return movieList;
            }

            logger.Info("Movie list  sent");
            movieList.Data = movieLists;
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

            var logger = NLog.LogManager.GetCurrentClassLogger();

            var movie = await _movieService.GetMovieById(movie_id);

            if (movie == null)
            {
                logger.Info("movie didn't send");
                response.Data = null;
                response.ErrorMessages = "movie didn't send";
                return response;
            }

            logger.Info("movie sent");
            response.Data = movie;
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
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var rate = await _movieService.GetRateMovie(movieId, sessionId, guestId);

            if (rate == null)
            {
                logger.Info("Rate movie didn't send");
                return null;
            }

            logger.Info("Rate movie sent");
            return rate;
        }

        
        /// <summary>
        /// User vote for the movie 
        /// </summary>
        /// <param name="rateMovie"></param>
        /// <returns>string rate movie</returns>
        [HttpPost("rate-movie")]
        public Task<string> RateMovie([FromBody] RateMovie rateMovie)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var rateMovies = _movieService.RateMovie(rateMovie);
            
            if (rateMovies == null)
            {
                logger.Info("Rate movie didn't send");
                return null;
            }

            logger.Info("Rate movie sent");
            return rateMovies;

        }
    }
}
