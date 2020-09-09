using System.Collections.Generic;
using Domain.Contracts;

namespace Domain.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        //Relationships 
        //Role has many Users
        public virtual ICollection<UserEntity> Users { get; set; }
        //Role has many Permissions
        public virtual ICollection<PermissionEntity> Permissions { get; set; }
    }
}