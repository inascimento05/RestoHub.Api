using RestoHub.Api.Modules.UsersModule.Domain.Entities;

namespace RestoHub.Api.Modules.UsersModule.Domain.Interfaces
{
    public interface IUsersService
    {
        Task<int> CreateUsersAsync(Users entity);
        Task<Users> GetUsersByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllUserssAsync(int pageNumber, int pageSize);
        Task<Users> UpdateUsersAsync(Users entity);
        Task<bool?> RemoveUsersByIdAsync(int id);
    }
}
