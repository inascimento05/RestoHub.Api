using FluentValidator;
using FluentValidator.Validation;
using MediatR;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Create
{
    public class CreateUsersRequest : Notifiable, IRequest<DataResult<UsersDto>>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public CreateUsersRequest(string name, string description)
        {
            Name = name;
            Description = description;

            AddNotifications(new ValidationContract()
                .IsNotNullOrEmpty(Name, "Name", "Name is required.")
                .IsLowerOrEqualsThan(Name?.Length ?? 0, 100, "Name", "Name cannot be longer than 100 characters.")
                .IsNotNullOrEmpty(Description, "Description", "Description is required.")
                .IsLowerOrEqualsThan(Description?.Length ?? 0, 200, "Description", "Description cannot be longer than 200 characters."));
        }
    }
}
