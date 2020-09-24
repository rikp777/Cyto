using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Experiment;
using DAL.Repository.Project;
using Domain.Resources;

namespace LOGIC.Services.Project
{
    public class ProjectAuditTrailService
    {
        private readonly ProjectAuditTrailRepository _projectAuditTrailRepository;
        public ProjectAuditTrailService(DatabaseContext context)
        {
            _projectAuditTrailRepository = new ProjectAuditTrailRepository(context);
        }
        public List<AuditTrailResource> GetAll(int experimentId)
        {
            return _projectAuditTrailRepository
                .GetAll(experimentId)
                .Select(AuditTrailResource.FromEntity)
                .ToList();
        }

        public AuditTrailResource GetById(int experimentId, int auditTrailId)
        {
            return AuditTrailResource.FromEntity(_projectAuditTrailRepository.GetById(experimentId, auditTrailId));
        }
    }
}