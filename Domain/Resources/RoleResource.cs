using Domain.Entities;

namespace Domain.Resources
{
    public class RoleResource
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static RoleResource FromEntity(RoleEntity entity)
        {
            return new RoleResource()
            {
                Name = entity.Name,
                Email = entity.Description
            };
        }
    }
}