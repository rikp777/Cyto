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

        public RoleResource Attach(int userId, int roleId) =>
            RoleResource.FromEntity(_userRoleRepository.Attach(userId, roleId));

        public RoleResource Detach(int userId, int roleId) =>
            RoleResource.FromEntity(_userRoleRepository.Detach(userId, roleId));

        public RoleResource GetById(int userId, int roleId) =>
            RoleResource.FromEntity(_userRoleRepository.GetById(userId, roleId));

        public List<RoleResource> GetAll(int userId) =>
            _userRoleRepository.GetAll(userId).Select(RoleResource.FromEntity).ToList();
    }
}