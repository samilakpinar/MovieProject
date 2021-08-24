using Business.Abstract;
using Business.Models;
using Business.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Business.Concrete
{
    public class SidebarMenuManager : ISidebarMenuService
    {
        public List<SidebarMenu> GetMenu()
        {

            List<SidebarMenu> menu = new List<SidebarMenu>();
            menu.Add(new SidebarMenu(1,"Populer Movies", "movies"));
            menu.Add(new SidebarMenu(2,"Populer Cast", "cast"));

            return menu;
                               
        }
    }
}
