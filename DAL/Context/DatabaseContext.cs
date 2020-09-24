using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DAL.Builder;
using DAL.Interfaces;
using DAL.Repository.AuditTrail;
using Domain.Audit;
using Domain.Contracts;
using Domain.Entities;
using TrackerEnabledDbContext;
using TrackerEnabledDbContext.Common.Interfaces;

namespace DAL.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            //Bug fix 
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        // DB Sets 
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ExperimentEntity> Experiments { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<AuditTrailEntity> AuditTrails { get; set; }
        public DbSet<AuditTrailChangeLogEntity> AuditTrailChangeLogs { get; set; }

        public void MarkAsModified(BaseEntity entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public EntityState GetState(BaseEntity entity)
        {
            return Entry(entity).State;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>(
                );
            var userBuilder = new UserBuilder(modelBuilder.Entity<UserEntity>());
            var roleBuilder = new RoleBuilder(modelBuilder.Entity<RoleEntity>());
            var companyBuilder = new CompanyBuilder(modelBuilder.Entity<CompanyEntity>());
            var experimentBuilder = new ExperimentBuilder(modelBuilder.Entity<ExperimentEntity>());
            var permissionBuilder = new PermissionBuilder(modelBuilder.Entity<PermissionEntity>());
            var projectBuilder = new ProjectBuilder(modelBuilder.Entity<ProjectEntity>());
            var auditTrailBuilder = new AuditTrailBuilder(modelBuilder.Entity<AuditTrailEntity>());
            var auditTrailChangeLogBuilder = new AuditTrailChangeLogBuilder(modelBuilder.Entity<AuditTrailChangeLogEntity>());
        }
    }
}