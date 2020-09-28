using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.Company;
using DAL.Repository.Experiment;
using DAL.Repository.User;
using Domain.Audit;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Experiment
{
    public class ExperimentService : IGenericCrudService<ExperimentResource, ExperimentRequest>
    {
        private readonly ExperimentRepository _experimentRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly UserRepository _userRepository;

        public ExperimentService()
        {
            var context = new DatabaseContext();
            _userRepository = new UserRepository(context);
            _companyRepository = new CompanyRepository(context);
            _experimentRepository = new ExperimentRepository(context);
        }

        public ExperimentService(IDatabaseContext context)
        {
            _experimentRepository = new ExperimentRepository(context);
            _userRepository = new UserRepository(context);
            _companyRepository = new CompanyRepository(context);
            _experimentRepository = new ExperimentRepository(context);
        }


        public ExperimentResource GetById(int id)
        {
            var experimentEntity = _experimentRepository.GetById(id);
            return experimentEntity == null ? null : ExperimentResource.FromEntity(experimentEntity);
        }

        public List<ExperimentResource> GetAll()
        {
            return _experimentRepository
                .GetAll()
                .Select(ExperimentResource.FromEntity)
                .ToList();
        }

        public bool Create(ExperimentRequest entity, HttpContext current)
        {
            var auditMetaData = new AuditTrailMetaData()
            {
                User = _userRepository.GetById(1),
                Company = _companyRepository.GetById(1),
                Permission = new PermissionEntity(),
                RequestMethod = current.Request.HttpMethod,
                RequestBaseUrl = current.Request.Path,
                RequestIpAddress = current.Request.UserHostAddress
            };

            Console.WriteLine(auditMetaData.RequestMethod + "aaa");
            Console.WriteLine(auditMetaData.RequestBaseUrl + " bbb");
            Console.WriteLine(auditMetaData.RequestIpAddress + "vvv");

            var success = false;
            success = _experimentRepository.Create(ExperimentRequest.ToEntity(entity));
            success = _experimentRepository.SaveChanges(auditMetaData);
            return success;
        }

        public bool Update(int id, ExperimentRequest entity, HttpContext current)
        {
            var entityOld = _experimentRepository.GetById(id, new List<string>() {"Project.Company"});
            //var company = entityOld.Project.Company;
            var auditMetaData = new AuditTrailMetaData()
            {
                User = _userRepository.GetById(1),
                Company = _companyRepository.GetById(1),
                Permission = new PermissionEntity(),
                RequestMethod = current.Request.HttpMethod,
                RequestBaseUrl = current.Request.Path,
                RequestIpAddress = current.Request.UserHostAddress
            };
            //var update = ExperimentRequest.ToEntity(entity);
            //update.Id = id;
            entityOld.Name = entity.Name;
            entityOld.Description = entity.Description;


            var success = false;
            success = _experimentRepository.Update(id, entityOld);
            success = _experimentRepository.SaveChanges(auditMetaData);
            return success;
        }

        public bool Delete(int id, HttpContext current)
        {
            var auditMetaData = new AuditTrailMetaData()
            {
                User = _userRepository.GetById(1),
                Company = _companyRepository.GetById(1),
                RequestMethod = current.Request.HttpMethod,
                RequestBaseUrl = current.Request.ToString(),
                RequestIpAddress = current.Request.UserHostAddress
            };

            _experimentRepository.Delete(id);
            return _experimentRepository.SaveChanges(auditMetaData);
        }
    }
}