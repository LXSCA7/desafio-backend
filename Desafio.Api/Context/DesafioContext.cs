using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Context
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options) : base (options) {}

        public DbSet<Pessoa> Pessoas { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pessoa>(entity =>
            {
               entity.HasKey(e => e.Id); 
               // outras configuracoes abaixo;
            
            });
        }
    }
}