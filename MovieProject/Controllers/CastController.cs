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

        [HttpGet("get-populer-cast")]
        public async Task<BaseResponse<List<Cast>>> GetPopulerCast(int movieId)
        {
            
            BaseResponse<List<Cast>> baseResponse = new BaseResponse<List<Cast>>();

            baseResponse.Data  = await _castService.GetPopulerCast(movieId);
            baseResponse.ErrorMessages = null;
            return baseResponse;
        }

        //oyuncu adı ile girilen oyuncuyu getirecek NOT: yukarıda tüm popüler oyuncularun listesi dönüyor. Buradan arama işlemi yapılacaktır.
    }
}
