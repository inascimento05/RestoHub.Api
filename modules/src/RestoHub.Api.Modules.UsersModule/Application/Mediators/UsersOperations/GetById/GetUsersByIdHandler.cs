using RestoHub.Api.Modules.Shared.Application.Mediators;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetById
{
    public class GetUsersByIdHandler : BaseHandler<UsersDto>, IBaseHandler<GetUsersByIdRequest, DataResult<UsersDto>>
    {
        private readonly IUsersService _service;

        public GetUsersByIdHandler(IUsersService service)
        {
            _service = service;
        }

        public async Task<DataResult<UsersDto>> Handle(GetUsersByIdRequest request, CancellationToken cancellationToken)
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

            try
            {
                var entity = await _service.GetUsersByIdAsync(request.Id);

                if (entity == null)
                {
                    result.AddNotification("Not found.");
                    result.Error = ErrorCode.NotFound;
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
