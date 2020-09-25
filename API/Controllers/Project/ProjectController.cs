using System.Web;
using System.Web.Http;
using DAL.Interfaces;
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

        public ProjectController(IDatabaseContext context)
        {
            _projectService = new ProjectService(context);
        }

        [HttpGet]
        [Route("projects")]
        public IHttpActionResult GetAll()
        {
            var results = _projectService.GetAll();
            if (results.Count == 0) return Ok("There are no projects found");
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
        public IHttpActionResult Create(ProjectRequest projectRequest)
        {
            if (projectRequest == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }

            var result = _projectService.Create(projectRequest, HttpContext.Current);
            return Ok(result);
        }

        [HttpPut]
        [Route("projects/{id}")]
        public IHttpActionResult Update(int id, ProjectRequest projectRequest)
        {
            if (projectRequest == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }

            var project = _projectService.GetById(id);
            if (project == null) return NotFound();

            var result = _projectService.Update(id, projectRequest, HttpContext.Current);
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