using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Results;
using API.Controllers.User;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.User
{
    public class UserTest : ITestSkeleton
    {
        private TestContext _context;
        private UserController _controller;

        [SetUp]
        public void Init()
        {
            _context = new TestContext();
            _controller = new UserController(_context);
        }

        [Test]
        public void GetAll_GoodWeather()
        {
            _context.Users.Add(new UserEntity()
                {Name = "Tommy Gunn", Email = "tommy@gunn.nl"});
            _context.Users.Add(new UserEntity()
                {Name = "Kolo Toure", Email = "kolo@toure.nl"});
            var res = _controller.GetAll() as OkNegotiatedContentResult<List<UserResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("Tommy Gunn", res.Content[0].Name);
            Assert.AreEqual("Kolo Toure", res.Content[1].Name);
        }

        [Test]
        public void GetAll_BadWeather()
        {
            var res = _controller.GetAll() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(res);
            Assert.AreEqual("There are no users found", res.Content);
        }

        [Test]
        public void GetById_GoodWeather()
        {
            _context.Users.Add(new UserEntity()
                {Name = "Tommy Gunn", Email = "tommy@gunn.nl"});
            var res = _controller.GetById(1) as OkNegotiatedContentResult<UserResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("Tommy Gunn", res.Content.Name);
        }

        [Test]
        public void GetById_BadWeather()
        {
            var res = _controller.GetById(1);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void Model_Validation()
        {
            var userRequest = new UserRequest()
                {Email = "tommy@gunn.nl"};
            var context = new ValidationContext(userRequest, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(userRequest, context, results, true);
            Assert.IsFalse(isModelStateValid);
        }

        [Test]
        public void Create_GoodWeather()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void Create_BadWeather()
        {
            // #1 - Try creating a project with empty/null body
            var res = _controller.Create(null) as BadRequestErrorMessageResult;

            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try creating two users with the same email
            var userRequest = new UserRequest()
                {Name = "Tommy Gunn", Email = "tommy@gunn.nl"};
            _controller.Create(userRequest);

            userRequest = new UserRequest() {Name = "Bobby Firmino", Email = "tommy@gunn.nl"};
            res = _controller.Create(userRequest) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A user with that email already exists!", res.Message);
            
            // #3 - Try creating a user without providing a user name
            _controller.ModelState.AddModelError("error", "A user name must be provided!");
            userRequest = new UserRequest()
                {Email = "tommy@gunn.nl"};
            res = _controller.Create(userRequest) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A user name must be provided!", res.Message);
            
            // #4 - Try creating a user without providing an email
            _controller.ModelState.Clear();
            _controller.ModelState.AddModelError("error", "An email of user must be provided!");
            userRequest = new UserRequest()
                {Name = "Tommy Gunn"};
            res = _controller.Create(userRequest) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("An email of user must be provided!", res.Message);
            
            // // #5 - Try creating a user providing an invalid email
            _controller.ModelState.Clear();
            _controller.ModelState.AddModelError("error", "The Email field is not a valid e-mail address.");
            userRequest = new UserRequest()
                {Name = "Tommy Gunn", Email = "invalidemail"};
            res = _controller.Create(userRequest) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("The Email field is not a valid e-mail address.", res.Message);
        }

        [Test]
        public void Update_GoodWeather()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void Update_BadWeather()
        {
            throw new System.NotImplementedException();
        }
    }
}