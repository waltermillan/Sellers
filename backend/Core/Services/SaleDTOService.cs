using Core.DTOs;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SaleDTOService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IProductRepository _productRepository;

        public SaleDTOService(ISaleRepository saleRepository,
                              IBuyerRepository buyerRepository,
                              ISellerRepository sellerRepository,
                              IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
            _productRepository = productRepository;
        }

        public async Task<SaleDTO> GetSaleAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            var buyer = await _buyerRepository.GetByIdAsync(sale.IdBuyer);
            var seller = await _sellerRepository.GetByIdAsync(sale.IdSeller);
            var product = await _productRepository.GetByIdAsync(sale.IdProduct);

            if (sale == null || buyer == null || seller == null || product == null)
            {
                return null;
            }

            var saleDTO = new SaleDTO
            {
                Id = sale.Id,

                IdSeller = seller.Id,
                SellerName = seller.Name,

                IdBuyer = buyer.Id,
                BuyerName = buyer.Name,

                IdProduct = product.Id,
                ProductName = product.Name,

                Date = DateTime.Parse(sale.Date)
            };

            return saleDTO;
        }

        public async Task<IEnumerable<SaleDTO>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllAsync();

            if (sales is null || !sales.Any())

                return Enumerable.Empty<SaleDTO>();

            var salesDTO = new List<SaleDTO>();

            foreach (var sale in sales)
            {
                var buyer = await _buyerRepository.GetByIdAsync(sale.IdBuyer);
                var seller = await _sellerRepository.GetByIdAsync(sale.IdSeller);
                var product = await _productRepository.GetByIdAsync(sale.IdProduct);

                if (buyer is null || seller is null || product is null)
                    continue;

                var saleDTO = new SaleDTO
                {
                    Id = sale.Id,
                    IdSeller = seller.Id,
                    SellerName = seller.Name,
                    IdBuyer = buyer.Id,
                    BuyerName = buyer.Name,
                    IdProduct = product.Id,
                    ProductName = product.Name,
                    Date = DateTime.Parse(sale.Date)
                };

                salesDTO.Add(saleDTO);
            }

            return salesDTO;
        }

    }
}
