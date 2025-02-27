using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class SaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Sale> GetSaleById(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);

        if (sale is null)
            throw new KeyNotFoundException("Sale not found");

        return sale;
    }

    public async Task<IEnumerable<Sale>> GetAllSales()
    {
        return await _saleRepository.GetAllAsync();
    }

    public void AddSale(Sale sale)
    {
        _saleRepository.AddAsync(sale);
    }

    public void AddSales(IEnumerable<Sale> sales)
    {
        foreach (var sale in sales)
            _saleRepository.AddAsync(sale);
    }

    public void UpdateSale(Sale sale)
    {
        var exists = _saleRepository.GetByIdAsync(sale.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Sale to update not found");

        _saleRepository.UpdateAsync(sale);
    }

    public void DeleteSale(Sale sale)
    {
        var exits = _saleRepository.GetByIdAsync(sale.Id).Result;

        if (exits is null)
            throw new KeyNotFoundException("Sale not found");

        _saleRepository.DeleteAsync(sale.Id);
    }
}
