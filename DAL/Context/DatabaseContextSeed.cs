using System.Linq;
using DAL.Seeds;
using Domain.Entities;

namespace DAL.Context
{
    public static class DatabaseContextSeed
    {
        public static void Seed(DatabaseContext context)
        {
            PermissonSeeder.Seeder(context.Permissions);
            context.SaveChanges();
            
            RoleSeeder.Seeder(context.Roles);
            context.SaveChanges();
            
            UserSeeder.Seeder(context.Users);
            context.SaveChanges();
            
            CompanySeeder.Seeder(context.Companies);
            context.SaveChanges();
            
            ProjectSeeder.Seeder(context.Projects, context.Companies);
            context.SaveChanges();
            
            ExperimentSeeder.Seeder(context.Experiments, context.Projects);
            context.SaveChanges();
            
            LicenseTypeSeeder.Seeder(context.LicenseTypes);
            context.SaveChanges();
            
            LicenseSeeder.Seeder(context.Licenses, context.LicenseTypes, context.Companies);
            context.SaveChanges();
        }
    }
}