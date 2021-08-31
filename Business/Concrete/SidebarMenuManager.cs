using Business.Abstract;
using Business.Models;
using Business.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Business.Concrete
{
    public class SidebarMenuManager :ISidebarMenuService
    {
        
        public List<SidebarMenu> GetMenuList(string role)
        {

            if (role == "1")
            {

                return new List<SidebarMenu>() { new SidebarMenu(2, "Populer Cast", "cast") }; ;

            }
            else if (role == "2")
            {

                return new List<SidebarMenu>() { new SidebarMenu(1, "Populer Movies", "movies") };

            }
            else if (role == "3")
            {
                return new List<SidebarMenu>() { new SidebarMenu(1, "Populer Movies", "movies"), new SidebarMenu(2, "Populer Cast", "cast") };

            }
            else
            {
                return null;
            }

        }
    }
}
