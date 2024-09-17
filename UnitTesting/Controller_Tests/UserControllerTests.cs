using Data_Access_Layer.Hasher;
using ExpenseReportAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Controller_Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepoBL> _mockRepo;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockRepo = new Mock<IUserRepoBL>();
            _mockConfig = new Mock<IConfiguration>();
            _controller = new UserController(_mockConfig.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new Users { Id = userId, Name = "Test User" };
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<Users>(okResult.Value);
            Assert.Equal(userId, returnedUser.Id);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((Users)null);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddUser_ReturnsOkResult_WhenUserIsAdded()
        {
            // Arrange
            var newUser = new IncomingUser { Name = "Test User" };
            var user = new Users { Id = Guid.NewGuid(), Name = "Test User" };
            _mockRepo.Setup(repo => repo.AddUser(newUser)).ReturnsAsync(user);

            // Act
            var result = await _controller.AddUser(newUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<Users>(okResult.Value);
            Assert.Equal("Test User", returnedUser.Name);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOkResult_WhenUserIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteUser(userId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Deleted", okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteUser(userId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOkResult_WhenUserIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUser = new IncomingUser { Name = "Updated User" };
            var user = new Users { Id = userId, Name = "Updated User" };
            _mockRepo.Setup(repo => repo.UpdateUser(userId, updateUser)).ReturnsAsync(user);

            // Act
            var result = await _controller.UpdateUser(userId, updateUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<Users>(okResult.Value);
            Assert.Equal("Updated User", returnedUser.Name);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUser = new IncomingUser { Name = "Updated User" };
            _mockRepo.Setup(repo => repo.UpdateUser(userId, updateUser)).ReturnsAsync((Users)null);

            // Act
            var result = await _controller.UpdateUser(userId, updateUser);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WhenUsersExist()
        {
            // Arrange
            var users = new List<Users>
        {
            new Users { Id = Guid.NewGuid(), Name = "User1" },
            new Users { Id = Guid.NewGuid(), Name = "User2" }
        };
            _mockRepo.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsType<List<Users>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsNotFound_WhenNoUsersExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllUsers()).ReturnsAsync(new List<Users>());

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

       
    }
}
