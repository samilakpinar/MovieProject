using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Context
{
    public class MovieStoreContext : DbContext
    {
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
        { }

        //nesne ile veritabanını eşleştirmemiz gerekiyor.
        //Class olan users ile veritabanındaki users tablosuna karşılık gelir.
        // public DbSet<class_name> tablo_name { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }

    }
}
