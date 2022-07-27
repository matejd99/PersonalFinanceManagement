namespace PersonalFinanceManagement.Data
{
    using Microsoft.EntityFrameworkCore;
    using PersonalFinanceManagement.Models;

    public class PFMDbContext : DbContext
    {
        public DbSet<TransactionsModel> Transactions => Set<TransactionsModel>();
        public DbSet<TransactionsSplit> TransactionSplits => Set<TransactionsSplit>();
        public DbSet<CategoriesModel> Categories => Set<CategoriesModel>();

        public PFMDbContext(DbContextOptions options) : base(options) { }
    }
}