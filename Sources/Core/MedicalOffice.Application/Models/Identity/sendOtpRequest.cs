﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.Identity
{
    public class SendOtpRequest
    {
        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
    }
}
