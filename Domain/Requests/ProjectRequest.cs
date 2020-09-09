using Domain.Entities;

namespace Domain.Requests
{
    public class ProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static ProjectEntity ToEntity(ProjectRequest entity)
        {
            return new ProjectEntity()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}