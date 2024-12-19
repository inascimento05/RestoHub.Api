using RestoHub.Api.Modules.Shared.Application.Mediators;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetAll
{
    public class GetAllUserssHandler : BaseHandler<IEnumerable<UsersDto>>, IBaseHandler<GetAllUserssRequest, DataResult<IEnumerable<UsersDto>>>
    {

        private readonly IUsersService _service;

        public GetAllUserssHandler(IUsersService service)
        {
            _service = service;
        }

        public async Task<DataResult<IEnumerable<UsersDto>>> Handle(GetAllUserssRequest request, CancellationToken cancellationToken)
        {
            var result = new DataResult<IEnumerable<UsersDto>>();
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
                var entities = await _service.GetAllUserssAsync(request.PageNumber, request.PageSize);

                if (entities == null)
                {
                    result.AddNotification("Not found.");
                    result.Error = ErrorCode.NotFound;
                    return result;
                }

                result.Data = entities.Select(entity => (UsersDto)entity);
            }
            catch (Exception ex)
            {
                return ProcessException(result, ex);
            }

            return result;
        }
    }
}
