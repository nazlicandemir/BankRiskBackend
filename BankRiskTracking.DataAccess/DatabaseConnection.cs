using BankRiskTracking.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankRiskTracking.DataAccess
{
    public  class DatabaseConnection : DbContext
    {
        public  DbSet<Customer> Customer { get; set; }

        public DbSet<CustomerNote> CustomerNotes { get; set; }

        public DbSet<RiskHistory> RiskHistories { get; set; }

        public DbSet<Transaction> TransactionHistories { get; set; }

        public DbSet<User> Users { get; set; }

        public DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options)
        {

        }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             //optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=BankRiskDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
         }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
         }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---- CustomerNote mapping ----
            modelBuilder.Entity<CustomerNote>(e =>
            {
                e.ToTable("CustomerNotes");          // tablo adı net

                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(x => x.NoteText)
                    .IsRequired();

                    
                e.Property(x => x.CreatedDate)
                    .HasDefaultValueSql("SYSUTCDATETIME()")
                    .ValueGeneratedOnAdd();

                e.HasOne(x => x.Customer)
                    .WithMany()                        
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);  
            });

          
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<RiskHistory>().ToTable("RiskHistories");
            modelBuilder.Entity<Transaction>().ToTable("TransactionHistories");
            modelBuilder.Entity<User>().ToTable("Users");
        }

    }
}


//Data Source=localhost\SQLEXPRESS;Initial Catalog=BankRiskDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False