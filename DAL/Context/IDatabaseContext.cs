using System;
using System.Data.Entity;
using Domain.Contracts;
using Domain.Entities;

namespace DAL.Context
{
    public interface IDatabaseContext : IDisposable
    {
        
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }
        DbSet<CompanyEntity> Companies { get; set; }
        DbSet<ExperimentEntity> Experiments { get; set; }
        DbSet<ProjectEntity> Projects { get; set; }
        DbSet<AuditTrailEntity> AuditTrails { get; set; }
        DbSet<AuditTrailChangeLogEntity> AuditTrailChangeLogs { get; set; }

        bool Save();
        void MarkAsModified(BaseEntity entity);    
        EntityState GetState(BaseEntity entity);    


    }
}