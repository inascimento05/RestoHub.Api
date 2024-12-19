using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Update;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using Xunit;

namespace RestoHub.Api.Modules.UsersModule.Tests.Application.Mediators.UsersOperations.Udpate
{
    public class UpdateUsersHandlerTests
    {
        [Fact]
        public async Task Handle_ValidInput_ReturnUpdatedUsers()
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

            var expectedUpdatedUsers = new Users
            {
                Id = TemplateFromDatabase.Id,
                Name = TemplateFromDatabase.Name,
                Description = TemplateToUpdate.Description
            };

            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.UpdateUsersAsync(It.IsAny<Users>()))
                .ReturnsAsync(expectedUpdatedUsers);

            var updateUsersRequestHandler = new UpdateUsersHandler(TemplateServiceMock.Object);
            var request = new UpdateUsersRequest(TemplateToUpdate.Id, TemplateToUpdate.Name, TemplateToUpdate.Description);

            // Act
            var result = await updateUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeTrue();
            result.Invalid.Should().BeFalse();
            result.Notifications.Count.Should().Be(0);
            result.Data.Id.Should().Be(expectedUpdatedUsers.Id);
            result.Data.Name.Should().Be(expectedUpdatedUsers.Name);
            result.Data.Description.Should().Be(expectedUpdatedUsers.Description);

            // Verify
            TemplateServiceMock.Verify(service => service.UpdateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UsersUpdateFails_ReturnsNotFoundErrorCode()
        {
            // Arrange
            var TemplateToUpdate = new Users
            {
                Id = 1,
                Description = "Updated Test Description"
            };

            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.UpdateUsersAsync(It.IsAny<Users>()))
                .ReturnsAsync((Users)null);

            var updateUsersRequestHandler = new UpdateUsersHandler(TemplateServiceMock.Object);
            var request = new UpdateUsersRequest(TemplateToUpdate.Id, TemplateToUpdate.Name, TemplateToUpdate.Description);

            // Act
            var result = await updateUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.NotFound);

            // Verify
            TemplateServiceMock.Verify(service => service.UpdateUsersAsync(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UsersUpdateFails_ReturnsInternalServerErrorErrorCode()
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

            var TemplateServiceMock = new Mock<IUsersService>();
            TemplateServiceMock.Setup(service => service.UpdateUsersAsync(It.IsAny<Users>()))
                .Throws(new Exception());

            var updateUsersRequestHandler = new UpdateUsersHandler(TemplateServiceMock.Object);
            var request = new UpdateUsersRequest(TemplateToUpdate.Id, TemplateToUpdate.Name, TemplateToUpdate.Description);

            // Act
            var result = await updateUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.InternalServerError);

            // Verify
            TemplateServiceMock.Verify(service => service.UpdateUsersAsync(It.IsAny<Users>()), Times.Once);
        }


        [Theory]
        [MemberData(nameof(UpdateUsers_InvalidUpdateUsersRequestsToTest))]
        public async Task Handle_UsersUpdateFails_ReturnsBadRequestErrorCode(UpdateUsersRequest request)
        {
            // Arrange
            var TemplateServiceMock = new Mock<IUsersService>();
            var updateUsersRequestHandler = new UpdateUsersHandler(TemplateServiceMock.Object);

            // Act
            var result = await updateUsersRequestHandler.Handle(request, default);

            // Assert
            result.Valid.Should().BeFalse();
            result.Invalid.Should().BeTrue();
            result.Notifications.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Error.Should().Be(ErrorCode.BadRequest);

            // Verify
            TemplateServiceMock.Verify(service => service.UpdateUsersAsync(It.IsAny<Users>()), Times.Never);
        }
        public static IEnumerable<object[]> UpdateUsers_InvalidUpdateUsersRequestsToTest =>
           new List<object[]>
           {
                new object[] { null },
                new object[] { new UpdateUsersRequest(0, string.Empty, string.Empty) }
           };
    }
}
