using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class AuditTrailBuilder
    {
        private const string TableName = "AuditTrail";

        public AuditTrailBuilder(EntityTypeConfiguration<AuditTrailEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasKey(b => b.Id);
        }
    }
}