using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetById;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using Xunit;

namespace RestoHub.Api.Modules.UsersModule.Tests.Application.Mediators.UsersOperations.GetById
{
    public class GetUsersByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ValidInput_ReturnsUsers()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var getUsersByIdHandler = new GetUsersByIdHandler(TemplateServiceMock.Object);
            var expectedUsers = new Users
            {
                Id = 1,
                Name = "Test Users",
                Description = "Test Description"
            };

            TemplateServiceMock.Setup(service => service.GetUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedUsers);

            var request = new GetUsersByIdRequest(expectedUsers.Id);

            // Act
            var result = await getUsersByIdHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeTrue();
            result.Invalid.Should().BeFalse();
            result.Notifications.Count.Should().Be(0);
            result.Data.Id.Should().Be(expectedUsers.Id);
            result.Data.Name.Should().Be(expectedUsers.Name);
            result.Data.Description.Should().Be(expectedUsers.Description);

            // Verify
            TemplateServiceMock.Verify(service => service.GetUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UsersGetByIdFails_ReturnsNotFoundErrorCode()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var getUsersByIdHandler = new GetUsersByIdHandler(TemplateServiceMock.Object);

            TemplateServiceMock.Setup(service => service.GetUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Users)null);

            var request = new GetUsersByIdRequest(1);

            // Act
            var result = await getUsersByIdHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.NotFound);

            // Verify
            TemplateServiceMock.Verify(service => service.GetUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UsersGetByIdFails_ReturnsInternalServerErrorErrorCode()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var getUsersByIdHandler = new GetUsersByIdHandler(TemplateServiceMock.Object);

            TemplateServiceMock.Setup(service => service.GetUsersByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var request = new GetUsersByIdRequest(1);

            // Act
            var result = await getUsersByIdHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.InternalServerError);

            // Verify
            TemplateServiceMock.Verify(service => service.GetUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetUsersById_InvalidGetUsersByIdRequestsToTest))]
        public async Task Handle_NullOrZeroInput_ReturnsBadRequestErrorCode(GetUsersByIdRequest request)
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var getUsersByIdHandler = new GetUsersByIdHandler(TemplateServiceMock.Object);

            // Act
            var result = await getUsersByIdHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.BadRequest);

            // Verify
            TemplateServiceMock.Verify(service => service.GetUsersByIdAsync(It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> GetUsersById_InvalidGetUsersByIdRequestsToTest =>
           new List<object[]>
           {
                new object[] { null },
                new object[] { new GetUsersByIdRequest(0) }
           };
    }
}
