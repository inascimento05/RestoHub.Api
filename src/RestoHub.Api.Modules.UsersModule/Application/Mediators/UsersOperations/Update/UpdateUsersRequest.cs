using FluentValidator;
using FluentValidator.Validation;
using MediatR;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Update
{
    public class UpdateUsersRequest : Notifiable, IRequest<DataResult<UsersDto>>
    {
        public UpdateUsersRequest(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

            AddNotifications(new ValidationContract()
                .IsNotNull(Id, nameof(Id), "Id cannot be null.")
                .IsGreaterOrEqualsThan(Id, 1, nameof(Id), "Id should be greater or equals than 1."));
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
