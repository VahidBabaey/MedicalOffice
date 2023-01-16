﻿using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientIllnessFormRepository : IGenericRepository<PatientIllnessForm, Guid>
    {
        Task<bool> CheckExistPatientIllnessFormId(Guid patientIllnessFormId);
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId();
        Task<IReadOnlyList<PatientIllnessForm>> GetByPatientId(Guid patientid);
    }
}
