using System.Collections.Generic;

namespace LOGIC.Services.Interfaces
{
    public interface IGenericRelationshipService<TEntity>
    {
        TEntity Attach(int parentId, int childId);
        TEntity Detach(int parentId, int childId);

        TEntity GetById(int parentId, int childId);
        List<TEntity> GetAll(int parentId);
    }
}