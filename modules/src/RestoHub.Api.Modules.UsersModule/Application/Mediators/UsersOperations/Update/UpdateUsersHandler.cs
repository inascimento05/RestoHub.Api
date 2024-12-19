using RestoHub.Api.Modules.Shared.Application.Mediators;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Update
{
    public class UpdateUsersHandler : BaseHandler<UsersDto>, IBaseHandler<UpdateUsersRequest, DataResult<UsersDto>>
    {
        private readonly IUsersService _service;

        public UpdateUsersHandler(IUsersService service)
        {
            _service = service;
        }

        public async Task<DataResult<UsersDto>> Handle(UpdateUsersRequest request, CancellationToken cancellationToken)
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

            var entityToUpdate = new Users
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };

            try
            {
                var updatedUsers = await _service.UpdateUsersAsync(entityToUpdate);

                if (updatedUsers == null)
                {
                    result.AddNotification("Not found.");
                    result.Error = ErrorCode.NotFound;
                    return result;
                }

                result.Data = (UsersDto)updatedUsers;
            }
            catch (Exception ex)
            {
                return ProcessException(result, ex);
            }

            return result;
        }
    }
}
