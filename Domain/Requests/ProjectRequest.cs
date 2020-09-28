using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Requests
{
    public class ProjectRequest
    {
        [Required(ErrorMessage = "A project name must be provided!")]
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