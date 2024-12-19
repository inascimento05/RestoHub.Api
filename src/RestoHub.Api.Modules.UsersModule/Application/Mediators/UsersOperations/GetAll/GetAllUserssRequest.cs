using FluentValidator.Validation;
using FluentValidator;
using MediatR;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetAll
{
    public class GetAllUserssRequest : Notifiable, IRequest<DataResult<IEnumerable<UsersDto>>>
    {
        public GetAllUserssRequest(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            PageNumber = pageNumber;
            PageSize = pageSize;

            AddNotifications(new ValidationContract()
                .IsNotNull(PageNumber, nameof(PageNumber), "Id cannot be null.")
                .IsGreaterOrEqualsThan(PageNumber, 1, nameof(PageNumber), "Id should be greater or equals than 1.")
                .IsNotNull(pageSize, nameof(pageSize), "Id cannot be null.")
                .IsGreaterOrEqualsThan(pageSize, 1, nameof(pageSize), "Id should be greater or equals than 1."));
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
    }
}
