using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Company;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Company
{
    public class CompanyProjectService : IGenericRelationshipService<ProjectResource>
    {
        private readonly CompanyProjectRepository _companyProjectRepository;

        public CompanyProjectService()
        {
            _companyProjectRepository = new CompanyProjectRepository(new DatabaseContext());
        }

        public CompanyProjectService(IDatabaseContext context)
        {
            _companyProjectRepository = new CompanyProjectRepository(context);
        }

        public ProjectResource Attach(int companyId, int projectId)
        {
            var projectEntity = _companyProjectRepository.Attach(companyId, projectId);
            return projectEntity == null ? null : ProjectResource.FromEntity(projectEntity);
        }

        public ProjectResource Detach(int companyId, int projectId)
        {
            var projectEntity = _companyProjectRepository.Detach(companyId, projectId);
            return projectEntity == null ? null : ProjectResource.FromEntity(projectEntity);
        }

        public ProjectResource GetById(int companyId, int projectId)
        {
            var projectEntity = _companyProjectRepository.GetById(companyId, projectId);
            return projectEntity == null ? null : ProjectResource.FromEntity(projectEntity);
        }

        public List<ProjectResource> GetAll(int companyId)
            => _companyProjectRepository.GetAll(companyId)
                ?.Select(ProjectResource.FromEntity).ToList();
    }
}