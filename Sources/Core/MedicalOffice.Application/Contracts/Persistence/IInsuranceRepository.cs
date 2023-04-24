﻿using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IInsuranceRepository : IGenericRepository<Insurance, Guid>
    {
        Task<bool> CheckExistInsuranceId(Guid officeId, Guid? insuranceId);
        Task<bool> CheckExistInsuranceName(Guid officeId, string insuranceName);
        Task<List<AdditionalInsuranceNamesDTO>> GetAdditionalInsuranceNames(Guid officeId);
        Task<List<InsuranceNamesDTO>> GetAllAdditionalInsuranceNames(Guid officeId);
        Task<List<Insurance>> GetInsuranceBySearch(string name, Guid officeId);
        Task<List<InsuranceNamesDTO>> GetInsuranceNames(Guid officeId);
        Task<Insurance> GetTariffTypeByInsuranceId(Guid insuranceId, Guid officeId);
    }
}
