using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Company;
using DAL.Repository.Experiment;
using DAL.Repository.User;
using Domain.Audit;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Audit_Trail;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Experiment
{
    public class ExperimentService : IGenericCrudService<ExperimentResource, ExperimentRequest>
    {
        private readonly ExperimentRepository _experimentRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly AuditTrailService _auditTrailService;
        private readonly UserRepository _userRepository;
        
        public ExperimentService()
        {
            DatabaseContext context = new DatabaseContext();
            _companyRepository = new CompanyRepository(context);
            _auditTrailService = new AuditTrailService(context);
            _userRepository = new UserRepository(context);
            
            _experimentRepository = new ExperimentRepository(context);
        }

        public ExperimentResource GetById(int id) => ExperimentResource.FromEntity(_experimentRepository.GetById(id));
        public List<ExperimentResource> GetAll(int size, int page) =>_experimentRepository
            .GetAll().Skip(size * (page -1)).Take(size)
            .Select(ExperimentResource.FromEntity)
            .ToList();

        public bool Create(ExperimentRequest entity) => _experimentRepository.Create(ExperimentRequest.ToEntity(entity));
        public bool Update(int id, ExperimentRequest entity)
        {
            var authUser = _userRepository.GetById(2);
            var entityOld = _experimentRepository.GetById(id, new List<string>(){"Project", "Company"});
            var entityNew = ExperimentRequest.ToEntity(entity);
            var company = _companyRepository.GetById(1);
            
            var changes = new List<AuditTrailChangeLogEntity>();
            var one = new AuditTrailChangeLogEntity("Name", entityOld.Name, entityNew.Name);
            if (one.Changed) changes.Add(one);
            
            var two = new AuditTrailChangeLogEntity("Description", entityOld.Description, entityNew.Description);
            if (two.Changed) changes.Add(two);
            
            var update = ExperimentRequest.ToEntity(entity);
            update.Id = id;
            var success = _experimentRepository.Update(id, update);
            if (success) _auditTrailService.Capture(authUser, company, null, (AuditActionType) 2, "ExperimentService", "Update", entityOld, changes);

            return true;
        } 
        
        
        public bool Delete(int id) => _experimentRepository.Delete(id);
    }
}