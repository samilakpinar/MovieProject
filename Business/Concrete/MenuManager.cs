using Business.Abstract;
using Business.Responses;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MenuManager : IMenuService
    {
        private IMenuDal _menuDal;

        public MenuManager(IMenuDal menuDal)
        {
            _menuDal = menuDal;
        }

        public List<Menu> GetAllMenu()
        {
            return _menuDal.GetList().ToList();
        }

        public List<Menu> GetMenuByPermissonId(int permissonId)
        {
            if (permissonId == 1)
            {
                return _menuDal.GetList(m => m.Id == permissonId).ToList();
            
            }
            else if (permissonId == 2)
            {
                return _menuDal.GetList(m => m.Id == permissonId).ToList();
            }
            else if (permissonId == 3)
            {
                return _menuDal.GetList().ToList();
            }
            else
            {
                return null;
            }
        }

        public ResultResponse Add(Menu menu)
        {
            ResultResponse response = new ResultResponse();

            if(_menuDal.Get(m => m.Title == menu.Title) == null)
            {
                _menuDal.Add(menu);
                response.ErrorMessage = null;
            }
            else
            {
                response.ErrorMessage = "Menu ismi mevcut";
            }

            return response;

            
        }

        public ResultResponse Delete(Menu menu)
        {
            ResultResponse response = new ResultResponse();

            if(_menuDal.Get(m => m.Title == menu.Title) == null)
            {
                response.ErrorMessage = "Silinecek menu bulunamadı.";
            }
            else
            {
                _menuDal.Delete(menu);
                response.ErrorMessage = null;
            }

            return response;
        }


        public ResultResponse Update(Menu menu)
        {
            ResultResponse response = new ResultResponse();

            if(_menuDal.Get(m => m.Title == menu.Title) == null)
            {
                response.ErrorMessage = "Güncellenecek menu bulunamadı.";

            }else
            {
                _menuDal.Update(menu);
                response.ErrorMessage = null;
            }

            return response;
        }

        
    }
}
