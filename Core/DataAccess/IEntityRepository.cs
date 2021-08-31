using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

//Tüm entitylerde ortaktır. CRUD operasyonları tüm projelerde kullanılır.

//where ile T'yi kısıtlıyoruz. generic kısıt denir.
// generic constrain nedir? Buradaki T'yi sınırlıyoruz. T'ye class veriyoruz.
// Buradaki class ise referans tiptir demektir. T bir IEntity implementasyonu olacak demektir.
// new() kısmı ise newlenebilir demektir. new() tam classı karşılayandır demek.
// new() koyduğumuz zaman ne interface ne de abstract olabilir demektir.

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new() 
    {
        T Get(Expression<Func<T, bool>> filter); //bir nesneyi filtreleme yapıyor. İdye göre kitap getirme, Expression aslında T dir => r.Get(b=>b.Id == 2). b kısmı Expression dır.
        IList<T> GetList(Expression<Func<T, bool>> filter = null); //birden fazla nesneyi filtreleme yapar.  Adında b geçen filmler gibi
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
    
}
