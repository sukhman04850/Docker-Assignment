using AutoMapper;
using Business_Layer.BusinessLayerRepository;
using Moq;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class ExpenseGroupRepoBLTests
    {
        private readonly Mock<IExpenseGroupRepo> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ExpenseGroupRepoBL _expenseGroupRepoBL;

        public ExpenseGroupRepoBLTests()
        {
            _repoMock = new Mock<IExpenseGroupRepo>();
            _mapperMock = new Mock<IMapper>();
            _expenseGroupRepoBL = new ExpenseGroupRepoBL(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddMembers_ShouldReturnMembers()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var userIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var expectedMembers = new List<Users>
        {
            new Users { Name = "User1" },
            new Users { Name = "User2" }
        };
            _repoMock.Setup(repo => repo.AddMembers(groupId, userIds)).ReturnsAsync(expectedMembers);

            // Act
            var result = await _expenseGroupRepoBL.AddMembers(groupId, userIds);

            // Assert
            Assert.Equal(expectedMembers.Count, result.Count);
            Assert.Equal(expectedMembers[0].Name, result[0].Name);
            Assert.Equal(expectedMembers[1].Name, result[1].Name);
        }

        [Fact]
        public async Task CreateExpenseGroup_ShouldReturnGroup()
        {
            // Arrange
            var id = Guid.NewGuid();
            var incomingGroup = new IncomingExpenseGroupDTO
            {
                Name = "Group1",
                Description = "Group Description",

            };
            var expenseGroup = new ExpenseGroup
            {
                GroupId = Guid.NewGuid(),
                Name = "Group1",
                Description = "Group Description",
                CreatedDate = DateTime.Now,
                Users = new List<Users>
            {
                new Users { Name = "User1" },
                new Users { Name = "User2" }
            }
            };
            var expectedGroup = new ExpenseGroup
            {
                GroupId = expenseGroup.GroupId,
                Name = "Group1",
                Description = "Group Description",
                CreatedDate = expenseGroup.CreatedDate,
                Users = expenseGroup.Users
            };
            _mapperMock.Setup(mapper => mapper.Map<ExpenseGroup>(incomingGroup)).Returns(expenseGroup);
            _repoMock.Setup(repo => repo.CreateExpenseGroup(id, expenseGroup)).ReturnsAsync(expectedGroup);

            // Act
            var result = await _expenseGroupRepoBL.CreateExpenseGroup(id, incomingGroup);

            // Assert
            Assert.Equal(expectedGroup.GroupId, result.GroupId);
            Assert.Equal(expectedGroup.Name, result.Name);
            Assert.Equal(expectedGroup.Description, result.Description);
            Assert.Equal(expectedGroup.CreatedDate, result.CreatedDate);
            Assert.Equal(expectedGroup.Users.Count, result.Users.Count);
            Assert.Equal(expectedGroup.Users[0].Name, result.Users[0].Name);
            Assert.Equal(expectedGroup.Users[1].Name, result.Users[1].Name);
        }

        [Fact]
        public async Task DeleteExpenseGroup_ShouldReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(repo => repo.DeleteExpenseGroup(id)).ReturnsAsync(true);

            // Act
            var result = await _expenseGroupRepoBL.DeleteExpenseGroup(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllExpenseGroups_ShouldReturnGroups()
        {
            // Arrange
            var expectedGroups = new List<ExpenseGroup>
        {
            new ExpenseGroup { Name = "Group1" },
            new ExpenseGroup { Name = "Group2" }
        };
            _repoMock.Setup(repo => repo.GetAllExpenseGroups()).ReturnsAsync(expectedGroups);

            // Act
            var result = await _expenseGroupRepoBL.GetAllExpenseGroups();

            // Assert
            Assert.Equal(expectedGroups.Count(), result.Count());
            Assert.Equal(expectedGroups.First().Name, result.First().Name);
            Assert.Equal(expectedGroups.Last().Name, result.Last().Name);
        }

        [Fact]
        public async Task GetExpenseGroupById_ShouldReturnGroup()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedGroup = new ExpenseGroup { Name = "Group1" };
            _repoMock.Setup(repo => repo.GetExpenseGroupById(id)).ReturnsAsync(expectedGroup);

            // Act
            var result = await _expenseGroupRepoBL.GetExpenseGroupById(id);

            // Assert
            Assert.Equal(expectedGroup.Name, result.Name);
        }

        [Fact]
        public async Task GetExpenseGroupByUserId_ShouldReturnGroupDTOs()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenseGroups = new List<ExpenseGroup>
        {
            new ExpenseGroup { Name = "Group1" },
            new ExpenseGroup { Name = "Group2" }
        };
            var expectedGroupDTOs = new List<ExpenseGroupDTO>
        {
            new ExpenseGroupDTO { Name = "Group1" },
            new ExpenseGroupDTO { Name = "Group2" }
        };
            _repoMock.Setup(repo => repo.GetExpenseGroupByUserId(userId)).ReturnsAsync(expenseGroups);
            _mapperMock.Setup(mapper => mapper.Map<List<ExpenseGroupDTO>>(expenseGroups)).Returns(expectedGroupDTOs);

            // Act
            var result = await _expenseGroupRepoBL.GetExpenseGroupByUserId(userId);

            // Assert
            Assert.Equal(expectedGroupDTOs.Count, result.Count);
            Assert.Equal(expectedGroupDTOs[0].Name, result[0].Name);
            Assert.Equal(expectedGroupDTOs[1].Name, result[1].Name);
        }

        [Fact]
        public async Task GetNonMemberByGroupId_ShouldReturnNonMembers()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var expectedNonMembers = new List<Users>
        {
            new Users { Name = "User3" },
            new Users { Name = "User4" }
        };
            _repoMock.Setup(repo => repo.GetNonMemberByGroupId(groupId)).ReturnsAsync(expectedNonMembers);

            // Act
            var result = await _expenseGroupRepoBL.GetNonMemberByGroupId(groupId);

            // Assert
            Assert.Equal(expectedNonMembers.Count, result.Count);
            Assert.Equal(expectedNonMembers[0].Name, result[0].Name);
            Assert.Equal(expectedNonMembers[1].Name, result[1].Name);
        }

        [Fact]
        public async Task GetUsersByGroupId_ShouldReturnUsers()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var expectedUsers = new List<Users>
        {
            new Users { Name = "User1" },
            new Users { Name = "User2" }
        };
            _repoMock.Setup(repo => repo.GetUsersByGroupId(groupId)).ReturnsAsync(expectedUsers);

            // Act
            var result = await _expenseGroupRepoBL.GetUsersByGroupId(groupId);

            // Assert
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.Equal(expectedUsers[0].Name, result[0].Name);
            Assert.Equal(expectedUsers[1].Name, result[1].Name);
        }

        [Fact]
        public async Task UpdateExpenseGroup_ShouldReturnUpdatedGroup()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expenseGroup = new ExpenseGroup
            {
                GroupId = Guid.NewGuid(),
                Name = "Group1",
                Description = "Updated Description",
                CreatedDate = DateTime.Now,
                Users = new List<Users>
            {
                new Users { Name = "User1" },
                new Users { Name = "User2" }
            }
            };
            var expectedGroup = new ExpenseGroup
            {
                GroupId = expenseGroup.GroupId,
                Name = "Group1",
                Description = "Updated Description",
                CreatedDate = expenseGroup.CreatedDate,
                Users = expenseGroup.Users
            };
            _repoMock.Setup(repo => repo.UpdateExpenseGroup(id, expenseGroup)).ReturnsAsync(expectedGroup);

            // Act
            var result = await _expenseGroupRepoBL.UpdateExpenseGroup(id, expenseGroup);

            // Assert
            Assert.Equal(expectedGroup.GroupId, result.GroupId);
            Assert.Equal(expectedGroup.Name, result.Name);
            Assert.Equal(expectedGroup.Description, result.Description);
            Assert.Equal(expectedGroup.CreatedDate, result.CreatedDate);
            Assert.Equal(expectedGroup.Users.Count, result.Users.Count);
            Assert.Equal(expectedGroup.Users[0].Name, result.Users[0].Name);
            Assert.Equal(expectedGroup.Users[1].Name, result.Users[1].Name);
        }
    }
}
