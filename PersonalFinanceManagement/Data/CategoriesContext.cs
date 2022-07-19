using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Data
{
    public class CategoriesContext : DbContext
    {
        public DbSet<CategoriesModel> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=PFMDb;trusted_connection=true;");
        }

    }
}
