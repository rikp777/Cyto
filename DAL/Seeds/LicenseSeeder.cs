using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using Domain.Entities;

namespace DAL.Seeds
{
    public class LicenseSeeder
    {
        public static void Seeder(IDbSet<LicenseEntity> licenses, IDbSet<LicenseTypeEntity> licenseTypes, IDbSet<CompanyEntity> companies)
        {
            var licenseTypesOne = licenseTypes.Find(1);
            if (licenseTypesOne == null) throw new Exception("LicenseType One not found");
            var companyOne = companies.Find(1);
            if (companyOne == null) throw new Exception("Company One not found");

            var licenseOne = new LicenseEntity()
            {
                Code = "SDF&328SDHnls76Dlsaeow",
                LicenseType = licenseTypesOne,
                Company = companyOne,
                ValidTillDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };
            licenses.AddOrUpdate(x => x.Code, licenseOne);
        }
    }
}