using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Xml.Linq;

namespace Tests.UnitTests
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ILoggingService _loggingService;

        public ProductControllerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductController(_mockRepo.Object, _loggingService);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            var result = await _controller.GetById(1);

            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenProductExists()
        {
            //arrange
            DateTime now = DateTime.Now;
            var mockServerRepository = new Mock<IProductRepository>();
            var product = new Product { Id = 1, Name = "Walter", Price = 10, PackagingDate = now };
            mockServerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            var productService = new ProductService(mockServerRepository.Object);

            //fact
            var result = await productService.GetProductById(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Walter", result.Name);
            Assert.Equal(10, result.Price);
            Assert.Equal(now, result.PackagingDate);
        }

        [Fact]
        public void AddProduct_AddsProduct_WhenProductIsValid()
        {
            // arrange
            var mockProductRepository = new Mock<IProductRepository>();
            DateTime now = DateTime.Now;
            var product = new Product { Id = 9, Name = "Walter", Price = 10, PackagingDate = now };

            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

            var stateService = new ProductService(mockProductRepository.Object);

            // act
            stateService.AddProduct(product);

            // assert
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Walter" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
        }

        [Fact]
        public void AddProducts_AddsProducts_WhenProductsIsValid()
        {
            // arrange
            var mockProductRepository = new Mock<IProductRepository>();
            DateTime now = DateTime.Now;
            var products = new List<Product>
            {
                new () { Id = 1, Name = "Walter", Price =10, PackagingDate = now },
                new () { Id = 2, Name = "Daniel", Price =10, PackagingDate = now },
                new () { Id = 3, Name = "Joseph", Price =10, PackagingDate = now }
            };

            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

            var stateService = new ProductService(mockProductRepository.Object);

            // act
            foreach (var product in products)
            {
                stateService.AddProduct(product);
            }


            // assert
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Walter" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Daniel" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Joseph" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
        }

        [Fact]
        public void UpdateSelles_UpdatesProduct_WhenProductIsValid()
        {
            // arrange
            var mockProductRepository = new Mock<IProductRepository>();
            DateTime now = DateTime.Now;
            var product = new Product { Id = 1, Name = "Walter", Price = 10, PackagingDate = now };

            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

            var stateService = new ProductService(mockProductRepository.Object);

            // act
            stateService.AddProduct(product);


            // assert
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Walter" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
        }

        [Fact]
        public void DeleteProduct_DeletesProduct_WhenProductExists()
        {
            // arrange
            var mockProductRepository = new Mock<IProductRepository>();
            DateTime now = DateTime.Now;
            var product = new Product { Id = 1, Name = "Walter", Price = 10, PackagingDate = now };

            // Configuramos el mock para que devuelva el estado que estamos eliminando
            mockProductRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Product {Id = 1, Name = "Walter", Price = 10, PackagingDate = now }); // Simulamos que el estado con Id 1 existe

            mockProductRepository.Setup(repo => repo.DeleteAsync(1));

            var stateService = new ProductService(mockProductRepository.Object);

            // act
            stateService.DeleteProduct(product);

            // assert
            mockProductRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public void UpdateProduct_ThrowsException_WhenProductToUpdateDoesNotExist()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var mockProductRepository = new Mock<IProductRepository>();
            var product = new Product { Id = 999, Name = "Product", Price = 10, PackagingDate = now }; // ID que no existe
            mockProductRepository.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync((Product)null); // Simulamos que el vendedor (Product), no existe.

            var stateService = new ProductService(mockProductRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateProduct(product));
            Assert.Equal("Product to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

        [Fact]
        public async void GetStateById_ThrowsException_WhenStateDoesNotExist()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var mockProductRepository = new Mock<IProductRepository>();
            var productId = 999; // ID que no existe en la base de datos

            // Configuramos el mock para que devuelva el lenguage que estamos buscando
            mockProductRepository.Setup(repo => repo.GetByIdAsync(9)).ReturnsAsync(new Product { Id = 999, Name = "Product", Price = 10, PackagingDate = now }); // Simulamos que el vendedor (Product), con Id 999 que no existe

            mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null); // Simulamos que no se encuentra el continente.

            var productService = new ProductService(mockProductRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => productService.GetProductById(productId));
            Assert.Equal("Product not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }
    }


}
