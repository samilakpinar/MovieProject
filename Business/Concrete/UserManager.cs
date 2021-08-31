using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal; //Veri erişim katmanını kullanırız. Özellik ile userDalı kullanıyoruz.

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public void Add(Users user)
        {
            //User password encryp and decryp
            var SCollection = new ServiceCollection();

            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<SecurityManager>(LockerKey);
            string encryptKey = locker.Encrypt(user.Password);
            //string deencryptKey = locker.Decrypt(encryptKey);

            user.Password = encryptKey;
            
            if(user.Permisson <= 0 || user.Permisson > 3)
            {
                user.Permisson = 3;
            }

            if (_userDal.Get(u => u.Email == user.Email) == null )
            {
                _userDal.Add(user);
            }
            else
            {
                throw new Exception("Bu email adı zaten mevcut");
            }
                
        }

        public void Delete(Users user)
        {
            if (_userDal.Get(u => u.Email == user.Email) == null)
            {
                throw new Exception("Bu email adında bir kullanıcı yok");
            }
            else
            {
                _userDal.Delete(user);
            }

        }

        public List<Users> GetAll()
        {
            return _userDal.GetList().ToList();
        }

        
        public Users GetByEmailAndPassword(string email, string password)
        {

            var SCollection = new ServiceCollection();

            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<SecurityManager>(LockerKey);          

            var user = _userDal.Get(u => u.Email == email);

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

        
        public void Update(Users user)
        {

            if (_userDal.Get(u => u.Email == user.Email) == null)
            {
                throw new Exception("Bu email adında bir kullanıcı yok");
            }
            else
            {
                _userDal.Update(user);
            }

        }
    }
}
