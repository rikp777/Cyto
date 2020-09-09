using System.Web.Http;
using LOGIC.Services.Company;
using LOGIC.Services.Project;

namespace API.Controllers.Project
{
    [RoutePrefix("api/projects")]
    public class ProjectExperimentController : ApiController
    {

        private readonly ProjectExperimentService _projectExperimentService;

        public ProjectExperimentController()
        {
            _projectExperimentService = new ProjectExperimentService();
        }
        
        [HttpGet]
        [Route("{projectId}/experiments")]
        public IHttpActionResult GetAll(int projectId)
        {
            var results = _projectExperimentService.GetAll(projectId);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("{projectId}/experiments/{experimentId}")]
        public IHttpActionResult GetById(int projectId, int experimentId)
        {
            var results = _projectExperimentService.GetById(projectId, experimentId);
            return Ok(results);
        }
        
        [HttpPost]
        [Route("{projectId}/experiments")]
        public IHttpActionResult Attach(int projectId, int experimentId)
        {
            var results = _projectExperimentService.Attach(projectId, experimentId);
            return Ok(results);
        }
        
        [HttpDelete]
        [Route("{projectId}/experiments")]
        public IHttpActionResult Detach(int projectId, int experimentId)
        {
            var results = _projectExperimentService.Detach(projectId, experimentId);
            return Ok(results);
        }
    }
}