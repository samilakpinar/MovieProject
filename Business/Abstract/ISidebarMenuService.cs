using Business.Models;
using Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISidebarMenuService
    {
        List<SidebarMenu> GetMenuList(string role);
    }
}
