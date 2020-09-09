using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DAL.Builder;
using Domain.Entities;
using TrackerEnabledDbContext;
using TrackerEnabledDbContext.Common.Interfaces;

namespace DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
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
            var saved = SaveChanges();
            return saved > 0;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

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