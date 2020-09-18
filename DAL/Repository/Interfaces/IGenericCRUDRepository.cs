using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repository.Interfaces
{
    public interface IGenericCrudRepository<TEntity>
    {
        List<TEntity> GetAll( Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includedProperties = "");
        TEntity GetById(int id, List<string> includes = null);

        bool Create(TEntity entity);
        bool Update(int id, TEntity entity);
        bool Delete(int id);
        bool Save();
    }
}