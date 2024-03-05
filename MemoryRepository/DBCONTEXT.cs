using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryRepository
{
    public class DBCONTEXT : DbContext
    {
        public DBCONTEXT(DbContextOptions<DBCONTEXT> options, ServiceLifetime service) : base(options) { }
        public DbSet<Calculation> calculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calculation>()
                .Property(U => U.UserId)
                .IsRequired();
            modelBuilder.Entity<Calculation>()
                .HasKey(U => U.UserId);
        }
    }
}
