using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Create;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using Xunit;

namespace RestoHub.Api.Modules.UsersModule.Tests.Application.Mediators.UsersOperations.Create
{
    public class CreateUsersHandlerTests
    {
        [Fact]
        public async Task Handle_ValidInput_ReturnsCreatedUsers()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var createUsersRequestHandler = new CreateUsersHandler(TemplateServiceMock.Object);
            var expectedCreatedUsersId = 1;
            TemplateServiceMock.Setup(service => service.CreateUsersAsync(It.IsAny<Users>()))
                .ReturnsAsync(expectedCreatedUsersId);

            var request = new CreateUsersRequest("Test Users", "Test Description");

            // Act
            var result = await createUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeTrue();
            result.Invalid.Should().BeFalse();
            result.Notifications.Count.Should().Be(0);
            result.Data.Id.Should().Be(expectedCreatedUsersId);

            // Verify
            TemplateServiceMock.Verify(service => service.CreateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UsersCreationFails_ReturnsUnprocessableEntityErrorCode()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var createUsersRequestHandler = new CreateUsersHandler(TemplateServiceMock.Object);

            TemplateServiceMock.Setup(service => service.CreateUsersAsync(It.IsAny<Users>()))
                .ReturnsAsync(0);

            var request = new CreateUsersRequest("Test Users", "Test Description");

            // Act
            var result = await createUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.UnprocessableEntity);

            // Verify
            TemplateServiceMock.Verify(service => service.CreateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UsersCreationFails_ReturnsInternalServerErrorErrorCode()
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var createUsersRequestHandler = new CreateUsersHandler(TemplateServiceMock.Object);

            TemplateServiceMock.Setup(service => service.CreateUsersAsync(It.IsAny<Users>()))
                .ThrowsAsync(new Exception());

            var request = new CreateUsersRequest("Test Users", "Test Description");

            // Act
            var result = await createUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.InternalServerError);

            // Verify
            TemplateServiceMock.Verify(service => service.CreateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateUsers_InvalidCreateUsersRequestsToTest))]
        public async Task Handle_NullOrEmptyInput_ReturnsBadRequestErrorCode(CreateUsersRequest request)
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var createUsersRequestHandler = new CreateUsersHandler(TemplateServiceMock.Object);

            // Act
            var result = await createUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.BadRequest);

            // Verify
            TemplateServiceMock.Verify(service => service.CreateUsersAsync(It.IsAny<Users>()), Times.Never);
        }

        public static IEnumerable<object[]> CreateUsers_InvalidCreateUsersRequestsToTest =>
           new List<object[]>
           {
                new object[] { null },
                new object[] { new CreateUsersRequest(null, "Test Description") },
                new object[] { new CreateUsersRequest("Test Users", null) },
                new object[] { new CreateUsersRequest(string.Empty, "Test Description") },
                new object[] { new CreateUsersRequest("Test Users", string.Empty) },
           };
    }
}
