using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Permission;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Permission
{
    public class PermissionService : IGenericCrudService<PermissionResource, PermissionRequest>
    {
        private readonly PermissionRepository _permissionRepository;
        public PermissionService()
        {
            _permissionRepository = new PermissionRepository(new DatabaseContext());
        }

        public PermissionResource GetById(int id) => PermissionResource.FromEntity(_permissionRepository.GetById(id));
        public List<PermissionResource> GetAll(int size, int page) =>_permissionRepository
            .GetAll().Skip(size * (page -1)).Take(size)
            .Select(PermissionResource.FromEntity)
            .ToList();

        
        
        public bool Create(PermissionRequest entity) => _permissionRepository
            .Create(PermissionRequest.ToEntity(entity));
        
        
        public bool Update(int id, PermissionRequest entity) => _permissionRepository
            .Update(id, PermissionRequest.ToEntity(entity));
        
        
        public bool Delete(int id) => _permissionRepository.Delete(id);
    }
}