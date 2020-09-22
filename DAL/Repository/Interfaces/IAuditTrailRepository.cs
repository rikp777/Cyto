using Domain.Audit;
using Domain.Entities;

namespace DAL.Repository.Interfaces
{
    public interface IAuditTrailRepository
    {
        bool Capture(AuditTrailMetaData auditTrailMetaData);
    }
}