using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.User
{
    public class UserRepository :  GenericCrudRepository<UserEntity>, IGenericCrudRepository<UserEntity>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}