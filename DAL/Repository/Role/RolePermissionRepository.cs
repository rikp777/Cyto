using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository
{
    public class RolePermissionRepository : IGenericRelationshipRepository<PermissionEntity>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<RoleEntity> _dbSetRoles;
        private readonly DbSet<PermissionEntity> _dbSetPermissions;

        public RolePermissionRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetRoles = context.Set<RoleEntity>();
            this._dbSetPermissions = context.Set<PermissionEntity>();
        }

        public PermissionEntity Attach(int roleId, int permissionId)
        {
            var role = _dbSetRoles.Find(roleId);
            var permission = _dbSetPermissions.Find(permissionId);

            role?.Permissions.Add(permission);

            _context.SaveChanges();
            return permission;
        }

        public PermissionEntity Detach(int roleId, int permissionId)
        {
            var role = _dbSetRoles.Find(roleId);
            var permission = _dbSetPermissions.Find(permissionId);

            role?.Permissions.Remove(permission);

            _context.SaveChanges();
            return permission;
        }

        public PermissionEntity GetById(int roleId, int permissionId)
        {
            var role = _dbSetRoles.Find(roleId);

            return role?.Permissions.First(p => p.Id == permissionId);
        }

        public List<PermissionEntity> GetAll(int roleId)
        {
            var role = _dbSetRoles.Find(roleId);

            return role?.Permissions.ToList();
        }
    }
}