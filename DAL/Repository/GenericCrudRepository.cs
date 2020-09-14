using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Contracts;

namespace DAL.Repository
{
    public abstract class GenericCrudRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IDatabaseContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected GenericCrudRepository(IDatabaseContext context, DbSet<TEntity> dbSet)
        {
            this.Context = context;
            this.DbSet = dbSet;
        }


        public List<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includedProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

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


        public TEntity GetById(int id)
        {
            TEntity data = DbSet.Find(id);
            return data;
        }


        public bool Create(TEntity entity)
        {
            DbSet.Add(entity);

            // return Save();
            return Context.Save();
        }


        public bool Delete(int id)
        {
            var entityToDelete = DbSet.Find(id);
            Console.WriteLine(entityToDelete);
            Delete(entityToDelete);
            // return Save();
            return Context.Save();
        }


        private void Delete(TEntity entityToDelete)
        {
            if (Context.GetState(entityToDelete) == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
            // return Save();
        }


        public bool Update(int id, TEntity entityToUpdate)
        {
            // _dbSet.Attach(entityToUpdate);
            // var temp = _dbSet.First(u => u.Id == id);
            // Console.WriteLine(temp);
            // temp = entityToUpdate;
            // _context.Entry(temp).Property(x=>x.Id).IsModified = false;
            // _context.Entry(temp).State = EntityState.Modified;

            // _context.Entry(entityToUpdate).State = EntityState.Modified;

            // var entity = DbSet.Find(id);
            // if (entity == null) return false;

            // Context.Entry(entity).CurrentValues.SetValues(entityToUpdate);

            // return Save();
            return Context.Save();
            
        }

        //
        // protected bool Save()
        // {
        //     var saved = Context.SaveChanges();
        //     return saved > 0;
        // }
    }
}