using System.Data.Entity;
using System.Data.Entity.Migrations;
using Domain.Entities;

namespace DAL.Seeds
{
    public class RoleSeeder
    {
        public static void Seeder(IDbSet<RoleEntity> roles)
        {
            var roleOne = new RoleEntity()
            {
                Name = "Admin",
                Description = "God of the application"
            };
            roles.AddOrUpdate(x => x.Name, roleOne);
        }
    }
}