using Domain.Entities;

namespace Domain.Resources
{
    public class CompanyResource
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static CompanyResource FromEntity(CompanyEntity entity)
        {
            return new CompanyResource()
            {
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}