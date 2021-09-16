using Business.Responses;
using Entities.Concrete;
using System.Collections.Generic;

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
