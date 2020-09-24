using System.Web;
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
            var results = _projectService.GetAll(1,1);
            return Ok(results);
        }

        [HttpGet]
        [Route("projects/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("projects")]
        public IHttpActionResult Create(ProjectRequest entity)
        {
            var result = _projectService.Create(entity, HttpContext.Current);
            return Ok(result);
        }

        [HttpPut]
        [Route("projects/{id}")]
        public IHttpActionResult Update(int id, ProjectRequest entity)
        {
            var result = _projectService.Update(id, entity, HttpContext.Current);
            return Ok(result);
        }

        [HttpDelete]
        [Route("projects/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _projectService.Delete(id, HttpContext.Current);
            return Ok(result);
        }
    }
}