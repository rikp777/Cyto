using System;
using System.Data.Entity;
using DAL.Context;
using Domain.Contracts;
using Domain.Entities;

namespace API.Tests
{
    public class TestContext : IDatabaseContext 
    {
        public TestContext()
        {
            Companies = new TestDbSet<CompanyEntity>();
            Projects = new TestDbSet<ProjectEntity>();
            Experiments = new TestDbSet<ExperimentEntity>();
        }


        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ExperimentEntity> Experiments { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<AuditTrailEntity> AuditTrails { get; set; }
        public DbSet<AuditTrailChangeLogEntity> AuditTrailChangeLogs { get; set; }

        public bool Save()
        {
            return true;
        }

        public void MarkAsModified(BaseEntity item) { }
        public EntityState GetState(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }
    }
}