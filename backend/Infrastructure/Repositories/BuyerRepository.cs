using Core.Entities;
using Core.Interfases;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class BuyerRepository : IBuyerRepository
{
    private readonly ApplicationDbContext _context;

    public BuyerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Buyer>> GetAllAsync()
    {
        return await _context.Buyers.ToListAsync();
    }

    public async Task<Buyer> GetByIdAsync(int id)
    {
        return await _context.Buyers.FindAsync(id);
    }

    public async Task AddAsync(Buyer buyer)
    {
        await _context.Buyers.AddAsync(buyer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Buyer buyer)
    {
        _context.Buyers.Update(buyer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var buyer = await _context.Buyers.FindAsync(id);
        if (buyer != null)
        {
            _context.Buyers.Remove(buyer);
            await _context.SaveChangesAsync();
        }
    }
}
