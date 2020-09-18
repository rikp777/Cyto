using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Interfaces;
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
        
        public CompanyService(IDatabaseContext context)
        {
            _companyRepository = new CompanyRepository(context);
            _auditTrailService = new AuditTrailService(context);
            _userRepository = new UserRepository(context);
        }
        
 


        public CompanyResource GetById(int id)
        {
            var companyEntity = _companyRepository.GetById(id);
            return companyEntity == null ? null : CompanyResource.FromEntity(companyEntity);
        }
        
        public List<CompanyResource> GetAll() => _companyRepository
            .GetAll()
            .Select(CompanyResource.FromEntity)
            .ToList();

        public bool Create(CompanyRequest entity) => _companyRepository.Create(CompanyRequest.ToEntity(entity));

        public bool Update(int id, CompanyRequest entity)
        {
            
            //var entityOld = _companyRepository.GetById(id);
            //var entityNew = CompanyRequest.ToEntity(entity);
            //var authUser = _userRepository.GetById(1);
           // var company = _companyRepository.GetById(1);

            //List<AuditTrailChangeLogEntity> changes = new List<AuditTrailChangeLogEntity>();

            //var one = new AuditTrailChangeLogEntity("Name", entityOld.Name, entityNew.Name);
            //if (one.Changed) changes.Add(one);
            
            //var two = new AuditTrailChangeLogEntity("Description", entityOld.Description, entityNew.Description);
            //if (two.Changed) changes.Add(two);
            
            var update = CompanyRequest.ToEntity(entity);
            update.Id = id;
             bool success = _companyRepository.Update(id, update);
             //if (success) _auditTrailService.Capture(authUser, company, null, (AuditActionType) 1, "CompanyService", "Update", entityOld.Id.ToString(), entityOld, changes);

            return true;
        } 
        public bool Delete(int id) => _companyRepository.Delete(id);

        public CompanyResource GetByName(string name)
        {
            var companyEntity = _companyRepository.GetByName(name);
            return companyEntity == null ? null : CompanyResource.FromEntity(companyEntity);
        }
    }
}