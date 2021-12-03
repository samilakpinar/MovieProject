using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Caching;
using MovieProject.Result;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/cast")]
    [ApiController]
    public class CastController : Controller
    {
        private readonly ICastService _castService;
        private readonly ICacheService _cacheService;
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public CastController(ICastService castService, ICacheService cacheService)
        {
            _castService = castService;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Get populer cast
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>List Cast</returns>
        [HttpGet("get-populer-cast/{movieId}")]
        public async Task<ServiceResult<List<Cast>>> GetPopulerCast(int movieId)
        {
            List<Cast> CastList;

            var httpContext = HttpContext.Request.Path.Value;

            //get data from cache
            var jsonString = await _cacheService.GetDataFromCache(httpContext);

            if (jsonString != null)
            {
                CastList = JsonConvert.DeserializeObject<List<Cast>>(jsonString);
            }
            else
            {
                CastList = await _castService.GetPopulerCast(movieId);

                if (CastList == null)
                {
                    logger.Info("Cast list didn't send");
                    return ServiceResult<List<Cast>>.CreateError(HttpStatusCode.BadRequest, "Cast list didn't send");
                }

                //set data to cache
                _cacheService.SetDataToCache(httpContext, CastList);
            }

            logger.Info("Cast list sent");
            return ServiceResult<List<Cast>>.CreateResult(CastList);

        }


        /// <summary>
        /// Get cast by id
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="castId"></param>
        /// <returns>Cast</returns>
        [HttpGet("get-cast-by-id/{movieId}/{castId}")]
        public async Task<ServiceResult<Cast>> GetCastById(int movieId, int castId)
        {
            Cast cast;

            var httpContext = HttpContext.Request.Path.Value;

            var jsonString = await _cacheService.GetDataFromCache(httpContext);

            if (jsonString != null)
            {
                cast = JsonConvert.DeserializeObject<Cast>(jsonString);
            }
            else
            {
                var castList = await _castService.GetPopulerCast(movieId);

                if (castList == null)
                {
                    logger.Info("Cast didn't send");
                    return ServiceResult<Cast>.CreateError(HttpStatusCode.BadRequest, "Cast didn't send");
                }

                cast = castList.FirstOrDefault<Cast>(c => c.castId == castId);

                _cacheService.SetDataToCache(httpContext, cast);
            }

            logger.Info("Cast id sent");
            return ServiceResult<Cast>.CreateResult(cast);


        }

    }
}