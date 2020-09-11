using System;
using System.Web.Http;
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

        [HttpGet]
        [Route("{companyId}/projects")]
        public IHttpActionResult GetAll(int companyId)
        {
            Console.WriteLine(companyId);
            var results = _companyProjectService.GetAll(companyId);
            Console.WriteLine(results == null);
            return Ok(results);
        }

        [HttpGet]
        [Route("{companyId}/projects/{projectId}")]
        public IHttpActionResult GetById(int companyId, int projectId)
        {
            var results = _companyProjectService.GetById(companyId, projectId);
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