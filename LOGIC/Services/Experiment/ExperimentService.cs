using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            _userRepository = new UserRepository(context);
            _companyRepository = new CompanyRepository(context);
            _auditTrailService = new AuditTrailService(context);

            _experimentRepository = new ExperimentRepository(context);
        }

        public ExperimentService(IDatabaseContext context)
        {
            _experimentRepository = new ExperimentRepository(context);
        }

        public ExperimentResource GetById(int id)
        {
            var experimentEntity = _experimentRepository.GetById(id);
            return experimentEntity == null ? null : ExperimentResource.FromEntity(experimentEntity);
        }

        public List<ExperimentResource> GetAll(int size, int page) => _experimentRepository
            .GetAll().Skip(size * (page -1)).Take(size)
            .Select(ExperimentResource.FromEntity)
            .ToList();

        public bool Create(ExperimentRequest entity) => _experimentRepository.Create(ExperimentRequest.ToEntity(entity));
        public bool Update(int id, ExperimentRequest entity)
        {
            var user = _userRepository.GetById(2);
            var entityOld = _experimentRepository.GetById(id, new List<string>(){"Project.Company"});
            var company = entityOld.Project.Company;

            
            var update = ExperimentRequest.ToEntity(entity);
            update.Id = id;
            
            _experimentRepository.Update(id, update);
            _experimentRepository.Save(user, company);
            
            return true;
        } 
        
        
        public bool Delete(int id) => _experimentRepository.Delete(id);
    }
}