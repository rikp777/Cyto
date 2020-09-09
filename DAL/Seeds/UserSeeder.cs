using System.Data.Entity;
using System.Data.Entity.Migrations;
using Domain.Entities;

namespace DAL.Seeds
{
    public static class UserSeeder
    {
        public static void Seeder(IDbSet<UserEntity> users)
        {
            var userOne = new UserEntity()
            {
                Name = "Test",
                Email = "test@cyto.com"
            };
            var userTwo = new UserEntity()
            {
                Name = "Employee",
                Email = "Employee@cyto.com"
            };
            
            users.AddOrUpdate(x => x.Name, userOne, userTwo);
        }
    }
}