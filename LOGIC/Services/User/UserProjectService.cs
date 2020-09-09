using System.Collections.Generic;
using DAL.Context;
using DAL.Repository.User;
using Domain.Entities;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.User
{
    public class UserProjectService : IGenericRelationshipService<ProjectEntity>
    {
        private readonly UserProjectRepository _userProjectRepository;
        public UserProjectService()
        {
            _userProjectRepository = new UserProjectRepository(new DatabaseContext());
        }
        
        public bool Attach(int userId, int projectId) => _userProjectRepository.Attach(userId, projectId);
        public bool Detach(int userId, int projectId) => _userProjectRepository.Detach(userId, projectId);
        
        public ProjectEntity GetById(int userId, int projectId) => _userProjectRepository.GetById(userId, projectId);
        public List<ProjectEntity> GetAll(int userId) => _userProjectRepository.GetAll(userId);
    }
}