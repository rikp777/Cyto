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
        public UserRepository(IDatabaseContext context) : base(context, context.Users)
        {
        }

        public UserEntity GetByName(string name)
        {
            var data = DbSet.SingleOrDefault(user => user.Name == name);
            return data;
        }

        public UserEntity GetByEmail(string email)
        {
            var data = DbSet.SingleOrDefault(user => user.Email == email);
            return data;
        }
    }
}