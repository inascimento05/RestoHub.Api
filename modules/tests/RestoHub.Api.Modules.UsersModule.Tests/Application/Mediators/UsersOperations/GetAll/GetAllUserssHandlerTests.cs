using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetAll;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using Xunit;

namespace RestoHub.Api.Modules.UsersModule.Tests.Application.Mediators.UsersOperations.GetAll
{
    public class GetAllUserssHandlerTests
    {
        [Fact]
        public async Task Handle_ValidInput_ReturnsEmptyUserssList()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var getAllUsersHandler = new GetAllUserssHandler(TemplateServiceMock.Object);
            var expectedUsers = new List<Users>();

            TemplateServiceMock.Setup(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var request = new GetAllUserssRequest(1, 10);

            // Act
            var response = await getAllUsersHandler.Handle(request, default);

            // Assert
            response.Valid.Should().BeTrue();
            response.Invalid.Should().BeFalse();
            response.Notifications.Count.Should().Be(0);
            response.Data.Count().Should().Be(0);

            // Verify
            TemplateServiceMock.Verify(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidInput_ReturnsAllUserss()
        {
            // Arrange
            var expectedUsers = new List<Users> {
                new Users {
                    Id = 1,
                    Name = "Test Users",
                    Description = "Test Description"
                },
                new Users {
                    Id = 2,
                    Name = "Test Users 2",
                    Description = "Test Description 2"
                }
            };

            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var getAllUsersHandler = new GetAllUserssHandler(TemplateServiceMock.Object);
            var request = new GetAllUserssRequest(1, 10);

            // Act
            var response = await getAllUsersHandler.Handle(request, default);

            // Assert
            response.Valid.Should().BeTrue();
            response.Invalid.Should().BeFalse();
            response.Notifications.Count.Should().Be(0);
            response.Data.Count().Should().Be(2);

            // Verify
            TemplateServiceMock.Verify(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetAllUserssRequest_ValidPaginationInputsToTest))]
        public async Task Handle_ValidPaginationInput_ReturnsUserssPaginated(int page)
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
            var expectedUsers = TemplatesFromDatabase.Skip(page - 1)
                .Take(1);

            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var getAllUsersHandler = new GetAllUserssHandler(TemplateServiceMock.Object);
            var request = new GetAllUserssRequest(page, 1);

            // Act
            var response = await getAllUsersHandler.Handle(request, default);

            // Assert
            response.Valid.Should().BeTrue();
            response.Invalid.Should().BeFalse();
            response.Notifications.Count.Should().Be(0);
            response.Data.Count().Should().Be(1);
            response.Data.First().Id.Should().Be(TemplatesFromDatabase[(page - 1)].Id);
            response.Data.First().Name.Should().Be(TemplatesFromDatabase[(page - 1)].Name);
            response.Data.First().Description.Should().Be(TemplatesFromDatabase[(page - 1)].Description);

            // Verify
            TemplateServiceMock.Verify(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        public static IEnumerable<object[]> GetAllUserssRequest_ValidPaginationInputsToTest =>
        new List<object[]> {
            new object[] { 1 },
                new object[] { 2 }
            };

        [Theory]
        [MemberData(nameof(GetAllUserssRequest_ZeroInputsToTest))]
        public async Task Handle_ZeroInput_ReturnsUserssPaginated(GetAllUserssRequest request)
        {
            // Arrange
            var TemplatesFromDatabase = new List<Users>();
            for (int i = 0; i < request.PageSize; i++)
            {
                TemplatesFromDatabase.Add(
                    new Users
                    {
                        Id = i,
                        Name = $"Test Users {i}",
                        Description = $"Test Description {i}"
                    }
                );
            }

            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TemplatesFromDatabase);

            var getAllUsersHandler = new GetAllUserssHandler(TemplateServiceMock.Object);

            // Act
            var result = await getAllUsersHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeTrue();
            result.Invalid.Should().BeFalse();
            result.Notifications.Count.Should().Be(0);
            result.Data.Count().Should().BeLessThanOrEqualTo(request.PageSize);

            // Verify
            TemplateServiceMock.Verify(service => service.GetAllUserssAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
        public static IEnumerable<object[]> GetAllUserssRequest_ZeroInputsToTest =>
            new List<object[]> {
                new object[] { new GetAllUserssRequest(0, 10) },
                new object[] { new GetAllUserssRequest(1, 0) },
                new object[] { new GetAllUserssRequest(0, 0) }
            };
    }
}
