using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using Domain.Contracts;
using Domain.Entities;
using TrackerEnabledDbContext.Common.Interfaces;

namespace DAL.Interfaces
{
    public interface IDatabaseContext : IDbContext 
    {
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }
        DbSet<CompanyEntity> Companies { get; set; }
        DbSet<ExperimentEntity> Experiments { get; set; }
        DbSet<ProjectEntity> Projects { get; set; }
        DbSet<AuditTrailEntity> AuditTrails { get; set; }
        DbSet<AuditTrailChangeLogEntity> AuditTrailChangeLogs { get; set; }
        
        void MarkAsModified(BaseEntity entity);    
        EntityState GetState(BaseEntity entity);
    }
}