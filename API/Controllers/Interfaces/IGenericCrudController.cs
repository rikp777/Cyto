using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Antlr.Runtime.Misc;

namespace API.Controllers.Interfaces
{
    public interface IGenericCrudController<TEntity>
    {
        List<TEntity> GetAll( Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includedProperties = "");
        TEntity GetById(int id);

        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}