using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.AuditTrail;
using DAL.Repository.Company;
using Domain.Resources;

namespace LOGIC.Services.Company
{
    public class CompanyAuditTrailService
    {
        private readonly CompanyAuditTrailRepository _companyAuditTrailRepository;
        public CompanyAuditTrailService(DatabaseContext context)
        {
            _companyAuditTrailRepository = new CompanyAuditTrailRepository(context);
        }
        public List<AuditTrailResource> GetAll(int companyId)
        {
            return _companyAuditTrailRepository
                .GetAll(companyId)
                .Select(AuditTrailResource.FromEntity)
                .ToList();
        }

        public AuditTrailResource GetById(int companyId, int auditTrailId)
        {
            return AuditTrailResource.FromEntity(_companyAuditTrailRepository.GetById(companyId, auditTrailId));
        }
    }
}