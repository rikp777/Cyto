using System.Web.Http;
using Domain.Requests;
using LOGIC.Services.Project;
using LOGIC.Services.Role;

namespace API.Controllers.Role
{
    [RoutePrefix("api")]
    public class RoleController : ApiController
    {
        private readonly RoleService _roleService;

        public RoleController()
        {
            _roleService = new RoleService();
        }

        [HttpGet]
        [Route("roles")]
        public IHttpActionResult GetAll()
        {
            var results = _roleService.GetAll(1,1);
            return Ok(results);
        }

        [HttpGet]
        [Route("roles/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _roleService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("roles")]
        public IHttpActionResult Create(RoleRequest entity)
        {
            var result = _roleService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("roles/{id}")]
        public IHttpActionResult Update(int id, RoleRequest entity)
        {
            var result = _roleService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("roles/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _roleService.Delete(id);
            return Ok(result);
        }
    }
}