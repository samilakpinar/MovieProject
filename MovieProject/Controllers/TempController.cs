using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Result;
using System.Collections.Generic;
using System.Net;

namespace MovieProject.Controllers
{

    [Route("api/v1/temp")]
    [ApiController]
    public class TempController : Controller
    {
        private readonly IMovieService _movieService;

        public TempController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public ServiceResult<List<Movie>> GetMovies()
        {

            var movies = _movieService.GetAllPopulerMovies(1).Result;

            if (movies == null)
            {
                return ServiceResult<List<Movie>>.CreateError(HttpStatusCode.BadRequest, "Movie list didn't get");
            }

            //throw new System.Exception("movie list error"); for exception middleware 

            return ServiceResult<List<Movie>>.CreateResult(movies);
        }
    }
}
