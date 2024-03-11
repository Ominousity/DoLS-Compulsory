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
        public List<Calculation> GetCalculations(Guid UserId);
        public void SaveCalculation(Calculation calculation);
        public void Rebuild();
    }
}
