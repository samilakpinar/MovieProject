using Business.Abstract;
using Business.Responses;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class MenuManager : IMenuService
    {
        private IMenuRepository _menuRepository;

        public MenuManager(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public List<Menu> GetAllMenu()
        {
            return _menuRepository.GetList().ToList();
        }

        public List<Menu> GetMenuByPermissonId(int permissonId)
        {
            if (permissonId == 1)
            {
                return _menuRepository.GetList(m => m.Id == permissonId).ToList();

            }
            else if (permissonId == 2)
            {
                return _menuRepository.GetList(m => m.Id == permissonId).ToList();
            }
            else if (permissonId == 3)
            {
                return _menuRepository.GetList().ToList();
            }
            else
            {
                return null;
            }
        }

        public ResultResponse Add(Menu menu)
        {
            ResultResponse response = new ResultResponse();

            if (_menuRepository.Get(m => m.Title == menu.Title) == null)
            {
                _menuRepository.Add(menu);
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

            if (_menuRepository.Get(m => m.Title == menu.Title) == null)
            {
                response.ErrorMessage = "Silinecek menu bulunamadı.";
            }
            else
            {
                _menuRepository.Delete(menu);
                response.ErrorMessage = null;
            }

            return response;
        }


        public ResultResponse Update(Menu menu)
        {
            ResultResponse response = new ResultResponse();

            if (_menuRepository.Get(m => m.Title == menu.Title) == null)
            {
                response.ErrorMessage = "Güncellenecek menu bulunamadı.";

            }
            else
            {
                _menuRepository.Update(menu);
                response.ErrorMessage = null;
            }

            return response;
        }


    }
}
