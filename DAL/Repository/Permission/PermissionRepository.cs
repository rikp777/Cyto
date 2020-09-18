using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Permission
{
    public class PermissionRepository : GenericCrudRepository<PermissionEntity>, IGenericCrudRepository<PermissionEntity>
    {
        public PermissionRepository(DatabaseContext context) : base(context, context.Permissions)
        {
        }
    }
}