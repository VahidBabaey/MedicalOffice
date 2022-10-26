﻿using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientAddressRepository : IGenericRepository<PatientAddress, Guid>
    {
        Task<bool> RemovePatientAddress(Guid patientId);
    }
}
