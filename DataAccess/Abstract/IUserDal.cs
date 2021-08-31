using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//normalde IBookDal => veritabanı işlemleri yapılırdı.
//Core katmanı olduğu için IEntityRepository içindeki metotları kullanamızı sağlıyor.

//User için ekleme, silme, güncelleme işlemi yapılıyor. 
//IEntityRepository içindeki 5 fonksiyonu kullanıyor. 
//User a özel operasyonları yazmak için bunu yaptık.

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<Users> 
    {

    }
}
