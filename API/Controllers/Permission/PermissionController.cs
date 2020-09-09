using System.Web.Http;
using Domain.Requests;
using LOGIC.Services.Experiment;
using LOGIC.Services.Permission;

namespace API.Controllers.Permission
{
    [RoutePrefix("api")]
    public class PermissionController : ApiController
    {
        private readonly PermissionService _permissionService;
        
        public PermissionController()
        {
            _permissionService = new PermissionService();
        }
        
        [HttpGet]
        [Route("Permissions")]
        public IHttpActionResult GetAll()
        {
            var results = _permissionService.GetAll(1, 1);
            return Ok(results);
        }

        [HttpGet]
        [Route("Permissions/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _permissionService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("Permissions")]
        public IHttpActionResult Create(PermissionRequest entity)
        {
            var result = _permissionService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("Permissions/{id}")]
        public IHttpActionResult Update(int id, PermissionRequest entity)
        {
            var result = _permissionService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Permissions/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _permissionService.Delete(id);
            return Ok(result);
        }
    }
}