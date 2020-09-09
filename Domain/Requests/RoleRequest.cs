using Domain.Entities;

namespace Domain.Requests
{
    public class RoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static RoleEntity ToEntity(RoleRequest entity)
        {
            return new RoleEntity()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}