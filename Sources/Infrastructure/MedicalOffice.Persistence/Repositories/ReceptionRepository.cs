using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Repositories.RepoHelpers;
using Microsoft.EntityFrameworkCore;
using OtpNet;

namespace MedicalOffice.Persistence.Repositories;

public class ReceptionRepository : GenericRepository<Reception, Guid>, IReceptionRepository
{
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionDetailRepository;
    private readonly IGenericRepository<ReceptionMedicalStaff, Guid> _receptionDetailMedicalStaffRepository;
    private readonly IGenericRepository<ReceptionDetailService, Guid> _receptionDetailServiceRepository;
    private readonly ApplicationDbContext _dbContext;
    string medicalStaffNames = "";
    public ReceptionRepository(IGenericRepository<ReceptionDetailService, Guid> receptionDetailServiceRepository, IGenericRepository<ReceptionMedicalStaff, Guid> receptionDetailMedicalStaffRepository, IGenericRepository<ReceptionDetail, Guid> receptionDetailRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _receptionDetailRepository = receptionDetailRepository;
        _receptionDetailMedicalStaffRepository = receptionDetailMedicalStaffRepository;
        _receptionDetailServiceRepository = receptionDetailServiceRepository;
    }

    public async Task<Guid> AddReceptionService(Guid receptionId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, long received, Guid discountTypeId, Guid[] MedicalStaffs)
    {
        var reception = await _dbContext.Receptions.SingleAsync(r => r.Id == receptionId);
        var service = await _dbContext.Services.SingleAsync(s => s.Id == serviceId);
        var insurance = await _dbContext.Insurances.SingleAsync(i => i.Id == insuranceId);
        var additionalInsurance = await _dbContext.Insurances.SingleAsync(i => i.Id == additionalInsuranceId);
        var discountValue = await _dbContext.ReceptionDiscounts.SingleAsync(dt =>  dt.MembershipId == discountTypeId);
        var MedicalStaffsCheck = MedicalStaffs.All(id => _dbContext.MedicalStaffs.Any(u => u.Id == id));
        if (!MedicalStaffsCheck)
            throw new NullReferenceException();

        var cost = await GetReceptionServiceCost(serviceId, serviceCount, insuranceId);
        var finalRecieved = received - discountValue.Discount;
        var debt = cost > finalRecieved ? cost - finalRecieved : 0;
        var deposit = finalRecieved > cost ? finalRecieved - cost : 0;

        ReceptionDetail detail = new()
        {
            AdditionalInsuranceId = additionalInsuranceId,
            Cost = cost,
            Received = received,
            Deposit = deposit,
            Debt = debt,
            InsuranceId = insuranceId,
            IsDeleted = false,
            OfficeId = service.OfficeId,
            ReceptionId = reception.Id,
            ServiceCount = serviceCount,
            ReceptionDiscountId = discountValue.Id
        };

        var addedDetail = await _receptionDetailRepository.Add(detail);

        foreach (var MedicalStaffId in MedicalStaffs)
        {
            var receptionMedicalStaff = new ReceptionMedicalStaff()
            {
                IsDeleted = false,
                //UserOfficeRoleId = MedicalStaffId,
                ReceptionDetailId = addedDetail.Id,
            };
            await _receptionDetailMedicalStaffRepository.Add(receptionMedicalStaff);
        }

        var receptionDetailService = new ReceptionDetailService()
        {
            ReceptionDetailId = addedDetail.Id,
            ServiceId = service.Id
        };
        await _receptionDetailServiceRepository.Add(receptionDetailService);

        return addedDetail.Id;
    }

    public async Task<Guid> CreateNewReception(Guid MedicalStaffId, Guid shiftId, Guid officeId, Guid patientId, ReceptionType receptionType)
    {
        var factorNo = await GetFactorNo();
        var factorNoToday = await GetFactorNoToday();

        Reception reception = new()
        {
            FactorNo = factorNo,
            FactorNoToday = factorNoToday,
            IsCancelled = false,
            IsDeleted = false,
            IsReturned = false,
            LoggedInMedicalStaffId = MedicalStaffId,
            OfficeId = officeId,
            PatientId = patientId,
            ReceptionDate = DateTime.Now.ToPersianDate(),
            ReceptionSubmitHour = DateTime.Now.ToShortTimeString(),
            ReceptionType = receptionType,
            ShiftId = shiftId,
            TotalDebt = default,
            TotalDeposit = default,
            TotalReceived = default,
            TotalReceptionCost = default
        };

        var addedReception = await _dbContext.Receptions.AddAsync(reception);

        await _dbContext.SaveChangesAsync();

        return addedReception.Entity.Id;
    }

    public async Task DeleteReceptionService(Guid receptionDetailId)
    {
        var detail = await _dbContext.ReceptionDetails
            .SingleOrDefaultAsync(rd => rd.Id == receptionDetailId);

        if (detail != null)
        {
            _dbContext.ReceptionDetails.Remove(detail);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<int> GetFactorNo()
    {
        if (_dbContext.Receptions.Any() == false)
        {
            return  1;
        }
        else
        {
        var lastNo = await _dbContext.Receptions.Select(p => p.FactorNo).MaxAsync();
            return lastNo + 1;
        }
    }

    public async Task<int> GetFactorNoToday()
    {
        var lastReception = await _dbContext.Receptions
            .OrderByDescending(r => r.CreatedDate)
            .FirstOrDefaultAsync();

        int nextNo = default;

        if (lastReception != null)
        {
            if (DateTime.Today.CompareTo(lastReception.CreatedDate) > 0)
                nextNo = 1;
            else
                nextNo = lastReception.FactorNoToday + 1;
        }
        else
            nextNo = 1;

        return nextNo;
    }

    public async Task<long> GetReceptionServiceCost(Guid serviceId, int serviceCount, Guid insuranceId)
    {
        var service = await _dbContext.Tariffs.Where(p => (p.ServiceId == serviceId) && (p.InsuranceId == insuranceId)).FirstOrDefaultAsync();
        return (long)Convert.ToDouble(service.TariffValue * serviceCount); 
    }

    //public async Task<ReceptionServiceDto> GetReceptionServiceInfo(Guid receptionDetailId)
    //{
        //var detail = await _dbContext.ReceptionDetails.SingleOrDefaultAsync(rd => rd.Id == receptionDetailId);

        //if (detail != null)
        //{
        //    await _dbContext
        //        .Entry(detail)
        //        .Collection(d => d.ReceptionDiscounts).EntityEntry
        //        .Collection(d => d.ReceptionMedicalStaffs).EntityEntry
        //        .Reference(d => d.Service)
        //        .LoadAsync();

        //    long discount = default;
        //    Guid discountTypeId = default;
        //    Guid[]? MedicalStaffs = default;
        //    if (detail.ReceptionDiscounts.Any())
        //    {
        //        discount = detail.ReceptionDiscounts.Sum(d => d.Amount);
        //        discountTypeId = detail.ReceptionDiscounts.First().DiscountTypeId;
        //    }
        //    if (detail.ReceptionMedicalStaffs.Any())
        //    {
        //        MedicalStaffs = detail.ReceptionMedicalStaffs
        //            .Select(u => u.Id)
        //            .ToArray();
        //    }

        //    ReceptionServiceDto dto = new()
        //    {
        //        AdditionalInsuranceId = detail.AdditionalInsuranceId,
        //        DiscountAmount = discount,
        //        DiscountTypeId = discountTypeId,
        //        InsuranceId = detail.InsuranceId,
        //        ServiceId = detail.ServiceId,
        //        MedicalStaffs = MedicalStaffs,
        //        ServiceName = detail.Service.Name,
        //        Received = detail.Received,
        //        ServiceCount = detail.ServiceCount
        //    };

        //    return dto;
        //}
        //else
        //    throw new NullReferenceException();
    //}
    public async Task <DetailsofAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId)
    {
        var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId).ToListAsync();
        DetailsofAllReceptionsDTO detailsofAllReceptions = new DetailsofAllReceptionsDTO();
        foreach (var item in _list)
        {
            detailsofAllReceptions.ReceptionId = item.Id;
            detailsofAllReceptions.Cost += item.Cost;
            detailsofAllReceptions.Debt += item.Debt;
            detailsofAllReceptions.Deposit += item.Deposit;
        }
        return detailsofAllReceptions;
    }

    public async Task<List<ReceptionDetailListDTO>> GetReceptionDetailList(Guid patientId)
    {
        List<ReceptionDetailListDTO> receptionDetailListDTO = new();
        var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId).ToListAsync();
        foreach (var item in _list)
        {
            var serviceId =  _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == item.Id).FirstOrDefault().ServiceId;
            var serviceName = _dbContext.Services.Where(p => p.Id == serviceId).FirstOrDefault().Name;
            var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == item.Id).ToListAsync();
            var discount = _dbContext.ReceptionDiscounts.Where(p => p.Id == item.ReceptionDiscountId).FirstOrDefault();
            foreach (var item2 in medicalStaffIds)
            {
                medicalStaffNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == item2.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
            }
            ReceptionDetailListDTO receptiondetaillistDTO = new()
            {
                Id = item.Id,
                ServiceName = serviceName,
                ServiceCount = item.ServiceCount,
                MedicalStaffs = medicalStaffNames,
                Cost = item.Cost,
                Discount = discount.Discount,
                Deposit = item.Deposit,
                Debt = item.Debt,
                Total = item.Received
            };
            receptionDetailListDTO.Add(receptiondetaillistDTO);
            medicalStaffNames = "";
        }
        return receptionDetailListDTO;
    }

    public async Task<ReceptionSummaryDto> GetReceptionSummary(Guid receptionId)
    {
        var reception = await SummarizeReception(receptionId);

        var dto = new ReceptionSummaryDto()
        {
            Debt = reception.TotalDebt,
            Deposit = reception.TotalDeposit,
            Payable = reception.TotalReceived
        };

        return dto;

    }
    public async Task<IEnumerable<MembershipNamesDTO>> GetAllMembershipNames()
    {

        var membershipNames = await _dbContext.Memberships.Select(p => new MembershipNamesDTO
        {
            Id = p.Id,
            Name = p.Name,
            Discount = p.Discount
        }).ToListAsync();

        return membershipNames;

    }

    public async Task<decimal> GetReceptionTotal(Guid id)
    {
        var totalDebt = await _dbContext.Receptions.Where(r => r.PatientId == id).SumAsync(r => r.TotalDebt);
        var totalDeposit = await _dbContext.Receptions.Where(r => r.PatientId == id).SumAsync(r => r.TotalDeposit);
        var balance = totalDeposit - totalDebt;

        return balance;
    }

    public async Task<Reception> SummarizeReception(Guid receptionId)
    {
        var reception = await _dbContext.Receptions.SingleOrDefaultAsync(r => r.Id == receptionId);

        if (reception == null)
            throw new NullReferenceException();

        await _dbContext
            .Entry(reception)
            .Collection(r => r.ReceptionDetails)
            .LoadAsync();

        reception.TotalReceptionCost = reception.ReceptionDetails.Sum(rd => rd.Cost);
        reception.TotalReceived = reception.ReceptionDetails.Sum(rd => rd.Received);
        reception.TotalDeposit = reception.ReceptionDetails.Sum(rd => rd.Deposit);
        reception.TotalDebt = reception.ReceptionDetails.Sum(rd => rd.Debt);

        await _dbContext.SaveChangesAsync();

        return reception;
    }

    public async Task<Guid> UpdateReceptionService(Guid receptionDetailId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, long received, long discount, Guid discountTypeId, Guid[] MedicalStaffs)
    {
        throw new NotImplementedException();
    }
}
