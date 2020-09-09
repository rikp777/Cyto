using Domain.Entities;
using Domain.Requests;

namespace Domain.Resources
{
    public class ProjectResource
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static ProjectResource FromEntity(ProjectEntity entity)
        {
            return new ProjectResource()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}