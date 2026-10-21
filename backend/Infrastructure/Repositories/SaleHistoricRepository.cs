using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

public class SaleHistoricRepository : ISaleHistoricRepository
{
    private readonly ApplicationDbContext _context;

    public SaleHistoricRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SaleHistoric> GetSaleIdAsync(int saleId)
    {
        return await _context.SalesHistoric
                             .FirstOrDefaultAsync(a => a.Id == saleId);
    }

    public async Task<IEnumerable<SaleHistoric>> GetAllAsync()
    {
        return await _context.SalesHistoric.ToListAsync();
    }

    public async Task<SaleHistoric> GetByIdAsync(int id)
    {
        return await _context.SalesHistoric.FindAsync(id);
    }

    public async Task AddAsync(SaleHistoric saleHistoric)
    {
        await _context.SalesHistoric.AddAsync(saleHistoric);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SaleHistoric saleHistoric)
    {
        _context.SalesHistoric.Update(saleHistoric);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var saleHistoric = await _context.SalesHistoric.FindAsync(id);

        if (saleHistoric is not null)
        {
            _context.SalesHistoric.Remove(saleHistoric);
            await _context.SaveChangesAsync();
        }
    }
}
