using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class RoleBuilder
    {
        private const string TableName = "Role";
        
        public RoleBuilder(EntityTypeConfiguration<RoleEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasKey(b => b.Id);
            modelBuilder.Property(b => b.Name).IsRequired().HasMaxLength(250);
            modelBuilder.Property(b => b.Description).IsRequired().HasMaxLength(500);
            
            modelBuilder
                .HasMany<PermissionEntity>(roleEntity => roleEntity.Permissions)
                .WithMany(permissionEntity => permissionEntity.Roles)
                .Map(pivotTable =>
                {
                    pivotTable.MapLeftKey("RoleId");
                    pivotTable.MapRightKey("PermissionId");
                    pivotTable.ToTable("RolePermission");
                });
        }
    }
}