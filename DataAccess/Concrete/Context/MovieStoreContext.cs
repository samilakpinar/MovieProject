using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Context
{
    public class MovieStoreContext : DbContext
    {
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
        { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }

    }
}
