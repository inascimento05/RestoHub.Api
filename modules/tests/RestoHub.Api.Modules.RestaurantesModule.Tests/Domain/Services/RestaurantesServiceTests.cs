using FluentAssertions;
using Moq;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Entities;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Services;
using System.Runtime.ConstrainedExecution;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestoHub.Api.Modules.RestaurantesModule.Tests.Domain.Services
{
    public class RestaurantesServiceTests
    {
        private readonly Mock<IRestaurantesRepository> _restaurantesRepositoryMock;

        private RestaurantesService _underTest;

        public RestaurantesServiceTests()
        {
            _restaurantesRepositoryMock = new Mock<IRestaurantesRepository>();

            _underTest = new RestaurantesService(_restaurantesRepositoryMock.Object);
        }
        #region Create
        [Fact]
        public async Task CreateRestauranteAsync_ValidaRestaurante_ShouldCreateRestaurante()
        {
            // Arrange
            var expectedRestaurante = new Restaurante
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

            _restaurantesRepositoryMock.Setup(repo => repo.CreateRestauranteAsync(It.IsAny<Restaurante>()))
                .ReturnsAsync(expectedRestaurante);

            // Act
            var restaurante = await _underTest.CreateRestauranteAsync((RestauranteDto)expectedRestaurante);

            // Assert
            restaurante.ID.Should().Be(expectedRestaurante.ID);

            // Verify
            _restaurantesRepositoryMock.Verify(repo => repo.CreateRestauranteAsync(It.IsAny<Restaurante>()), Times.Once);
        }

        [Fact]
        public async Task CreateRestauranteAsync_NullInput_ThrowsException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _underTest.CreateRestauranteAsync((RestauranteDto)null));

            // Verify
            _restaurantesRepositoryMock.Verify(repo => repo.CreateRestauranteAsync(It.IsAny<Restaurante>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(CreateRestaurante_InvalidInputsToTest))]
        public async Task CreateRestaurante_InvalidInput_ThrowsException(RestauranteDto restaurante)
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _underTest.CreateRestauranteAsync(restaurante));

            // Verify
            _restaurantesRepositoryMock.Verify(repo => repo.CreateRestauranteAsync(It.IsAny<Restaurante>()), Times.Never);
        }

        public static IEnumerable<object[]> CreateRestaurante_InvalidInputsToTest =>
            new List<object[]>
            {
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = string.Empty,
                    NomeFantasia = "Test",
                    CNPJ = "12345678932654",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    NomeFantasia = "Test",
                    CNPJ = "12345678932654",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = string.Empty,
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = string.Empty,
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "123456789326",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "123456789326555",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = string.Empty,
                    DDD = "84",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = string.Empty,
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "8",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "843",
                    Telefone = "996144492",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "99614449",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "9961444955",
                    Logradouro = "Test",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = string.Empty,
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    Numero = "35A",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = string.Empty,
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "548a5s4q8a5s4q5a2s36a",
                    Bairro = "Nova Descoberta",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = string.Empty,
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    Cidade = "Natal",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = string.Empty,
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = "Test",
                    Estado = "RN",
                    CEP = string.Empty,
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = "Test",
                    Estado = "RN",
                    CEP = "845632147",
                    Complemento = "Test",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = "Test",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    NomeUsuario = "Test",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = "Test",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = string.Empty,
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } },
                new object[]{ new RestauranteDto
                {
                    ID = Guid.NewGuid(),
                    RazaoSocial = "Test",
                    NomeFantasia = "Test",
                    CNPJ = "1234567893265",
                    Email = "email@email.com",
                    DDD = "84",
                    Telefone = "996144495",
                    Logradouro = "test",
                    Numero = "35A",
                    Bairro = "Test",
                    Cidade = "Test",
                    Estado = "RN",
                    CEP = "59056000",
                    Complemento = "Test",
                    NomeUsuario = "No crepúsculo da noite, os suspiros do vento sussurram segredos ancestrais enquanto as estrelas observam em silêncio. " +
                    "Nas vielas estreitas da cidade, as sombras dançam ao som dos passos solitários, tecendo histórias perdidas no labirinto do tempo tempo tempo.",
                    Senha = "Test",
                    QRCode = "Test",
                    IsMatriz = true,
                    AdicionadoDataHora = DateTime.Now
                } }
            };
        #endregion
    }
}
