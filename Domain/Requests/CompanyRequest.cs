using Domain.Entities;

namespace Domain.Requests
{
    public class CompanyRequest
    {
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
    }
}