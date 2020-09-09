using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Project;
using Domain.Entities;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Project
{
    public class ProjectExperimentService : IGenericRelationshipService<ExperimentResource>
    {
        private readonly ProjectExperimentRepository _projectExperimentRepository;
        public ProjectExperimentService()
        {
            _projectExperimentRepository = new ProjectExperimentRepository(new DatabaseContext());
        }

        public bool Attach(int projectId, int experimentId) => _projectExperimentRepository.Attach(projectId, experimentId);
        public bool Detach(int projectId, int experimentId) => _projectExperimentRepository.Detach(projectId, experimentId);
        
        public ExperimentResource GetById(int projectId, int experimentId) => ExperimentResource.FromEntity(_projectExperimentRepository.GetById(projectId, experimentId));
        public List<ExperimentResource> GetAll(int projectId) => _projectExperimentRepository
            .GetAll(projectId)
            .Select(ExperimentResource.FromEntity)
            .ToList();

    }
}