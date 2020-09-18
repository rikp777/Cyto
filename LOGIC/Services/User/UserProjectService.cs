using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.User;
using Domain.Entities;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.User
{
    public class UserProjectService : IGenericRelationshipService<ProjectResource>
    {
        private readonly UserProjectRepository _userProjectRepository;

        public UserProjectService()
        {
            _userProjectRepository = new UserProjectRepository(new DatabaseContext());
        }

        public ProjectResource Attach(int userId, int projectId) =>
            ProjectResource.FromEntity(_userProjectRepository.Attach(userId, projectId));

        public ProjectResource Detach(int userId, int projectId) =>
            ProjectResource.FromEntity(_userProjectRepository.Detach(userId, projectId));

        public ProjectResource GetById(int userId, int projectId) =>
            ProjectResource.FromEntity(_userProjectRepository.GetById(userId, projectId));

        public List<ProjectResource> GetAll(int userId) => _userProjectRepository.GetAll(userId)
            ?.Select(ProjectResource.FromEntity).ToList();
    }
}