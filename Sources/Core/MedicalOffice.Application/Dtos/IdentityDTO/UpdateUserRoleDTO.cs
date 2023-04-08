using MedicalOffice.Application.Dtos.Common.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class UpdateUserRoleDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public Guid RoleId { get; set; }
    }
}
