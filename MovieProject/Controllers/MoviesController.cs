using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Caching;
using MovieProject.Result;
using Newtonsoft.Json;
using NLog;
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
        private readonly ICacheService _cacheService;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        

        public MoviesController(IMovieService movieService, ICacheService cacheService)
        {
            _movieService = movieService;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Get populer movie
        /// </summary>
        /// <param name="page"></param>
        /// <returns>List Movie</returns>
        [HttpGet("get-populer-movie/{page}")]
        public async Task<ServiceResult<List<Movie>>> GetPopulerMovie(int page)
        {
            List<Movie> movieList;

            var httpContext = HttpContext.Request.Path.Value;

            var jsonString = await _cacheService.GetDataFromCache(httpContext);

            if (jsonString != null)
            {
                movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
            }
            else
            {
                movieList = _movieService.GetAllPopulerMovies(page).Result;

                if (movieList == null)
                {
                    logger.Error("Movie list didn't send");
                    return ServiceResult<List<Movie>>.CreateError(HttpStatusCode.BadRequest, "Movie list didn't send");
                }

                _cacheService.SetDataToCache(httpContext, movieList);
            }

            logger.Info("Movie list  sent");
            return ServiceResult<List<Movie>>.CreateResult(movieList);

        }


        /// <summary>
        /// Get Movie By Id
        /// </summary>
        /// <param name="movie_id"></param>
        /// <returns>BaseResponse Movie</returns>
        [HttpGet("get-movie-by-id/{movie_id}")]
        public async Task<ServiceResult<Movie>> GetMovieById(int movie_id)
        {
            Movie movie;

            var httpContext = HttpContext.Request.Path.Value;

            var jsonString = await _cacheService.GetDataFromCache(httpContext);

            if (jsonString != null)
            {
                movie = JsonConvert.DeserializeObject<Movie>(jsonString);
            }
            else
            {
                movie = await _movieService.GetMovieById(movie_id.ToString());

                if (movie == null)
                {
                    logger.Error("movie didn't send");
                    return ServiceResult<Movie>.CreateError(HttpStatusCode.BadRequest, "Movie didn't send");
                }

                logger.Info("movie sent");
                _cacheService.SetDataToCache(httpContext, movie);

            }

            return ServiceResult<Movie>.CreateResult(movie);

        }

        /// <summary>
        /// Get rate movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="sessionId"></param>
        /// <param name="guestId"></param>
        /// <returns></returns>
        [HttpGet("get-rate-movie")]
        public ServiceResult<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {

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
        [HttpGet("upcoming-movies/{page}")]
        public async Task<ServiceResult<List<Movie>>> GetUpcomingMovie(int page)
        {
            List<Movie> movieList;

            var httpContext  = HttpContext.Request.Path.Value;

            var jsonString = await _cacheService.GetDataFromCache(httpContext);

            if (jsonString != null)
            {
                movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
            }
            else
            {
                movieList = await _movieService.GetUpcomingMovie(page);

                if (movieList == null)
                {
                    logger.Error("Movie list didn't send");
                    return ServiceResult<List<Movie>>.CreateError(HttpStatusCode.BadRequest, "Movie list didn't send");
                }

             await _cacheService.SetDataToCache(httpContext, movieList);

            }

            logger.Info("Upcoming movie sent");
            return ServiceResult<List<Movie>>.CreateResult(movieList);

        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-movie-video-by-id/{movieId}")]
        public ServiceResult<List<MovieVideo>> GetMovieVideoById(int movieId)
        {
            
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