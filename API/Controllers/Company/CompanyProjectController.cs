using System;
using System.Linq;
using System.Web.Http;
using DAL.Context;
using DAL.Interfaces;
using LOGIC.Services.Company;
using LOGIC.Services.User;

namespace API.Controllers.Company
{
    [RoutePrefix("api/companies")]
    public class CompanyProjectController : ApiController
    {
        private readonly CompanyProjectService _companyProjectService;

        public CompanyProjectController()
        {
            _companyProjectService = new CompanyProjectService(new DatabaseContext());
        }
        public CompanyProjectController(IDatabaseContext context)
        {
            _companyProjectService = new CompanyProjectService(context);
        }

        
        [HttpGet]
        [Route("{companyId}/projects")]
        public IHttpActionResult GetAll(int companyId)
        {
            var results = _companyProjectService.GetAll(companyId);
            if (results == null || results.Count == 0) return NotFound();
            return Ok(results);
        }

        [HttpGet]
        [Route("{companyId}/projects/{projectId}")]
        public IHttpActionResult GetById(int companyId, int projectId)
        {
            var result = _companyProjectService.GetById(companyId, projectId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("{companyId}/projects")]
        public IHttpActionResult Attach(int companyId, int projectId)
        {
            var results = _companyProjectService.Attach(companyId, projectId);
            if (results == null) return NotFound();
            return Ok(results);
        }

        [HttpDelete]
        [Route("{companyId}/projects")]
        public IHttpActionResult Detach(int companyId, int projectId)
        {
            var results = _companyProjectService.Detach(companyId, projectId);
            if (results == null) return NotFound();
            return Ok(results);
        }
    }
}