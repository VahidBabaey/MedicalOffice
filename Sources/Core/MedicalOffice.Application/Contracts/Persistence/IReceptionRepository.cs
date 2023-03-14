using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Data.SqlTypes;

namespace MedicalOffice.Application.Contracts.Persistence;

public interface IReceptionRepository : IGenericRepository<Reception, Guid>
{
    Task<Guid> CreateNewReception
        (
        Guid officeId,
        Guid patientId,
        ReceptionType receptionType
        );
    Task<long> GetReceptionServiceCost
        (
        Guid serviceId,
        int serviceCount,
        Guid? insuranceId
        );

    //Task<ReceptionServiceDto> GetReceptionServiceInfo(Guid receptionDetailId);
    Task<Reception> SummarizeReception(Guid receptionId);
    Task<ReceptionSummaryDto> GetReceptionSummary(Guid receptionId);
    Task DeleteReceptionService(Guid receptionDetailId, Guid officeId);
    Task<int> GetFactorNo();
    Task<int> GetFactorNoToday();

    Task<decimal> GetReceptionTotal(Guid id);
    Task<IEnumerable<MembershipNamesDTO>> GetAllMembershipNames();
    Task<DetailsofAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId, Guid receptionId);
    Task<List<ReceptionDetailListDTO>> GetReceptionDetailList(Guid receptionId, Guid patientId);
    Task<Reception> CreateNewReceptionDebt(long Debt, Guid officeId, Guid receptionId);
    Task<ReceptionDetail> CreateNewReceptionDetailDebt(long Debt, Guid officeId, Guid receptionId);
    Task<bool> CheckExistReceptionId(Guid officeId, Guid receptionId);
    Task<List<ReceptionListDTO>> GetReceptionList(Guid patientId);
    int GetServiceCountsOfPatient(Guid receptionId);
    Task<List<ReceptionDetailListForReceptionDTO>> GetReceptionDetailListForReception(Guid patientId, Guid receptionId);
    Task<bool> CheckExistReceptionDetailId(Guid officeId, Guid receptiondetailId);
    Task UpdatereceptionDescription(Guid receptionid, string description);
    Task<int> CalculateDiscount(Guid officeId, Guid serviceId, Guid membershipId);
    Task<long> CalculateServiceTariff(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, int? discount);
    Task<ReceptionDetail> AddReceptionService(Guid officeId, ReceptionType receptionType, Guid patientid, Guid receptionId, Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, Guid? membershipId, Guid[] MedicalStaffs, long costd);
    Task<Guid> UpdateReceptionService(Guid receptionDetailId, Guid officeId, Guid receptionId, Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, Guid[] MedicalStaffs, long costd);
    Task<long> GetPatientShareofServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId);
    Task<long> GetOrganShareofServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId);
    Task<long> GetAdditionalServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalinsuranceId);
    Task<long> GetInsuranceServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId);
    Task<long> CalculateAdditionalServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalinsuranceId);
}
