using System.Collections.Generic;

namespace LOGIC.Services.Interfaces
{
    public interface IGenericRelationshipService<TEntity>
    {
        bool Attach(int parentId, int childId);
        bool Detach(int parentId, int childId);

        TEntity GetById(int parentId, int childId);
        List<TEntity> GetAll(int parentId);
    }
}