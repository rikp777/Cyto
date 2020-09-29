using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class LicenseTypeBuilder
    {
        private const string TableName = "LicenseType";
        
        public LicenseTypeBuilder(EntityTypeConfiguration<LicenseTypeEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasKey(b => b.Id);
        }
    }
}