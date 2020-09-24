using System.Web.Http;
using DAL.Context;
using LOGIC.Services.Experiment;
using LOGIC.Services.Project;

namespace API.Controllers.Project
{
    [RoutePrefix("api/projects")]
    public class ProjectAuditTrailController : ApiController
    {
        private readonly ProjectAuditTrailService _projectAuditTrailService;

        public ProjectAuditTrailController()
        {
            _projectAuditTrailService = new ProjectAuditTrailService(new DatabaseContext());
        }


        [HttpGet]
        [Route("{projectId}/audittrails")]
        public IHttpActionResult GetAll(int projectId)
        {
            var results = _projectAuditTrailService.GetAll(projectId);
            return Ok(results);
        }

        [HttpGet]
        [Route("{projectId}/audittrails/{audittrailId}")]
        public IHttpActionResult GetById(int projectId, int auditTrailId)
        {
            var result = _projectAuditTrailService.GetById(projectId, auditTrailId);
            return Ok(result);
        }
    }
}