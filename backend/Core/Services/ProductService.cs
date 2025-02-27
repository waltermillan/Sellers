using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository sellerRepository)
    {
        _productRepository = sellerRepository;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            throw new KeyNotFoundException("Product not found");

        return product;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _productRepository.GetAllAsync();
    }

    public void AddProduct(Product product)
    {
        _productRepository.AddAsync(product);
    }

    public void AddProducts(IEnumerable<Product> products)
    {
        foreach (var product in products)
            _productRepository.AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        var exists = _productRepository.GetByIdAsync(product.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Product to update not found");

        _productRepository.UpdateAsync(product);
    }

    public void DeleteProduct(Product product)
    {
        var exists = _productRepository.GetByIdAsync(product.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Product not found");

        _productRepository.DeleteAsync(product.Id);
    }
}
