using Business.Abstract;
using Business.Models;
using Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SidebarMenuManager : ISidebarMenuService
    {
        public List<SidebarMenu> GetMenu()
        {
            //veritabanından liste alınması gerekir.

            List<SidebarMenu> menu = new List<SidebarMenu>();
            menu.Add(new SidebarMenu("Populer Movies", "movies"));
            menu.Add(new SidebarMenu("Populer Cast", "cast"));

            
            return menu;
                               
        }
    }
}
