using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.Linq;
using DAL.Builder;
using Domain.Audit;
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
        
        //AuditTrail Data
        private bool _audit = false;        
        private UserEntity _user = null;
        private CompanyEntity _company = null;
        private string _serviceName = null;
        private string _methodName = null;

        // Save Audit Trail disabled 
        public bool Save()
        {
            _audit = false;
            var saved = SaveChanges();
            return saved > 0;
        }
        
        // Save Audit Trail enabled
        public bool Save(UserEntity user, CompanyEntity company, string serviceName = null, string methodName = null)
        {
            _audit = true;
            _company = company;
            _user = user;

            _serviceName = serviceName;
            _methodName = methodName;
            
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

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        private void AuditTrail()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            string primaryKey = null;
            
            var changes = new List<AuditTrailChangeLogEntity>();
            foreach (var change in modifiedEntities)
            {
                //var entityName = change.Entity.GetType().Name;
                primaryKey = GetPrimaryKeyValue(change).ToString();
                foreach(var columnName in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[columnName].ToString();
                    var currentValue = change.CurrentValues[columnName].ToString();
                    if (originalValue != currentValue)
                    {
                        var auditChangeLog = new AuditTrailChangeLogEntity
                        {
                            ColumnName = columnName,
                            ValueBefore = originalValue, 
                            ValueAfter = currentValue
                        };
                        changes.Add(auditChangeLog);
                        
                        Console.WriteLine(@"Audit Trail: 'Value Changed': [Original value: " + originalValue + @"] - [Current value: " + currentValue + @"] - [Column name: " + columnName + @"]");
                    }
                }
            }



            var auditTrail = new AuditTrailEntity
            {
                User = _user,
                Company = _company,
                Permission = null,
                
                ServiceName = _serviceName,
                MethodName = _methodName,
                MethodColor = "Red",
                ActionType = AuditActionType.Index,
                
                IpAddress = null,
                Reason = null,

                TableName = null,
                PrimaryKey = primaryKey,
                
                CreatedAt = now.ToString(CultureInfo.InvariantCulture),
                AuditTrailChangeLog = changes,
            };
            this.Set<AuditTrailEntity>().Add(auditTrail);
        }

        public override int SaveChanges()
        {
            //Audit trailing automation
            if (_audit) this.AuditTrail();

            return base.SaveChanges();
        }
    }
}