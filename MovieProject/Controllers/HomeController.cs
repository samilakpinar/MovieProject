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
    [Route("api/v1/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private ISidebarMenuService _sidebarMenu;
        public HomeController(ISidebarMenuService sidebarMenu)
        {
            _sidebarMenu = sidebarMenu;
        }

        /// <summary>
        /// get menu list
        /// </summary>
        /// <returns>menu list</returns>
        [AllowAnonymous]
        [HttpGet("get-menu")]
        public BaseResponse<List<SidebarMenu>> GetMenu()
        {
            //var tokenValue = new JwtSecurityToken(jwtEncodedString: token);
            //var role = tokenValue.Claims.First(c => c.Type == "role").Value;
            //Console.WriteLine("email => " + tokenValue.Claims.FirstOrDefault(c => c.Value == "role").Value);

            //BaseResponse<string> response = new BaseResponse<string>();
            //response.Data = role;
            //response.ErrorMessages = null;
            //return response;

            BaseResponse<List<SidebarMenu>> response = new BaseResponse<List<SidebarMenu>>();
            response.Data = _sidebarMenu.GetMenu();
            response.ErrorMessages = null;
            return response;
        }
    }
}
