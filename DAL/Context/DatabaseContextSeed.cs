using System.Linq;
using DAL.Seeds;

namespace DAL.Context
{
    public static class DatabaseContextSeed
    {
        public static void Seed(DatabaseContext context)
        {
            PermissonSeeder.Seeder(context.Permissions);
            context.Save();
            
            RoleSeeder.Seeder(context.Roles);
            context.Save();
            
            UserSeeder.Seeder(context.Users);
            context.Save();
            
            CompanySeeder.Seeder(context.Companies);
            context.Save();
            
            ProjectSeeder.Seeder(context.Projects, context.Companies);
            context.Save();
            
            ExperimentSeeder.Seeder(context.Experiments, context.Projects);
            context.Save();
        }
    }
}