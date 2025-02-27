using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Tests.UnitTests
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly ILoggingService _loggingService;

        public UserControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _controller = new UserController(_mockRepo.Object, _loggingService);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\GetById_ReturnsNotFound_WhenUserDoesNotExist.json");
            var json = File.ReadAllText(jsonFilePath);

            var user = JsonConvert.DeserializeObject<User>(json);

            _mockRepo.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync((User)null); // Simulamos que el vendedor no existe

            // Act
            var result = await _controller.GetById(user.Id);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\GetById_ReturnsUser_WhenUserExists.json");
            var json = File.ReadAllText(jsonFilePath);

            var user = JsonConvert.DeserializeObject<User>(json);

            var mockServerRepository = new Mock<IUserRepository>();
            mockServerRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);

            var userService = new UserService(mockServerRepository.Object);

            // Act
            var result = await userService.GetUserById(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.PasswordHash, result.PasswordHash);
            Assert.Equal(user.CreatedAt, result.CreatedAt);
            Assert.Equal(user.UpdatedAt, result.UpdatedAt);
        }

        [Fact]
        public void AddUser_AddsUser_WhenUserIsValid()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\AddUser_AddsUser_WhenUserIsValid.json");
            var json = File.ReadAllText(jsonFilePath);

            var user = JsonConvert.DeserializeObject<User>(json);

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()));

            var stateService = new UserService(mockUserRepository.Object);

            // Act
            stateService.AddUser(user);

            // Assert
            mockUserRepository.Verify(repo => repo.AddAsync(It.Is<User>(c => c.UserName == user.UserName && c.CreatedAt == user.CreatedAt && c.UpdatedAt == user.UpdatedAt)), Times.Once);
        }


        [Fact]
        public void AddUsers_AddsUsers_WhenUsersIsValid()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");

            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Tests", "UnitTests", "AddUsers_AddsUsers_WhenUsersIsValid.json");
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\AddUsers_AddsUsers_WhenUsersIsValid.json");

            var usersJson = File.ReadAllText(jsonFilePath);
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()));

            var stateService = new UserService(mockUserRepository.Object);

            //Act
            foreach (var user in users)
                stateService.AddUser(user);

            //Assert
            foreach (var user in users)
                mockUserRepository.Verify(repo => repo.AddAsync(It.Is<User>(c => c.UserName == user.UserName && c.CreatedAt == now && c.UpdatedAt == now)), Times.Once);
        }

        [Fact]
        public void UpdateUser_UpdatesUser_WhenUserIsValid()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\UpdateUser_UpdatesUser_WhenUserIsValid.json");

            var userJson = File.ReadAllText(jsonFilePath);
            var user = JsonConvert.DeserializeObject<User>(userJson);

            mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()));

            var stateService = new UserService(mockUserRepository.Object);

            //Act
            stateService.AddUser(user);

            //Assert
            mockUserRepository.Verify(repo => repo.AddAsync(It.Is<User>(c => c.UserName == user.UserName && c.CreatedAt == now && c.UpdatedAt == now)), Times.Once);
        }


        [Fact]
        public void DeleteUser_DeletesUser_WhenUserExists()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            DateTime now = DateTime.Parse("2025-02-27T00:00:00");

            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\DeleteUser_DeletesUser_WhenUserExists.json");

            var userJson = File.ReadAllText(jsonFilePath);
            var user = JsonConvert.DeserializeObject<User>(userJson);

            mockUserRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user); // Simulamos que el estado con Id existe
            mockUserRepository.Setup(repo => repo.DeleteAsync(user.Id));
            var stateService = new UserService(mockUserRepository.Object);

            //Act
            stateService.DeleteUser(user);

            //Assert
            mockUserRepository.Verify(repo => repo.DeleteAsync(user.Id), Times.Once);
        }


        [Fact]
        public void UpdateUser_ThrowsException_WhenUserToUpdateDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\UpdateUser_ThrowsException_WhenUserToUpdateDoesNotExist.json");

            var userJson = File.ReadAllText(jsonFilePath);
            var user = JsonConvert.DeserializeObject<User>(userJson); // Deserializamos el JSON

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync((User)null); // Simulamos que el vendedor no existe

            var stateService = new UserService(mockUserRepository.Object);

            //Act
            var exception = Assert.Throws<KeyNotFoundException>(() => stateService.UpdateUser(user));

            //Assert
            Assert.Equal("User to update not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

        [Fact]
        public async void GetUserById_ThrowsException_WhenUserDoesNotExist()
        {
            //Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\Users\GetUserById_ThrowsException_WhenUserDoesNotExist.json");
            var userJson = File.ReadAllText(jsonFilePath);
            var user = JsonConvert.DeserializeObject<User>(userJson); // Deserializamos el JSON

            var mockUserRepository = new Mock<IUserRepository>();
            var userId = user.Id; // Usamos el ID del vendedor deserializado

            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null); // Simulamos que no se encuentra el vendedor

            var userService = new UserService(mockUserRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => userService.GetUserById(userId));

            //Assert
            Assert.Equal("User not found", exception.Message); // Verificamos que el mensaje de la excepción sea el esperado
        }

    }


}
