using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class MovieStoreContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MovieStore;Trusted_Connection=true");
        }


        //nesne ile veritabanını eşleştirmemiz gerekiyor.
        //Class olan users ile veritabanındaki users tablosuna karşılık gelir.
        public DbSet<Users> Users { get; set; }
    }
}
