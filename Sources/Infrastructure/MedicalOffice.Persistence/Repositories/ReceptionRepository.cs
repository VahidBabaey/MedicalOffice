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
using NLog.LayoutRenderers;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

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

    public async Task<int> CalculateDiscount(Guid officeId, Guid serviceId, Guid membershipId)
    {

        var membershipServices = await _dbContext.MemberShipServices.Include(c => c.Service).Include(c => c.MemberShip).Where(c => c.MembershipId == membershipId && c.OfficeId == officeId && c.Service.IsDeleted == false && c.IsDeleted == false && c.ServiceId == serviceId).FirstOrDefaultAsync();
        if (membershipServices != null && Convert.ToInt32(membershipServices.Discount) != 0)
        {
            return Convert.ToInt32(membershipServices.Discount);
        }
        if (membershipServices != null && Convert.ToInt32(membershipServices.Discount) == 0)
        {
            return Convert.ToInt32(membershipServices.MemberShip.Discount);
        }
        else
        {
            return 0;
        }
    }
    public async Task<long> GetReceptionServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId)
    {
        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == insuranceId).FirstOrDefaultAsync();
        return (long)Convert.ToDouble(service.TariffValue * serviceCount);
    }
    public async Task<long> GetPatientShareofServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId)
    {

        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == insuranceId).FirstOrDefaultAsync();
        return (long)Convert.ToDouble(((service.TariffValue * serviceCount) - ((service.TariffValue * serviceCount) * service.InsurancePercent / 100)) + service.Difference);

    }
    public async Task<long> GetOrganShareofServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId)
    {

        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == insuranceId).FirstOrDefaultAsync();
        return (long)Convert.ToDouble(((service.TariffValue * serviceCount) * service.InsurancePercent / 100));

    }
    public async Task<long> GetAdditionalServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalinsuranceId)
    {

        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == additionalinsuranceId).FirstOrDefaultAsync();
        if (service.TariffValue != 0)
        {
            return (long)Convert.ToDouble(service.TariffValue * serviceCount);
        }
        else
        {
            return await GetInsuranceServiceCost(serviceId, serviceCount, insuranceId);
        }

    }
    public async Task<long> GetInsuranceServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId)
    {

        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == insuranceId).FirstOrDefaultAsync();

        return (long)Convert.ToDouble(service.TariffValue * serviceCount);

    }
    public async Task<long> CalculateAdditionalServiceCost(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalinsuranceId)
    {
        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == additionalinsuranceId).FirstOrDefaultAsync();
        var servicetariff = GetAdditionalServiceCost(serviceId, serviceCount, insuranceId, additionalinsuranceId);

        return ((Convert.ToInt64(servicetariff) * Convert.ToInt64(service.InsurancePercent) / 100) - await GetOrganShareofServiceCost(serviceId, serviceCount, insuranceId));

    }
    public async Task<long?> CalculateServiceTariff(Guid serviceId, int serviceCount, Guid? insuranceId, Guid? additionalInsuranceId, int? discount)
    {
        var cost = await GetPatientShareofServiceCost(serviceId, serviceCount, insuranceId);
        if (additionalInsuranceId != null)
        {
            cost = cost - await CalculateAdditionalServiceCost(serviceId, serviceCount, insuranceId, additionalInsuranceId);
        }
        var costd = discount > 0 ? cost - ((cost * discount) / 100) : cost;

        return costd;
    }

    public async Task<ReceptionDetail> AddReceptionService(
        Guid officeId,
        ReceptionType receptionType,
        Guid patientid,
        Guid receptionId,
        Guid serviceId,
        int serviceCount,
        Guid? insuranceId,
        Guid? additionalInsuranceId,
        Guid? membershipId,
        Guid[] MedicalStaffs,
        long costd)
    {
        long cost;
        var receptionpatient = await _dbContext.Receptions.SingleOrDefaultAsync(r => r.PatientId == patientid && r.CreatedDate.Date == DateTime.Now.Date);
        if (receptionpatient == null)
        {
            var recid = await CreateNewReception(officeId, patientid, receptionType);
            receptionID = _dbContext.Receptions.Where(r => r.Id == recid).FirstOrDefault().Id;
        }
        else if (receptionpatient != null)
        {
            receptionID = _dbContext.Receptions.Where(r => r.Id == receptionId).FirstOrDefault().Id;
        }
        if (additionalInsuranceId != null)
        {
            cost = await GetPatientShareofServiceCost(serviceId, serviceCount, insuranceId) - await CalculateAdditionalServiceCost(serviceId, serviceCount, insuranceId, additionalInsuranceId);
        }
        else
        {
            cost = await GetPatientShareofServiceCost(serviceId, serviceCount, insuranceId);
        }

        ReceptionDetail detail = new()
        {
            AdditionalInsuranceId = additionalInsuranceId,
            Cost = cost,
            Received = 0,
            Deposit = 0,
            Debt = costd,
            Discount = cost - costd,
            InsuranceId = insuranceId,
            IsDeleted = false,
            IsDebt = true,
            OfficeId = officeId,
            ReceptionId = receptionID,
            ServiceCount = serviceCount,
            OrganShare = await GetOrganShareofServiceCost(serviceId, serviceCount, insuranceId)
        };

        var addedDetail = await _receptionDetailRepository.Add(detail);

        try
        {
            foreach (var MedicalStaffId in MedicalStaffs)
            {
                var receptionMedicalStaff = new ReceptionMedicalStaff()
                {
                    IsDeleted = false,
                    ReceptionDetailId = addedDetail.Id,
                    MedicalStaffId = MedicalStaffId,
                };
                await _receptionDetailMedicalStaffRepository.Add(receptionMedicalStaff);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        try
        {
            var receptionDetailService = new ReceptionDetailService()
            {
                IsDeleted = false,
                ReceptionDetailId = addedDetail.Id,
                ServiceId = serviceId,
            };
            await _receptionDetailServiceRepository.Add(receptionDetailService);

            return addedDetail;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Guid> UpdateReceptionService(
        Guid receptionDetailId,
        Guid officeId,
        Guid receptionId,
        Guid serviceId,
        int serviceCount,
        Guid? insuranceId,
        Guid? additionalInsuranceId,
        Guid[] MedicalStaffs,
        long costd)
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

        var receptionID = _dbContext.Receptions.Where(r => r.Id == receptionId).FirstOrDefault().Id;
        long cost;
        if (additionalInsuranceId != null)
        {
            cost = await GetPatientShareofServiceCost(serviceId, serviceCount, insuranceId) - await CalculateAdditionalServiceCost(serviceId, serviceCount, insuranceId, additionalInsuranceId);
        }
        else
        {
            cost = await GetPatientShareofServiceCost(serviceId, serviceCount, insuranceId);
        }
        long cost1 = costd;
        long recieved = 0;
        long debt = 0;
        long discount = 0;
        long deposit = 0;
        if (receptionDetailList.Received == 0)
        {
            recieved = 0;
            debt = cost1;
            discount = cost - cost1;
            deposit = 0;
        }
        if (receptionDetailList.Received > 0 && receptionDetailList.Received > cost1 && receptionDetailList.Debt > cost1)
        {
            recieved = receptionDetailList.Received;
            debt = receptionDetailList.Debt < 0 ? cost1 - receptionDetailList.Received + receptionDetailList.Debt : cost1 - receptionDetailList.Received;
            discount = cost - cost1;
            deposit = 0;
        }
        if (receptionDetailList.Received > 0 && receptionDetailList.Received > cost1 && receptionDetailList.Debt < cost1)
        {
            recieved = receptionDetailList.Received;
            debt = receptionDetailList.Debt < 0 ? cost1 - receptionDetailList.Received + receptionDetailList.Debt : cost1 - receptionDetailList.Received;
            discount = cost - cost1;
            deposit = 0;
        }
        if (receptionDetailList.Received > 0 && receptionDetailList.Received <= cost1 && receptionDetailList.Debt < cost1)
        {
            recieved = receptionDetailList.Received;
            debt = receptionDetailList.Debt < 0 ? cost1 - recieved + Math.Abs(receptionDetailList.Debt) : cost1 - recieved;
            discount = cost - cost1;
            deposit = 0;
        }
        if (receptionDetailList.Received > 0 && receptionDetailList.Received <= cost1 && receptionDetailList.Debt > cost1)
        {
            recieved = receptionDetailList.Received;
            debt = receptionDetailList.Debt < 0 ? cost1 - recieved + Math.Abs(receptionDetailList.Debt) : cost1 - recieved;
            discount = cost - cost1;
            deposit = 0;
        }

        ReceptionDetail detail = new()
        {
            Id = receptionDetailId,
            AdditionalInsuranceId = additionalInsuranceId,
            Cost = cost,
            Received = recieved,
            Deposit = deposit,
            Debt = debt,
            Discount = cost - cost1,
            InsuranceId = insuranceId,
            IsDeleted = false,
            IsDebt = false,
            OfficeId = officeId,
            ReceptionId = receptionID,
            ServiceCount = serviceCount,
            OrganShare = await GetOrganShareofServiceCost(serviceId, serviceCount, insuranceId)
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

        var receptionDetailServices = new ReceptionDetailService()
        {
            ReceptionDetailId = addedDetail.Id,
            ServiceId = serviceId
        };
        await _receptionDetailServiceRepository.Add(receptionDetailServices);

        return addedDetail.Id;
    }
    public async Task DeleteReceptionService(Guid receptionDetailId, Guid officeId)
    {
        var receptionDetailService = await _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == receptionDetailId).FirstOrDefaultAsync();
        if (receptionDetailService != null)
            await _receptionDetailServiceRepository.SoftDelete(receptionDetailService.Id);

        var receptionDetailMedicalStaff = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == receptionDetailId).ToListAsync();
        foreach (var item in receptionDetailMedicalStaff)
        {
            await _receptionDetailMedicalStaffRepository.SoftDelete(item.Id);
        }

        var receptionDetailList = await _dbContext.ReceptionDetails.Where(p => p.Id == receptionDetailId && p.OfficeId == officeId).FirstOrDefaultAsync();
        if (receptionDetailList != null)
            await _receptionDetailRepository.SoftDelete(receptionDetailList.Id);
    }

    public async Task<Guid> CreateNewReception(Guid officeId, Guid patientId, ReceptionType receptionType)
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
            OfficeId = officeId,
            PatientId = patientId,
            ReceptionType = receptionType,
            ShiftId = default,
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
            OfficeId = officeId,
            PatientId = patientId,
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


    public async Task<DetailsofAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId, Guid receptionId)
    {
        var reception = await _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefaultAsync();
        reception.TotalDebt = 0;
        reception.TotalDeposit = 0;
        reception.TotalReceived = 0;

        await _receptionReception.Update(reception);

        var username = await _dbContext.Users.Where(p => p.Id == reception.CreatedById).FirstOrDefaultAsync();
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
        detailsofAllReceptions.Description = reception.Description;
        detailsofAllReceptions.Date = reception.CreatedDate.ToString();
        detailsofAllReceptions.Username = username.FirstName + " " + username.LastName;

        reception.TotalDebt = detailsofAllReceptions.Debt;
        reception.TotalDeposit = detailsofAllReceptions.Deposit;
        reception.TotalReceived += detailsofAllReceptions.Recieved;

        await _receptionReception.Update(reception);


        return detailsofAllReceptions;
    }

    public async Task<List<ReceptionDetailListDTO>> GetReceptionDetailList(Guid receptionId, Guid patientId)
    {
        List<ReceptionDetailListDTO> receptionDetailListDTO = new();
        var _list = await _dbContext.ReceptionDetails.Where(x => x.ServiceCount > 0 && x.IsDeleted == false).Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId).ToListAsync();
        foreach (var item in _list)
        {
            var serviceId = _dbContext.ReceptionDetailServices.Where(p => p.ReceptionDetailId == item.Id).FirstOrDefault().ServiceId;
            var serviceName = _dbContext.Services.Where(p => p.Id == serviceId).FirstOrDefault().Name;
            var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == item.Id).ToListAsync();
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
                Discount = item.Discount,
                Deposit = item.Deposit,
                Debt = item.Debt,
                OrganShare = item.OrganShare,
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
            foreach (var medicalStaffitem in medicalStaffIds)
            {
                //var medicalStaff = await _dbContext.MedicalStaffRoles.Where(p => p.MedicalStaffId == medicalStaffitem.MedicalStaffId).FirstOrDefaultAsync();
                //var roleId = await _dbContext.Roles.Where(p => p.Id == medicalStaff.RoleId).FirstOrDefaultAsync();

                //if (roleId.PersianName == "کارشناس")
                //{
                //    ExpertsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                //}
                //else
                //{
                //    DoctorsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                //}
            }
            medicalStaffIds = null;
            ReceptionDetailListForReceptionDTO receptiondetaillistForReceptionDTO = new()
            {
                Id = item.Id,
                ServiceName = serviceName,
                ServiceCount = item.ServiceCount,
                MedicalStaffs = medicalStaffNames,
                Cost = item.Cost,
                //Discount = discount.Discount,
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
                    //var medicalStaff = await _dbContext.MedicalStaffRoles.Where(p => p.MedicalStaffId == medicalStaffitem.MedicalStaffId).FirstOrDefaultAsync();
                    //var roleId = await _dbContext.Roles.Where(p => p.Id == medicalStaff.RoleId).FirstOrDefaultAsync();

                    //if (roleId.PersianName == "کارشناس")
                    //{
                    //    ExpertsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                    //}
                    //else
                    //{
                    //    DoctorsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString() + "، ";
                    //}
                }
                medicalStaffIds = null;
            }
            ReceptionListDTO receptionListDTO = new()
            {
                Id = receptionItem.Id,
                ReceptionDate = receptionItem.CreatedDate.ToString(),
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
    public async Task<bool> CheckExistReceptionDetailId(Guid officeId, Guid receptiondetailId)
    {
        bool isExist = await _dbContext.ReceptionDetails.AnyAsync(p => p.Id == receptiondetailId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task UpdatereceptionDescription(Guid receptionid, string description)
    {
        var reception = await _dbContext.Receptions.Where(p => p.Id == receptionid).FirstOrDefaultAsync();
        reception.Description = description;

        await _receptionReception.Update(reception);
    }
}
