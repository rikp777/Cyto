using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Context;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public abstract class GenericCrudRepository<TEntity> where TEntity : class 
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected GenericCrudRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
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

        
        
        public TEntity GetById(int id)
        {
            TEntity data = _dbSet.Find(id);
            return data;
        }

        
        
        public bool Create(TEntity entity)
        {
            _dbSet.Add(entity);

            return Save();
        }

        
        
        public bool Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            return Save();
        }

        
        
        public bool Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
            return Save();
        }

        
        
        public bool Update(int id, TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}