using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class MovieStoreContext : DbContext
    {
        private readonly IConfiguration _configuration;
       
        public MovieStoreContext()
        {

        }

        public MovieStoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // string _settings = _configuration.GetConnectionString("ConnectionString");

           //optionsBuilder.UseSqlServer(_settings);


          optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MovieStoreDb;Trusted_Connection=true");

        }


        //nesne ile veritabanını eşleştirmemiz gerekiyor.
        //Class olan users ile veritabanındaki users tablosuna karşılık gelir.
        // public DbSet<class_name> tablo_name { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
