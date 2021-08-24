using Business.Abstract;
using Business.Models;
using Business.Responses;
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
        private ISidebarMenuService _sidebarMenu;
        public HomeController(ISidebarMenuService sidebarMenu)
        {
            _sidebarMenu = sidebarMenu;
        }

        /// <summary>
        /// Get menu list
        /// </summary>
        /// <param name="token"></param>
        /// <returns>menu or menu list</returns>
        [HttpGet("get-menu")]
        public BaseResponse<List<SidebarMenu>> GetMenu(string token)
        {
            BaseResponse<List<SidebarMenu>> response = new BaseResponse<List<SidebarMenu>>();

            List<SidebarMenu> sidebarMenu = new List<SidebarMenu>();

            try
            {
                var tokenValue = new JwtSecurityToken(jwtEncodedString: token);
                var role = tokenValue.Claims.FirstOrDefault(c => c.Type == "role").Value;

                var menuList = _sidebarMenu.GetMenu();

          
                if (role == "1")
                {
                    sidebarMenu.Add(menuList.FirstOrDefault(x => x.Id == 1));

                    response.Data = sidebarMenu;
                    response.ErrorMessages = null;

                    return response;
                }
                else if (role == "2")
                {
                    sidebarMenu.Clear();
                    sidebarMenu.Add(menuList.FirstOrDefault(x => x.Id == 2));

                    response.Data = sidebarMenu;
                    response.ErrorMessages = null;

                    return response;
                }
                else if (role == "3")
                {
                    response.Data = menuList;
                    response.ErrorMessages = null;

                    return response;
                }
                else
                {
                    response.Data = null;
                    response.ErrorMessages = "Access Denied";
                    return response;
                }

            }
            catch
            {
                response.Data = null;
                response.ErrorMessages = "Invalid Token Value";

                return response;
            }


        }
    }
}
