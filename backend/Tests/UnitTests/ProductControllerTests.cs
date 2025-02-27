using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
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
            //arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\GetById_ReturnsNotFound_WhenProductDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            //Act
            var result = await _controller.GetById(product.Id);

            //Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\GetById_ReturnsProduct_WhenProductExists.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            var mockServerRepository = new Mock<IProductRepository>();
            mockServerRepository.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

            var productService = new ProductService(mockServerRepository.Object);

            // Act
            var result = await productService.GetProductById(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Product", result.Name);
            Assert.Equal(10.99m, result.Price);
            Assert.Equal(DateTime.Parse("2025-02-27T12:00:00"), result.PackagingDate);
        }

        [Fact]
        public void AddProduct_AddsProduct_WhenProductIsValid()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\AddProduct_AddsProduct_WhenProductIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

            var stateService = new ProductService(mockProductRepository.Object);

            //Act
            stateService.AddProduct(product);

            //Assert
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Ribbon – 35mm Wide on 50M Rolls – Red – Single Roll" && c.Price == 10 && c.PackagingDate == product.PackagingDate)), Times.Once);
        }

        [Fact]
        public void AddProducts_AddsProducts_WhenProductsIsValid()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            DateTime now = DateTime.Now;
            var products = new List<Product>
            {
                new () { Id = 1, Name = "Escaner 3D Leica BLK360 G2", Price =10, PackagingDate = now },
                new () { Id = 2, Name = "JMMO proyector inteligente", Price =10, PackagingDate = now },
                new () { Id = 3, Name = "Compra HomePod mini", Price =10, PackagingDate = now }
            };

            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

            var stateService = new ProductService(mockProductRepository.Object);

            //Act
            foreach (var product in products)
                stateService.AddProduct(product);

            //Assert
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Escaner 3D Leica BLK360 G2" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "JMMO proyector inteligente" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == "Compra HomePod mini" && c.Price == 10 && c.PackagingDate == now)), Times.Once);
        }

        [Fact]
        public void UpdateSelles_UpdatesProduct_WhenProductIsValid()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\UpdateSelles_UpdatesProduct_WhenProductIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

            var stateService = new ProductService(mockProductRepository.Object);

            //Act
            stateService.AddProduct(product);

            //Assert
            mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(c => c.Name == product.Name && c.Price == product.Price && c.PackagingDate == product.PackagingDate)), Times.Once);
        }

        [Fact]
        public void DeleteProduct_DeletesProduct_WhenProductExists()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\UpdateSelles_UpdatesProduct_WhenProductIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);  // Simulamos que el producto con Id 1 existe

            mockProductRepository.Setup(repo => repo.DeleteAsync(product.Id));

            var stateService = new ProductService(mockProductRepository.Object);

            //Act
            stateService.DeleteProduct(product);

            //Assert
            mockProductRepository.Verify(repo => repo.DeleteAsync(product.Id), Times.Once);
        }

        [Fact]
        public void UpdateProduct_ThrowsException_WhenProductToUpdateDoesNotExist()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\UpdateProduct_ThrowsException_WhenProductToUpdateDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync((Product)null); // Simulamos que el producto no existe.

            var stateService = new ProductService(mockProductRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateProduct(product));
            Assert.Equal("Product to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }


        [Fact]
        public async Task GetStateById_ThrowsException_WhenStateDoesNotExist()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Products\GetStateById_ThrowsException_WhenStateDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var product = JsonConvert.DeserializeObject<Product>(json);

            var mockProductRepository = new Mock<IProductRepository>();
            var productId = 999; // ID que no existe en la base de datos

            mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null); // Simulamos que el producto con ID 999 no existe

            var productService = new ProductService(mockProductRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => productService.GetProductById(productId));
            Assert.Equal("Product not found", exception.Message);
        }

    }


}
