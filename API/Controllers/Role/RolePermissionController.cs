using System.Web.Http;
using LOGIC.Services.Role;

namespace API.Controllers.Role
{
    [RoutePrefix("api/roles")]
    public class RolePermissionController : ApiController
    {
        private readonly RolePermissionService _rolePermissionService;

        public RolePermissionController()
        {
            _rolePermissionService = new RolePermissionService();
        }

        [HttpGet]
        [Route("{roleId}/permissions")]
        public IHttpActionResult GetAll(int roleId)
        {
            var results = _rolePermissionService.GetAll(roleId);
            return Ok(results);
        }

        [HttpGet]
        [Route("{roleId}/permissions/{permissionId}")]
        public IHttpActionResult GetById(int roleId, int permissionId)
        {
            var results = _rolePermissionService.GetById(roleId, permissionId);
            return Ok(results);
        }

        [HttpPost]
        [Route("{roleId}/permissions")]
        public IHttpActionResult Attach(int roleId, int permissionId)
        {
            var results = _rolePermissionService.Attach(roleId, permissionId);
            return Ok(results);
        }

        [HttpDelete]
        [Route("{roleId}/permissions")]
        public IHttpActionResult Detach(int roleId, int permissionId)
        {
            var results = _rolePermissionService.Detach(roleId, permissionId);
            return Ok(results);
        }
    }
}