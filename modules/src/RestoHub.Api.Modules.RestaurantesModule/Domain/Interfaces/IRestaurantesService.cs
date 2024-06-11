using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;

namespace RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces
{
    public interface IRestaurantesService
    {
        Task<RestauranteDto> CreateRestauranteAsync(RestauranteDto restaurante);
        //Task<Restaurantes> GetRestaurantesByIdAsync(int id);
        //Task<IEnumerable<Restaurantes>> GetAllRestaurantessAsync(int pageNumber, int pageSize);
        //Task<Restaurantes> UpdateRestaurantesAsync(Restaurantes entity);
        //Task<bool?> RemoveRestaurantesByIdAsync(int id);
    }
}
