using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;
using TrackerEnabledDbContext.Common.Interfaces;

namespace DAL.Repository
{
    public abstract class GenericCrudRepository<TEntity> where TEntity : class 
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected GenericCrudRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public List<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includedProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includedProperties);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        
        
        public TEntity GetById(
            int id,
            List<string> includes = null
        )
        {
            var dbSet = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    dbSet.Include(include);
                }
            }
            TEntity data = dbSet.Find(id);
            
            return data;
        }

        
        
        public bool Create(TEntity entity)
        {
            _dbSet.Add(entity);

            return true;
        }

        
        
        public bool Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            return true;
        }

        
        
        public bool Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
            return true;
        }

        
        
        public bool Update(int id, TEntity entityToUpdate)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return false;
            
            _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);

            return true;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Save(UserEntity user, CompanyEntity company)
        {
            return _context.SaveChanges(user, company) > 0;
        }
    }
}