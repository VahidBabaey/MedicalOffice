using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class AuthenticatedUserDTO
    {
        public Guid Id { get; set; }= Guid.NewGuid();   

        public string? UserName { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; }= string.Empty;

        public string? Token { get; set; }
    }
}
