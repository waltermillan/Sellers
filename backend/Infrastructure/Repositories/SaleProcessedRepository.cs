using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

public class SaleProcessedRepository : ISaleProcessedRepository
{
    private readonly ApplicationDbContext _context;

    public SaleProcessedRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SaleProcessed> GetSaleIdAsync(int saleProcessedId)
    {
        return await _context.SalesProcessed
                             .FirstOrDefaultAsync(a => a.Id == saleProcessedId);
    }

    public async Task<IEnumerable<SaleProcessed>> GetAllAsync()
    {
        return await _context.SalesProcessed.ToListAsync();
    }

    public async Task<SaleProcessed> GetByIdAsync(int id)
    {
        return await _context.SalesProcessed.FindAsync(id);
    }

    public async Task AddAsync(SaleProcessed saleProcessed)
    {
        await _context.SalesProcessed.AddAsync(saleProcessed);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SaleProcessed saleProcessed)
    {
        _context.SalesProcessed.Update(saleProcessed);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sale = await _context.SalesProcessed.FindAsync(id);

        if (sale is not null)
        {
            _context.SalesProcessed.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
