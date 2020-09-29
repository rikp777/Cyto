using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class LicenseBuilder
    {
        private const string TableName = "License";
        
        public LicenseBuilder(EntityTypeConfiguration<LicenseEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasIndex(b => b.Id);
        }
    }
}