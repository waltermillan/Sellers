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

        if (product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

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
        {
            _productRepository.AddAsync(product);
        }
    }

    public void UpdateProduct(Product product)
    {
        var existingProduct = _productRepository.GetByIdAsync(product.Id).Result;
        if (existingProduct == null)
        {
            throw new KeyNotFoundException("Product to update not found");
        }
        _productRepository.UpdateAsync(product);
    }

    public void DeleteProduct(Product product)
    {
        var existingState = _productRepository.GetByIdAsync(product.Id).Result;
        if (existingState == null)
        {
            throw new KeyNotFoundException("Product not found");
        }
        _productRepository.DeleteAsync(product.Id);
    }
}
