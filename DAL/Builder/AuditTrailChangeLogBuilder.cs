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
            modelBuilder.Property(b => b.ColumnName);
            modelBuilder.Property(b => b.ValueBefore);
            modelBuilder.Property(b => b.ValueAfter);
            modelBuilder.Ignore(b => b.Changed);
        }
    }
}