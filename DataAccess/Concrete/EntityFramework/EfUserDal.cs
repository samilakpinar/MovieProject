using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

//interfaceteki soyut versiyonu somut olarak burada kullandık. Veritabanı metotlarını burada yazacağız.
//Entity frameworkte yazacağımız için EfUserDal diyoruz.

//Bu yapı ile usera özel ekleme, silme, güncelleme ve filtreleme işlemleri yapılıyor.
//Not: Context nedir? Entity frameworkte nesnen ile veritabanını eşleştiriyorusun.

namespace DataAccess.Concrete.EntityFramework
{
    //MovieStoreContext => Veritavanı bağlaması yapıyor. bizim Crud operasyonları hazır oluyor. 
    public class EfUserDal: EfEntityRepositoryBase<Users, MovieStoreContext> ,IUserDal
    {
    }
}
