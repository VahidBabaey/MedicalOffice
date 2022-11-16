using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities
{
    public class PermissionCategory: BaseDomainEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public string PersianName{ get; set; } = string.Empty;

        public ICollection<Role>? Role { get; set; }
    }
}