using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using API.Controllers.Project;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Project
{
    [TestOf("ProjectController")]
    [TestFixture]
    public class ProjectTest : ITestSkeleton
    {
        private TestContext _context;
        private ProjectController _controller;

        [SetUp]
        public void Init()
        {
            _context = new TestContext();
            _controller = new ProjectController(_context);
        }

        [Test]
        public void GetAll_GoodWeather()
        {
            _context.Projects.Add(new ProjectEntity()
                {Name = "MySuccessfulProject", Description = "Best project ever"});
            _context.Projects.Add(new ProjectEntity()
                {Name = "MyBadProject", Description = "Worst project ever"});
            var res = _controller.GetAll() as OkNegotiatedContentResult<List<ProjectResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("MySuccessfulProject", res.Content[0].Name);
            Assert.AreEqual("MyBadProject", res.Content[1].Name);
        }


        [Test]
        public void GetAll_BadWeather()
        {
            var res = _controller.GetAll() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(res);
            Assert.AreEqual("There are no projects found", res.Content);
        }

        [Test]
        public void GetById_GoodWeather()
        {
            _context.Projects.Add(new ProjectEntity()
                {Name = "MySuccessfulProject", Description = "Best project ever"});
            var res = _controller.GetById(1) as OkNegotiatedContentResult<ProjectResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("MySuccessfulProject", res.Content.Name);
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
            var projectRequest = new ProjectRequest()
                {Description = "A project without a name shouldn't pass"};
            var context = new ValidationContext(projectRequest, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(projectRequest, context, results, true);
            Assert.IsFalse(isModelStateValid);
        }

        [Test]
        public void Create_GoodWeather()
        {
            HttpContext.Current = MockHelper.FakeHttpContext("projects");

            _context.Users.Add(new UserEntity() {Name = "Bobby", Email = "bobby@something.nl"});
            _context.Companies.Add(new CompanyEntity()
                {Name = "Some crazy company", Description = "That is the most crazy company"});

            var projectRequest = new ProjectRequest()
                {Name = "CrazyProject", Description = "The most crazy project you can find out there"};
            var res = _controller.Create(projectRequest) as OkNegotiatedContentResult<bool>;

            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, _context.Projects.ToList()[0].Id);
            Assert.AreEqual("CrazyProject", _context.Projects.ToList()[0].Name);
            Assert.AreEqual("The most crazy project you can find out there", _context.Projects.ToList()[0].Description);
        }

        [Test]
        public void Create_BadWeather()
        {
            // #1 - Try creating a project with empty/null body
            var res = _controller.Create(null) as BadRequestErrorMessageResult;

            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try creating a project without providing an experiment name
            _controller.ModelState.AddModelError("error", "A project name must be provided!");
            var projectRequest = new ProjectRequest()
                {Description = "The most crazy project you can find out there"};
            res = _controller.Create(projectRequest) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A project name must be provided!", res.Message);
        }

        [Test]
        public void Update_GoodWeather()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void Update_BadWeather()
        {
            // #1 - Try updating a project with empty/null body
            var res = _controller.Update(1, null) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try updating a non-existing project
            var updatedProject = new ProjectRequest()
                {Name = "MySuccessfulProject", Description = "Some new crazy description"};
            var res1 = _controller.Update(1, updatedProject);
            Assert.IsNotNull(res1);
            Assert.IsInstanceOf<NotFoundResult>(res1);

            // #3 - Try updating a project without providing a project name
            _controller.ModelState.AddModelError("error", "A project name must be provided!");
            updatedProject = new ProjectRequest()
                {Description = "The most crazy experiment you can find out there"};
            res = _controller.Update(1, updatedProject) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A project name must be provided!", res.Message);
        }
    }
}