using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository
{
    public class RoleRepository : GenericCrudRepository<RoleEntity>, IGenericCrudRepository<RoleEntity>
    {
        public RoleRepository(DatabaseContext context) : base(context)
        {
        }
    }
}