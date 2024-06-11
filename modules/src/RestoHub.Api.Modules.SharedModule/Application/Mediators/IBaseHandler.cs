using MediatR;

namespace RestoHub.Api.Modules.Shared.Application.Mediators
{
    public interface IBaseHandler<T, K> : IRequestHandler<T, K> where T : IRequest<K> { }
}
