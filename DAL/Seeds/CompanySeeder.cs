using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Seeds
{
    public class CompanySeeder
    {
        public static void Seeder(IDbSet<CompanyEntity> companies)
        {
            var companyOne = new CompanyEntity()
            {
                Name = "Cyto",
                Description = "Company desc"
            };
            var companyTwo = new CompanyEntity()
            {
                Name = "TestComp",
                Description = "Company Test desc"
            };
            companies.AddOrUpdate(x => x.Name, companyOne, companyTwo);
        }
    }
}