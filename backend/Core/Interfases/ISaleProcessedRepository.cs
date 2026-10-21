using Core.Entities;
using System.Net;

namespace Core.Interfases;

public interface ISaleProcessedRepository
{
    Task<SaleProcessed> GetSaleIdAsync(int saleProcessedId);
    Task<SaleProcessed> GetByIdAsync(int id);
    Task<IEnumerable<SaleProcessed>> GetAllAsync();
    Task AddAsync(SaleProcessed saleProcessed);
    Task UpdateAsync(SaleProcessed saleProcessed);
    Task DeleteAsync(int id);
}
