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
    public class UserRepoBLTests
    {

        private readonly Mock<IUserRepo> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserRepoBL _userRepoBL;

        public UserRepoBLTests()
        {
            _repoMock = new Mock<IUserRepo>();
            _mapperMock = new Mock<IMapper>();
            _userRepoBL = new UserRepoBL(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddUser_ShouldReturnNewUser()
        {
            // Arrange
            var incomingUser = new IncomingUser { Name = "User1", Email = "user1@example.com" };
            var user = new Users { Name = "User1", Email = "user1@example.com" };
            _mapperMock.Setup(mapper => mapper.Map<Users>(incomingUser)).Returns(user);
            _repoMock.Setup(repo => repo.AddUser(user)).ReturnsAsync(user);

            // Act
            var result = await _userRepoBL.AddUser(incomingUser);

            // Assert
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(repo => repo.DeleteUser(id)).ReturnsAsync(true);

            // Act
            var result = await _userRepoBL.DeleteUser(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new Users { Name = "User1", Email = "user1@example.com" };
            _repoMock.Setup(repo => repo.GetUserById(id)).ReturnsAsync(user);

            // Act
            var result = await _userRepoBL.GetUserById(id);

            // Assert
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public void Login_ShouldReturnUser()
        {
            // Arrange
            var login = new Login { Email = "user1@example.com", Password = "password" };
            var user = new Users { Name = "User1", Email = "user1@example.com" };
            _repoMock.Setup(repo => repo.Login(login)).Returns(user);

            // Act
            var result = _userRepoBL.Login(login);

            // Assert
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnUpdatedUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var incomingUser = new IncomingUser { Name = "UpdatedUser", Email = "updateduser@example.com" };
            var user = new Users { Name = "UpdatedUser", Email = "updateduser@example.com" };
            _mapperMock.Setup(mapper => mapper.Map<Users>(incomingUser)).Returns(user);
            _repoMock.Setup(repo => repo.UpdateUser(id, user)).ReturnsAsync(user);

            // Act
            var result = await _userRepoBL.UpdateUser(id, incomingUser);

            // Assert
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<Users>
        {
            new Users { Name = "User1", Email = "user1@example.com" },
            new Users { Name = "User2", Email = "user2@example.com" }
        };
            _repoMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

            // Act
            var result = await _userRepoBL.GetAllUsers();

            // Assert
            Assert.Equal(users.Count, result.Count);
            Assert.Equal(users[0].Name, result[0].Name);
            Assert.Equal(users[1].Name, result[1].Name);
        }
    }
}
