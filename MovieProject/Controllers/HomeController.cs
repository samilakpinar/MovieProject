using Business.Abstract;
using Business.Models;
using Business.Responses;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    [Authorize]
    [Route("api/v1/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private IMenuService _menuService;
        public HomeController( IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Get menu list
        /// </summary>
        /// <param name="token"></param>
        /// <returns>menu or menu list</returns>
        [AllowAnonymous]
        [HttpGet("get-menu")]
        public BaseResponse<List<Menu>> GetMenu(string token)
        {
            BaseResponse<List<Menu>> response = new BaseResponse<List<Menu>>();

            var logger = NLog.LogManager.GetCurrentClassLogger();

            try
            {
                var tokenValue = new JwtSecurityToken(jwtEncodedString: token);
                var role = tokenValue.Claims.FirstOrDefault(c => c.Type == "role").Value;


               var menuList = _menuService.GetMenuByPermissonId(Convert.ToInt32(role));
                
                if(menuList == null)
                {
                    response.Data = null;
                    response.ErrorMessages = "Access Denied";
                    logger.Info("Access Denied");
                }

                response.Data = menuList;
                response.ErrorMessages = null;


            }
            catch
            {
                response.Data = null;
                response.ErrorMessages = "Invalid Token Value";
                logger.Info("Invalid Token Value");

            }

            logger.Info("Sidebar menu list sent");
            return response;
        }

    }
}
