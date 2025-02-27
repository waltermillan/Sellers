using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Tests.UnitTests
{
    public class SellerControllerTests
    {
        private readonly SellerController _controller;
        private readonly Mock<ISellerRepository> _mockRepo;
        private readonly ILoggingService _loggingService;

        public SellerControllerTests()
        {
            _mockRepo = new Mock<ISellerRepository>();
            _controller = new SellerController(_mockRepo.Object, _loggingService);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenSellerDoesNotExist()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\GetById_ReturnsNotFound_WhenSellerDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var seller = JsonConvert.DeserializeObject<Seller>(json);

            _mockRepo.Setup(repo => repo.GetByIdAsync(seller.Id)).ReturnsAsync((Seller)null); // Simulamos que el vendedor no existe

            // Act
            var result = await _controller.GetById(seller.Id);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsSeller_WhenSellerExists()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\GetById_ReturnsSeller_WhenSellerExists.json");
            var json = File.ReadAllText(jsonFilePath);

            var seller = JsonConvert.DeserializeObject<Seller>(json);

            var mockServerRepository = new Mock<ISellerRepository>();
            mockServerRepository.Setup(repo => repo.GetByIdAsync(seller.Id)).ReturnsAsync(seller);

            var sellerService = new SellerService(mockServerRepository.Object);

            // Act
            var result = await sellerService.GetSellerById(seller.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(seller.Id, result.Id);
            Assert.Equal(seller.Name, result.Name);
            Assert.Equal(seller.Birthday, result.Birthday);
        }

        [Fact]
        public void AddSeller_AddsSeller_WhenSellerIsValid()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\AddSeller_AddsSeller_WhenSellerIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var seller = JsonConvert.DeserializeObject<Seller>(json);

            var mockSellerRepository = new Mock<ISellerRepository>();
            mockSellerRepository.Setup(repo => repo.AddAsync(It.IsAny<Seller>()));

            var stateService = new SellerService(mockSellerRepository.Object);

            // Act
            stateService.AddSeller(seller);

            // Assert
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == seller.Name && c.Birthday == seller.Birthday)), Times.Once);
        }


        [Fact]
        public void AddSellers_AddsSellers_WhenSellersIsValid()
        {
            //Arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");

            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Tests", "UnitTests", "AddSellers_AddsSellers_WhenSellersIsValid.json");
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\AddSellers_AddsSellers_WhenSellersIsValid.json");

            var sellersJson = File.ReadAllText(jsonFilePath);
            var sellers = JsonConvert.DeserializeObject<List<Seller>>(sellersJson);

            mockSellerRepository.Setup(repo => repo.AddAsync(It.IsAny<Seller>()));

            var stateService = new SellerService(mockSellerRepository.Object);

            //Act
            foreach (var seller in sellers)
                stateService.AddSeller(seller);

            //Assert
            foreach (var seller in sellers)
                mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == seller.Name && c.Birthday == now)), Times.Once);
        }

        [Fact]
        public void UpdateSeller_UpdatesSeller_WhenSellerIsValid()
        {
            //Arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\UpdateSeller_UpdatesSeller_WhenSellerIsValid.json");

            var sellerJson = File.ReadAllText(jsonFilePath);
            var seller = JsonConvert.DeserializeObject<Seller>(sellerJson);

            mockSellerRepository.Setup(repo => repo.AddAsync(It.IsAny<Seller>()));

            var stateService = new SellerService(mockSellerRepository.Object);

            //Act
            stateService.AddSeller(seller);

            //Assert
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == seller.Name && c.Birthday == now)), Times.Once);
        }


        [Fact]
        public void DeleteSeller_DeletesSeller_WhenSellerExists()
        {
            //Arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");

            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\DeleteSeller_DeletesSeller_WhenSellerExists.json");

            var sellerJson = File.ReadAllText(jsonFilePath);
            var seller = JsonConvert.DeserializeObject<Seller>(sellerJson);

            mockSellerRepository.Setup(repo => repo.GetByIdAsync(seller.Id)).ReturnsAsync(seller); // Simulamos que el estado con Id existe
            mockSellerRepository.Setup(repo => repo.DeleteAsync(seller.Id));
            var stateService = new SellerService(mockSellerRepository.Object);

            //Act
            stateService.DeleteSeller(seller);

            //Assert
            mockSellerRepository.Verify(repo => repo.DeleteAsync(seller.Id), Times.Once);
        }


        [Fact]
        public void UpdateSeller_ThrowsException_WhenSellerToUpdateDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\UpdateSeller_ThrowsException_WhenSellerToUpdateDoesNotExist.json");

            var sellerJson = File.ReadAllText(jsonFilePath);
            var seller = JsonConvert.DeserializeObject<Seller>(sellerJson); // Deserializamos el JSON

            var mockSellerRepository = new Mock<ISellerRepository>();
            mockSellerRepository.Setup(repo => repo.GetByIdAsync(seller.Id)).ReturnsAsync((Seller)null); // Simulamos que el vendedor no existe

            var stateService = new SellerService(mockSellerRepository.Object);

            //Act
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateSeller(seller));

            //Assert
            Assert.Equal("Seller to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

        [Fact]
        public async void GetSellerById_ThrowsException_WhenSellerDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Sellers\GetSellerById_ThrowsException_WhenSellerDoesNotExist.json");
            var sellerJson = File.ReadAllText(jsonFilePath);
            var seller = JsonConvert.DeserializeObject<Seller>(sellerJson); // Deserializamos el JSON

            var mockSellerRepository = new Mock<ISellerRepository>();
            var sellerId = seller.Id; // Usamos el ID del vendedor deserializado

            mockSellerRepository.Setup(repo => repo.GetByIdAsync(sellerId)).ReturnsAsync((Seller)null); // Simulamos que no se encuentra el vendedor

            var sellerService = new SellerService(mockSellerRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => sellerService.GetSellerById(sellerId));

            //Assert
            Assert.Equal("Seller not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

    }


}
