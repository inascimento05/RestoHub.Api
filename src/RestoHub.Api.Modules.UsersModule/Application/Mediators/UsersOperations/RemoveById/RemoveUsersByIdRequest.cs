using FluentValidator;
using FluentValidator.Validation;
using MediatR;
using RestoHub.Api.Modules.Shared.Application.Notifications;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.RemoveById
{
    public class RemoveUsersByIdRequest : Notifiable, IRequest<DataResult<bool>>
    {
        public int Id { get; private set; }

        public RemoveUsersByIdRequest(int id)
        {
            Id = id;

            AddNotifications(new ValidationContract()
                .IsNotNull(Id, nameof(Id), "Id cannot be null.")
                .IsGreaterOrEqualsThan(Id, 1, nameof(Id), "Id should be greater or equals than 1."));
        }
    }
}
