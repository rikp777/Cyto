using System.Data.Entity.Migrations;
using DAL.Context;
using Domain.Entities;

namespace DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            DatabaseContextSeed.Seed(context);
        }
    } 
}