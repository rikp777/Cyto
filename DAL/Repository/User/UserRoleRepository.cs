using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.User
{
    public class UserRoleRepository : IGenericRelationshipRepository<RoleEntity>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserEntity> _dbSetUsers;
        private readonly DbSet<RoleEntity> _dbSetRoles;

        public UserRoleRepository(DatabaseContext context)
        {
            _context = context;
            _dbSetUsers = context.Set<UserEntity>();
            _dbSetRoles = context.Set<RoleEntity>();
        }

        public RoleEntity Attach(int userId, int roleId)
        {
            var user = _dbSetUsers.Find(userId);
            var role = _dbSetRoles.Find(roleId);

            user?.Roles.Add(role);

            _context.Save();
            return role;
        }


        public RoleEntity Detach(int userId, int roleId)
        {
            var user = _dbSetUsers.Find(userId);
            var role = _dbSetRoles.Find(roleId);

            user?.Roles.Remove(role);

            _context.Save();
            return role;
        }

        public RoleEntity GetById(int userId, int roleId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.Roles.First(r => r.Id == roleId);
        }

        public List<RoleEntity> GetAll(int userId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.Roles.ToList();
        }
    }
}