using System.Collections.Generic;

namespace DAL.Repository.Interfaces
{
    public interface IGenericRelationshipRepository<TEntity>
    {
        bool Attach(int parentId, int childId);
        bool Detach(int parentId, int childId);
        TEntity GetById(int parentId, int childId);
        List<TEntity> GetAll(int parentId);
    }
}