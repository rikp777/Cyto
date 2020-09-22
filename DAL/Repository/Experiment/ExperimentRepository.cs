using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.AuditTrail;
using DAL.Repository.Interfaces;
using Domain.Audit;
using Domain.Entities;

namespace DAL.Repository.Experiment
{
    public class ExperimentRepository : GenericCrudRepository<ExperimentEntity>, IGenericCrudRepository<ExperimentEntity>
    {
        private readonly IAuditTrailRepository _auditTrailRepository;
        private readonly IDatabaseContext _context;
        public ExperimentRepository(IDatabaseContext context) : base(context, context.Experiments)
        {
            _context = context;
            _auditTrailRepository = new AuditTrailRepository(context);
        }

        public bool SaveChanges(AuditTrailMetaData auditTrailMetaData)
        {
            _auditTrailRepository.Capture(auditTrailMetaData);
            return _context.SaveChanges() > 0;
        }
    }
}