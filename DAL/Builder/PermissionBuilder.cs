using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class PermissionBuilder
    {
        private const string TableName = "Permission";
        
        public PermissionBuilder(EntityTypeConfiguration<PermissionEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
        }
    }
}