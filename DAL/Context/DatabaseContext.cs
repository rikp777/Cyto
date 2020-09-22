using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.Linq;
using DAL.Builder;
using DAL.Interfaces;
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

        // public List<AuditTrailChangeLogEntity> getChanges()
        // {
        //     var changes = new List<AuditTrailChangeLogEntity>();
        //     var modifiedEntities = ChangeTracker.Entries()
        //         .Where(p => p.State == EntityState.Modified)
        //         .ToList();
        //
        //     if (modifiedEntities.Count > 0)
        //     {
        //         foreach (var change in modifiedEntities)
        //         {
        //             foreach(var columnName in change.OriginalValues.PropertyNames)
        //             {
        //                 var originalValue = change.OriginalValues[columnName].ToString();
        //                 var currentValue = change.CurrentValues[columnName].ToString();
        //                 if (originalValue != currentValue)
        //                 {
        //                     var auditChangeLog = new AuditTrailChangeLogEntity
        //                     {
        //                         ColumnName = columnName,
        //                         ValueBefore = originalValue, 
        //                         ValueAfter = currentValue
        //                     };
        //                     changes.Add(auditChangeLog);
        //                 
        //                     Console.WriteLine(@"Audit Trail: 'Value Changed': [Original value: " + originalValue + @"] - [Current value: " + currentValue + @"] - [Column name: " + columnName + @"]");
        //                 }
        //             }
        //         }
        //     }
        //
        //     return changes;
        // }
        //
        // public string getPrimarykey(ObjectStateEntry state)
        // {
        //     SaveChanges();
        //     var key =  state.EntityKey.EntityKeyValues[0].Value.ToString();
        //     return key;
        // }
        //
        // public void AuditTrail(UserEntity user, CompanyEntity company, PermissionEntity permission = null, string serviceName = null, string methodName = null)
        // {
        //     if(user == null) throw new Exception("AuditTrail Error: User is mandatory");
        //     if(company == null) throw new Exception("AuditTrail Error: Company is mandatory");
        //     
        //     
        //     var states = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted);
        //     var now = DateTime.UtcNow;
        //
        //     foreach (var state in states)
        //     {
        //         var tableName = state.Entity.GetType().Name;
        //
        //         switch (state.State)
        //         {
        //             case EntityState.Added:
        //             {
        //                 var auditTrailEntity = new AuditTrailEntity()
        //                 {
        //                     User = user,
        //                     Company = company,
        //                     Permission = permission,
        //         
        //                     ServiceName = serviceName,
        //                     MethodName = methodName,
        //                     MethodColor = "Green",
        //                     ActionType = AuditActionType.Create,
        //
        //                     TableName = tableName,
        //                     PrimaryKey = getPrimarykey(state),
        //         
        //                     CreatedAt = now.ToString(CultureInfo.InvariantCulture),
        //                 };
        //                 Set<AuditTrailEntity>().Add(auditTrailEntity);
        //                 break;
        //             }
        //             case EntityState.Deleted:
        //             {
        //                 var auditTrailDelete = new AuditTrailEntity()
        //                 {
        //                     User = user,
        //                     Company = company,
        //                     Permission = permission,
        //         
        //                     ServiceName = serviceName,
        //                     MethodName = methodName,
        //                     MethodColor = "Red",
        //                     ActionType = AuditActionType.Delete,
        //
        //                     TableName = tableName,
        //                     PrimaryKey = getPrimarykey(state),
        //         
        //                     CreatedAt = now.ToString(CultureInfo.InvariantCulture),
        //                 };
        //                 Set<AuditTrailEntity>().Add(auditTrailDelete);
        //                 break;
        //             }
        //             case EntityState.Modified:
        //             {
        //                 var auditTrailUpdate = new AuditTrailEntity()
        //                 {
        //                     User = user,
        //                     Company = company,
        //                     Permission = permission,
        //         
        //                     ServiceName = serviceName,
        //                     MethodName = methodName,
        //                     MethodColor = "Yellow",
        //                     ActionType = AuditActionType.Update,
        //                 
        //                     TableName = tableName,
        //                     AuditTrailChangeLog = getChanges(),
        //                     PrimaryKey = getPrimarykey(state),
        //
        //                     CreatedAt = now.ToString(CultureInfo.InvariantCulture),
        //                 };
        //                 Set<AuditTrailEntity>().Add(auditTrailUpdate);
        //                 break;
        //             }
        //         }
        //     }
        //     
        // }
        //
        //
        // //TODO add permission
        // public int SaveChanges(UserEntity user, CompanyEntity company, PermissionEntity permission = null, string serviceName = null, string methodName = null)
        // {
        //     AuditTrail(user, company, permission, serviceName, methodName);
        //     return SaveChanges();
        // }
        // public int SaveChanges(UserEntity user, CompanyEntity company)
        // {
        //     AuditTrail(user, company);
        //     return SaveChanges();
        // }
    }
}