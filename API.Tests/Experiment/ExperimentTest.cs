using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using API.Controllers.Experiment;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Experiment
{
    [TestFixture]
    public class ExperimentTest : ITestSkeleton
    {
        private TestContext _context;
        private ExperimentController _controller;

        [SetUp]
        public void Init()
        {
            _context = new TestContext();
            _controller = new ExperimentController(_context);
        }

        [Test]
        public void GetAll_GoodWeather()
        {
            _context.Experiments.Add(new ExperimentEntity()
                {Name = "MySuccessfulExperiment", Description = "Super hard experiment"});
            _context.Experiments.Add(new ExperimentEntity()
                {Name = "MyStupidExperiment", Description = "Worst experiment ever"});
            var res = _controller.GetAll() as OkNegotiatedContentResult<List<ExperimentResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("MySuccessfulExperiment", res.Content[0].Name);
            Assert.AreEqual("MyStupidExperiment", res.Content[1].Name);
        }

        [Test]
        public void GetAll_BadWeather()
        {
            var res = _controller.GetAll() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(res);
            Assert.AreEqual("There are no experiments found", res.Content);
        }

        [Test]
        public void GetById_GoodWeather()
        {
            _context.Experiments.Add(new ExperimentEntity()
                {Name = "MySuccessfulExperiment", Description = "Super hard experiment"});

            var res = _controller.GetById(1) as OkNegotiatedContentResult<ExperimentResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("MySuccessfulExperiment", res.Content.Name);
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
            var experimentToCreate = new ExperimentRequest()
                {Description = "An experiment without a name shouldn't pass"};
            var context = new ValidationContext(experimentToCreate, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(experimentToCreate, context, results, true);
            Assert.IsFalse(isModelStateValid);
        }

        [Test]
        public void Create_BadWeather()
        {
            // #1 - Try creating an experiment with empty/null body
            var res = _controller.Create(null) as BadRequestErrorMessageResult;

            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try creating an experiment without providing an experiment name
            _controller.ModelState.AddModelError("error", "An experiment name must be provided!");
            var experimentRequest = new ExperimentRequest()
                {Description = "The most crazy experiment you can find out there"};
            res = _controller.Create(experimentRequest) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("An experiment name must be provided!", res.Message);
        }

        [Test]
        public void Update_GoodWeather()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Update_BadWeather()
        {
            // #1 - Try updating an experiment with empty/null body
            var res = _controller.Update(1, null) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try updating a non-existing company
            var updatedExperiment = new ExperimentRequest()
                {Name = "MySuccessfulExperiment", Description = "Some new crazy description"};
            var res1 = _controller.Update(1, updatedExperiment);
            Assert.IsNotNull(res1);
            Assert.IsInstanceOf<NotFoundResult>(res1);

            // #3 - Try updating a company without providing a company name
            _controller.ModelState.AddModelError("error", "An experiment name must be provided!");
            updatedExperiment = new ExperimentRequest()
                {Description = "The most crazy experiment you can find out there"};
            res = _controller.Update(1, updatedExperiment) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("An experiment name must be provided!", res.Message);
        }


        [Test]
        public void Create_GoodWeather()
        {
            HttpContext.Current = MockHelper.FakeHttpContext("experiments");

            _context.Users.Add(new UserEntity() {Name = "Bobby", Email = "bobby@something.nl"});
            _context.Companies.Add(new CompanyEntity()
                {Name = "Some crazy company", Description = "That is the most crazy company"});
            var experimentToCreate = new ExperimentRequest()
                {Name = "Some amazing experiment", Description = "The most crazy cells you can find out there"};
            var res = _controller.Create(experimentToCreate) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, _context.Experiments.ToList()[0].Id);
            Assert.AreEqual("Some amazing experiment", _context.Experiments.ToList()[0].Name);
            Assert.AreEqual("The most crazy cells you can find out there",
                _context.Experiments.ToList()[0].Description);
        }
    }
}