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
    public  class ExpenseControllerTests
    {
        private readonly Mock<IExpenseBL> _mockRepo;
        private readonly ExpenseController _controller;

        public ExpenseControllerTests()
        {
            _mockRepo = new Mock<IExpenseBL>();
            _controller = new ExpenseController(_mockRepo.Object);
        }
        [Fact]
        public async Task GetAllExpenses_ReturnsOkResult_WithExpenses()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var expenses = new List<Expenses> { new Expenses { Id = Guid.NewGuid(), Description = "Test Expense",Amount=100,Date=DateTime.Now} };
            _mockRepo.Setup(repo => repo.GetExpenseByGroupuserId(userId, groupId)).ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetAllExpenses(userId, groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnExpenses = Assert.IsType<List<Expenses>>(okResult.Value);
            Assert.Single(returnExpenses);
        }

        [Fact]
        public async Task GetAllExpenses_ReturnsBadRequest_OnException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetExpenseByGroupuserId(userId, groupId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllExpenses(userId, groupId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PendingExpense_ReturnsOkResult_WithPendingExpenses()
        {
            // Arrange
            var id = Guid.NewGuid();
            var pendingExpenses = new List<ExpenseShareDTO> { new ExpenseShareDTO { ExpenseId = Guid.NewGuid(), Amount = 100 } };
            _mockRepo.Setup(repo => repo.GetPendingExpense(id)).ReturnsAsync(pendingExpenses);

            // Act
            var result = await _controller.PendingExpense(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnExpenses = Assert.IsType<List<ExpenseShareDTO>>(okResult.Value);
            Assert.Single(returnExpenses);
        }

        [Fact]
        public async Task PendingExpense_ReturnsBadRequest_OnException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetPendingExpense(id)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.PendingExpense(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetExpenseById_ReturnsOkResult_WithExpense()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expense = new Expenses { Id = id, Description = "Test Expense" };
            _mockRepo.Setup(repo => repo.GetExpenseById(id)).ReturnsAsync(expense);

            // Act
            var result = await _controller.GetExpenseById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnExpense = Assert.IsType<Expenses>(okResult.Value);
            Assert.Equal(id, returnExpense.Id);
        }

        [Fact]
        public async Task GetExpenseById_ReturnsNotFound_OnException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetExpenseById(id)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetExpenseById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetMembersById_ReturnsOkResult_WithUsers()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            var users = new List<Users> { new Users { Id = Guid.NewGuid(), Name = "Test User" } };
            _mockRepo.Setup(repo => repo.GetUsersByExpenseId(expenseId)).ReturnsAsync(users);

            // Act
            var result = await _controller.GetMembersById(expenseId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUsers = Assert.IsType<List<Users>>(okResult.Value);
            Assert.Single(returnUsers);
        }

        [Fact]
        public async Task GetMembersById_ReturnsNotFound_OnException()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetUsersByExpenseId(expenseId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetMembersById(expenseId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddExpense_ReturnsOkResult_WithNewExpense()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var expense = new IncomingExpense { Description = "Test Expense",Amount=100 };
            var newExpense = new Expenses { Id = Guid.NewGuid(), Description = "Test Expense",Amount=100,Date = DateTime.Now };
            _mockRepo.Setup(repo => repo.AddExpenseWithMembers(userId, groupId, expense)).ReturnsAsync(newExpense);

            // Act
            var result = await _controller.AddExpense(userId, groupId, expense);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnExpense = Assert.IsType<Expenses>(okResult.Value);
            Assert.Equal("Test Expense", returnExpense.Description);
        }

        [Fact]
        public async Task AddExpense_ReturnsBadRequest_OnException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var expense = new IncomingExpense { Description = "Test Expense" };
            _mockRepo.Setup(repo => repo.AddExpenseWithMembers(userId, groupId, expense)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddExpense(userId, groupId, expense);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task SettleShare_ReturnsOkResult_WithSettledExpense()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.SettleExpense(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.SettleShare(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnResult = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnResult);
        }

        [Fact]
        public async Task SettleShare_ReturnsBadRequest_OnException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.SettleExpense(id)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.SettleShare(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteExpense_ReturnsOkResult_WithDeletionStatus()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteExpense(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteExpense(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnResult = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnResult);
        }

        [Fact]
        public async Task DeleteExpense_ReturnsBadRequest_OnException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteExpense(id)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.DeleteExpense(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
