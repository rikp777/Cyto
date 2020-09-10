using System.Collections.Generic;
using Domain.Contracts;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            Companies = new List<CompanyEntity>();
            Roles = new List<RoleEntity>();
            Projects = new List<ProjectEntity>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        
        //Relationships 
        //User has many Companies
        public virtual ICollection<CompanyEntity> Companies { get; set; }
        //User has many Roles
        public virtual ICollection<RoleEntity> Roles { get; set; }
        //User has many Projects
        public virtual ICollection<ProjectEntity> Projects { get; set; }
        //User has many AuditTrails
        public virtual ICollection<AuditTrailEntity> AuditTrails { get; set; }
        
    }
}