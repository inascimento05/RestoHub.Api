using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;
using RestoHub.Api.Modules.Shared.Application.Mediators;
using RestoHub.Api.Modules.Shared.Application.Notifications;

namespace RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Create
{
    public class CreateRestauranteHandler : BaseHandler<RestauranteDto>, IBaseHandler<CreateRestauranteRequest, DataResult<RestauranteDto>>
    {
        private readonly IRestaurantesService _service;

        public CreateRestauranteHandler(IRestaurantesService service)
        {
            _service = service;
        }

        public async Task<DataResult<RestauranteDto>> Handle(CreateRestauranteRequest request, CancellationToken cancellationToken)
        {
            var result = new DataResult<RestauranteDto>();
            if (request == null)
            {
                result.AddNotification("Request", "Request cannot be null.");
                result.Error = ErrorCode.BadRequest;
                return result;
            }

            result.AddNotifications(request.Notifications);
            if (result.Invalid)
            {
                result.Error = ErrorCode.BadRequest;
                return result;
            }

            var restauranteDto = new RestauranteDto
            {
                ID = Guid.NewGuid(),
                RazaoSocial = request.InputDto.RazaoSocial,
                NomeFantasia = request.InputDto.NomeFantasia,
                CNPJ = request.InputDto.CNPJ,
                Email = request.InputDto.Email,
                DDD = request.InputDto.DDD,
                Telefone = request.InputDto.Telefone,
                Logradouro = request.InputDto.Logradouro,
                Numero = request.InputDto.Numero,
                Bairro = request.InputDto.Bairro,
                Cidade = request.InputDto.Cidade,
                Estado = request.InputDto.Estado,
                CEP = request.InputDto.CEP,
                Complemento = request.InputDto.Complemento,
                NomeUsuario = request.InputDto.NomeUsuario,
                Senha = request.InputDto.Senha,
                QRCode = request.InputDto.QRCode,
                IsMatriz = request.InputDto.IsMatriz,
                AdicionadoDataHora = DateTime.Now
            };
            
            try
            {
                var restaurante = await _service.CreateRestauranteAsync(restauranteDto);

                result.Data = restaurante;
            }
            catch (Exception ex)
            {
                return ProcessException(result, ex);
            }

            return result;
        }
    }
}
