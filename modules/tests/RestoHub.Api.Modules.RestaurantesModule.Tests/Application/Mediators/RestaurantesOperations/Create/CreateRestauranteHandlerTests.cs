using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Create;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;
using Xunit;

namespace RestoHub.Api.Modules.RestaurantesModule.Tests.Application.Mediators.RestaurantesOperations.Create
{
    public class CreateRestauranteHandlerTests
    {
        private readonly Mock<IRestaurantesService> _restaurantesServiceMock;
        private CreateRestauranteHandler _underTest;

        public CreateRestauranteHandlerTests()
        {
            _restaurantesServiceMock = new Mock<IRestaurantesService>();

            _underTest = new CreateRestauranteHandler(_restaurantesServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ValidaInput_ResturnsRestaurante()
        {
            // Arragne
            var inputDto = new CreateRestauranteDto
            {
                RazaoSocial = "Razão social test",
                NomeFantasia = "Nome Fantasia test",
                CNPJ = "12345699711265",
                Email = "email@email.com",
                DDD = "84",
                Telefone = "456974598",
                Logradouro = "Rua test",
                Numero = "35A",
                Bairro = "Bairro test",
                Cidade = "Natal",
                Estado = "RN",
                CEP = "55588847",
                Complemento = "Complemento test",
                NomeUsuario = "Nome Usuário test",
                Senha = "Senha test",
                QRCode = "QR Code test",
                IsMatriz = true
            };

            var expectedRestaurante = new RestauranteDto
            {
                ID = Guid.NewGuid(),
                RazaoSocial = "Razão social test",
                NomeFantasia = "Razão social test",
                CNPJ = "12345678900012",
                Email = "email@email.com",
                DDD = "84",
                Telefone = "999999999",
                Logradouro = "Rua test",
                Numero = "35A",
                Bairro = "Bairro test",
                Cidade = "Cidade test",
                Estado = "RN",
                CEP = "59056510",
                Complemento = "Complemento test",
                NomeUsuario = "Nome usuário teste",
                Senha = "Senha test",
                QRCode = "QR code test",
                IsMatriz = true,
                AdicionadoDataHora = DateTime.Now,
                ModificadoDataHora = null,
                ExcluidoDataHora = null
            };

            _restaurantesServiceMock.Setup(service => service.CreateRestauranteAsync(It.IsAny<RestauranteDto>()))
                .ReturnsAsync(expectedRestaurante);

            var request = new CreateRestauranteRequest(inputDto);

            // Act
            var result = await _underTest.Handle(request, default);

            // Assert
            result.Valid.Should().BeTrue();
            result.Invalid.Should().BeFalse();
            result.Notifications.Count.Should().Be(0);


            // Verify
            _restaurantesServiceMock.Verify(service => service.CreateRestauranteAsync(It.IsAny<RestauranteDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NullRequest_ShouldReturnBadRequest()
        {
            // Arrange
            _restaurantesServiceMock.Setup(service => service.CreateRestauranteAsync(It.IsAny<RestauranteDto>()))
                .ReturnsAsync((RestauranteDto)null);

            // Act

            // Assert
            // Verify
        }
    }
}
