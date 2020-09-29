using Domain.Contracts;

namespace Domain.Entities
{
    public class LicenseTypeEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}