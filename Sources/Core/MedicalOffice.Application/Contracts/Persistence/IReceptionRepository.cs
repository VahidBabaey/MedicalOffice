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

    Task<long> GetReceptionTotal(Guid id);
    Task<IEnumerable<MembershipNamesDTO>> GetAllMembershipNames();
    Task<DetailsOfAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId, Guid receptionId);
    Task<List<ReceptionDetailListDTO>> GetReceptionDetailList(Guid receptionId, Guid patientId);
    Task<Reception> CreateNewReceptionDebt(long Debt, Guid officeId, Guid receptionId);
    Task<ReceptionDetail> CreateNewReceptionDetailDebt(long Debt, Guid officeId, Guid receptionId);
    Task<bool> CheckExistReceptionId(Guid officeId, Guid receptionId);
    Task<List<ReceptionListDTO>> GetReceptionList(Guid patientId);
    int GetServiceCountsOfPatient(Guid receptionId);
    Task<List<ReceptionDetailListForReceptionDTO>> GetReceptionDetailListForReception(Guid officeId, Guid patientId, Guid receptionId);
    Task<bool> CheckExistReceptionDetailId(Guid officeId, Guid receptiondetailId);
    Task UpdatereceptionDescription(Guid receptionid, string? description);
    Task<int> CalculateDiscount(Guid officeId, Guid serviceId, Guid membershipId);
    Task<ReceptionDetailSharesDTO> CalculateServiceTariff(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, int? discount, long Tariff);
    Task<ReceptionDetail> AddReceptionService(Guid officeId, Guid? receptionId, ReceptionType receptionType, Guid patientid, Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, Guid? membershipId, Guid[] MedicalStaffs, long recieved, long organshare, long patientshare, long addshare, long tariff);
    Task<Guid> UpdateReceptionService(Guid receptionDetailId, Guid officeId, Guid receptionId, Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, Guid[] MedicalStaffs, long Recieved, long organshare, long patientshare, long addshare, long tariff);
}
