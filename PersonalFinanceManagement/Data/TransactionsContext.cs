﻿using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Data
{
    public class TransactionsContext : DbContext
    {
        public DbSet<TransactionsModel> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=PFMDb;trusted_connection=true;");
        }   

    }
}
