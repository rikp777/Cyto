using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Company;
using DAL.Repository.User;
using Domain.Audit;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Audit_Trail;
using LOGIC.Services.Interfaces;
using LOGIC.Services.User;

namespace LOGIC.Services.Company
{
    public class CompanyService : IGenericCrudService<CompanyResource, CompanyRequest>
    {
        private readonly CompanyRepository _companyRepository;
        private readonly AuditTrailService _auditTrailService;
        private readonly UserRepository _userRepository;
        public CompanyService()
        {
            DatabaseContext context = new DatabaseContext();
            _companyRepository = new CompanyRepository(context);
            _auditTrailService = new AuditTrailService(context);
            _userRepository = new UserRepository(context);
        }

        public CompanyResource GetById(int id) => CompanyResource.FromEntity(_companyRepository.GetById(id));
        public List<CompanyResource> GetAll(int size, int page) =>_companyRepository
            .GetAll().Skip(size * (page -1)).Take(size)
            .Select(CompanyResource.FromEntity)
            .ToList();

        public bool Create(CompanyRequest entity) => _companyRepository.Create(CompanyRequest.ToEntity(entity));
        public bool Update(int id, CompanyRequest entity)
        {
            
            var entityOld = _companyRepository.GetById(id);
            var entityNew = CompanyRequest.ToEntity(entity);
            var authUser = _userRepository.GetById(1);
            var company = _companyRepository.GetById(1);

            List<AuditTrailChangeLogEntity> changes = new List<AuditTrailChangeLogEntity>();

            var one = new AuditTrailChangeLogEntity("Name", entityOld.Name, entityNew.Name);
            if (one.Changed) changes.Add(one);
            
            var two = new AuditTrailChangeLogEntity("Description", entityOld.Description, entityNew.Description);
            if (two.Changed) changes.Add(two);
            
            var update = CompanyRequest.ToEntity(entity);
            update.Id = id;
             bool success = _companyRepository.Update(id, update);
             if (success) _auditTrailService.Capture(authUser, company, null, (AuditActionType) 1, "CompanyService", "Update", entityOld, changes);

            return true;
        } 
        public bool Delete(int id) => _companyRepository.Delete(id);
    }
}