using Business.Responses;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMenuService
    {
        List<Menu> GetAllMenu();
        List<Menu> GetMenuByPermissonId(int id);
        ResultResponse Add(Menu menu);
        ResultResponse Delete(Menu menu);
        ResultResponse Update(Menu menu);
    }
}
