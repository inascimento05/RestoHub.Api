using FluentValidator;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RestoHub.Api.Controllers;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Create;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Presenters;
using System.Net;

namespace RestoHub.Api.Tests.Controllers
{
    public class RestaurantesControllerTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IPresenter _presenter;

        private RestaurantesController _underTest;

        public RestaurantesControllerTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _mediatorMock = new Mock<IMediator>();
            _presenter = new Presenter();

            var defaultPageSizeSection = new Mock<IConfigurationSection>();
            defaultPageSizeSection
                .Setup(s => s.Value)
                .Returns("20");
            _configurationMock.Setup(config => config.GetSection("DefaultPageSize"))
                .Returns(defaultPageSizeSection.Object);

            _underTest = new RestaurantesController(
                _configurationMock.Object,
                _mediatorMock.Object,
                _presenter);
        }

        #region Create
        [Fact]
        public async Task CreateRestaurante_ValidInput_ShouldReturnSuccess()
        {
            // Arrange
            var input = new CreateRestauranteDto
            {
                RazaoSocial = "Razão social test",
                NomeFantasia = "Nome fantasia test",
                CNPJ = "11144477788851",
                Email = "email@email.com",
                DDD = "84",
                Telefone = "999999999",
                Logradouro = "Rua test",
                Numero = "35A",
                Bairro = "Bairo test",
                Cidade = "Cidade test",
                Estado = "RN",
                CEP = "59056510",
                Complemento = "Complemento test",
                NomeUsuario = "Nome usuário test",
                Senha = "Senha test",
                QRCode = "QR Code test",
                IsMatriz = true
            };

            var expecteRestaurante = new RestauranteDto
            {
                ID = Guid.NewGuid(),
                RazaoSocial = "Razão social test",
                NomeFantasia = "Nome fantasia test",
                CNPJ = "11144477788851",
                Email = "email@email.com",
                DDD = "84",
                Telefone = "999999999",
                Logradouro = "Rua test",
                Numero = "35A",
                Bairro = "Bairo test",
                Cidade = "Cidade test",
                Estado = "RN",
                CEP = "59056510",
                Complemento = "Complemento test",
                NomeUsuario = "Nome usuário test",
                Senha = "Senha test",
                QRCode = "QR Code test",
                IsMatriz = true,
                AdicionadoDataHora = DateTime.Now,
                ModificadoDataHora = null,
                ExcluidoDataHora = null
            };

            var expectedResult = new DataResult<RestauranteDto>(new List<Notification>(), expecteRestaurante);

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateRestauranteRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _underTest.CreateRestaurante(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<RestauranteDto>(okResult.Value);

            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response.ID.Should().Be(expecteRestaurante.ID);
            response.RazaoSocial.Should().Be(expecteRestaurante.RazaoSocial);
            response.NomeFantasia.Should().Be(expecteRestaurante.NomeFantasia);
            response.Email.Should().Be(expecteRestaurante.Email);
            response.DDD.Should().Be(expecteRestaurante.DDD);
            response.Telefone.Should().Be(expecteRestaurante.Telefone);
            response.Bairro.Should().Be(expecteRestaurante.Bairro);
            response.Cidade.Should().Be(expecteRestaurante.Cidade);
            response.Estado.Should().Be(expecteRestaurante.Estado);
            response.Complemento.Should().Be(expecteRestaurante.Complemento);
            response.NomeUsuario.Should().Be(expecteRestaurante.NomeUsuario);
            response.Senha.Should().Be(expecteRestaurante.Senha);
            response.QRCode.Should().Be(expecteRestaurante.QRCode);
            response.IsMatriz.Should().Be(expecteRestaurante.IsMatriz);
            response.AdicionadoDataHora.Should().Be(expecteRestaurante.AdicionadoDataHora);
            response.ModificadoDataHora.Should().Be(expecteRestaurante.ModificadoDataHora);
            response.ExcluidoDataHora.Should().Be(expecteRestaurante.ExcluidoDataHora);

            // Verify
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<CreateRestauranteRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateRestaurante_InvalidInput_ShouldReturnBadRequest()
        {
            // Arrange
            var notifications = new List<Notification>()
            {
                new Notification("test", "Invalid request")
            };

            var expectedResult = new DataResult<RestauranteDto>(notifications, (RestauranteDto)null);

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateRestauranteRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _underTest.CreateRestaurante((CreateRestauranteDto)null);

            // Assert
            var errorResult = Assert.IsType<BadRequestObjectResult>(result);

            errorResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            // Verify
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<CreateRestauranteRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
