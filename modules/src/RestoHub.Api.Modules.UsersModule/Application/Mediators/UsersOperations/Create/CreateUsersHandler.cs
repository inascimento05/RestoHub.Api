using RestoHub.Api.Modules.Shared.Application.Mediators;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Create
{
    public class CreateUsersHandler : BaseHandler<UsersDto>, IBaseHandler<CreateUsersRequest, DataResult<UsersDto>>
    {
        private readonly IUsersService _service;

        public CreateUsersHandler(IUsersService service)
        {
            _service = service;
        }

        public async Task<DataResult<UsersDto>> Handle(CreateUsersRequest request, CancellationToken cancellationToken)
        {
            var result = new DataResult<UsersDto>();
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

            var entity = new Users
            {
                Name = request.Name,
                Description = request.Description
            };

            try
            {
                entity.Id = await _service.CreateUsersAsync(entity);

                if (entity.Id <= 0)
                {
                    result.AddNotification("Failure to create new.");
                    result.Error = ErrorCode.UnprocessableEntity;
                    return result;
                }

                result.Data = (UsersDto)entity;
            }
            catch (Exception ex)
            {
                return ProcessException(result, ex);
            }

            return result;
        }
    }
}
