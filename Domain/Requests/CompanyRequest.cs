using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Requests
{
    public class CompanyRequest
    {
        [Required(ErrorMessage = "A company name must be provided!")]
        public string Name { get; set; }
        public string Description { get; set; }

        public static CompanyEntity ToEntity(CompanyRequest entity)
        {
            return new CompanyEntity()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public override string ToString()
        {
            return Name + " " + Description;
        }
    }
}