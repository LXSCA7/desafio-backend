using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Desafio.Api.Models;

namespace Desafio.Api.Context
{
    public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.FirstName).IsRequired();
                e.Property(e => e.Document).IsRequired();
                e.Property(e => e.Email).IsRequired();
                e.Property(e => e.UserType).IsRequired();
                e.Property(e => e.Balance);
            });

            modelBuilder.Entity<User>().HasIndex(e => e.Document).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
        }
    }
}