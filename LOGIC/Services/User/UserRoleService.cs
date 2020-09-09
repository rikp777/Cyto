using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.User;
using Domain.Entities;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.User
{
    public class UserRoleService : IGenericRelationshipService<RoleResource>
    {
        private readonly UserRoleRepository _userRoleRepository;
        public UserRoleService()
        {
            _userRoleRepository = new UserRoleRepository(new DatabaseContext());
        }

        public bool Attach(int userId, int roleId) => _userRoleRepository.Attach(userId, roleId);
        public bool Detach(int userId, int roleId) => _userRoleRepository.Detach(userId, roleId);
        
        public RoleResource GetById(int userId, int roleId) =>RoleResource.FromEntity(_userRoleRepository.GetById(userId, roleId));
        public List<RoleResource> GetAll(int userId) => _userRoleRepository.GetAll(userId).Select(RoleResource.FromEntity).ToList();
    }
}