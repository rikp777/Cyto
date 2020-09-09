using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Domain.Entities;

namespace DAL.Seeds
{
    public class ProjectSeeder
    {
        public static void Seeder(IDbSet<ProjectEntity> projects, IDbSet<CompanyEntity> companies)
        {
            var companyOne = companies.Find(1);
            var companyTwo = companies.Find(2);
            if (companyOne == null) throw new Exception("Company One not found");
            if (companyTwo == null) throw new Exception("Company Two not found");
            
            
            var projectOne = new ProjectEntity()
            {
                Name = "Project One",
                Description = "This is the first test project",
                Company = companyOne
            };
            var projectTwo = new ProjectEntity()
            {
                Name = "Project two",
                Description = "This is the second test project",
                Company = companyOne
            };
            var projectThree = new ProjectEntity()
            {
                Name = "Project three",
                Description = "This is the third test project",
                Company = companyOne
            };
            var projectFour = new ProjectEntity()
            {
                Name = "Project four",
                Description = "This is the fourth test project",
                Company = companyTwo
            };
            
            projects.AddOrUpdate(x => x.Name, 
                projectOne, 
                projectTwo, 
                projectThree, 
                projectFour);
        }
    }
}