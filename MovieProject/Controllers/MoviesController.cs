using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Result;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

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
        public async Task<ServiceResult<List<Movie>>> GetPopulerMovie(int page)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var movieLists = await _movieService.GetAllPopulerMovies(page);

            if (movieLists == null)
            {

                logger.Error("Movie list didn't send");
                return ServiceResult<List<Movie>>.CreateError(HttpStatusCode.BadRequest, "Movie list didn't send");
            }

            logger.Info("Movie list  sent");
            return ServiceResult<List<Movie>>.CreateResult(movieLists);
        }


        /// <summary>
        /// Get Movie By Id
        /// </summary>
        /// <param name="movie_id"></param>
        /// <returns>BaseResponse Movie</returns>
        [HttpGet("get-movie-by-id")]
        public async Task<ServiceResult<Movie>> GetMovieById(int movie_id)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var movie = await _movieService.GetMovieById(movie_id.ToString());

            if (movie == null)
            {
                logger.Error("movie didn't send");
                return ServiceResult<Movie>.CreateError(HttpStatusCode.BadRequest, "movie didn't send");
            }

            logger.Info("movie sent");
            return ServiceResult<Movie>.CreateResult(movie);
        }

        /// <summary>
        /// Get rate movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="sessionId"></param>
        /// <param name="guestId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-rate-movie")]
        public ServiceResult<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var rate = _movieService.GetRateMovie(movieId, sessionId, guestId).Result;

            if (rate == null)
            {
                logger.Error("Rate movie didn't send");
                return ServiceResult<string>.CreateError(HttpStatusCode.BadRequest, "Rate movie didn't send");
            }

            logger.Info("Rate movie sent");
            return ServiceResult<string>.CreateResult(rate);
        }


        /// <summary>
        /// User vote for the movie 
        /// </summary>
        /// <param name="rateMovie"></param>
        /// <returns>string rate movie</returns>
        [AllowAnonymous]
        [HttpPost("rate-movie")]
        public ServiceResult<string> RateMovie([FromBody] RateMovie rateMovie)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var rateMovies = _movieService.RateMovie(rateMovie).Result;

            if (rateMovies == null)
            {
                logger.Error("Rate movie didn't send");
                return ServiceResult<string>.CreateError(HttpStatusCode.BadRequest, "Rate movie didn't send");
            }

            logger.Info("Rate movie sent");
            return ServiceResult<string>.CreateResult(rateMovies);

        }


        /// <summary>
        /// Upcoming movie
        /// </summary>
        /// <param name="page"></param>
        /// <returns>List of movie</returns>
        [HttpGet("upcoming-movies")]
        public ServiceResult<List<Movie>> GetUpcomingMovie(int page)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var upcomingMovie = _movieService.GetUpcomingMovie(page).Result;

            if (upcomingMovie == null)
            {
                logger.Error("Upcoming movie list didn't send");
                return ServiceResult<List<Movie>>.CreateError(HttpStatusCode.BadRequest, "Upcoming movie list didn't send");
            }

            logger.Info("Upcoming movie sent ");
            return ServiceResult<List<Movie>>.CreateResult(upcomingMovie);

        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-movie-video-by-id")]
        public ServiceResult<List<MovieVideo>> GetMovieVideoById(int movieId)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            var movieVideo = _movieService.GetMovieVideoById(movieId).Result;

            if (movieVideo == null)
            {
                logger.Error("Movie video list didn't send");
                return ServiceResult<List<MovieVideo>>.CreateError(HttpStatusCode.BadRequest, "Movie video list didn't send");
            }

            logger.Info("Movie video list sent");
            return ServiceResult<List<MovieVideo>>.CreateResult(movieVideo);

        }
    }
}