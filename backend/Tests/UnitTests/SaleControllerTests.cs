using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Tests.UnitTests
{
    public class SaleControllerTests
    {
        private readonly SaleController _controller;
        private readonly Mock<ISaleRepository> _mockRepo;
        private readonly ILoggingService _loggingService;

        public SaleControllerTests()
        {
            _mockRepo = new Mock<ISaleRepository>();
            _controller = new SaleController(_mockRepo.Object, _loggingService);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenSaleDoesNotExist()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\GetById_ReturnsNotFound_WhenSaleDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var sale = JsonConvert.DeserializeObject<Sale>(json);

            _mockRepo.Setup(repo => repo.GetByIdAsync(sale.Id)).ReturnsAsync((Sale)null); // Simulamos que el vendedor no existe

            // Act
            var result = await _controller.GetById(sale.Id);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsSale_WhenSaleExists()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\GetById_ReturnsSale_WhenSaleExists.json");
            var json = File.ReadAllText(jsonFilePath);

            var sale = JsonConvert.DeserializeObject<Sale>(json);

            var mockServerRepository = new Mock<ISaleRepository>();
            mockServerRepository.Setup(repo => repo.GetByIdAsync(sale.Id)).ReturnsAsync(sale);

            var saleService = new SaleService(mockServerRepository.Object);

            // Act
            var result = await saleService.GetSaleById(sale.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Id);
            Assert.Equal(sale.Date, result.Date);
            Assert.Equal(sale.IdBuyer, result.IdBuyer);
            Assert.Equal(sale.IdProduct, result.IdProduct);
            Assert.Equal(sale.IdSeller, result.IdSeller);
        }

        [Fact]
        public void AddSale_AddsSale_WhenSaleIsValid()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\AddSale_AddsSale_WhenSaleIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var sale = JsonConvert.DeserializeObject<Sale>(json);

            var mockSaleRepository = new Mock<ISaleRepository>();
            mockSaleRepository.Setup(repo => repo.AddAsync(It.IsAny<Sale>()));

            var stateService = new SaleService(mockSaleRepository.Object);

            // Act
            stateService.AddSale(sale);

            // Assert
            mockSaleRepository.Verify(repo => repo.AddAsync(It.Is<Sale>(c => c.Date == sale.Date && c.IdSeller == sale.IdSeller && c.IdBuyer == sale.IdBuyer && c.IdProduct == sale.IdProduct)), Times.Once);
        }


        [Fact]
        public void AddSales_AddsSales_WhenSalesIsValid()
        {
            //Arrange
            var mockSaleRepository = new Mock<ISaleRepository>();
            string now = "2025-02-27T00:00:00";

            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Tests", "UnitTests", "AddSales_AddsSales_WhenSalesIsValid.json");
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\AddSales_AddsSales_WhenSalesIsValid.json");

            var salesJson = File.ReadAllText(jsonFilePath);
            var sales = JsonConvert.DeserializeObject<List<Sale>>(salesJson);

            mockSaleRepository.Setup(repo => repo.AddAsync(It.IsAny<Sale>()));

            var stateService = new SaleService(mockSaleRepository.Object);

            //Act
            foreach (var sale in sales)
                stateService.AddSale(sale);

            //Assert
            foreach (var sale in sales)
                mockSaleRepository.Verify(repo => repo.AddAsync(It.Is<Sale>(c => c.IdBuyer == sale.IdBuyer && c.IdSeller == sale.IdSeller && c.IdProduct == sale.IdProduct && c.Date == now)), Times.Once);
        }

        [Fact]
        public void UpdateSale_UpdatesSale_WhenSaleIsValid()
        {
            //Arrange
            var mockSaleRepository = new Mock<ISaleRepository>();
            string now = "2025-02-27T00:00:00";
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\UpdateSale_UpdatesSale_WhenSaleIsValid.json");

            var saleJson = File.ReadAllText(jsonFilePath);
            var sale = JsonConvert.DeserializeObject<Sale>(saleJson);

            mockSaleRepository.Setup(repo => repo.AddAsync(It.IsAny<Sale>()));

            var stateService = new SaleService(mockSaleRepository.Object);

            //Act
            stateService.AddSale(sale);

            //Assert
            mockSaleRepository.Verify(repo => repo.AddAsync(It.Is<Sale>(c => c.IdBuyer == sale.IdBuyer && c.IdSeller == sale.IdSeller && c.IdProduct == sale.IdProduct && c.Date == now)), Times.Once);
        }


        [Fact]
        public void DeleteSale_DeletesSale_WhenSaleExists()
        {
            //Arrange
            var mockSaleRepository = new Mock<ISaleRepository>();

            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\DeleteSale_DeletesSale_WhenSaleExists.json");

            var saleJson = File.ReadAllText(jsonFilePath);
            var sale = JsonConvert.DeserializeObject<Sale>(saleJson);

            mockSaleRepository.Setup(repo => repo.GetByIdAsync(sale.Id)).ReturnsAsync(sale); // Simulamos que el estado con Id existe
            mockSaleRepository.Setup(repo => repo.DeleteAsync(sale.Id));
            var stateService = new SaleService(mockSaleRepository.Object);

            //Act
            stateService.DeleteSale(sale);

            //Assert
            mockSaleRepository.Verify(repo => repo.DeleteAsync(sale.Id), Times.Once);
        }


        [Fact]
        public void UpdateSale_ThrowsException_WhenSaleToUpdateDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\UpdateSale_ThrowsException_WhenSaleToUpdateDoesNotExist.json");

            var saleJson = File.ReadAllText(jsonFilePath);
            var sale = JsonConvert.DeserializeObject<Sale>(saleJson); // Deserializamos el JSON

            var mockSaleRepository = new Mock<ISaleRepository>();
            mockSaleRepository.Setup(repo => repo.GetByIdAsync(sale.Id)).ReturnsAsync((Sale)null); // Simulamos que el vendedor no existe

            var stateService = new SaleService(mockSaleRepository.Object);

            //Act
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateSale(sale));

            //Assert
            Assert.Equal("Sale to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

        [Fact]
        public async void GetSaleById_ThrowsException_WhenSaleDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sales\GetSaleById_ThrowsException_WhenSaleDoesNotExist.json");
            var saleJson = File.ReadAllText(jsonFilePath);
            var sale = JsonConvert.DeserializeObject<Sale>(saleJson); // Deserializamos el JSON

            var mockSaleRepository = new Mock<ISaleRepository>();
            var saleId = sale.Id; // Usamos el ID del vendedor deserializado

            mockSaleRepository.Setup(repo => repo.GetByIdAsync(saleId)).ReturnsAsync((Sale)null); // Simulamos que no se encuentra el vendedor

            var saleService = new SaleService(mockSaleRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => saleService.GetSaleById(saleId));

            //Assert
            Assert.Equal("Sale not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

    }


}
