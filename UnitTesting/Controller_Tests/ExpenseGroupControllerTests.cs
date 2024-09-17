using ExpenseReportAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
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
    public class ExpenseGroupControllerTests
    {
        private readonly Mock<IExpenseGroupBL> _mockRepo;
        private readonly ExpenseGroupController _controller;

        public ExpenseGroupControllerTests()
        {
            _mockRepo = new Mock<IExpenseGroupBL>();
            _controller = new ExpenseGroupController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetMembers_ReturnsOkResult_WithListOfMembers()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var users = new List<Users> { new Users { Id = Guid.NewGuid(), Name = "User1", Email="user@mail.com"} };
            _mockRepo.Setup(repo => repo.GetUsersByGroupId(groupId)).ReturnsAsync(users);

            // Act
            var result = await _controller.GetMembers(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Users>>(okResult.Value);
            Assert.Equal(users, returnValue);
        }

        [Fact]
        public async Task GetMembers_ReturnsNotFound_WhenExceptionIsThrown()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetUsersByGroupId(groupId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetMembers(groupId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetExpenseGroupById_ReturnsOkResult_WithExpenseGroup()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var expenseGroup = new ExpenseGroup { GroupId = groupId, Name = "Group1",Description="Group Descriptions" };
            _mockRepo.Setup(repo => repo.GetExpenseGroupById(groupId)).ReturnsAsync(expenseGroup);

            // Act
            var result = await _controller.GetExpenseGroupById(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ExpenseGroup>(okResult.Value);
            Assert.Equal(expenseGroup, returnValue);
        }

        [Fact]
        public async Task GetExpenseGroupById_ReturnsNotFound_WhenExceptionIsThrown()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetExpenseGroupById(groupId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetExpenseGroupById(groupId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateExpenseGroup_ReturnsOkResult_WithExpenseGroup()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var incomingExpenseGroup = new IncomingExpenseGroupDTO { Name = "Group1",Description="Group Description" };
            var expenseGroup = new ExpenseGroup { GroupId = Guid.NewGuid(), Name = "Group1",Description= "Group Description" };
            _mockRepo.Setup(repo => repo.CreateExpenseGroup(userId, incomingExpenseGroup)).ReturnsAsync(expenseGroup);

            // Act
            var result = await _controller.CreateExpenseGroup(userId, incomingExpenseGroup);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ExpenseGroup>(okResult.Value);
            Assert.Equal(expenseGroup.Name, returnValue.Name);
            Assert.Equal(expenseGroup.Description, returnValue.Description);
            Assert.Equal(expenseGroup, returnValue);
        }

        [Fact]
        public async Task CreateExpenseGroup_ReturnsNotFound_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var incomingExpenseGroup = new IncomingExpenseGroupDTO { Name = "Group1" , Description = "Group Description" };
            _mockRepo.Setup(repo => repo.CreateExpenseGroup(userId, incomingExpenseGroup)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateExpenseGroup(userId, incomingExpenseGroup);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetExpenseGroupByUserId_ReturnsOkResult_WithExpenseGroups()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenseGroups = new List<ExpenseGroupDTO> { new ExpenseGroupDTO { GroupId = Guid.NewGuid(), Name = "Group1",Description="Group Description" } };
            _mockRepo.Setup(repo => repo.GetExpenseGroupByUserId(userId)).ReturnsAsync(expenseGroups);

            // Act
            var result = await _controller.GetExpenseGroupByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ExpenseGroupDTO>>(okResult.Value);
            Assert.Equal(expenseGroups, returnValue);
        }

        [Fact]
        public async Task GetExpenseGroupByUserId_ReturnsNotFound_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetExpenseGroupByUserId(userId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetExpenseGroupByUserId(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddMembers_ReturnsOkResult_WithMembers()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var userIds = new List<Guid> { Guid.NewGuid() };
            var users = new List<Users> { new Users { Id = Guid.NewGuid(), Name = "User1",Email="user@mail.com" } };
            _mockRepo.Setup(repo => repo.AddMembers(groupId, userIds)).ReturnsAsync(users);

            // Act
            var result = await _controller.AddMembers(groupId, userIds);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Users>>(okResult.Value);
            Assert.Equal(users, returnValue);
        }

        [Fact]
        public async Task AddMembers_ReturnsNotFound_WhenExceptionIsThrown()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var userIds = new List<Guid> { Guid.NewGuid() };
            _mockRepo.Setup(repo => repo.AddMembers(groupId, userIds)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddMembers(groupId, userIds);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetNonMembers_ReturnsOkResult_WithNonMembers()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var users = new List<Users> { new Users { Id = Guid.NewGuid(), Name = "User1", Email="user@mail.com"} };
            _mockRepo.Setup(repo => repo.GetNonMemberByGroupId(groupId)).ReturnsAsync(users);

            // Act
            var result = await _controller.GetNonMembers(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Users>>(okResult.Value);
            Assert.Equal(users, returnValue);
        }

        [Fact]
        public async Task GetNonMembers_ReturnsNotFound_WhenExceptionIsThrown()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetNonMemberByGroupId(groupId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetNonMembers(groupId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
