using MediatR;

namespace RestoHub.Api.Modules.Shared.Application.Interfaces
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
