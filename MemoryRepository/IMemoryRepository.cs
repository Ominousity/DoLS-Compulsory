using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryRepository
{
    public interface IMemoryRepository
    {
        public Task<List<Calculation>> GetCalculations(Guid UserId);
        public Task SaveCalculation(Calculation calculation);
        public Task RebuildAsync();
    }
}
