using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository;
using Domain.Entities;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Role
{
    public class RolePermissionService : IGenericRelationshipService<PermissionResource>
    {
        private readonly RolePermissionRepository _rolePermissionRepository;
        public RolePermissionService()
        {
            _rolePermissionRepository = new RolePermissionRepository(new DatabaseContext());
        }
        
        public bool Attach(int roleId, int permissionId) => _rolePermissionRepository.Attach(roleId, permissionId);
        public bool Detach(int roleId, int permissionId) => _rolePermissionRepository.Detach(roleId, permissionId);
        
        public PermissionResource GetById(int roleId, int permissionId) => PermissionResource.FromEntity(_rolePermissionRepository.GetById(roleId, permissionId));
        public List<PermissionResource> GetAll(int roleId) => _rolePermissionRepository.GetAll(roleId).Select(PermissionResource.FromEntity).ToList();
    }
}