﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class RegisterUserDTO
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string NationalID { get; set; } = string.Empty;

        public Guid? OfficeId { get; set; }

        public Guid[]? RoleIds{ get; set; }

    }
}
