using System.Data.Entity;
using System.Data.Entity.Migrations;
using Domain.Entities;

namespace DAL.Seeds
{
    public class PermissonSeeder
    {
        public static void Seeder(IDbSet<PermissionEntity> permissions)
        {
            var permissionOne = new PermissionEntity()
            {
                Name = "Add Project",
                Description = "Add a new project"
            };
            permissions.AddOrUpdate(x => x.Name, permissionOne);
        }
    }
}