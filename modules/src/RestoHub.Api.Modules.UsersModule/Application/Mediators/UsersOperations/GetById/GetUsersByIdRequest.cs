using FluentValidator;
using FluentValidator.Validation;
using MediatR;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetById
{
    public class GetUsersByIdRequest : Notifiable, IRequest<DataResult<UsersDto>>
    {
        public int Id { get; private set; }

        public GetUsersByIdRequest(int id)
        {
            Id = id;

            AddNotifications(new ValidationContract()
                .IsNotNull(Id, nameof(Id), "Id cannot be null.")
                .IsGreaterOrEqualsThan(Id, 1, nameof(Id), "Id should be greater or equals than 1."));
        }

    }
}
