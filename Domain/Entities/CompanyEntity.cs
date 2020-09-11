using System.Collections.Generic;
using Domain.Contracts;

namespace Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        
        //Relationships 
        //Company has many projects
        public virtual ICollection<ProjectEntity> Projects { get; set; }
        //Company has many users
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}