using System.Data.Entity;
using System.Data.Entity.Migrations;
using Domain.Entities;

namespace DAL.Seeds
{
    public class LicenseTypeSeeder
    {
        public static void Seeder(IDbSet<LicenseTypeEntity> licenseTypes)
        {
            var licenseTypeOne = new LicenseTypeEntity()
            {
                Name = "CFR",
                Description = "With this license you can turn on CFR regulations"
            };
 
            licenseTypes.AddOrUpdate(x => x.Name, licenseTypeOne);
        }
    }
}