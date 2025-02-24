using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Seller)null);

            var result = await _controller.GetById(1);

            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsSeller_WhenSellerExists()
        {
            //arrange
            DateTime now = DateTime.Now;
            var mockServerRepository = new Mock<ISellerRepository>();
            var seller = new Seller { Id = 1, Name = "Walter", Birthday = now };
            mockServerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(seller);

            var sellerService = new SellerService(mockServerRepository.Object);

            //fact
            var result = await sellerService.GetSellerById(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Walter", result.Name);
            Assert.Equal(now, result.Birthday);
        }

        [Fact]
        public void AddSeller_AddsSeller_WhenSellerIsValid()
        {
            // arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Now;
            var seller = new Seller { Id = 9, Name = "Walter", Birthday = now };

            mockSellerRepository.Setup(repo => repo.AddAsync(It.IsAny<Seller>()));

            var stateService = new SellerService(mockSellerRepository.Object);

            // act
            stateService.AddSeller(seller);

            // assert
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == "Walter" && c.Birthday == now)), Times.Once);
        }

        [Fact]
        public void AddSellers_AddsSellers_WhenSellersIsValid()
        {
            // arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Now;
            var sellers = new List<Seller>
            {
                new () { Id = 1, Name = "Walter", Birthday = now },
                new () { Id = 2, Name = "Daniel", Birthday = now },
                new () { Id = 3, Name = "Joseph", Birthday = now }
            };

            mockSellerRepository.Setup(repo => repo.AddAsync(It.IsAny<Seller>()));

            var stateService = new SellerService(mockSellerRepository.Object);

            // act
            foreach (var seller in sellers)
            {
                stateService.AddSeller(seller);
            }


            // assert
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == "Walter" && c.Birthday == now)), Times.Once);
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == "Daniel" && c.Birthday == now)), Times.Once);
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == "Joseph" && c.Birthday == now)), Times.Once);
        }

        [Fact]
        public void UpdateSelles_UpdatesSeller_WhenSellerIsValid()
        {
            // arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Now;
            var seller = new Seller { Id = 1, Name = "Walter", Birthday = now };

            mockSellerRepository.Setup(repo => repo.AddAsync(It.IsAny<Seller>()));

            var stateService = new SellerService(mockSellerRepository.Object);

            // act
            stateService.AddSeller(seller);


            // assert
            mockSellerRepository.Verify(repo => repo.AddAsync(It.Is<Seller>(c => c.Name == "Walter" && c.Birthday == now)), Times.Once);
        }

        [Fact]
        public void DeleteSeller_DeletesSeller_WhenSellerExists()
        {
            // arrange
            var mockSellerRepository = new Mock<ISellerRepository>();
            DateTime now = DateTime.Now;
            var seller = new Seller { Id = 1, Name = "Walter", Birthday = now };

            // Configuramos el mock para que devuelva el estado que estamos eliminando
            mockSellerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Seller {Id = 1, Name = "Walter", Birthday = now }); // Simulamos que el estado con Id 1 existe

            mockSellerRepository.Setup(repo => repo.DeleteAsync(1));

            var stateService = new SellerService(mockSellerRepository.Object);

            // act
            stateService.DeleteSeller(seller);

            // assert
            mockSellerRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public void UpdateSeller_ThrowsException_WhenSellerToUpdateDoesNotExist()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var mockSellerRepository = new Mock<ISellerRepository>();
            var seller = new Seller { Id = 999, Name = "Seller", Birthday = now }; // ID que no existe
            mockSellerRepository.Setup(repo => repo.GetByIdAsync(seller.Id)).ReturnsAsync((Seller)null); // Simulamos que el vendedor (Seller), no existe.

            var stateService = new SellerService(mockSellerRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateSeller(seller));
            Assert.Equal("Seller to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

        [Fact]
        public async void GetStateById_ThrowsException_WhenStateDoesNotExist()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var mockSellerRepository = new Mock<ISellerRepository>();
            var sellerId = 999; // ID que no existe en la base de datos

            // Configuramos el mock para que devuelva el lenguage que estamos buscando
            mockSellerRepository.Setup(repo => repo.GetByIdAsync(9)).ReturnsAsync(new Seller { Id = 999, Name = "Seller", Birthday = now }); // Simulamos que el vendedor (Seller), con Id 999 que no existe

            mockSellerRepository.Setup(repo => repo.GetByIdAsync(sellerId)).ReturnsAsync((Seller)null); // Simulamos que no se encuentra el continente.

            var sellerService = new SellerService(mockSellerRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => sellerService.GetSellerById(sellerId));
            Assert.Equal("Seller not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }
    }


}
