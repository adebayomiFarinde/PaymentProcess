using Hangfire.Logging;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
               .Entity<Business>()
               .HasOne<Contact>()
               .WithOne()
               .HasForeignKey<Business>(x => x.ContactId)
               .IsRequired(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.TransactionHistories)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired();

        }
        internal DbSet<Business> Businesses { get; set; }
        internal DbSet<Contact> Contacts { get; set; }
        internal DbSet<TransactionHistory> TransactionHistories { get; set; }
        internal DbSet<Customer> Customers { get; set; }

    }
}
