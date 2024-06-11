using FluentValidator;
using FluentValidator.Validation;
using MediatR;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.Shared.Application.Notifications;

namespace RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Create
{
    public class CreateRestauranteRequest : Notifiable, IRequest<DataResult<RestauranteDto>>
    {
        public CreateRestauranteDto InputDto { get; set; }

        public CreateRestauranteRequest(CreateRestauranteDto inputDto)
        {
            InputDto = inputDto;

            AddNotifications(new ValidationContract()
                .IsNotNull(InputDto, "Body", "Invalid body request"));

            if (InputDto != null)
            {
                InputDto.Validate();
                AddNotifications(inputDto.Notifications);
            }
        }
    }
}
