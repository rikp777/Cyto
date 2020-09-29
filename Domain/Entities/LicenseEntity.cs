using Domain.Contracts;

namespace Domain.Entities
{
    public class LicenseEntity : BaseEntity
    {
        public string Code { get; set; }
        public string ValidTillDate { get; set; }
        public virtual LicenseTypeEntity LicenseType { get; set; }
        public virtual CompanyEntity Company { get; set; }
    }
}