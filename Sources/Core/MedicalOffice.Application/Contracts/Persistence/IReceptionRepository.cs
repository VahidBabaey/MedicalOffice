using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Data.SqlTypes;

namespace MedicalOffice.Application.Contracts.Persistence;

public interface IReceptionRepository : IGenericRepository<Reception, Guid>
{
    Task<Guid> CreateNewReception
        (
        Guid userId,
        Guid shiftId,
        Guid officeId,
        Guid patientId,
        ReceptionType receptionType
        );
    Task<long> GetReceptionServiceCost
        (
        Guid serviceId,
        int serviceCount,
        Guid insuranceId,
        Guid additionalInsuranceId
        );
    Task<Guid> AddReceptionService
        (
        Guid receptionId,
        Guid serviceId,
        int serviceCount,
        Guid insuranceId,
        Guid additionalInsuranceId,
        long received,
        long discount,
        Guid discountTypeId,
        Guid[] Users
        );
    Task<Guid> UpdateReceptionService
        (
        Guid receptionDetailId,
        Guid serviceId,
        int serviceCount,
        Guid insuranceId,
        Guid additionalInsuranceId,
        long received,
        long discount,
        Guid discountTypeId,
        Guid[] Users
        );
    Task<ReceptionServiceDto> GetReceptionServiceInfo(Guid receptionDetailId);
    Task<Reception> SummarizeReception(Guid receptionId);
    Task<ReceptionSummaryDto> GetReceptionSummary(Guid receptionId);
    Task DeleteReceptionService(Guid receptionDetailId);
    Task<int> GetFactorNo();
    Task<int> GetFactorNoToday();
}
