﻿using MedicalOffice.Application.Dtos.Common.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class UserStatusRequestDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }
    }
}
