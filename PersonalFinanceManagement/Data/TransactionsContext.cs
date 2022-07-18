using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Data
{
    public class TransactionsContext : DbContext
    {
        public DbSet<TransactionsModel> Transactions { get; set; }
    }
}
