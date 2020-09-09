using Domain.Contracts;

namespace Domain.Entities
{
    public class ExperimentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships 
        //Experiment has one project
        public ProjectEntity Project { get; set; }
    }
}