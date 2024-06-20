using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Desafio.Api.Models;

namespace Desafio.Api.Context
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options) : base (options) {}

        public DbSet<User> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeCompleto).IsRequired();
                entity.Property(e => e.CPF).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.UserType).IsRequired();
                entity.ToTable("Users", t => t.HasCheckConstraint("CK_UserType", "'UserType' IN ('Cliente', 'Lojista')"));
                entity.Property(e => e.Saldo);

            });
        }
    }
}