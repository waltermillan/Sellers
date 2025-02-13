using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class SellerRepository : ISellerRepository
{
    private readonly ApplicationDbContext _context;

    public SellerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Seller>> GetAllAsync()
    {
        return await _context.Sellers.ToListAsync();
    }

    public async Task<Seller> GetByIdAsync(int id)
    {
        return await _context.Sellers.FindAsync(id);
    }



    public async Task AddAsync(Seller seller)
    {
        await _context.Sellers.AddAsync(seller);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seller seller)
    {
        _context.Sellers.Update(seller);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller != null)
        {
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
        }
    }
}
