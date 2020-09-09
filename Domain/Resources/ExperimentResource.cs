using Domain.Entities;

namespace Domain.Resources
{
    public class ExperimentResource
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static ExperimentResource FromEntity(ExperimentEntity entity)
        {
            return new ExperimentResource()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}