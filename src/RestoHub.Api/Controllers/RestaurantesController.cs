using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Create;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Presenters;

namespace RestoHub.Api.Controllers
{
    public class RestaurantesController : BaseController
    {
        private readonly IPresenter _presenter;

        public RestaurantesController(IConfiguration config,
                                      IMediator meditor,
                                      IPresenter presenter) 
            : base(config, meditor)
        {
            _presenter = presenter;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestauranteDto))]
        public async Task<IActionResult> CreateRestaurante([FromBody] CreateRestauranteDto body)
        {
            var request = new CreateRestauranteRequest(inputDto: body);

            var result = await _mediator.Send(request);
            if (result.Invalid)
            {
                return BadRequest(result.Notifications);
            }

            return _presenter.GetResult(result);
        }

    }
}
