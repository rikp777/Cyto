using System.Web.Http;
using Domain.Requests;
using LOGIC.Services.Permission;
using LOGIC.Services.Project;

namespace API.Controllers.Project
{
    [RoutePrefix("api")]
    public class ProjectController : ApiController
    {
        private readonly ProjectService _projectService;
        
        public ProjectController()
        {
            _projectService = new ProjectService();
        }
        
        [HttpGet]
        [Route("projects")]
        public IHttpActionResult GetAll()
        {
            var results = _projectService.GetAll(1, 1);
            return Ok(results);
        }

        [HttpGet]
        [Route("Permissions/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("Permissions")]
        public IHttpActionResult Create(ProjectRequest entity)
        {
            var result = _projectService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("Permissions/{id}")]
        public IHttpActionResult Update(int id, ProjectRequest entity)
        {
            var result = _projectService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Permissions/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _projectService.Delete(id);
            return Ok(result);
        }
    }
}