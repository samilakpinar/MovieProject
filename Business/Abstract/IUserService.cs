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
        void Add(Users user);
        void Delete(Users user);
        void Update(Users user);
    }
}
