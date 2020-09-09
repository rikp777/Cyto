using Domain.Entities;

namespace Domain.Requests
{
    public class PermissionRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static PermissionEntity ToEntity(PermissionRequest entity)
        {
            return new PermissionEntity()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}