namespace Sample.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SampleAccountContext : DbContext
    {
        public SampleAccountContext()
            : base("name=SampleAccountContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyRate> CurrencyRates { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ResponseLog> ResponseLogs { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<TransactionLog> TransactionLogs { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.TransactionLogs)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountType>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.IsoCode)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.BaseCurrencyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.CurrencyRates)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.CurrencyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.CurrencyRates1)
                .WithRequired(e => e.Currency1)
                .HasForeignKey(e => e.RefCurrencyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.TransactionDetails)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.CurrencyDepositId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurrencyRate>()
                .Property(e => e.Rate)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ResponseLog>()
                .Property(e => e.Request)
                .IsUnicode(false);

            modelBuilder.Entity<ResponseLog>()
                .Property(e => e.Response)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.AmountInBaseCurrency)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.CurrentBalance)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TransactionDetail>()
                .HasMany(e => e.TransactionLogs)
                .WithOptional(e => e.TransactionDetail)
                .HasForeignKey(e => e.TransactionId);

            modelBuilder.Entity<TransactionLog>()
                .Property(e => e.TransactionCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionLog>()
                .Property(e => e.CurrencyRateWithBase)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TransactionLog>()
                .Property(e => e.BaseCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionLog>()
                .Property(e => e.BalanceAmountBaseCurrency)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TransactionLog>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.TransactionDetails)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.TransactionLogs)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);
        }
    }
}
