﻿using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class RegisterUserDTO : IPhoneNumberDTO, INationalIdDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NationalID { get; set; } = string.Empty;

    }
}
