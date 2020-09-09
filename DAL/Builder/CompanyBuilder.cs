using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class CompanyBuilder
    {
        private const string TableName = "Company";

        public CompanyBuilder(EntityTypeConfiguration<CompanyEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasKey(b => b.Id);
            modelBuilder.Property(b => b.Name).IsRequired().HasMaxLength(250);
            modelBuilder.Property(b => b.Description).IsRequired().HasMaxLength(500);
        }
    }
}