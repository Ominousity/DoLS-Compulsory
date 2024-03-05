using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryRepository
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly ILogger<MemoryRepository> _logger;
        private DbContextOptions<DBCONTEXT> _options;
        public MemoryRepository(ILogger<MemoryRepository> logger)
        {
            _logger = logger;
            _options = new DbContextOptionsBuilder<DBCONTEXT>().UseSqlServer("").Options;
        }
        public List<Calculation> GetCalculations(Guid UserId)
        {
            _logger.LogInformation($"Getting calculations from Database for user : {UserId}");
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                return context.calculations.Where(c => c.UserId == UserId).ToList();
            }
        }

        public void SaveCalculation(Calculation calculation)
        {
            _logger.LogInformation($"Saving calculation to Database for user {calculation.UserId}");
            using (var context = new DBCONTEXT(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                _ = context.Add(calculation);
                context.SaveChanges();
            }
        }
    }
}
