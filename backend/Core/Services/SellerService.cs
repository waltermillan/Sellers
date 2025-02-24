using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class SellerService
{
    private readonly ISellerRepository _sellerRepository;

    public SellerService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<Seller> GetSellerById(int id)
    {
        var seller = await _sellerRepository.GetByIdAsync(id);

        if (seller == null)
        {
            throw new KeyNotFoundException("Seller not found");
        }

        return seller;
    }

    public async Task<IEnumerable<Seller>> GetAllSellers()
    {
        return await _sellerRepository.GetAllAsync();
    }

    public void AddSeller(Seller seller)
    {
        _sellerRepository.AddAsync(seller);
    }

    public void AddSellers(IEnumerable<Seller> sellers)
    {
        foreach (var seller in sellers)
        {
            _sellerRepository.AddAsync(seller);
        }
    }

    public void UpdateSeller(Seller seller)
    {
        var existingSeller = _sellerRepository.GetByIdAsync(seller.Id).Result;
        if (existingSeller == null)
        {
            throw new KeyNotFoundException("Seller to update not found");
        }
        _sellerRepository.UpdateAsync(seller);
    }

    public void DeleteSeller(Seller seller)
    {
        var existingState = _sellerRepository.GetByIdAsync(seller.Id).Result;
        if (existingState == null)
        {
            throw new KeyNotFoundException("Seller not found");
        }
        _sellerRepository.DeleteAsync(seller.Id);
    }
}
