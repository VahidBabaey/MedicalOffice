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
        Guid MedicalStaffId,
        Guid shiftId,
        Guid officeId,
        Guid patientId,
        ReceptionType receptionType
        );
    Task<long> GetReceptionServiceCost
        (
        Guid serviceId,
        int serviceCount,
        Guid insuranceId
        );
    Task<ReceptionDetail> AddReceptionService
        (
        Guid receptionId,
        Guid serviceId,
        int serviceCount,
        Guid insuranceId,
        Guid additionalInsuranceId,
        long received,
        Guid discountTypeId,
        Guid[] MedicalStaffs
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
        Guid[] MedicalStaffs
        );
    //Task<ReceptionServiceDto> GetReceptionServiceInfo(Guid receptionDetailId);
    Task<Reception> SummarizeReception(Guid receptionId);
    Task<ReceptionSummaryDto> GetReceptionSummary(Guid receptionId);
    Task DeleteReceptionService(Guid receptionDetailId);
    Task<int> GetFactorNo();
    Task<int> GetFactorNoToday();

    Task<decimal> GetReceptionTotal(Guid id);
    Task<IEnumerable<MembershipNamesDTO>> GetAllMembershipNames();
    Task<DetailsofAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId);
    Task<List<ReceptionDetailListDTO>> GetReceptionDetailList(Guid patientId);
    Task<Reception> CreateNewReceptionDebt(long Debt, Guid officeId, Guid receptionId);
    Task<ReceptionDetail> CreateNewReceptionDetailDebt(long Debt, Guid officeId, Guid receptionId);
    Task<bool> CheckExistReceptionId(Guid officeId, Guid receptionId);
}
