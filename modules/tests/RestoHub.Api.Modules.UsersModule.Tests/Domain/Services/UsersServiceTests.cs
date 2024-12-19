using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using RestoHub.Api.Modules.UsersModule.Domain.Services;
using Xunit;

namespace RestoHub.Api.Modules.UsersModule.Tests.Domain.Services
{
    public class UsersServiceTests
    {
        #region Create
        [Fact]
        public async Task CreateUsersAsync_ValidUsers_CreatesUsers()
        {
            // Arrange
            var expectedUsersId = 1;
            var Template = new Users
            {
                Name = "Test Users",
                Description = "Test Description"
            };

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.CreateUsersAsync(It.IsAny<Users>()))
                .ReturnsAsync(expectedUsersId);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            Template.Id = await TemplateService.CreateUsersAsync(Template);

            // Assert
            Template.Id.Should().Be(expectedUsersId);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.CreateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task CreateUsersAsync_NullInput_ThrowsException()
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => TemplateService.CreateUsersAsync((Users)null));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.CreateUsersAsync(It.IsAny<Users>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(CreateUsers_InvalidInputsToTest))]
        public async Task CreateUsersAsync_InvalidInput_ThrowsException(Users Template)
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => TemplateService.CreateUsersAsync(Template));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.CreateUsersAsync(It.IsAny<Users>()), Times.Never);
        }

        public static IEnumerable<object[]> CreateUsers_InvalidInputsToTest =>
            new List<object[]>
                {
                    new object[] { new Users { Name = null, Description = "Test Description" } },
                    new object[] { new Users { Name = "Test Users", Description = null } },
                    new object[] { new Users { Name = string.Empty, Description = "Test Description" } },
                    new object[] { new Users { Name = "Test Users", Description = string.Empty } },
                    new object[] { new Users { Name = "Test Users".PadRight(101,'.'), Description = "Test Description" } },
                    new object[] { new Users { Name = "Test Users", Description = "Test Description".PadRight(201,'.') } },
                };
        #endregion

        #region Read
        [Fact]
        public async Task GetAllUserssAsync_ValidPagination_ReturnZeroUserss()
        {
            // Arrange
            var expectedUsers = new List<Users>();

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            var Templates = await TemplateService.GetAllUserssAsync(1, 10);

            // Assert
            Templates.Count().Should().Be(0);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllUserssAsync_ValidPagination_ReturnUserss()
        {
            // Arrange
            var expectedUsers = new List<Users> {
                new Users
                {
                    Id = 1,
                    Name = "Test Users",
                    Description = "Test Description"
                },
                new Users
                {
                    Id = 2,
                    Name = "Test Users 2",
                    Description = "Test Description 2"
                },
            };

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            var Templates = await TemplateService.GetAllUserssAsync(1, 10);

            // Assert
            Templates.Count().Should().BeGreaterThanOrEqualTo(1);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetAllUserssAsync_ValidPaginationToTest))]
        public async Task GetAllUserssAsync_ValidPaginationPagination_ReturnUserss(int page)
        {
            // Arrange
            var TemplatesFromDatabase = new List<Users> {
                new Users
                {
                    Id = 1,
                    Name = "Test Users",
                    Description = "Test Description"
                },
                new Users
                {
                    Id = 2,
                    Name = "Test Users 2",
                    Description = "Test Description 2"
                },
            };

            var returnedUserss = TemplatesFromDatabase.Skip(page - 1)
                .Take(1);

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(returnedUserss);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            var Templates = await TemplateService.GetAllUserssAsync(page, 1);

            // Assert
            Templates.Count().Should().Be(1);
            Templates.First().Id.Should().Be(TemplatesFromDatabase[(page - 1)].Id);
            Templates.First().Name.Should().Be(TemplatesFromDatabase[(page - 1)].Name);
            Templates.First().Description.Should().Be(TemplatesFromDatabase[(page - 1)].Description);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        public static IEnumerable<object[]> GetAllUserssAsync_ValidPaginationToTest =>
            new List<object[]>
                {
                    new object[] { 1 },
                    new object[] { 2 }
                };

        [Theory]
        [MemberData(nameof(GetAllUserssAsync_InvalidPaginationToTest))]
        public async Task GetAllUserssAsync_InvalidPaginationPagination_ThrowsException(int pageNumber, int pageSize)
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => TemplateService.GetAllUserssAsync(pageNumber, pageSize));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> GetAllUserssAsync_InvalidPaginationToTest =>
            new List<object[]>
                {
                    new object[] { null, 10 },
                    new object[] { 1, null },
                    new object[] { 0, 10 },
                    new object[] { 1, 0 }
                };

        [Fact]
        public async Task GetUsersByIdAsync_ValidId_GetUsers()
        {
            // Arrange
            var expectedUsers = new Users
            {
                Id = 1,
                Name = "Test Users",
                Description = "Test Description"
            };

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.GetUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            var Template = await TemplateService.GetUsersByIdAsync(expectedUsers.Id);

            // Assert
            Template.Id.Should().Be(expectedUsers.Id);
            Template.Name.Should().Be(expectedUsers.Name);
            Template.Description.Should().Be(expectedUsers.Description);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetUsersByIdAsync_InvalidInput_ThrowsException()
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => TemplateService.GetUsersByIdAsync(0));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetUsersByIdAsync(It.IsAny<int>()), Times.Never);
        }
        #endregion

        #region Update
        [Fact]
        public async Task UpdateUsersAsync_UpdateDescription_UpdatesUsers()
        {
            // Arrange
            var TemplateFromDatabase = new Users
            {
                Id = 1,
                Name = "Test Users",
                Description = "Test Description"
            };

            var TemplateToUpdate = new Users
            {
                Id = TemplateFromDatabase.Id,
                Description = "Updated Test Description"
            };

            var expectedUsers = new Users
            {
                Id = TemplateFromDatabase.Id,
                Name = TemplateFromDatabase.Name,
                Description = TemplateToUpdate.Description
            };

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.GetUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(TemplateFromDatabase);

            TemplateRepositoryMock.Setup(repo => repo.UpdateUsersAsync(It.IsAny<Users>()))
                .ReturnsAsync(expectedUsers);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            var updatedUsers = await TemplateService.UpdateUsersAsync(TemplateToUpdate);

            // Assert
            updatedUsers.Id.Should().Be(expectedUsers.Id);
            updatedUsers.Name.Should().Be(expectedUsers.Name);
            updatedUsers.Description.Should().Be(expectedUsers.Description);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetUsersByIdAsync(It.IsAny<int>()), Times.Once);
            TemplateRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUsersAsync_NullInput_ThrowsException()
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => TemplateService.UpdateUsersAsync((Users)null));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetUsersByIdAsync(It.IsAny<int>()), Times.Never);
            TemplateRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Users>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(UpdateUsers_InvalidInputsToTest))]
        public async Task UpdateUsersAsync_InvalidInput_ThrowsException(Users Template)
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => TemplateService.UpdateUsersAsync(Template));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.GetUsersByIdAsync(It.IsAny<int>()), Times.Never);
            TemplateRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Users>()), Times.Never);
        }

        public static IEnumerable<object[]> UpdateUsers_InvalidInputsToTest =>
            new List<object[]>
                {
                    new object[] { new Users { Name = "Test Users", Description = "Test Description" } },
                    new object[] { new Users { Id = 1, Name = "Test Users".PadRight(101,'.'), Description = "Test Description" } },
                    new object[] { new Users { Id = 1, Name = "Test Users", Description = "Test Description".PadRight(201,'.') } },
                };
        #endregion

        #region Delete
        [Fact]
        public async Task RemoveUsersByIdAsync_ValidUserss_RemovesUsers()
        {
            // Arrange
            var TemplateFromDatabase = new Users
            {
                Id = 1,
                Name = "Test Users",
                Description = "Test Description"
            };
            var expected = true;

            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            TemplateRepositoryMock.Setup(repo => repo.GetUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(TemplateFromDatabase);
            TemplateRepositoryMock.Setup(repo => repo.DeleteUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expected);

            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act
            var isDeleted = await TemplateService.RemoveUsersByIdAsync(1);

            // Assert
            isDeleted.Should().Be(expected);

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.DeleteUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task RemoveUsersByIdAsync_InvalidInput_ThrowsException()
        {
            // Arrange
            var TemplateRepositoryMock = new Mock<IUsersRepository>();
            var TemplateService = new UsersService(TemplateRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => TemplateService.RemoveUsersByIdAsync(0));

            // Verify
            TemplateRepositoryMock.Verify(repo => repo.DeleteUsersByIdAsync(It.IsAny<int>()), Times.Never);
        }
        #endregion
    }
}
