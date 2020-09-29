using Domain.Contracts;

namespace Domain.Entities
{
    public class LicenseEntity : BaseEntity
    {
        public string Code { get; set; }
        public string ValidTillDate { get; set; }
        public LicenseTypeEntity LicenseType { get; set; }
        public CompanyEntity Company { get; set; }
    }
}