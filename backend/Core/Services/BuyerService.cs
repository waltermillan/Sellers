using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class BuyerService
{
    private readonly IBuyerRepository _buyerRepository;

    public BuyerService(IBuyerRepository buyerRepository)
    {
        _buyerRepository = buyerRepository;
    }

    public async Task<Buyer> GetBuyerById(int id)
    {
        var buyer = await _buyerRepository.GetByIdAsync(id);

        if (buyer is null)
            throw new KeyNotFoundException("Buyer not found");

        return buyer;
    }

    public async Task<IEnumerable<Buyer>> GetAllBuyers()
    {
        return await _buyerRepository.GetAllAsync();
    }

    public void AddBuyer(Buyer buyer)
    {
        _buyerRepository.AddAsync(buyer);
    }

    public void AddBuyers(IEnumerable<Buyer> buyers)
    {
        foreach (var buyer in buyers)
            _buyerRepository.AddAsync(buyer);
    }

    public void UpdateBuyer(Buyer buyer)
    {
        var exists = _buyerRepository.GetByIdAsync(buyer.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Buyer to update not found");

        _buyerRepository.UpdateAsync(buyer);
    }

    public void DeleteBuyer(Buyer buyer)
    {
        var exists = _buyerRepository.GetByIdAsync(buyer.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Buyer not found");

        _buyerRepository.DeleteAsync(buyer.Id);
    }
}
