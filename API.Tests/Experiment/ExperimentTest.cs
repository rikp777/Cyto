using System.Collections.Generic;
using System.Web.Http.Results;
using API.Controllers.Experiment;
using Domain.Entities;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Experiment
{
    [TestFixture]
    public class ExperimentTest
    {
        [Test]
        public void GetAllExperiments_GoodWeather()
        {
            var context = new TestContext();
            context.Experiments.Add(new ExperimentEntity()
                {Name = "MySuccessfulExperiment", Description = "Super hard experiment"});
            context.Experiments.Add(new ExperimentEntity()
                {Name = "MyStupidExperiment", Description = "Worst experiment ever"});
            var controller = new ExperimentController(context);
            var res = controller.GetAll() as OkNegotiatedContentResult<List<ExperimentResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("MySuccessfulExperiment", res.Content[0].Name);
            Assert.AreEqual("MyStupidExperiment", res.Content[1].Name);
        }

        [Test]
        public void GetAllExperiments_BadWeather()
        {
            var context = new TestContext();
            var controller = new ExperimentController(context);
            var res = controller.GetAll() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(res);
            Assert.AreEqual("There are no experiments found", res.Content);
        }

        [Test]
        public void GetExperimentById_GoodWeather()
        {
            var context = new TestContext();
            context.Experiments.Add(new ExperimentEntity()
                {Name = "MySuccessfulExperiment", Description = "Super hard experiment"});

            var controller = new ExperimentController(context);
            var res = controller.GetById(1) as OkNegotiatedContentResult<ExperimentResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("MySuccessfulExperiment", res.Content.Name);
        }
        
        [Test]
        public void GetExperimentById_BadWeather()
        {
            var context = new TestContext();
            var controller = new ExperimentController(context);
            var res = controller.GetById(1);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }
        
        
        
        // [HttpGet]
        // [Route("experiments/{id}")]
        // public IHttpActionResult GetById(int id)
        // {
        //     var result = _experimentService.GetById(id);
        //     if (result == null) return NotFound();
        //     return Ok(result);
        // }
    }
}