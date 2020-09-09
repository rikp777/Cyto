using System.Web.Http;
using LOGIC.Services.User;

namespace API.Controllers.User
{
    
    [RoutePrefix("api/users")]
    public class UserRoleController : ApiController
    {

        private readonly UserRoleService _userRoleService;

        public UserRoleController()
        {
            _userRoleService = new UserRoleService();
        }
        
        [HttpGet]
        [Route("{userId}/roles")]
        public IHttpActionResult GetAllRoles(int userId)
        {
            var results = _userRoleService.GetAll(userId);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("{userId}/roles/{roleId}")]
        public IHttpActionResult GetById(int userId, int roleId)
        {
            var results = _userRoleService.GetById(userId, roleId);
            return Ok(results);
        }
        
        [HttpPost]
        [Route("{userId}/roles")]
        public IHttpActionResult Attach(int userId, int roleId)
        {
            var results = _userRoleService.Attach(userId, roleId);
            return Ok(results);
        }
        
        [HttpDelete]
        [Route("{userId}/roles")]
        public IHttpActionResult Detach(int userId, int roleId)
        {
            var results = _userRoleService.Attach(userId, roleId);
            return Ok(results);
        }
    }
}