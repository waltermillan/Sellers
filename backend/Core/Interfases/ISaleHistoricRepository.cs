using Core.Entities;
using System.Net;

namespace Core.Interfases;

public interface ISaleHistoricRepository
{
    Task<SaleHistoric> GetSaleIdAsync(int saleHistoricId);
    Task<SaleHistoric> GetByIdAsync(int id);
    Task<IEnumerable<SaleHistoric>> GetAllAsync();
    Task AddAsync(SaleHistoric saleHistoric);
    Task UpdateAsync(SaleHistoric saleHistoric);
    Task DeleteAsync(int id);
}
