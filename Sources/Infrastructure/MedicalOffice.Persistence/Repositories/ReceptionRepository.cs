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
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Constants;

namespace MedicalOffice.Persistence.Repositories;

public class ReceptionRepository : GenericRepository<Reception, Guid>, IReceptionRepository
{
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionDetailRepository;
    private readonly IGenericRepository<ReceptionMedicalStaff, Guid> _receptionDetailMedicalStaffRepository;
    private readonly IGenericRepository<Reception, Guid> _receptionReception;
    private readonly ApplicationDbContext _dbContext;
    string medicalStaffNames = "";
    string servicesNames = string.Empty;
    string DoctorsNames = "";
    string ExpertsNames = "";
    //float organshare;
    //float patientshare;
    //float additionalinsuranceshare;
    //Guid receptionID;
    public ReceptionRepository(IGenericRepository<Reception, Guid> receptionReception, IGenericRepository<ReceptionMedicalStaff, Guid> receptionDetailMedicalStaffRepository, IGenericRepository<ReceptionDetail, Guid> receptionDetailRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _receptionDetailRepository = receptionDetailRepository;
        _receptionDetailMedicalStaffRepository = receptionDetailMedicalStaffRepository;
        _receptionReception = receptionReception;
    }
    // این تابع درصد تخفیف سرویس انتخاب شده بر اساس نوع عضویت را بر میگرداند
    public async Task<int> CalculateDiscount(Guid officeId, Guid serviceId, Guid membershipId)
    {
        var memberShip = await _dbContext.Memberships
            .Where(x => x.Id == membershipId && x.OfficeId == officeId && !x.IsDeleted)
            .FirstOrDefaultAsync();

        var membershipService = await _dbContext.MemberShipServices
            .Include(c => c.Service)
            .Include(c => c.MemberShip)
            .Where(c =>
                c.MembershipId == membershipId &&
                c.ServiceId == serviceId &&
                c.OfficeId == officeId &&
                !c.IsDeleted &&
                !c.MemberShip.IsDeleted &&
                !c.Service.IsDeleted)
            .FirstOrDefaultAsync();

        if (membershipService == null && memberShip != null)
            return Convert.ToInt32(memberShip.Discount);

        if (membershipService != null)
            return Convert.ToInt32(membershipService.Discount);

        //if (membershipServices != null && Convert.ToInt32(membershipServices.Discount) != 0)

        //    return Convert.ToInt32(membershipServices.Discount);

        //if (membershipServices != null && Convert.ToInt32(membershipServices.Discount) == 0)

        //    return Convert.ToInt32(membershipServices.MemberShip.Discount);

        else
            return 0;

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
        int? discountPercent,
        // تعرفه سرویس
        long Tariff)
    {
        long organshare = 0; // سهم سازمان
        long patientshare = 0; // سهم بیمار
        long payable = 0; // قابل پرداخت
        long total = 0; // جمع کل
        long insPercent = 0; // درصد بیمه
        long TariffDiff = 0; // مابه تفاوت
        long insAddPercent = 0; // درصد بیمه تکمیلی
        long InsAddTariff = 0; // تعرفه بیمه تکمیلی
        long AddShare = 0; // سهم بیمه تکمیلی
        long discount = 0; // تخفیف

        //  تعرفه سرویس 
        var serviceTariff = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId &&
            p.InsuranceId == insuranceId).FirstOrDefaultAsync();

        if (serviceTariff != null)
        {
            TariffDiff = (serviceTariff?.Difference != null ? (long)serviceTariff.Difference : 0) * serviceCount;
            // اگر درصد دارد
            if (serviceTariff?.InsurancePercent != null && serviceTariff?.InsurancePercent.Value > 0)
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

        total = patientshare + TariffDiff;

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
                var insurance = await _dbContext.Insurances.Where(p => p.Id == additionalInsuranceId).SingleAsync();
                insAddPercent = insurance.InsurancePercent;

                InsAddTariff = Tariff;
            }
            AddShare = ((InsAddTariff * serviceCount) * insAddPercent / 100) > organshare ? ((InsAddTariff * serviceCount) * insAddPercent / 100) - organshare : 0;

            total = patientshare + TariffDiff - AddShare <= 0 ? 0 : patientshare + TariffDiff - AddShare;

            discount = ((long)(discountPercent > 0 ? ((total * discountPercent) / 100) : 0));

            payable = total - discount;

            if (total <= 0)
            {
                discountPercent = 0;
                payable = 0;
            }

        }
        else
        {
            discount = ((long)(discountPercent > 0 ? ((total * discountPercent) / 100) : 0));

            payable = total - discount;
        }
        // به صورت یک دی تی او به سمت فرانت میرود
        ReceptionDetailSharesDTO receptionDetailSharesDTO = new()
        {
            Payable = payable,
            Total = total,
            Tariff = Tariff * serviceCount,
            OrganShare = organshare,
            PatientShare = patientshare,
            AdditionalInsuranceShare = AddShare,
            Discount = discount

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
        Guid[]? MedicalStaffs,
        long payable,
        long total,
        long organshare,
        long patientshare,
        long addshare,
        long tariff,
        long discount)
    {
        try
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
                Tariff = tariff,
                Deposit = 0,
                Debt = payable,
                Payable = payable,
                Total = total,
                Received = 0,
                Discount = discount,
                InsuranceId = insuranceId,
                ServiceId = serviceId,
                IsDeleted = false,
                IsDebt = true,
                OfficeId = officeId,
                ReceptionId = (Guid)receptionId,
                ServiceCount = serviceCount,
                OrganShare = organshare,
                PatientShare = patientshare,
                AdditionalInsuranceShare = addshare,
            };

            var addedDetail = await _receptionDetailRepository.Add(detail);

            if (MedicalStaffs!=null)
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
            return addedDetail;
        }
        catch (Exception)
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
        Guid[]? MedicalStaffs,
        long payable,
        long total,
        long organshare,
        long patientshare,
        long addshare,
        long tariff,
        long discount
        )
    {
        try
        {

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

            long old_received = 0;
            long debt = 0;
            //long discount = 0;
            long deposit = 0;
            if (receptionDetailList.Received == 0)
            {
                old_received = 0;
                debt = payable;
                //discount = discount;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received > payable && receptionDetailList.Debt > payable)
            {
                old_received = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? payable - receptionDetailList.Received + receptionDetailList.Debt : payable - receptionDetailList.Received;
                //discount = discount;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received > payable && receptionDetailList.Debt < payable)
            {
                old_received = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? payable - receptionDetailList.Received + receptionDetailList.Debt : payable - receptionDetailList.Received;
                //discount = discount;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received <= payable && receptionDetailList.Debt < payable)
            {
                old_received = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? payable - old_received + Math.Abs(receptionDetailList.Debt) : payable - old_received;
                //discount = discount;
                deposit = 0;
            }
            if (receptionDetailList.Received > 0 && receptionDetailList.Received <= payable && receptionDetailList.Debt > payable)
            {
                old_received = receptionDetailList.Received;
                debt = receptionDetailList.Debt < 0 ? payable - old_received + Math.Abs(receptionDetailList.Debt) : payable - old_received;
                //discount = discount;
                deposit = 0;
            }

            ReceptionDetail detail = new()
            {
                Id = receptionDetailId,
                AdditionalInsuranceId = additionalInsuranceId,
                Tariff = tariff,
                Payable = payable,
                Deposit = deposit,
                Total = total,
                Received = old_received,
                Debt = debt,
                Discount = discount,
                InsuranceId = insuranceId,
                ServiceId = serviceId,
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

            return addedDetail.Id;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public async Task DeleteReceptionService(Guid receptionDetailId, Guid officeId)
    {

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
        await GetFactorNoToday();

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
            Tariff = default,
            Payable = Debt,
            Deposit = default,
            Debt = default,
            InsuranceId = default,
            IsDeleted = false,
            OfficeId = officeId,
            IsDebt = true,
            ReceptionId = receptionId,
            ServiceCount = default,
        };

        await _dbContext.ReceptionDetails.AddAsync(receptionDetail);

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

        //await _receptionReception.Update(reception);

        var username = await _dbContext.Users.Where(p => p.Id == reception.CreatedById).FirstOrDefaultAsync();
        var facNo = await _dbContext.Receptions.Where(p => p.PatientId == patientId).FirstOrDefaultAsync();
        var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId && p.IsDeleted == false).ToListAsync();
        DetailsOfAllReceptionsDTO detailsofAllReceptions = new ();
        foreach (var item in _list)
        {
            detailsofAllReceptions.ReceptionId = item.ReceptionId;
            detailsofAllReceptions.Tariff += item.Tariff;
            detailsofAllReceptions.Debt += item.Debt;
            detailsofAllReceptions.Deposit += item.Deposit;
            detailsofAllReceptions.Recieved += item.Payable;
        }
        detailsofAllReceptions.FactorNo = facNo.FactorNo;
        detailsofAllReceptions.Description = reception.Description ?? string.Empty;
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
        var _list = await _dbContext.ReceptionDetails
            .Where(x => x.ServiceCount > 0 && x.IsDeleted == false)
            .Include(p => p.Reception)
            .Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId)
            .ToListAsync();

        foreach (var item in _list)
        {
            var serviceId = _dbContext.ReceptionDetails.Where(p => p.Id == item.Id).FirstOrDefault()?.ServiceId;
            var serviceName = _dbContext.Services.Where(p => p.Id == serviceId).FirstOrDefault()?.Name;
            var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == item.Id).ToListAsync();

            ReceptionDetailListDTO receptiondetaillistDTO = new()
            {
                Id = item.Id,
                ServiceName = serviceName ?? string.Empty,
                ServiceId = serviceId ?? default,
                InsuranceId = item.InsuranceId,
                AdditionalInsuranceId = item.AdditionalInsuranceId,
                Tariff = item.Tariff,
                ServiceCount = item.ServiceCount,
                MedicalStaffs = new List<object>(),
                Recieved = item.Payable,
                Discount = item.Discount,
                Deposit = item.Deposit,
                Debt = item.Debt,
                OrganShare = item.OrganShare,
                PatientShare = item.PatientShare,
                AdditionalInsuranceShare = item.AdditionalInsuranceShare

            };

            foreach (var item2 in medicalStaffIds)
            {
                medicalStaffNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == item2.MedicalStaffId).FirstOrDefault().FullName.ToString();
                receptiondetaillistDTO.MedicalStaffs.Add(new { id = item2.MedicalStaffId, name = medicalStaffNames });
                medicalStaffNames = "";
            }

            receptionDetailListDTO.Add(receptiondetaillistDTO);
        }
        return receptionDetailListDTO;


    }
    public async Task<ReceptionDetailofPatientDTO> GetReceptionDetailofPatient(Guid receptiondetailId)
    {
        var _list = await _dbContext.ReceptionDetails.Where(x => x.ServiceCount > 0 && x.IsDeleted == false && x.Id == receptiondetailId).FirstOrDefaultAsync();

        var serviceId = _dbContext.ReceptionDetails.Where(p => p.Id == _list.Id).FirstOrDefault().ServiceId;
        var serviceName = _dbContext.Services.Where(p => p.Id == serviceId).FirstOrDefault().Name;
        var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == _list.Id).ToListAsync();

        ReceptionDetailofPatientDTO receptionDetailofPatientDTO = new()
        {
            Id = _list.Id,
            InsuranceId = _list.InsuranceId,
            AdditionalInsuranceId = _list.AdditionalInsuranceId,
            ServiceName = serviceName,
            Tariff = _list.Tariff,
            ServiceCount = _list.ServiceCount,
            MedicalStaffs = new List<object>(),
            Recieved = _list.Payable,
            Discount = _list.Discount,
            Deposit = _list.Deposit,
            Debt = _list.Debt,
            OrganShare = _list.OrganShare,
            PatientShare = _list.PatientShare,
            AdditionalInsuranceShare = _list.AdditionalInsuranceShare

        };

        foreach (var item2 in medicalStaffIds)
        {
            medicalStaffNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == item2.MedicalStaffId).FirstOrDefault().FullName.ToString();
            receptionDetailofPatientDTO.MedicalStaffs.Add(new { id = item2.MedicalStaffId, name = medicalStaffNames });
            medicalStaffNames = "";
        }
        return receptionDetailofPatientDTO;
    }
    public async Task<List<ReceptionDetailListForReceptionDTO>> GetReceptionDetailListForReception(Guid officeId, Guid patientId, Guid receptionId)
    {
        List<ReceptionDetailListForReceptionDTO> receptionDetailListForReceptionDTO = new();
        var _list = await _dbContext.ReceptionDetails.Where(x => x.ServiceCount > 0).Include(p => p.Reception).Where(p => p.Reception.PatientId == patientId && p.Reception.Id == receptionId).ToListAsync();
        foreach (var item in _list)
        {
            var serviceId = _dbContext.ReceptionDetails.Where(p => p.Id == item.Id).FirstOrDefault()?.ServiceId;
            var serviceName = _dbContext.Services.Where(p => p.Id == serviceId).FirstOrDefault()?.Name;
            var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == item.Id).ToListAsync();

            ReceptionDetailListForReceptionDTO receptiondetaillistForReceptionDTO = new()
            {
                Id = item.Id,
                ServiceName = serviceName ?? string.Empty,
                ServiceCount = item.ServiceCount,
                Tariff = item.Tariff,
                //Discount = discount.Discount,
                Deposit = item.Deposit,
                Debt = item.Debt,
                Total = item.Payable,
                DoctorsNames = new List<object>(),
                ExpertsNames = new List<object>(),
            };
            foreach (var medicalStaffitem in medicalStaffIds)
            {
                var medicalStaffId = await _dbContext.MedicalStaffs.Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefaultAsync();

                var listmedicalstaff = await _dbContext.UserOfficeRoles.Include(p => p.Role).Where(p => p.OfficeId == officeId && p.UserId == medicalStaffId.UserId).FirstOrDefaultAsync();

                if (listmedicalstaff.Role.Id == ExpertRole.Id)
                {
                    ExpertsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString();
                    receptiondetaillistForReceptionDTO.ExpertsNames.Add(new { id = medicalStaffitem.MedicalStaffId, name = ExpertsNames });
                    ExpertsNames = "";

                }
                if (listmedicalstaff.Role.Id == DoctorRole.Id)
                {
                    DoctorsNames += _dbContext.MedicalStaffs.Select(p => new { FullName = p.FirstName + " " + p.LastName, p.Id }).Where(p => p.Id == medicalStaffitem.MedicalStaffId).FirstOrDefault().FullName.ToString();
                    receptiondetaillistForReceptionDTO.DoctorsNames.Add(new { id = medicalStaffitem.MedicalStaffId, name = DoctorsNames });
                    DoctorsNames = "";
                }
                //medicalStaffIds = null;
            }
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
                var receptionDetailService = await _dbContext.ReceptionDetails.Include(x => x.Service).Where(p => p.Id == receptionDetails.Id).Select(x => x.Service).FirstOrDefaultAsync();
                servicesNames += receptionDetailService?.Name + ",";
                //var medicalStaffIds = await _dbContext.ReceptionMedicalStaffs.Where(p => p.ReceptionDetailId == receptionDetails.Id).ToListAsync();
                //foreach (var medicalStaffitem in medicalStaffIds)
                //{
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
                //}
                //medicalStaffIds = null;
            }
            ReceptionListDTO receptionListDTO = new()
            {
                Id = receptionItem.Id,
                ReceptionDate = receptionItem.CreatedDate.ToString(),
                ServicesNames = servicesNames.Remove(servicesNames.Length - 1),
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

        reception.TotalReceptionCost = reception.ReceptionDetails.Sum(rd => rd.Tariff);
        reception.TotalReceived = reception.ReceptionDetails.Sum(rd => rd.Payable);
        reception.TotalDeposit = reception.ReceptionDetails.Sum(rd => rd.Deposit);
        reception.TotalDebt = reception.ReceptionDetails.Sum(rd => rd.Debt);

        await _dbContext.SaveChangesAsync();

        return reception;
    }
    //public async Task<Guid> UpdateReceptionService(Guid receptionDetailId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, float received, float discount, Guid discountTypeId, Guid[] MedicalStaffs)
    //{
    //    throw new NotImplementedException();
    //}
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
    public async Task UpdatereceptionDescription(Guid receptionid, string? description)
    {
        var reception = await _dbContext.Receptions.Where(p => p.Id == receptionid).FirstOrDefaultAsync();
        if (reception != null)
        {
            reception.Description = description;
            await _receptionReception.Update(reception);
        }
    }
}
