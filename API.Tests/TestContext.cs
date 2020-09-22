using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interfaces;
using Domain.Contracts;
using Domain.Entities;

namespace API.Tests
{
    public class TestContext : IDatabaseContext 
    {
        private IDatabaseContext _databaseContextImplementation;

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
        public DbEntityEntry Entry(object entity)
        {
            return _databaseContextImplementation.Entry(entity);
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _databaseContextImplementation.Entry(entity);
        }

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return _databaseContextImplementation.GetValidationErrors();
        }

        public int SaveChanges()
        {
            return 1;
        }

        public Task<int> SaveChangesAsync()
        {
            return _databaseContextImplementation.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _databaseContextImplementation.SaveChangesAsync(cancellationToken);
        }

        public DbSet Set(Type entityType)
        {
            return _databaseContextImplementation.Set(entityType);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _databaseContextImplementation.Set<TEntity>();
        }

        public DbChangeTracker ChangeTracker => _databaseContextImplementation.ChangeTracker;

        public DbContextConfiguration Configuration => _databaseContextImplementation.Configuration;

        public Database Database => _databaseContextImplementation.Database;

        // public int SaveChanges(UserEntity user, CompanyEntity company)
        // {
        //     throw new NotImplementedException();
        // }
     

        public void MarkAsModified(BaseEntity item) { }
        public EntityState GetState(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }
    }
}