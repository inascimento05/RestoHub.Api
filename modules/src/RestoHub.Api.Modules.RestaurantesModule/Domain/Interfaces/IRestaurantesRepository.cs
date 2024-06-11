using RestoHub.Api.Modules.RestaurantesModule.Domain.Entities;

namespace RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces
{
    public interface IRestaurantesRepository
    {
        Task<Restaurante> CreateRestauranteAsync(Restaurante entity);
        //Task<Restaurantes> GetRestaurantesByIdAsync(int id);
        //Task<IEnumerable<Restaurantes>> GetAllRestaurantessAsync(int pageNumber, int pageSize);
        //Task<Restaurantes> UpdateRestaurantesAsync(Restaurantes entity);
        //Task<bool> DeleteRestaurantesByIdAsync(int id);
    }
}
