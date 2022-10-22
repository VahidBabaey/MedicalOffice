using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Repositories.RepoHelpers;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ReceptionRepository : GenericRepository<Reception, Guid>, IReceptionRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReceptionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddReceptionService(Guid receptionId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, long received, long discount, Guid discountTypeId, Guid[] users)
    {
        var reception = await _dbContext.Receptions.SingleAsync(r => r.Id == receptionId);
        var service = await _dbContext.Services.SingleAsync(s => s.Id == serviceId);
        var insurance = await _dbContext.Insurances.SingleAsync(i => i.Id == insuranceId);
        var additionalInsurance = await _dbContext.Insurances.SingleAsync(i => i.Id == additionalInsuranceId);
        var discountType = await _dbContext.DiscountTypes.SingleAsync(dt => dt.Id == discountTypeId);
        var usersCheck = users.All(id => _dbContext.MedicalStaffs.Any(u => u.Id == id));
        if (!usersCheck)
            throw new NullReferenceException();

        var cost = await GetReceptionServiceCost(serviceId, serviceCount, insuranceId, additionalInsuranceId);
        var debt = cost > received ? cost - received : 0;
        var deposit = received > cost ? received - cost : 0;

        ReceptionDetail detail = new()
        {
            AdditionalInsuranceId = additionalInsuranceId,
            Cost = cost,
            Received = received,
            Deposit = deposit,
            Debt = debt,
            InsuranceId = insuranceId,
            IsDeleted = false,
            OfficeId = reception.OfficeId,
            ReceptionId = reception.Id,
            ServiceCount = serviceCount,
            ServiceId = serviceId,
        };

        var addedDetail = await _dbContext.ReceptionDetails.AddAsync(detail);

        foreach (var userId in users)
        {
            var receptionUser = new ReceptionUser()
            {
                IsDeleted = false,
                UserOfficeRoleId = userId,
                ReceptionDetailId = addedDetail.Entity.Id,
            };

            await _dbContext.ReceptionUsers.AddAsync(receptionUser);
        }

        return addedDetail.Entity.Id;
    }

    public async Task<Guid> CreateNewReception(Guid userId, Guid shiftId, Guid officeId, Guid patientId, ReceptionType receptionType)
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
            LoggedInUserId = userId,
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
        var lastNo = await _dbContext.Receptions.MaxAsync(p => p.FactorNo);

        return lastNo + 1;
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

    public Task<long> GetReceptionServiceCost(Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId)
    {
        throw new NotImplementedException();
    }

    public async Task<ReceptionServiceDto> GetReceptionServiceInfo(Guid receptionDetailId)
    {
        var detail = await _dbContext.ReceptionDetails.SingleOrDefaultAsync(rd => rd.Id == receptionDetailId);

        if (detail != null)
        {
            await _dbContext
                .Entry(detail)
                .Collection(d => d.ReceptionDiscounts).EntityEntry
                .Collection(d => d.ReceptionUsers).EntityEntry
                .Reference(d => d.Service)
                .LoadAsync();

            long discount = default;
            Guid discountTypeId = default;
            Guid[]? users = default;
            if (detail.ReceptionDiscounts.Any())
            {
                discount = detail.ReceptionDiscounts.Sum(d => d.Amount);
                discountTypeId = detail.ReceptionDiscounts.First().DiscountTypeId;
            }
            if (detail.ReceptionUsers.Any())
            {
                users = detail.ReceptionUsers
                    .Select(u => u.Id)
                    .ToArray();
            }

            ReceptionServiceDto dto = new()
            {
                AdditionalInsuranceId = detail.AdditionalInsuranceId,
                DiscountAmount = discount,
                DiscountTypeId = discountTypeId,
                InsuranceId = detail.InsuranceId,
                ServiceId = detail.ServiceId,
                Users = users,
                ServiceName = detail.Service.Name,
                Received = detail.Received,
                ServiceCount = detail.ServiceCount
            };

            return dto;
        }
        else
            throw new NullReferenceException();
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

    public async Task<Guid> UpdateReceptionService(Guid receptionDetailId, Guid serviceId, int serviceCount, Guid insuranceId, Guid additionalInsuranceId, long received, long discount, Guid discountTypeId, Guid[] Users)
    {
        throw new NotImplementedException();
    }
}
