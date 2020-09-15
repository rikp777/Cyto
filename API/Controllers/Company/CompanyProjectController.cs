using System;
using System.Web.Http;
using DAL.Context;
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
            _companyProjectService = new CompanyProjectService();
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
            if (results == null) return NotFound();
            return Ok(results);
        }

        [HttpGet]
        [Route("{companyId}/projects/{projectId}")]
        public IHttpActionResult GetById(int companyId, int projectId)
        {
            var results = _companyProjectService.GetById(companyId, projectId);
            if (results == null) return NotFound();
            return Ok(results);
        }

        [HttpPost]
        [Route("{companyId}/projects")]
        public IHttpActionResult Attach(int companyId, int projectId)
        {
            Console.WriteLine(projectId);
            var results = _companyProjectService.Attach(companyId, projectId);
            return Ok(results);
        }

        [HttpDelete]
        [Route("{companyId}/projects")]
        public IHttpActionResult Detach(int userId, int projectId)
        {
            var results = _companyProjectService.Detach(userId, projectId);
            return Ok(results);
        }
    }
}