using Domain.Entities;

namespace Domain.Requests
{
    public class ExperimentRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static ExperimentEntity ToEntity(ExperimentRequest entity)
        {
            return new ExperimentEntity()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
        
        public override string ToString()
        {
            return Name + " " + Description;
        }
    }
}