using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.Company;
using DAL.Repository.Experiment;
using DAL.Repository.Project;
using DAL.Repository.User;
using Domain.Audit;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Project
{
    public class ProjectService : IGenericCrudService<ProjectResource, ProjectRequest>
    {
        private readonly ProjectRepository _projectRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly UserRepository _userRepository;
        public ProjectService()
        {
            var context = new DatabaseContext();
            _userRepository = new UserRepository(context);
            _companyRepository = new CompanyRepository(context);
            _projectRepository = new ProjectRepository(context);
        }
        
        public ProjectService(IDatabaseContext context)
        {
            _projectRepository = new ProjectRepository(context);
            _userRepository = new UserRepository(context);
            _companyRepository = new CompanyRepository(context);
        }

        public ProjectResource GetById(int id)
        {
            var projectEntity = _projectRepository.GetById(id);
            return projectEntity == null ? null : ProjectResource.FromEntity(projectEntity);
        }

        public List<ProjectResource> GetAll(int size, int page) => _projectRepository
            .GetAll()
            .Select(ProjectResource.FromEntity)
            .ToList();

        public bool Create(ProjectRequest entity, HttpContext current)
        {
            var auditMetaData = new AuditTrailMetaData()
            {
                User = _userRepository.GetById(1),
                Company = _companyRepository.GetById(1),
                Permission = new PermissionEntity(){Id = 1},
                RequestMethod = current.Request.HttpMethod,
                RequestBaseUrl = current.Request.ToString(),
                RequestIpAddress = current.Request.UserHostAddress
            };
            
            var success = false;
            success = _projectRepository.Create(ProjectRequest.ToEntity(entity));
            success = _projectRepository.SaveChanges(auditMetaData);
            return success;
        }


        public bool Update(int id, ProjectRequest entity, HttpContext current = null)
        {
            var auditMetaData = new AuditTrailMetaData()
            {
                User = _userRepository.GetById(1),
                Company = _companyRepository.GetById(1),
                Permission = new PermissionEntity(){Id = 1},
                RequestMethod = current.Request.HttpMethod,
                RequestBaseUrl = current.Request.Path,
                RequestIpAddress = current.Request.UserHostAddress
            };
            var update = ProjectRequest.ToEntity(entity);
            update.Id = id;
            
            var success = false;
            success = _projectRepository.Update(id, update);
            success = _projectRepository.SaveChanges(auditMetaData);
            return success;
        } 


        public bool Delete(int id, HttpContext current)
        {
            var auditMetaData = new AuditTrailMetaData()
            {
                User = _userRepository.GetById(1),
                Company = _companyRepository.GetById(1),
                RequestMethod = current.Request.HttpMethod,
                RequestBaseUrl = current.Request.ToString(),
                RequestIpAddress = current.Request.UserHostAddress
            };
            
            var success = false;
            success = _projectRepository.Delete(id);
            success = _projectRepository.SaveChanges(auditMetaData);
            return success;
        }
    }
}