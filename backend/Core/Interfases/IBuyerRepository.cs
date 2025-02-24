using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfases
{
    public interface IBuyerRepository
    {
        Task<Buyer> GetByIdAsync(int id);
        Task<IEnumerable<Buyer>> GetAllAsync();
        Task AddAsync(Buyer buyer);
        Task UpdateAsync(Buyer buyer);
        Task DeleteAsync(int id);
    }

}
