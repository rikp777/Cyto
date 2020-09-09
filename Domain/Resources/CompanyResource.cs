using Domain.Entities;

namespace Domain.Resources
{
    public class CompanyResource
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static CompanyResource FromEntity(CompanyEntity entity)
        {
            return new CompanyResource()
            {
                Name = entity.Name,
                Email = entity.Description
            };
        }
    }
}