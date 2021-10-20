using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Result;
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
        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        /// <summary>
        /// Get populer cast
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>List Cast</returns>
        [HttpGet("get-populer-cast")]
        public async Task<ServiceResult<List<Cast>>> GetPopulerCast(int movieId)
        {

            var logger = NLog.LogManager.GetCurrentClassLogger();

            var castList = await _castService.GetPopulerCast(movieId);

            if (castList == null)
            {
                logger.Info("Cast list didn't send");
                return ServiceResult<List<Cast>>.CreateError(HttpStatusCode.BadRequest, "Cast list didn't send");
            }

            logger.Info("Cast list sent");
            return ServiceResult<List<Cast>>.CreateResult(castList);
        }


        /// <summary>
        /// Get cast by id
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="castId"></param>
        /// <returns>Cast</returns>
        [HttpGet("get-cast-by-id")]
        public async Task<ServiceResult<Cast>> GetCastById(int movieId, int castId)
        {

            var logger = NLog.LogManager.GetCurrentClassLogger();

            var castList = await _castService.GetPopulerCast(movieId);

            if (castList == null)
            {
                logger.Info("Cast didn't send");
                return ServiceResult<Cast>.CreateError(HttpStatusCode.BadRequest, "Cast didn't send");
            }

            var cast = castList.FirstOrDefault<Cast>(c => c.castId == castId);

            logger.Info("Cast id sent");
            return ServiceResult<Cast>.CreateResult(cast);
        }

    }
}