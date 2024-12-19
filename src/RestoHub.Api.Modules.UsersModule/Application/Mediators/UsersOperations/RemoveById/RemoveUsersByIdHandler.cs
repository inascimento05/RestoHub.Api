using RestoHub.Api.Modules.Shared.Application.Mediators;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.RemoveById
{
    public class RemoveUsersByIdHandler : BaseHandler<bool>, IBaseHandler<RemoveUsersByIdRequest, DataResult<bool>>
    {
        private readonly IUsersService _service;

        public RemoveUsersByIdHandler(IUsersService service)
        {
            _service = service;
        }

        public async Task<DataResult<bool>> Handle(RemoveUsersByIdRequest request, CancellationToken cancellationToken)
        {
            var result = new DataResult<bool>();
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
                var removed = await _service.RemoveUsersByIdAsync(request.Id);

                if (removed == null)
                {
                    result.AddNotification("Not found.");
                    result.Error = ErrorCode.NotFound;
                    return result;
                }

                result.Data = (bool)removed;
            }
            catch (Exception ex)
            {
                return ProcessException(result, ex);
            }

            return result;
        }
    }
}
