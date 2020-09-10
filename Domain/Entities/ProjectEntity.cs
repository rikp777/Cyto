using System.Collections.Generic;
using Domain.Contracts;

namespace Domain.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships 
        //Project has one Company
        public CompanyEntity Company { get; set; }
        //Project has many Experiments
        public virtual ICollection<ExperimentEntity > Experiments { get; set; }
        //Project has many Users
        public virtual ICollection<UserEntity > Users { get; set; }

        public ProjectEntity() { }
    }
}