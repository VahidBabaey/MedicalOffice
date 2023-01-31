using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Constants
{
    public static class AdminRole
    {
        public static readonly string Name = "Admin";
        public static readonly Guid Id = Guid.Parse("70508b44-eae8-4d40-9318-651ae5b38f40");
    }

    public static class SuperAdminRole
    {
        public static readonly string Name = "SuperAdmin";
        public static readonly Guid Id = Guid.Parse("aca86b1a-8e36-4467-9e3c-2f826822df10");
    }

    public static class PatientRole
    {
        public static readonly string Name = "Patient";
        public static readonly Guid Id = Guid.Parse("95632500-3619-48e0-a774-2494b819b594");
    }
}