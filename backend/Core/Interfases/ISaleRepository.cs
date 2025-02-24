using Core.Entities;
using System.Net;

namespace Core.Interfases;

public interface ISaleRepository
{
    Task<Sale> GetSaleIdAsync(int saleId);
    Task<Sale> GetByIdAsync(int id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(int id);
}
