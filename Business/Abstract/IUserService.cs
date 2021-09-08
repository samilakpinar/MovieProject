using Business.Responses;
using Entities.Concrete;
using System;
using System.Collections.Generic;

//Repository tasarım deseni sadece veri erişimde kullanılıyor.

namespace Business.Abstract
{
    public interface IUserService
    {
        List<Users> GetAll();
        Users GetByEmailAndPassword(string email, string password);
        Users GetByEmail(string email);
        ResultResponse Add(Users user);
        ResultResponse Delete(Users user);
        ResultResponse Update(Users user);
    }
}
