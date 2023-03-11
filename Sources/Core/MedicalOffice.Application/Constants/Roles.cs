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
        public static readonly string NormalizedName = "ADMIN";
        public static readonly Guid Id = Guid.Parse("70508b44-eae8-4d40-9318-651ae5b38f40");
    }

    public static class SuperAdminRole
    {
        public static readonly string Name = "SuperAdmin";
        public static readonly string NormalizedName = "SUPERADMIN";
        public static readonly Guid Id = Guid.Parse("aca86b1a-8e36-4467-9e3c-2f826822df10");
    }

    public static class PatientRole
    {
        public static readonly string Name = "Patient";
        public static readonly string NormalizedName = "PATIENT";
        public static readonly Guid Id = Guid.Parse("95632500-3619-48e0-a774-2494b819b594");
    }

    public static class DoctorRole
    {
        public static readonly string Name = "Doctor";
        public static readonly string NormalizedName = "DOCTOR";
        public static readonly Guid Id = Guid.Parse("8c07113f-ec06-4db0-90c7-e1d292525c7c");
    }

    public static class SecretaryRole
    {
        public static readonly string Name = "Secretary";
        public static readonly string NormalizedName = "SECRETARY";
        public static readonly Guid Id = Guid.Parse("779c69ef-6857-4e19-b24e-1c4c4b2635d7");
    }

    public static class ExpertRole
    {
        public static readonly string Name = "Expert";
        public static readonly string NormalizedName = "EXPERT";
        public static readonly Guid Id = Guid.Parse("bdb58210-f29f-4114-8564-7f3d5d2d26d6");
    }

    public static class ExternalRefferer
    {
        public static readonly string Name = "ExternalReferrer";
        public static readonly string NormalizedName = "EXTERNALREFERRER";
        public static readonly Guid Id = Guid.Parse("60EEAF14-A2E6-40DF-ABDC-31DFB55D0488");
    }
}