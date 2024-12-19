using RestoHub.Api.Modules.UsersModule.Domain.Entities;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateUsersAsync(Users entity)
        {
            ValidadeCreateUsers(entity);

            return await _repository.CreateUsersAsync(entity);
        }

        public async Task<IEnumerable<Users>> GetAllUserssAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException(nameof(pageNumber), "PageNumber should be greater or equals than 1.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException(nameof(pageSize), "PageSize should be greater or equals than 1.");
            }

            return await _repository.GetAllUserssAsync(pageNumber, pageSize);
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            ValidadeUsersId(id);

            return await _repository.GetUsersByIdAsync(id);
        }

        public async Task<Users> UpdateUsersAsync(Users entity)
        {
            ValidateUpdateUsers(entity);

            var entityFromDatabase = await this.GetUsersByIdAsync(entity.Id);

            if (entityFromDatabase == null)
            {
                return (Users)null;
            }

            var entityToUpdate = entityFromDatabase;
            entityToUpdate.Name = entity.Name ?? entityFromDatabase.Name;
            entityToUpdate.Description = entity.Description ?? entityFromDatabase.Description;

            var updateUsers = await _repository.UpdateUsersAsync(entityToUpdate);

            return updateUsers;
        }

        public async Task<bool?> RemoveUsersByIdAsync(int id)
        {
            ValidadeUsersId(id);

            var entityFromDatabase = await this.GetUsersByIdAsync(id);

            if (entityFromDatabase == null)
            {
                return null;
            }

            return await _repository.DeleteUsersByIdAsync(id);
        }

        private void ValidadeUsersId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Id should be greater or equals than 1.");
            }
        }

        private void ValidateUsersNameLength(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.Length > 100)
            {
                throw new ArgumentException("Users name cannot be longer than 100 characters.");
            }
        }

        private void ValidateUsersDescriptionLength(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 200)
            {
                throw new ArgumentException("Users description cannot be longer than 200 characters.");
            }
        }

        private void ValidadeUsersNameNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Users name is a required field.");
            }
        }

        private void ValidadeUsersDescriptionNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Users description is a required field.");
            }
        }

        private void ValidadeUsersNull(Users entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Users cannot be null.");
            }
        }

        private void ValidadeCreateUsers(Users entity)
        {
            ValidateUpdateUsers(entity);
            ValidadeUsersNameNullOrEmpty(entity.Name);
            ValidadeUsersDescriptionNullOrEmpty(entity.Description);
        }

        private void ValidateUpdateUsers(Users entity)
        {
            ValidadeUsersNull(entity);
            ValidateUsersNameLength(entity.Name);
            ValidateUsersDescriptionLength(entity.Description);
        }
    }
}
