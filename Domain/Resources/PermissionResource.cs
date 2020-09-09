using Domain.Entities;

namespace Domain.Resources
{
    public class PermissionResource
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static PermissionResource FromEntity(PermissionEntity entity)
        {
            return new PermissionResource()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}