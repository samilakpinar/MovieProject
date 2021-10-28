using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Result;
using NLog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public HomeController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Get menu list
        /// </summary>
        /// <param name="token"></param>
        /// <returns>menu or menu list</returns>
        [HttpGet("get-menu")]
        public ServiceResult<List<Menu>> GetMenu(string token)
        {
            try
            {
                var tokenValue = new JwtSecurityToken(jwtEncodedString: token);
                var role = tokenValue.Claims.FirstOrDefault(c => c.Type == "role").Value;


                var menuList = _menuService.GetMenuByPermissonId(Convert.ToInt32(role));

                if (menuList == null)
                {
                    logger.Info("Access Denied");
                    return ServiceResult<List<Menu>>.CreateError(HttpStatusCode.BadRequest, "Access Denied");

                }

                logger.Info("Sidebar menu list sent");
                return ServiceResult<List<Menu>>.CreateResult(menuList);

            }
            catch
            {
                logger.Info("Invalid Token Value");
                return ServiceResult<List<Menu>>.CreateError(HttpStatusCode.BadRequest, "Invalid Token Value");

            }
        }
    }
}