using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests
{
    public class SellerControllerTests
    {
        private readonly SellerController _controller;
        private readonly Mock<ISellerRepository> _mockRepo;

        public SellerControllerTests()
        {
            _mockRepo = new Mock<ISellerRepository>();
            _controller = new SellerController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenSellerDoesNotExist()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Seller)null);

            var result = await _controller.GetById(1);

            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        // Aquí agregarías más pruebas
    }

}
