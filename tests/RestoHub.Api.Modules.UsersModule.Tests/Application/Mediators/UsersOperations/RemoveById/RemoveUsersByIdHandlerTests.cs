using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.RemoveById;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using Xunit;

namespace RestoHub.Api.Modules.UsersModule.Tests.Application.Mediators.UsersOperations.RemoveById
{
    public class RemoveUsersByIdHandlerTests
    {

        [Theory]
        [MemberData(nameof(RemoveUsersById_ValidRemoveUsersByIdResultsToTest))]
        public async Task Handle_ValidInput_ReturnsDeletedUsers(bool expected)
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.RemoveUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expected);

            var deleteUsersHandler = new RemoveUsersByIdHandler(TemplateServiceMock.Object);
            var request = new RemoveUsersByIdRequest(1);

            // Act
            var result = await deleteUsersHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeTrue();
            result.Invalid.Should().BeFalse();
            result.Notifications.Count.Should().Be(0);
            result.Data.Should().Be(expected);

            // Verify
            TemplateServiceMock.Verify(service => service.RemoveUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }
        public static IEnumerable<object[]> RemoveUsersById_ValidRemoveUsersByIdResultsToTest =>
           new List<object[]>
           {
                new object[] { true },
                new object[] { false }
           };

        [Fact]
        public async Task Handle_RemoveUsersByIdFails_ReturnsNotFoundErrorCode()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.RemoveUsersByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((bool?)null);

            var deleteUsersHandler = new RemoveUsersByIdHandler(TemplateServiceMock.Object);
            var request = new RemoveUsersByIdRequest(1);

            // Act
            var result = await deleteUsersHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.NotFound);

            // Verify
            TemplateServiceMock.Verify(service => service.RemoveUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_RemoveUsersByIdFails_ReturnsInternalServerErrorErrorCode()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.RemoveUsersByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var deleteUsersHandler = new RemoveUsersByIdHandler(TemplateServiceMock.Object);
            var request = new RemoveUsersByIdRequest(1);

            // Act
            var result = await deleteUsersHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.InternalServerError);

            // Verify
            TemplateServiceMock.Verify(service => service.RemoveUsersByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RemoveUsersById_InvalidRemoveUsersByIdRequestsToTest))]
        public async Task Handle_NullOrZeroInput_ReturnsBadRequestErrorCode(RemoveUsersByIdRequest request)
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var deleteUsersHandler = new RemoveUsersByIdHandler(TemplateServiceMock.Object);

            // Act
            var result = await deleteUsersHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.BadRequest);

            // Verify
            TemplateServiceMock.Verify(service => service.RemoveUsersByIdAsync(It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> RemoveUsersById_InvalidRemoveUsersByIdRequestsToTest =>
           new List<object[]>
           {
                new object[] { null },
                new object[] { new RemoveUsersByIdRequest(0) }
           };
    }
}
