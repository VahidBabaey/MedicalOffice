using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Domain
{
    public class Referrer :BaseDomainEntity<Guid>
    {
        public Guid UserId{ get; set; }

        public User User{ get; set; }

        public Guid OfficeId{ get; set; }

        public Office Office{ get; set; }

        public Guid ServiceId { get; set; }

        public Service Service{ get; set; }
    }
}