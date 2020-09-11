using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Role
{
    public class RoleService : IGenericCrudService<RoleResource, RoleRequest>
    {
        private readonly RoleRepository _roleRepository;

        public RoleService()
        {
            _roleRepository = new RoleRepository(new DatabaseContext());
        }

        public RoleResource GetById(int id) => RoleResource.FromEntity(_roleRepository.GetById(id));

        public List<RoleResource> GetAll() => _roleRepository
            .GetAll()
            .Select(RoleResource.FromEntity)
            .ToList();


        public bool Create(RoleRequest entity) => _roleRepository
            .Create(RoleRequest.ToEntity(entity));


        public bool Update(int id, RoleRequest entity) => _roleRepository
            .Update(id, RoleRequest.ToEntity(entity));


        public bool Delete(int id) => _roleRepository
            .Delete(id);
    }
}