using System.Data.Entity;
using Domain.Entities;
using TrackerEnabledDbContext.Common.Interfaces;

namespace DAL.Interfaces
{
    public interface IDatabaseContext 
    {
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }
        DbSet<CompanyEntity> Companies { get; set; }
        DbSet<ExperimentEntity> Experiments { get; set; }
        DbSet<ProjectEntity> Projects { get; set; }
        DbSet<AuditTrailEntity> AuditTrails { get; set; }
        DbSet<AuditTrailChangeLogEntity> AuditTrailChangeLogs { get; set; }

        int SaveChanges();
        int SaveChanges(UserEntity user, CompanyEntity company);
    }
}