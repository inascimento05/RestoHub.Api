using Dapper;
using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using System.Data;

namespace RestoHub.Api.Modules.UsersModule.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsersRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateUsersAsync(Users entity)
        {
            const string query = "INSERT INTO Users (Name, Description) VALUES (@Name, @Description); SELECT SCOPE_IDENTITY();";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, entity);
            return id;
        }

        public async Task<IEnumerable<Users>> GetAllUserssAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize < 1)
            {
                pageNumber = 10;
            }

            int offset = (pageNumber - 1) * pageSize;

            const string query = "SELECT * FROM Users ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var param = new { Offset = offset, PageSize = pageSize };
            return await _dbConnection.QueryAsync<Users>(query, param);
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            const string query = "SELECT * FROM Users WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Users>(query, new { Id = id });
        }

        public async Task<Users> UpdateUsersAsync(Users entity)
        {
            const string query = "UPDATE Users SET Name = @Name, Description = @Description OUTPUT INSERTED.* WHERE Id = @Id";
            var param = new { Id = entity.Id, Name = entity.Name, Description = entity.Description };
            await _dbConnection.ExecuteAsync(query, param);

            return entity;
        }

        public async Task<bool> DeleteUsersByIdAsync(int id)
        {
            const string query = "DELETE FROM Users WHERE Id = @Id";
            var parameters = new { Id = id };
            int rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);

            return rowsAffected > 0;
        }
    }
}
