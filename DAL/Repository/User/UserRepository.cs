using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.User
{
    public class UserRepository : GenericCrudRepository<UserEntity>, IGenericCrudRepository<UserEntity>
    {
        protected readonly DbSet<UserEntity> _dbSet;
        public UserRepository(IDatabaseContext context) : base(context, context.Users)
        {
            _dbSet = context.Users;
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