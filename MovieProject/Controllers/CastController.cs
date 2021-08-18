using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : Controller
    {
        private ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet("getPopulerCast")]
        public Task<List<Cast>> GetPopulerCast(int movieId)
        {
            return _castService.GetPopulerCast(movieId);
        }

        //oyuncu adı ile girilen oyuncuyu getirecek NOT: yukarıda tüm popüler oyuncularun listesi dönüyor. Buradan arama işlemi yapılacaktır.
    }
}
