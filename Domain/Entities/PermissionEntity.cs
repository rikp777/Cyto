using System.Collections.Generic;
using Domain.Contracts;

namespace Domain.Entities
{
    public class PermissionEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships 
        //Permission has many Roles 
        public virtual  ICollection<RoleEntity> Roles { get; set; }
    }
}