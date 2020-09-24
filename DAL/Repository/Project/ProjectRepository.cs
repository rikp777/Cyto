using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.AuditTrail;
using DAL.Repository.Interfaces;
using Domain.Audit;
using Domain.Entities;

namespace DAL.Repository.Project
{
    public class ProjectRepository : GenericCrudRepository<ProjectEntity>, IGenericCrudRepository<ProjectEntity>
    {
        private readonly IAuditTrailRepository _auditTrailRepository;
        private readonly IDatabaseContext _context;
        public ProjectRepository(IDatabaseContext context) : base(context, context.Projects)
        {
            _context = context;
            _auditTrailRepository = new AuditTrailRepository(context, "Project");
        }
        
        public bool SaveChanges(AuditTrailMetaData auditTrailMetaData)
        {
            _auditTrailRepository.Capture(auditTrailMetaData);
            return _context.SaveChanges() > 0;
        }
    }
}