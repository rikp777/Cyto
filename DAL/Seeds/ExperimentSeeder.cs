using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Seeds
{
    public class ExperimentSeeder
    {
        public static void Seeder(IDbSet<ExperimentEntity> experiments, IDbSet<ProjectEntity> projects)
        {
            var projectOne = projects.Find(1);
            var projectTwo = projects.Find(2);
            if (projectOne == null) throw new Exception("Project One not found");
            if (projectTwo == null) throw new Exception("Project Two not found");
            
            var experimentOne = new ExperimentEntity()
            {
                Name = "Experiment One",
                Description = "Experiment One Desc",
                Project = projectOne
            };
            var experimentTwo = new ExperimentEntity()
            {
                Name = "Experiment Two",
                Description = "Experiment Two Desc",
                Project = projectOne
            };
            var experimentThree = new ExperimentEntity()
            {
                Name = "Experiment Three",
                Description = "Experiment Three Desc",
                Project = projectTwo
            };
            var experimentFour = new ExperimentEntity()
            {
                Name = "Experiment Four",
                Description = "Experiment Four Desc",
                Project = projectTwo
            };
            
            experiments.AddOrUpdate(x => x.Name, experimentOne, experimentTwo, experimentThree, experimentFour);
        }
    }
}