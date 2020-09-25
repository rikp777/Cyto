using System.Collections.Generic;
using System.Web;

namespace LOGIC.Services.Interfaces
{
    public interface IGenericCrudService<TEntityResource, TEntityRequest>
    {
        TEntityResource GetById(int id);
        List<TEntityResource> GetAll();
        
        bool Create(TEntityRequest entity, HttpContext current = null);
        bool Update(int id, TEntityRequest entity, HttpContext current = null);
        bool Delete(int id, HttpContext current = null);
    }
}