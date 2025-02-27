using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Tests.UnitTests
{
    public class BuyerControllerTests
    {
        private readonly BuyerController _controller;
        private readonly Mock<IBuyerRepository> _mockRepo;
        private readonly ILoggingService _loggingService;

        public BuyerControllerTests()
        {
            _mockRepo = new Mock<IBuyerRepository>();
            _controller = new BuyerController(_mockRepo.Object, _loggingService);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenBuyerDoesNotExist()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\GetById_ReturnsNotFound_WhenBuyerDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var buyer = JsonConvert.DeserializeObject<Buyer>(json);

            _mockRepo.Setup(repo => repo.GetByIdAsync(buyer.Id)).ReturnsAsync((Buyer)null); // Simulamos que el vendedor no existe

            // Act
            var result = await _controller.GetById(buyer.Id);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsBuyer_WhenBuyerExists()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\GetById_ReturnsBuyer_WhenBuyerExists.json");
            var json = File.ReadAllText(jsonFilePath);

            var buyer = JsonConvert.DeserializeObject<Buyer>(json);

            var mockServerRepository = new Mock<IBuyerRepository>();
            mockServerRepository.Setup(repo => repo.GetByIdAsync(buyer.Id)).ReturnsAsync(buyer);

            var buyerService = new BuyerService(mockServerRepository.Object);

            // Act
            var result = await buyerService.GetBuyerById(buyer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(buyer.Id, result.Id);
            Assert.Equal(buyer.Name, result.Name);
            Assert.Equal(buyer.SocialSecurityNumber, result.SocialSecurityNumber);
        }

        [Fact]
        public void AddBuyer_AddsBuyer_WhenBuyerIsValid()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\AddBuyer_AddsBuyer_WhenBuyerIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var buyer = JsonConvert.DeserializeObject<Buyer>(json);

            var mockBuyerRepository = new Mock<IBuyerRepository>();
            mockBuyerRepository.Setup(repo => repo.AddAsync(It.IsAny<Buyer>()));

            var stateService = new BuyerService(mockBuyerRepository.Object);

            // Act
            stateService.AddBuyer(buyer);

            // Assert
            mockBuyerRepository.Verify(repo => repo.AddAsync(It.Is<Buyer>(c => c.Name == buyer.Name && c.SocialSecurityNumber == buyer.SocialSecurityNumber)), Times.Once);
        }


        [Fact]
        public void AddBuyers_AddsBuyers_WhenBuyersIsValid()
        {
            // Arrange
            var mockBuyerRepository = new Mock<IBuyerRepository>();

            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\AddBuyers_AddsBuyers_WhenBuyersIsValid.json");
            var buyersJson = File.ReadAllText(jsonFilePath);
            var buyers = JsonConvert.DeserializeObject<List<Buyer>>(buyersJson);

            mockBuyerRepository.Setup(repo => repo.AddAsync(It.IsAny<Buyer>()));

            var stateService = new BuyerService(mockBuyerRepository.Object);

            // Act
            foreach (var buyer in buyers)
                stateService.AddBuyer(buyer);

            // Assert
            foreach (var buyer in buyers)
                mockBuyerRepository.Verify(repo => repo.AddAsync(It.Is<Buyer>(c =>
                    c.Name == buyer.Name && c.SocialSecurityNumber == buyer.SocialSecurityNumber)), Times.Once);
        }


        [Fact]
        public void UpdateBuyer_UpdatesBuyer_WhenBuyerIsValid()
        {
            //Arrange
            var mockBuyerRepository = new Mock<IBuyerRepository>();
            int socialSecurityNumber = 253614759;
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\UpdateBuyer_UpdatesBuyer_WhenBuyerIsValid.json");

            var buyerJson = File.ReadAllText(jsonFilePath);
            var buyer = JsonConvert.DeserializeObject<Buyer>(buyerJson);

            mockBuyerRepository.Setup(repo => repo.AddAsync(It.IsAny<Buyer>()));

            var stateService = new BuyerService(mockBuyerRepository.Object);

            //Act
            stateService.AddBuyer(buyer);

            //Assert
            mockBuyerRepository.Verify(repo => repo.AddAsync(It.Is<Buyer>(c => c.Name == buyer.Name && c.SocialSecurityNumber == socialSecurityNumber)), Times.Once);
        }


        [Fact]
        public void DeleteBuyer_DeletesBuyer_WhenBuyerExists()
        {
            //Arrange
            var mockBuyerRepository = new Mock<IBuyerRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");

            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\DeleteBuyer_DeletesBuyer_WhenBuyerExists.json");

            var buyerJson = File.ReadAllText(jsonFilePath);
            var buyer = JsonConvert.DeserializeObject<Buyer>(buyerJson);

            mockBuyerRepository.Setup(repo => repo.GetByIdAsync(buyer.Id)).ReturnsAsync(buyer); // Simulamos que el estado con Id existe
            mockBuyerRepository.Setup(repo => repo.DeleteAsync(buyer.Id));
            var stateService = new BuyerService(mockBuyerRepository.Object);

            //Act
            stateService.DeleteBuyer(buyer);

            //Assert
            mockBuyerRepository.Verify(repo => repo.DeleteAsync(buyer.Id), Times.Once);
        }


        [Fact]
        public void UpdateBuyer_ThrowsException_WhenBuyerToUpdateDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\UpdateBuyer_ThrowsException_WhenBuyerToUpdateDoesNotExist.json");

            var buyerJson = File.ReadAllText(jsonFilePath);
            var buyer = JsonConvert.DeserializeObject<Buyer>(buyerJson); // Deserializamos el JSON

            var mockBuyerRepository = new Mock<IBuyerRepository>();
            mockBuyerRepository.Setup(repo => repo.GetByIdAsync(buyer.Id)).ReturnsAsync((Buyer)null); // Simulamos que el vendedor no existe

            var stateService = new BuyerService(mockBuyerRepository.Object);

            //Act
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateBuyer(buyer));

            //Assert
            Assert.Equal("Buyer to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

        [Fact]
        public async void GetBuyerById_ThrowsException_WhenBuyerDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Buyers\GetBuyerById_ThrowsException_WhenBuyerDoesNotExist.json");
            var buyerJson = File.ReadAllText(jsonFilePath);
            var buyer = JsonConvert.DeserializeObject<Buyer>(buyerJson); // Deserializamos el JSON

            var mockBuyerRepository = new Mock<IBuyerRepository>();
            var buyerId = buyer.Id; // Usamos el ID del vendedor deserializado

            mockBuyerRepository.Setup(repo => repo.GetByIdAsync(buyerId)).ReturnsAsync((Buyer)null); // Simulamos que no se encuentra el vendedor

            var buyerService = new BuyerService(mockBuyerRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => buyerService.GetBuyerById(buyerId));

            //Assert
            Assert.Equal("Buyer not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

    }


}
