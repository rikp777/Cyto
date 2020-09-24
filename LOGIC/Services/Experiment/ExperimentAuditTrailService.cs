using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.AuditTrail;
using DAL.Repository.Company;
using DAL.Repository.Experiment;
using Domain.Resources;

namespace LOGIC.Services.Experiment
{
    public class ExperimentAuditTrailService
    {
        private readonly ExperimentAuditTrailRepository _experimentAuditTrailRepository;
        public ExperimentAuditTrailService(DatabaseContext context)
        {
            _experimentAuditTrailRepository = new ExperimentAuditTrailRepository(context);
        }
        public List<AuditTrailResource> GetAll(int experimentId)
        {
            return _experimentAuditTrailRepository
                .GetAll(experimentId)
                .Select(AuditTrailResource.FromEntity)
                .ToList();
        }

        public AuditTrailResource GetById(int experimentId, int auditTrailId)
        {
            return AuditTrailResource.FromEntity(_experimentAuditTrailRepository.GetById(experimentId, auditTrailId));
        }
    }
}