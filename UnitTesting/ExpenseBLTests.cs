using AutoMapper;
using Business_Layer.BusinessLayerRepository;
using Moq;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;

namespace UnitTesting
{
    public class ExpenseBLTests
    {
        private readonly Mock<IExpenseRepo> _expenseRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ExpenseRepoBL _expenseRepoBL;

        public ExpenseBLTests()
        {
            _expenseRepoMock = new Mock<IExpenseRepo>();
            _mapperMock = new Mock<IMapper>();
            _expenseRepoBL = new ExpenseRepoBL(_expenseRepoMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task AddExpenseWithMembers_ShouldReturnAddedExpense()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var incomingExpense = new IncomingExpense
            {
                Description = "Test Expense",
                Amount = 100.0m,
                Payers = new List<Guid>()
            };
            var mappedExpense = new Expenses
            {
                Id = Guid.NewGuid(),
                Description = incomingExpense.Description,
                Amount = incomingExpense.Amount
            };
            var addedExpense = new Expenses
            {
                Id = Guid.NewGuid(),
                Description = "Test Expense",
                Amount = 100.0m
            };

            _mapperMock.Setup(m => m.Map<Expenses>(incomingExpense)).Returns(mappedExpense);
            _expenseRepoMock.Setup(r => r.AddExpenseWithMembers(userId, groupId, mappedExpense, incomingExpense.Payers))
                .ReturnsAsync(addedExpense);

            // Act
            var result = await _expenseRepoBL.AddExpenseWithMembers(userId, groupId, incomingExpense);

            // Assert
            Assert.Equal(addedExpense, result);
            _mapperMock.Verify(m => m.Map<Expenses>(incomingExpense), Times.Once);
            _expenseRepoMock.Verify(r => r.AddExpenseWithMembers(userId, groupId, mappedExpense, incomingExpense.Payers), Times.Once);
        }

        [Fact]
        public async Task DeleteExpense_ShouldReturnTrue()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            _expenseRepoMock.Setup(r => r.DeleteExpense(expenseId)).ReturnsAsync(true);

            // Act
            var result = await _expenseRepoBL.DeleteExpense(expenseId);

            // Assert
            Assert.True(result);
            _expenseRepoMock.Verify(r => r.DeleteExpense(expenseId), Times.Once);
        }

        [Fact]
        public async Task GetCreatedExpense_ShouldReturnListOfExpenses()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenses = new List<Expenses>
            {
                new Expenses { Id = Guid.NewGuid(), Description = "Expense 1", Amount = 50.0m },
                new Expenses { Id = Guid.NewGuid(), Description = "Expense 2", Amount = 75.0m }
            };
            _expenseRepoMock.Setup(r => r.GetCreatedExpense(userId)).ReturnsAsync(expenses);

            // Act
            var result = await _expenseRepoBL.GetCreatedExpense(userId);

            // Assert
            Assert.Equal(expenses, result);
            _expenseRepoMock.Verify(r => r.GetCreatedExpense(userId), Times.Once);
        }

        [Fact]
        public async Task GetExpenseByGroupuserId_ShouldReturnListOfExpenses()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var expenses = new List<Expenses>
            {
                new Expenses { Id = Guid.NewGuid(), Description = "Expense 1", Amount = 50.0m },
                new Expenses { Id = Guid.NewGuid(), Description = "Expense 2", Amount = 75.0m }
            };
            _expenseRepoMock.Setup(r => r.GetExpenseByGroupuserId(userId, groupId)).ReturnsAsync(expenses);

            // Act
            var result = await _expenseRepoBL.GetExpenseByGroupuserId(userId, groupId);

            // Assert
            Assert.Equal(expenses, result);
            _expenseRepoMock.Verify(r => r.GetExpenseByGroupuserId(userId, groupId), Times.Once);
        }

        [Fact]
        public async Task GetExpenseById_ShouldReturnExpense()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            var expense = new Expenses { Id = expenseId, Description = "Expense", Amount = 50.0m };
            _expenseRepoMock.Setup(r => r.GetExpenseById(expenseId)).ReturnsAsync(expense);

            // Act
            var result = await _expenseRepoBL.GetExpenseById(expenseId);

            // Assert
            Assert.Equal(expense, result);
            _expenseRepoMock.Verify(r => r.GetExpenseById(expenseId), Times.Once);
        }

        [Fact]
        public async Task GetExpenses_ShouldReturnListOfExpenses()
        {
            // Arrange
            var expenses = new List<Expenses>
            {
                new Expenses { Id = Guid.NewGuid(), Description = "Expense 1", Amount = 50.0m },
                new Expenses { Id = Guid.NewGuid(), Description = "Expense 2", Amount = 75.0m }
            };
            _expenseRepoMock.Setup(r => r.GetExpenses()).ReturnsAsync(expenses);

            // Act
            var result = await _expenseRepoBL.GetExpenses();

            // Assert
            Assert.Equal(expenses, result);
            _expenseRepoMock.Verify(r => r.GetExpenses(), Times.Once);
        }

        [Fact]
        public async Task GetPendingExpense_ShouldReturnListOfPendingExpenses()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenses = new List<ExpenseShareDTO>
            {
                new ExpenseShareDTO { ExpenseId = Guid.NewGuid(), Amount = 50.0m },
                new ExpenseShareDTO { ExpenseId = Guid.NewGuid(), Amount = 75.0m }
            };
            _expenseRepoMock.Setup(r => r.GetPendingExpense(userId)).ReturnsAsync(expenses);

            // Act
            var result = await _expenseRepoBL.GetPendingExpense(userId);

            // Assert
            Assert.Equal(expenses, result);
            _expenseRepoMock.Verify(r => r.GetPendingExpense(userId), Times.Once);
        }

        [Fact]
        public async Task GetUsersByExpenseId_ShouldReturnListOfUsers()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            var users = new List<Users>
            {
                new Users { Id = Guid.NewGuid(), Name = "User 1", Email = "user1@example.com" },
                new Users { Id = Guid.NewGuid(), Name = "User 2", Email = "user2@example.com" }
            };
            _expenseRepoMock.Setup(r => r.GetUsersByExpenseId(expenseId)).ReturnsAsync(users);

            // Act
            var result = await _expenseRepoBL.GetUsersByExpenseId(expenseId);

            // Assert
            Assert.Equal(users, result);
            _expenseRepoMock.Verify(r => r.GetUsersByExpenseId(expenseId), Times.Once);
        }

        [Fact]
        public async Task SettleExpense_ShouldReturnTrue()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            _expenseRepoMock.Setup(r => r.SettleExpense(expenseId)).ReturnsAsync(true);

            // Act
            var result = await _expenseRepoBL.SettleExpense(expenseId);

            // Assert
            Assert.True(result);
            _expenseRepoMock.Verify(r => r.SettleExpense(expenseId), Times.Once);
        }

        [Fact]
        public async Task UpdateExpense_ShouldReturnUpdatedExpense()
        {
            // Arrange
            var expenseId = Guid.NewGuid();
            var incomingExpense = new IncomingExpense { Description = "Updated Expense", Amount = 100.0m };
            var mappedExpense = new Expenses { Id = expenseId, Description = incomingExpense.Description, Amount = incomingExpense.Amount };
            var updatedExpense = new Expenses { Id = expenseId, Description = "Updated Expense", Amount = 100.0m };

            _mapperMock.Setup(m => m.Map<Expenses>(incomingExpense)).Returns(mappedExpense);
            _expenseRepoMock.Setup(r => r.UpdateExpense(expenseId, mappedExpense)).ReturnsAsync(updatedExpense);

            // Act
            var result = await _expenseRepoBL.UpdateExpense(expenseId, incomingExpense);

            // Assert
            Assert.Equal(updatedExpense, result);
            _mapperMock.Verify(m => m.Map<Expenses>(incomingExpense), Times.Once);
            _expenseRepoMock.Verify(r => r.UpdateExpense(expenseId, mappedExpense), Times.Once);
        }
    }
}
