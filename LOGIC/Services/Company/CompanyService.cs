using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Company;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Company
{
    public class CompanyService : IGenericCrudService<CompanyResource, CompanyRequest>
    {
        private readonly CompanyRepository _companyRepository;

        public CompanyService()
        {
            _companyRepository = new CompanyRepository(new DatabaseContext());
        }

        public CompanyResource GetById(int id)
        {
            var companyEntity = _companyRepository.GetById(id);
            return companyEntity == null ? null : CompanyResource.FromEntity(_companyRepository.GetById(id));
        }

        // public List<CompanyResource> GetAll(int size, int page) =>_companyRepository
        //     .GetAll().Skip(size * (page -1)).Take(size)
        //     .Select(CompanyResource.FromEntity)
        //     .ToList();
        public List<CompanyResource> GetAll() => _companyRepository
            .GetAll()
            .Select(CompanyResource.FromEntity)
            .ToList();

        public bool Create(CompanyRequest entity) => _companyRepository.Create(CompanyRequest.ToEntity(entity));

        public bool Update(int id, CompanyRequest entity) =>
            _companyRepository.Update(id, CompanyRequest.ToEntity(entity));

        public bool Delete(int id) => _companyRepository.Delete(id);

        public CompanyResource GetByName(string name)
        {
            var companyEntity = _companyRepository.GetByName(name);
            return companyEntity == null ? null : CompanyResource.FromEntity(companyEntity);
        }
    }
}