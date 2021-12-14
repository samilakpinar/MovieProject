using Business.Abstract;
using Business.Responses;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository; 

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResultResponse Add(Users user)
        {
            ResultResponse response = new ResultResponse();

            //User password encryp and decryp
            var SCollection = new ServiceCollection();

            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<SecurityManager>(LockerKey);
            string encryptKey = locker.Encrypt(user.Password);
            //string deencryptKey = locker.Decrypt(encryptKey);

            user.Password = encryptKey;

            if (user.Permisson <= 0 || user.Permisson > 3)
            {
                user.Permisson = 3;
            }

            if (_userRepository.Get(u => u.Email == user.Email) == null)
            {
                _userRepository.Add(user);
                response.ErrorMessage = null;
            }
            else
            {
                response.ErrorMessage = "Bu email adı zaten mevcut";
            }

            return response;

        }

        public ResultResponse Delete(Users user)
        {
            ResultResponse response = new ResultResponse();

            if (_userRepository.Get(u => u.Email == user.Email) == null)
            {
                response.ErrorMessage = "Bu email adında bir kullanıcı yok";
            }
            else
            {
                _userRepository.Delete(user);
                response.ErrorMessage = null;
            }

            return response;

        }

        public List<Users> GetAll()
        {
            return _userRepository.GetList().ToList();
        }

        public Users GetByEmail(string email)
        {
            var user = _userRepository.Get(user => user.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public Users GetByEmailAndPassword(string email, string password)
        {

            var SCollection = new ServiceCollection();

            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<SecurityManager>(LockerKey);

            var user = _userRepository.Get(u => u.Email == email);

            if (user == null)
                return null;

            string deencryptPassword = locker.Decrypt(user.Password);

            if (deencryptPassword != password)
            {
                return null;
            }
            else
            {
                return user;
            }

        }

        public ResultResponse Update(Users user)
        {
            ResultResponse response = new ResultResponse();

            if (_userRepository.Get(u => u.Email == user.Email) == null)
            {
                response.ErrorMessage = "Bu email adında bir kullanıcı yok";
            }
            else
            {
                _userRepository.Update(user);
                response.ErrorMessage = null;
            }

            return response;

        }
    }
}
