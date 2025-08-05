using BankRiskTracking.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.DataAccess
{
    public  class DatabaseConnection : DbContext
    {
        public  DbSet<Customer> Customers { get; set; }

        public DbSet<Customer> CustomerNotes { get; set; }

        public DbSet<RiskHistory> RiskHistories { get; set; }

        public DbSet<Transaction> TransactionHistories { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=BankRiskDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}


//Data Source=localhost\SQLEXPRESS;Initial Catalog=BankRiskDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False