using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;
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

        [HttpGet]
        [Route("users")]
        public IHttpActionResult GetAll()
        {
            var results = _userService.GetAll();
            if (results.ToArray().Length == 0) return Ok("There are no users found");

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
            if (userToCreate == null) return BadRequest("Empty request body!");
            if (userToCreate.Name == null) return BadRequest("You cannot create a user without specifying the name");
            if (userToCreate.Email == null) return BadRequest("You cannot create a user without specifying the email");
            if (!IsValid(userToCreate.Email)) return BadRequest("Invalid email address!");

            var temp = _userService.GetByName(userToCreate.Name);
            if (temp != null) return BadRequest("User with that name already exists!");
            temp = _userService.GetByEmail(userToCreate.Email);
            if (temp != null) return BadRequest("User with that email already exists!");

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

        public bool IsValid(string emailAddress)
        {
            var emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                             + "@"
                             + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            return Regex.IsMatch(emailAddress, emailRegex);
        }
    }
}