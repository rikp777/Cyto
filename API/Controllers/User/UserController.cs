using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;
using DAL.Interfaces;
using Domain.Requests;
using LOGIC.Services.User;

namespace API.Controllers.User
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        public UserController(IDatabaseContext context)
        {
            _userService = new UserService(context);
        }

        [HttpGet]
        [Route("users")]
        public IHttpActionResult GetAll()
        {
            var results = _userService.GetAll();
            if (results.Count == 0) return Ok("There are no users found");

            return Ok(results);
        }

        [HttpGet]
        [Route("users/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("users")]
        public IHttpActionResult Create([FromBody] UserRequest userToCreate)
        {
            if (userToCreate == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }

            if (!ControllerHelper.IsValidEmail(userToCreate.Email)) return BadRequest("Invalid email address!");

            var temp = _userService.GetByEmail(userToCreate.Email);
            if (temp != null) return BadRequest("A user with that email already exists!");

            _userService.Create(userToCreate);
            return Created("", userToCreate);
        }

        [HttpPut]
        [Route("users/{id}")]
        public IHttpActionResult Update(int id, UserRequest userToUpdate)
        {
            Console.WriteLine(userToUpdate.Email);
            Console.WriteLine(userToUpdate.Name);

            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            var result = _userService.Update(id, userToUpdate);
            return Ok(result);
        }

        [HttpDelete]
        [Route("users/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _userService.GetById(id);
            if (result == null) return NotFound();
            _userService.Delete(id);
            return Ok("User by id " + id + "has been deleted");
        }
    }
}