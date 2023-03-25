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
    //float organshare;
    //float patientshare;
    //float additionalinsuranceshare;
    //Guid receptionID;
    public ReceptionRepository(IGenericRepository<Reception, Guid> receptionReception, IGenericRepository<ReceptionDetailService, Guid> receptionDetailServiceRepository, IGenericRepository<ReceptionMedicalStaff, Guid> receptionDetailMedicalStaffRepository, IGenericRepository<ReceptionDetail, Guid> receptionDetailRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _receptionDetailRepository = receptionDetailRepository;
        _receptionDetailMedicalStaffRepository = receptionDetailMedicalStaffRepository;
        _receptionDetailServiceRepository = receptionDetailServiceRepository;
        _receptionReception = receptionReception;
    }

    // این تابع درصد تخفیف سرویس انتخاب شده بر اساس نوع عضویت را بر میگرداند
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
    //این تابع برای محاسبه دریافتی 
    public async Task<ReceptionDetailSharesDTO> CalculateServiceTariff(
        Guid serviceId,
        int serviceCount,
        Guid? insuranceId,
        Guid? additionalInsuranceId,
        int? discount,
        long Tariff) // تعرفه سرویس
    {
        long organshare = 0; // سهم سازمان
        long patientshare = 0; // سهم بیمار
        long recieved = 0; // دریافتی
        long insPercent = 0; // درصد بیمه
        long TariffDiff = 0; // مابه تفاوت
        long insAddPercent = 0; // درصد بیمه تکمیلی
        long InsAddTariff = 0; // تعرفه بیمه تکمیلی
        long AddShare = 0; // سهم بیمه تکمیلی

        //  تعرفه سرویس 
        var serviceTariff = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId &&
            p.InsuranceId == insuranceId).FirstOrDefaultAsync();

        if (serviceTariff != null)
        {
            TariffDiff = serviceTariff.Difference;
            // اگر درصد دارد
            if (serviceTariff.InsurancePercent != null && serviceTariff.InsurancePercent.Value > 0)
            {
                insPercent = serviceTariff.InsurancePercent.Value;
            }
            // در غیر این صورت درصد ثبت شده در جدول بیمه
            else
            {
                var insurance = await _dbContext.Insurances.Where(p => p.Id == insuranceId).SingleAsync();
                insPercent = insurance.InsurancePercent;
            }
        }
        else
        {
            var insurance = await _dbContext.Insurances.Where(p => p.Id == insuranceId).SingleAsync();
            insPercent = insurance.InsurancePercent;
        }

        organshare = ((Tariff * serviceCount) * insPercent / 100);
        patientshare = (Tariff * serviceCount) - organshare;

        recieved = patientshare + TariffDiff;

        recieved = ((long)(discount > 0 ? recieved - ((recieved * discount) / 100) : recieved));

        //اگر بیمه تکمیلی دارد
        if (additionalInsuranceId.HasValue)
        {
            var serviceTariffAdd = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == additionalInsuranceId).FirstOrDefaultAsync();

            if (serviceTariffAdd != null)
            {
                // اگر درصد دارد
                if (serviceTariffAdd.InsurancePercent.HasValue && serviceTariffAdd.InsurancePercent.Value > 0)
                {
                    insAddPercent = serviceTariffAdd.InsurancePercent.Value;
                }
                InsAddTariff = (long)serviceTariffAdd.TariffValue;
            }
            // در غیر این صورت درصد ثبت شده در جدول بیمه
            else
            {
                var insurance = await _dbContext.Insurances.Where(p => p.Id == insuranceId).SingleAsync();
                insAddPercent = insurance.InsurancePercent;

                InsAddTariff = Tariff;
            }
            AddShare = (InsAddTariff * insAddPercent / 100) > organshare ? (InsAddTariff * insAddPercent / 100) - organshare : 0;

            recieved = patientshare + TariffDiff - AddShare <= 0 ? 0 : patientshare + TariffDiff - AddShare;

            recieved = ((long)(discount > 0 ? recieved - ((recieved * discount) / 100) : recieved));

            if (recieved <= 0)
            {
                discount = 0;
                recieved = 0;
            }

        }
        // به صورت یک دی تی او به سمت فرانت میرود
        ReceptionDetailSharesDTO receptionDetailSharesDTO = new()
        {
            Recieved = recieved,
            Tariff = Tariff * serviceCount,
            OrganShare = organshare,
            PatientShare = patientshare,
            AdditionalInsuranceShare = AddShare
        };
        return receptionDetailSharesDTO;


    }

    // تابع ثبت نهایی
    public async Task<ReceptionDetail> AddReceptionService(
        Guid officeId,
        Guid? receptionId,
        ReceptionType receptionType,
        Guid patientid,
        Guid serviceId,
        int serviceCount,
        Guid? insuranceId,
        Guid? additionalInsuranceId,
        Guid? membershipId,
        Guid[] MedicalStaffs,
        long recieved,
        long organshare,
        long patientshare,
        long addshare,
        long tariff)
    {
        var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == insuranceId).FirstOrDefaultAsync();

        var receptionPatient = new Reception();
        if (receptionId == null)
        {
            receptionPatient = await _dbContext.Receptions.SingleOrDefaultAsync(r => r.PatientId == patientid && r.CreatedDate.Day == DateTime.Now.Day);
            // اگر بیمار در همون روز پذیرش نشده
            if (receptionPatient == null)
            {
                var recId = await CreateNewReception(officeId, patientid, receptionType);
                receptionId = _dbContext.Receptions.Where(r => r.Id == recId).FirstOrDefault().Id;
            }
            else if (receptionPatient != null)
            {
                receptionId = _dbContext.Receptions.Where(r => r.PatientId == patientid && r.CreatedDate.Day == DateTime.Now.Day).FirstOrDefault().Id;
            }
        }

        ReceptionDetail detail = new()
        {
            AdditionalInsuranceId = additionalInsuranceId,
            Cost = tariff,
            Received = 0,
            Deposit = 0,
            Debt = recieved,
            Discount = tariff - recieved,
            InsuranceId = insuranceId,
            IsDeleted = false,
            IsDebt = true,
            OfficeId = officeId,
            ReceptionId = (Guid)receptionId,
            ServiceCount = serviceCount,
            OrganShare = organshare,
            PatientShare = patientshare,
            AdditionalInsuranceShare = addshare
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
        long Recieved,
        long organshare,
        long patientshare,
        long addshare,
        long tariff
        )
    {
        try
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
            var service = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.InsuranceId == insuranceId).FirstOrDefaultAsync();

            long recieved = Recieved;
            long debt = 0;
            long discount = 0;
            long deposit = 0;
            if (receptionDetailList.Received == 0)
            {
                recieved = 0;
                debt = Recieved;
                discount = tariff - Recieved;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received > Recieved && receptionDetailList.Debt > Recieved)
            {
                recieved = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? Recieved - receptionDetailList.Received + receptionDetailList.Debt : Recieved - receptionDetailList.Received;
                discount = tariff - Recieved;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received > Recieved && receptionDetailList.Debt < Recieved)
            {
                recieved = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? Recieved - receptionDetailList.Received + receptionDetailList.Debt : Recieved - receptionDetailList.Received;
                discount = tariff - Recieved;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received <= Recieved && receptionDetailList.Debt < Recieved)
            {
                recieved = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? Recieved - recieved + Math.Abs(receptionDetailList.Debt) : Recieved - recieved;
                discount = tariff - Recieved;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received <= Recieved && receptionDetailList.Debt > Recieved)
            {
                recieved = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? Recieved - recieved + Math.Abs(receptionDetailList.Debt) : Recieved - recieved;
                discount = tariff - Recieved;
                deposit = 0;
            }

            ReceptionDetail detail = new()
            {
                Id = receptionDetailId,
                AdditionalInsuranceId = additionalInsuranceId,
                Cost = tariff,
                Received = recieved,
                Deposit = deposit,
                Debt = debt,
                Discount = tariff - Recieved,
                InsuranceId = insuranceId,
                IsDeleted = false,
                IsDebt = false,
                OfficeId = officeId,
                ReceptionId = receptionID,
                ServiceCount = serviceCount,
                OrganShare = organshare,
                PatientShare = patientshare,
                AdditionalInsuranceShare = addshare
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
        catch (Exception ex)
        {

            throw;
        }
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
            FactorNoToday = 1,
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
    public async Task<DetailsOfAllReceptionsDTO> GetDetailsofAllReceptions(Guid patientId, Guid receptionId)
    {
        var reception = await _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefaultAsync();
        reception.TotalDebt = 0;
        reception.TotalDeposit = 0;
        reception.TotalReceived = 0;

        await _receptionReception.Update(reception);

        var username = await _dbContext.Users.Where(p => p.Id == reception.CreatedById).FirstOrDefaultAsync();
        var facNo = await _dbContext.Receptions.Where(p => p.PatientId == patientId).FirstOrDefaultAsync();
        var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId).ToListAsync();
        DetailsOfAllReceptionsDTO detailsofAllReceptions = new DetailsOfAllReceptionsDTO();
        foreach (var item in _list)
        {
            detailsofAllReceptions.ReceptionId = item.ReceptionId;
            detailsofAllReceptions.Cost += item.Cost;
            detailsofAllReceptions.Debt += item.Debt;
            detailsofAllReceptions.Deposit += item.Deposit;
            detailsofAllReceptions.Recieved += item.Received;
        }
        detailsofAllReceptions.FactorNo = facNo.FactorNo;
        detailsofAllReceptions.Description = reception.Description == null ? "" : reception.Description;
        detailsofAllReceptions.Date = reception.CreatedDate.ToString();
        detailsofAllReceptions.Username = username == null ? "" : username.FirstName + " " + username.LastName;

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
                PatientShare = item.PatientShare,
                AdditionalInsuranceShare = item.AdditionalInsuranceShare

            };
            receptionDetailListDTO.Add(receptiondetaillistDTO);
            medicalStaffNames = "";
        }
        return receptionDetailListDTO;
    }
    public async Task<List<ReceptionDetailListForReceptionDTO>> GetReceptionDetailListForReception(Guid officeId, Guid patientId, Guid receptionId)
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
                var listmedicalstaff = await _dbContext.MedicalStaffs.Include(p => p.Role).Where(p => p.OfficeId == officeId && p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefaultAsync();

                if (listmedicalstaff.Role.PersianName == "کارشناس")
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
                Deposit = receptionItem.TotalDeposit,
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
    public async Task<long> GetReceptionTotal(Guid id)
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
    public async Task<Guid> UpdateReceptionService(Guid receptionDetailId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, float received, float discount, Guid discountTypeId, Guid[] MedicalStaffs)
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
