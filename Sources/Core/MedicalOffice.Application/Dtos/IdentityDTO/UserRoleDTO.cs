using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class UserRoleDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public Guid RoleId { get; set; }

        public Guid OfficeId { get; set; }

    }
}
