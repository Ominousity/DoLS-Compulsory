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
            _options = new DbContextOptionsBuilder<DBCONTEXT>().UseSqlServer("").Options;
        }
        public List<Calculation> GetCalculations(Guid UserId)
        {
            Log.Logger.Information($"Getting calculations from Database for user : {UserId}");
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                return context.calculations.Where(c => c.UserId == UserId).ToList();
            }
        }

        public void SaveCalculation(Calculation calculation)
        {
            Log.Logger.Information($"Saving calculation to Database for user {calculation.UserId}");
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                _ = context.Add(calculation);
                context.SaveChanges();
            }
        }
    }
}
