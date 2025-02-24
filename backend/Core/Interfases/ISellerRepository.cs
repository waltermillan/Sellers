using Core.Entities;
using System.Net;

namespace Core.Interfases;

public interface ISellerRepository
{
    Task<Seller> GetByIdAsync(int id);
    Task<IEnumerable<Seller>> GetAllAsync();
    Task AddAsync(Seller seller);
    Task UpdateAsync(Seller seller);
    Task DeleteAsync(int id);
}
