using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Context;
using DAL.Repository.Project;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Project
{
    public class ProjectService : IGenericCrudService<ProjectResource, ProjectRequest>
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectService()
        {
            _projectRepository = new ProjectRepository(new DatabaseContext());
        }

        public ProjectResource GetById(int id) => ProjectResource.FromEntity(_projectRepository.GetById(id));

        public List<ProjectResource> GetAll(int size, int page) => _projectRepository
            .GetAll()
            .Select(ProjectResource.FromEntity)
            .ToList();

        public bool Create(ProjectRequest entity, HttpContext current = null) => _projectRepository
            .Create(ProjectRequest.ToEntity(entity));


        public bool Update(int id, ProjectRequest entity, HttpContext current = null) => _projectRepository
            .Update(id, ProjectRequest.ToEntity(entity));


        public bool Delete(int id, HttpContext current = null) => _projectRepository
            .Delete(id);
    }
}