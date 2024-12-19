using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Create;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetAll;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.GetById;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.RemoveById;
using RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Update;

namespace RestoHub.Api.Modules.UsersModule.Infrastructure.Bootstrapers
{
    public static class MediatorBootstrap
    {
        public static IServiceCollection ConfigureMediators(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateUsersRequest, DataResult<UsersDto>>, CreateUsersHandler>();
            services.AddTransient<IRequestHandler<GetUsersByIdRequest, DataResult<UsersDto>>, GetUsersByIdHandler>();
            services.AddTransient<IRequestHandler<GetAllUserssRequest, DataResult<IEnumerable<UsersDto>>>, GetAllUserssHandler>();
            services.AddTransient<IRequestHandler<UpdateUsersRequest, DataResult<UsersDto>>, UpdateUsersHandler>();
            services.AddTransient<IRequestHandler<RemoveUsersByIdRequest, DataResult<bool>>, RemoveUsersByIdHandler>();

            return services;
        }
    }
}
