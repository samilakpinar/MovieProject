using Business.Abstract;
using Business.Models;
using Business.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/cast")]
    [ApiController]
    public class CastController : Controller
    {
        private ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        /// <summary>
        /// Get populer cast
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>List Cast</returns>
        [AllowAnonymous]
        [HttpGet("get-populer-cast")]
        public async Task<BaseResponse<List<Cast>>> GetPopulerCast(int movieId)
        {
            
            BaseResponse<List<Cast>> response = new BaseResponse<List<Cast>>();

            response.Data  = await _castService.GetPopulerCast(movieId);
            response.ErrorMessages = null;
            return response;
        }

        //oyuncu adı ile girilen oyuncuyu getirecek NOT: yukarıda tüm popüler oyuncularun listesi dönüyor. Buradan arama işlemi yapılacaktır.
    }
}
