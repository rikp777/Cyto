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
    public abstract class GenericCrudRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IDatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected GenericCrudRepository(IDatabaseContext context, DbSet<TEntity> dbSet)
        {
            this._context = context;
            this._dbSet = dbSet;
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

            foreach (var includeProperty in includedProperties.Split(new char[] {','},
                StringSplitOptions.RemoveEmptyEntries))
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

            // return Save();
            return Context.Save();
        }


        public bool Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            // return Save();
            return Context.Save();
        }


        private void Delete(TEntity entityToDelete)
        {
            if (_context.GetState(entityToDelete) == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
            // return Save();
        }

        
        
        public bool Update(int id, TEntity entityToUpdate)
        {

            var entity = _dbSet.Find(id);
            if (entity == null) return false;
            
            _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);

            return Save();
            // _dbSet.Attach(entityToUpdate);
            // _context.Entry(entityToUpdate).State = EntityState.Modified;
            //
            // return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}