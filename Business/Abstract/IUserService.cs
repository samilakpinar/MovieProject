using Business.Responses;
using Entities.Concrete;
using System.Collections.Generic;


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
