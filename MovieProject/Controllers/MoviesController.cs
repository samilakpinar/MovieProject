using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService _movieService;
        private ICastService _castService;
       

        public MoviesController(IMovieService movieService,ICastService castService)
        {
            this._movieService = movieService;
            this._castService = castService;
        }

        [HttpGet("createToken")]
        public Task<string> CreateToken()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("create token error");

            return _movieService.CreateToken();
        }

        [HttpPost("createSession")]
        public Task<string> CreateSession([FromBody] CreateSession token)
        {
            return _movieService.CreateSession(token);
        }

        [HttpGet("getPopulerMovie")]
        public Task<string> GetPopulerMovie(int page)
        {
            return _movieService.GetAllPopulerMovies(page);
        }

        [HttpGet("getMovieById")]
        public Task<string> GetMovieById(string movie_id)
        {
            return _movieService.GetMovieById(movie_id);
        }

        [HttpGet("getRateMovie")]
        public Task<string> GetRateMovie(int movieId, string sessionId, string guestId)
        {
            return _movieService.GetRateMovie(movieId, sessionId, guestId);
        }

        //Business alanında iş kodu yazılması gerekli çünkü verebileceği puan 1 ile 10 arasıdır. 1 ile 10 arası değil ise request atılmayacak.
        //Not ekleme olayı buarada yapılmalı, şuan puan ekleme oluyor
        [HttpPost("rateMovie")]
        public Task<string> RateMovie([FromBody] RateMovie rateMovie)
        {
            return _movieService.RateMovie(rateMovie);
        }

        [HttpGet("getPopulerCast")]
        public Task<string> GetPopulerCast(int movieId)
        {
            return _castService.GetPopulerCast(movieId);
        }

        //oyuncu adı ile girilen oyuncuyu getirecek NOT: yukarıda tüm popüler oyuncularun listesi dönüyor. Buradan arama işlemi yapılacaktır.


        //film adına göre filtreleme eklenmesi NOT: Tüm filmleri getirilecek sistem arkada tüm filmleri dolaşığ aranan filmi bulup kullanıcıya dönecektir.



    }
}
