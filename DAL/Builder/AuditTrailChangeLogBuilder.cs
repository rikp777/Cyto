using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class AuditTrailChangeLogBuilder
    {
        private const string TableName = "AuditTrailChangeLog";

        public AuditTrailChangeLogBuilder(EntityTypeConfiguration<AuditTrailChangeLogEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasKey(b => b.Id);
        }
    }
}