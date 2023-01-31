using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Repositories.RepoHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OtpNet;
using System.Net;
using System;

namespace MedicalOffice.Persistence.Repositories;

public class ReceptionRepository : GenericRepository<Reception, Guid>, IReceptionRepository
{
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionDetailRepository;
    private readonly IGenericRepository<ReceptionMedicalStaff, Guid> _receptionDetailMedicalStaffRepository;
    private readonly IGenericRepository<ReceptionDetailService, Guid> _receptionDetailServiceRepository;
    private readonly IGenericRepository<Reception, Guid> _receptionReception;
    private readonly ApplicationDbContext _dbContext;
    string medicalStaffNames = "";
    string servicesNames = "";
    string DoctorsNames = "";
    string ExpertsNames = "";
    Guid receptionID;
    public ReceptionRepository(IGenericRepository<Reception, Guid> receptionReception, IGenericRepository<ReceptionDetailService, Guid> receptionDetailServiceRepository, IGenericRepository<ReceptionMedicalStaff, Guid> receptionDetailMedicalStaffRepository, IGenericRepository<ReceptionDetail, Guid> receptionDetailRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _receptionDetailRepository = receptionDetailRepository;
        _receptionDetailMedicalStaffRepository = receptionDetailMedicalStaffRepository;
        _receptionDetailServiceRepository = receptionDetailServiceRepository;
        _receptionReception = receptionReception;
    }

    public async Task<ReceptionDetail> AddReceptionService(Guid medicalStaffId, Guid shiftId, Guid officeId, ReceptionType receptionType, Guid patientid, Guid? receptionId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, Guid discountTypeId, Guid[] MedicalStaffs)
    {
        var receptionpatient = await _dbContext.Receptions.SingleOrDefaultAsync(r => r.PatientId == patientid && r.CreatedDate.Date == DateTime.Now.Date);
        if (receptionpatient == null)
        {
            var recid = await CreateNewReception(medicalStaffId, shiftId, officeId, patientid, receptionType);
            receptionID = _dbContext.Receptions.Where(r => r.Id == recid).FirstOrDefault().Id;
        }
        else if (receptionpatient != null)
        {
            receptionID = _dbContext.Receptions.Where(r => r.Id == receptionId).FirstOrDefault().Id;
        }
        var service = await _dbContext.Services.SingleAsync(s => s.Id == serviceId);
        var insurance = await _dbContext.Insurances.SingleAsync(i => i.Id == insuranceId);
        var additionalInsurance = await _dbContext.Insurances.SingleAsync(i => i.Id == additionalInsuranceId);
        var discountValue = await _dbContext.ReceptionDiscounts.SingleAsync(dt => dt.MembershipId == discountTypeId);
        //var MedicalStaffsCheck = MedicalStaffs.All(id => _dbContext.MedicalStaffs.Any(u => u.Id == id));
        //if (!MedicalStaffsCheck)
        //    throw new NullReferenceException();

        var cost = await GetReceptionServiceCost(serviceId, serviceCount, insuranceId) - discountValue.Discount;
        var recieved = 0;
        var debt = cost;
        var discount = discountValue.Discount;
        var deposit = 0;

        ReceptionDetail detail = new()
        {
            AdditionalInsuranceId = additionalInsuranceId,
            Cost = cost,
            Received = recieved,
            Deposit = deposit,
            Debt = debt,
            InsuranceId = insuranceId,
            IsDeleted = false,
            IsDebt = false,
            OfficeId = service.OfficeId,
            ReceptionId = receptionID,
            ServiceCount = serviceCount,
            ReceptionDiscountId = discountValue.Id
        };

        var addedDetail = await _receptionDetailRepository.Add(detail);

        foreach (var MedicalStaffId in MedicalStaffs)
        {
            var receptionMedicalStaff = new ReceptionMedicalStaff()
            {
                IsDeleted = false,
                ReceptionDetailId = addedDetail.Id,
                MedicalStaffId = MedicalStaffId
            };
            await _receptionDetailMedicalStaffRepository.Add(receptionMedicalStaff);
        }

        var receptionDetailService = new ReceptionDetailService()
        {
            ReceptionDetailId = addedDetail.Id,
            ServiceId = service.Id
        };
        await _receptionDetailServiceRepository.Add(receptionDetailService);

        return addedDetail;
    }
    public async Task UpdateReceptionService(Guid receptionDetailId, Guid serviceId, Guid[] MedicalStaffs)
    {
        var receptionDetailService = await _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == receptionDetailId).FirstOrDefaultAsync();
        if (receptionDetailService != null)
            await _receptionDetailServiceRepository.Delete(receptionDetailService);

        var receptionDetailMedicalStaff = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == receptionDetailId).ToListAsync();
        foreach (var item in receptionDetailMedicalStaff)
        {
            await _receptionDetailMedicalStaffRepository.Delete(item);
        }

        var receptionDetailList = await _dbContext.ReceptionDetails.Where(p => p.Id == receptionDetailId).FirstOrDefaultAsync();
        if (receptionDetailList != null)
            await _receptionDetailRepository.Delete(receptionDetailList);
        ReceptionDetail detail = new()
        {
            Id = receptionDetailId,
            AdditionalInsuranceId = receptionDetailList.AdditionalInsuranceId,
            Cost = receptionDetailList.Cost,
            Received = receptionDetailList.Received,
            Deposit = receptionDetailList.Deposit,
            Debt = receptionDetailList.Debt,
            InsuranceId = receptionDetailList.InsuranceId,
            IsDeleted = false,
            IsDebt = false,
            OfficeId = receptionDetailList.OfficeId,
            ReceptionId = receptionDetailList.ReceptionId,
            ServiceCount = receptionDetailList.ServiceCount,
            ReceptionDiscountId = receptionDetailList.ReceptionDiscountId
        };
        var receptionDetail = await _receptionDetailRepository.Add(detail);

        foreach (var MedicalStaffId in MedicalStaffs)
        {
            var receptionMedicalStaff = new ReceptionMedicalStaff()
            {
                IsDeleted = false,
                ReceptionDetailId = receptionDetailId,
                MedicalStaffId = MedicalStaffId
            };
            //await _receptionDetailMedicalStaffRepository.Add(receptionMedicalStaff);
            await _dbContext.ReceptionMedicalStaffs.AddAsync(receptionMedicalStaff);
        }
        var newReceptionDetailService = new ReceptionDetailService()
        {
            ReceptionDetailId = receptionDetailId,
            ServiceId = serviceId
        };
        await _receptionDetailServiceRepository.Add(newReceptionDetailService);
    }
    public async Task DeleteReceptionService(Guid receptionDetailId)
    {
        var receptionDetailService = await _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == receptionDetailId).FirstOrDefaultAsync();
        if (receptionDetailService != null)
            await _receptionDetailServiceRepository.Delete(receptionDetailService);

        var receptionDetailMedicalStaff = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == receptionDetailId).ToListAsync();
        foreach (var item in receptionDetailMedicalStaff)
        {
            await _receptionDetailMedicalStaffRepository.Delete(item);
        }

        var receptionDetailList = await _dbContext.ReceptionDetails.Where(p => p.Id == receptionDetailId).FirstOrDefaultAsync();
        if (receptionDetailList != null)
            await _receptionDetailRepository.Delete(receptionDetailList);
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
            IsDebt = false,
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

        await _receptionReception.Add(reception);

        return reception.Id;
    }

    public async Task<Reception> CreateNewReceptionDebt(long Debt, Guid officeId, Guid receptionId)
    {
        var factorNo = await GetFactorNo();
        var factorNoToday = await GetFactorNoToday();
        var patientId = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().PatientId;

        Reception reception = new()
        {
            FactorNo = factorNo,
            FactorNoToday = factorNoToday,
            IsCancelled = false,
            IsDeleted = false,
            IsReturned = false,
            IsDebt = true,
            LoggedInMedicalStaffId = default,
            OfficeId = officeId,
            PatientId = patientId,
            ReceptionDate = DateTime.Now.ToPersianDate(),
            ReceptionSubmitHour = DateTime.Now.ToShortTimeString(),
            ReceptionType = (ReceptionType?)3,
            ShiftId = default,
            TotalDebt = default,
            TotalDeposit = default,
            TotalReceived = Debt,
            TotalReceptionCost = default
        };

        var addedReception = await _dbContext.Receptions.AddAsync(reception);

        await _dbContext.SaveChangesAsync();

        return reception;
    }

    public async Task<ReceptionDetail> CreateNewReceptionDetailDebt(long Debt, Guid officeId, Guid receptionId)
    {

        ReceptionDetail receptionDetail = new()
        {
            AdditionalInsuranceId = default,
            Cost = default,
            Received = Debt,
            Deposit = default,
            Debt = default,
            InsuranceId = default,
            IsDeleted = false,
            OfficeId = officeId,
            IsDebt = true,
            ReceptionId = receptionId,
            ServiceCount = default,
            ReceptionDiscountId = default
        };

        var addedReception = await _dbContext.ReceptionDetails.AddAsync(receptionDetail);

        await _dbContext.SaveChangesAsync();

        return receptionDetail;
    }

    public async Task<int> GetFactorNo()
    {
        if (_dbContext.Receptions.Any() == false)
        {
            return 1;
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

    public async Task<DetailsofAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId, Guid receptionId)
    {
        var facNo = await _dbContext.Receptions.Where(p => p.PatientId == patientId).FirstOrDefaultAsync();
        var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId).ToListAsync();
        DetailsofAllReceptionsDTO detailsofAllReceptions = new DetailsofAllReceptionsDTO();
        foreach (var item in _list)
        {
            detailsofAllReceptions.ReceptionId = item.ReceptionId;
            detailsofAllReceptions.Cost += item.Cost;
            detailsofAllReceptions.Debt += item.Debt;
            detailsofAllReceptions.Deposit += item.Deposit;
            detailsofAllReceptions.Recieved += item.Received;
        }
        detailsofAllReceptions.FactorNo = facNo.FactorNo;

        var reception = await _dbContext.Receptions.Where(p => p.Id == detailsofAllReceptions.ReceptionId).FirstOrDefaultAsync();

        reception.TotalDebt = detailsofAllReceptions.Debt;
        reception.TotalDeposit = detailsofAllReceptions.Deposit;
        reception.TotalReceived += detailsofAllReceptions.Recieved;

        await _receptionReception.Update(reception);

        return detailsofAllReceptions;
    }

    public async Task<List<ReceptionDetailListDTO>> GetReceptionDetailList(Guid patientId)
    {
        List<ReceptionDetailListDTO> receptionDetailListDTO = new();
        var _list = await _dbContext.ReceptionDetails.Where(x => x.ServiceCount > 0).Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId).ToListAsync();
        foreach (var item in _list)
        {
            var serviceId = _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == item.Id).FirstOrDefault().ServiceId;
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
                Cost = item.Cost,
                ServiceCount = item.ServiceCount,
                MedicalStaffs = medicalStaffNames,
                Recieved = item.Received,
                Discount = discount.Discount,
                Deposit = item.Deposit,
                Debt = item.Debt,
            };
            receptionDetailListDTO.Add(receptiondetaillistDTO);
            medicalStaffNames = "";
        }
        return receptionDetailListDTO;
    }
    public async Task<List<ReceptionDetailListForReceptionDTO>> GetReceptionDetailListForReception(Guid patientId, Guid receptionId)
    {
        List<ReceptionDetailListForReceptionDTO> receptionDetailListForReceptionDTO = new();
        var _list = await _dbContext.ReceptionDetails.Where(x => x.ServiceCount > 0).Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId).ToListAsync();
        foreach (var item in _list)
        {
            var serviceId = _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == item.Id).FirstOrDefault().ServiceId;
            var serviceName = _dbContext.Services.Where(p => p.Id == serviceId).FirstOrDefault().Name;
            var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == item.Id).ToListAsync();
            var discount = _dbContext.ReceptionDiscounts.Where(p => p.Id == item.ReceptionDiscountId).FirstOrDefault();
            foreach (var medicalStaffitem in medicalStaffIds)
            {
                var medicalStaff = await _dbContext.MedicalStaffRoles.Where(p => p.MedicalStaffId == medicalStaffitem.MedicalStaffId).FirstOrDefaultAsync();
                var roleId = await _dbContext.Roles.Where(p => p.Id == medicalStaff.RoleId).FirstOrDefaultAsync();

                if (roleId.PersianName == "کارشناس")
                {
                    ExpertsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                }
                else
                {
                    DoctorsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                }
            }
            medicalStaffIds = null;
            ReceptionDetailListForReceptionDTO receptiondetaillistForReceptionDTO = new()
            {
                Id = item.Id,
                ServiceName = serviceName,
                ServiceCount = item.ServiceCount,
                MedicalStaffs = medicalStaffNames,
                Cost = item.Cost,
                Discount = discount.Discount,
                Deposit = item.Deposit,
                Debt = item.Debt,
                Total = item.Received,
                DoctorsNames = DoctorsNames,
                ExpertsNames = ExpertsNames,
            };
            receptionDetailListForReceptionDTO.Add(receptiondetaillistForReceptionDTO);
            medicalStaffNames = "";
        }
        return receptionDetailListForReceptionDTO;
    }
    public async Task<List<ReceptionListDTO>> GetReceptionList(Guid patientId)
    {
        List<ReceptionListDTO> receptionListDTOs = new();
        var receptionList = await _dbContext.Receptions.Where(p => p.PatientId == patientId).ToListAsync();
        foreach (var receptionItem in receptionList)
        {
            var receptionDetailList = await _dbContext.ReceptionDetails.Where(p => p.ReceptionId == receptionItem.Id).ToListAsync();
            foreach (var receptionDetails in receptionDetailList)
            {
                var serviceIds = await _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == receptionDetails.Id).FirstOrDefaultAsync();
                servicesNames += _dbContext.Services.Select(p => new { p.Name, p.Id }).Where(p => p.Id == serviceIds.ServiceId).FirstOrDefault().Name.ToString() + "، ";
                var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == receptionDetails.Id).ToListAsync();
                foreach (var medicalStaffitem in medicalStaffIds)
                {
                    var medicalStaff = await _dbContext.MedicalStaffRoles.Where(p => p.MedicalStaffId == medicalStaffitem.MedicalStaffId).FirstOrDefaultAsync();
                    var roleId = await _dbContext.Roles.Where(p => p.Id == medicalStaff.RoleId).FirstOrDefaultAsync();

                    if (roleId.PersianName == "کارشناس")
                    {
                        ExpertsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                    }
                    else
                    {
                        DoctorsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                    }
                }
                medicalStaffIds = null;
            }
            ReceptionListDTO receptionListDTO = new()
            {
                Id = receptionItem.Id,
                ReceptionDate = receptionItem.ReceptionDate,
                ServicesNames = servicesNames,
                ServiceCount = GetServiceCountsOfPatient(receptionItem.Id),
                Cost = Convert.ToInt64(receptionItem.TotalReceived),
                Deposit = (float)receptionItem.TotalDeposit,
                Debt = Convert.ToInt64(receptionItem.TotalDebt),
                TotalReception = Convert.ToInt64(receptionItem.TotalReceptionCost),
                DoctorsNames = DoctorsNames,
                ExpertsNames = ExpertsNames,
            };
            receptionListDTOs.Add(receptionListDTO);
            ExpertsNames = "";
            DoctorsNames = "";
        }
        return receptionListDTOs;
    }
    public int GetServiceCountsOfPatient(Guid receptionId)
    {
        int serviceCount = _dbContext.ReceptionDetails.Where(p => p.ReceptionId == receptionId).Sum(p => p.ServiceCount);
        return serviceCount;
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
    public async Task<bool> CheckExistReceptionId(Guid officeId, Guid receptionId)
    {
        bool isExist = await _dbContext.Receptions.AnyAsync(p => p.Id == receptionId && p.OfficeId == officeId);
        return isExist;
    }
}
