using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryRepository
{
    public class MemoryRepository : IMemoryRepository
    {
        private DbContextOptions<DBCONTEXT> _options;
        public MemoryRepository()
        {
            _options = new DbContextOptionsBuilder<DBCONTEXT>().UseSqlServer("Server=db;Database=memory;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;").Options;
        }
        public async Task<List<Calculation>> GetCalculations(Guid UserId)
        {
            Log.Logger.Information($"Getting calculations from Database for user : {UserId}");
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                Task<List<Calculation>> calculations = context.calculations.Where(c => c.UserId == UserId).ToListAsync();
                return await calculations;
            }
        }

        public async Task SaveCalculation(Calculation calculation)
        {
            Log.Logger.Information($"Saving calculation to Database for user {calculation.UserId}");
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                _ = await context.AddAsync(calculation);
                context.SaveChanges();
            }
        }

        public async Task RebuildAsync()
        {
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }
        }
    }
}
