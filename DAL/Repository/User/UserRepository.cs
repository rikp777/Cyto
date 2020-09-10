using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.User
{
    public class UserRepository : GenericCrudRepository<UserEntity>, IGenericCrudRepository<UserEntity>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public UserEntity GetByName(string name)
        {
            var data = _dbSet.SingleOrDefault(user => user.Name == name);
            return data;
        }

        public UserEntity GetByEmail(string email)
        {
            var data = _dbSet.SingleOrDefault(user => user.Email == email);
            return data;
        }
    }
}