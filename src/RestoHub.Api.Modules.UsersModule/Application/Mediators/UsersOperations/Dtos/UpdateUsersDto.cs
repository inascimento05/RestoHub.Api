using System.Text.Json.Serialization;

namespace RestoHub.Api.Modules.UsersModule.Application.Mediators.UsersOperations.Dtos
{
    public class UpdateUsersDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
