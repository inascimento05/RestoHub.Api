using RestoHub.Api.Modules.UsersModule.Domain.Entities;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static explicit operator UsersDto(Users entity)
        {
            var valueDto = new UsersDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return valueDto;
        }
    }
}
