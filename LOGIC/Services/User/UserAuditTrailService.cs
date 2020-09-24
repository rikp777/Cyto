using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DAL.Context;
using DAL.Migrations;
using DAL.Repository.AuditTrail;
using DAL.Repository.User;
using Domain.Audit;
using Domain.Entities;
using Domain.Resources;

namespace LOGIC.Services.User
{
    public class UserAuditTrailService
    {
        private readonly UserAuditTrailRepository _userAuditTrailRepository;
        public UserAuditTrailService(DatabaseContext context)
        {
            _userAuditTrailRepository = new UserAuditTrailRepository(context);
        }
        public List<AuditTrailResource> GetAll(int userId)
        {
            return _userAuditTrailRepository.GetAll(userId).Select(AuditTrailResource.FromEntity).ToList();
        }

        public AuditTrailResource GetById(int userId, int auditTrailId)
        {
            return AuditTrailResource.FromEntity(_userAuditTrailRepository.GetById(userId, auditTrailId));
        }
    }
}