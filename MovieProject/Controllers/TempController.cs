using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Caching;
using MovieProject.Result;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{

    [Route("api/v1/temp")]
    [ApiController]
    public class TempController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICacheService _cacheService;

        public TempController(IMovieService movieService, ICacheService cacheService)
        {
            _movieService = movieService;
            _cacheService = cacheService;
        }


        [HttpGet("get-populer-movies/{page}")]
        public async Task<ServiceResult<List<Movie>>> GetMovies(int page)
        {


            List<Movie> listMovie;

            var httpContext = HttpContext.Request.Path.Value;

            //get data from cache
            var jsonString = await _cacheService.GetDataFromCache(httpContext);


            if (jsonString != null)
            {
                listMovie = JsonConvert.DeserializeObject<List<Movie>>(jsonString);

            }
            else
            {
                listMovie = _movieService.GetAllPopulerMovies(page).Result;

                if (listMovie == null)
                {
                    return ServiceResult<List<Movie>>.CreateError(HttpStatusCode.BadRequest, "movie list didn't get");
                }

                //set data to cache.
                _cacheService.SetDataToCache(httpContext, listMovie);

            }

            return ServiceResult<List<Movie>>.CreateResult(listMovie);

        }
    }
}
