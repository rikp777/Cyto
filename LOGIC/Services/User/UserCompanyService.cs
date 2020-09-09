using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.User;
using Domain.Entities;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.User
{
    public class UserCompanyService : IGenericRelationshipService<CompanyResource>
    {
        private readonly UserCompanyRepository _userCompanyRepository;
        public UserCompanyService()
        {
            _userCompanyRepository = new UserCompanyRepository(new DatabaseContext());
        }
        
        public bool Attach(int userId, int companyId) => _userCompanyRepository.Attach(userId, companyId);
        public bool Detach(int userId, int companyId) => _userCompanyRepository.Detach(userId, companyId);
        
        public CompanyResource GetById(int userId, int companyId) => CompanyResource.FromEntity(_userCompanyRepository.GetById(userId, companyId));
        public List<CompanyResource> GetAll(int userId) => _userCompanyRepository.GetAll(userId).Select(CompanyResource.FromEntity).ToList();
    }
}