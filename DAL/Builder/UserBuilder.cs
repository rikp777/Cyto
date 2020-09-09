using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Domain.Entities;
using TrackerEnabledDbContext.Common.Configuration;

namespace DAL.Builder
{
    public class UserBuilder
    {
        private const string TableName = "User";
        
        public UserBuilder(EntityTypeConfiguration<UserEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
            modelBuilder.HasIndex(b => b.Id);
            modelBuilder.Property(b => b.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Property(b => b.Name).IsRequired().HasMaxLength(200);

            
            modelBuilder
                .HasMany(user => user.Roles)
                .WithMany(role => role.Users)
                .Map(pivotTable =>
                {
                    pivotTable.MapLeftKey("UserId");
                    pivotTable.MapRightKey("RoleId");
                    pivotTable.ToTable("UserRole");
                });
            
            modelBuilder
                .HasMany(user => user.Projects)
                .WithMany(project => project.Users)
                .Map(pivotTable =>
                {
                    pivotTable.MapLeftKey("UserId");
                    pivotTable.MapRightKey("ProjectId");
                    pivotTable.ToTable("UserProject");
                });
            
            
            modelBuilder
                .HasMany(userEntity => userEntity.Companies)
                .WithMany(companyEntity => companyEntity.Users)
                .Map(pivotTable =>
                {
                    pivotTable.MapLeftKey("UserId");
                    pivotTable.MapRightKey("CompanyId");
                    pivotTable.ToTable("UserCompany");
                });
        }
    }
}