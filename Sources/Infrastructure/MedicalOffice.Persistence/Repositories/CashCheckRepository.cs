using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashCheckRepository : GenericRepository<CashCheck, Guid>, ICashCheckRepository
{
    private readonly IGenericRepository<Cash, Guid> _cashRepository;
    private readonly IGenericRepository<CashCheck, Guid> _cashCheckRepository;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;

    private readonly ApplicationDbContext _dbContext;

    public CashCheckRepository(IGenericRepository<Cash, Guid> cashRepository, IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<CashCheck, Guid> cashCheckRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashRepository = cashRepository;
        _receptionReceptionDetail = receptionReceptionDetail;
        _cashCheckRepository = cashCheckRepository;
    }
    public async Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId)
    {
        bool isExist = await _dbContext.Receptions.AnyAsync(p => p.OfficeId == officeId && p.Id == receptonId);
        return isExist;
    }
    public async Task<bool> CheckExistCashId(Guid officeId, Guid cashId)
    {
        bool isExist = await _dbContext.Cashes.AnyAsync(p => p.OfficeId == officeId && p.Id == cashId);
        return isExist;
    }
    public async Task<bool> CheckCashCheckId(Guid cashCheckId)
    {
        bool isExist = await _dbContext.CashChecks.AnyAsync(p => p.Id == cashCheckId);
        return isExist;
    }
    public async Task<CashCheckResponseDTO> AddCashCheckForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved, Guid bankid, string branch, string accountNumber)
    {
        try
        {
            Cash cash = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Recieved = recieved,
                IsReturned = false
            };
            await _cashRepository.Add(cash);

            CashCheck cashCheck = new()
            {
                AccountNumber = accountNumber,
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Cost = recieved,
                CashId = cash.Id,
                BankId = bankid,
                Branch = branch,
                CashType = CashType.Check
            };
            await _cashCheckRepository.Add(cashCheck);

            var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.Id == receptionId).ToListAsync();
            foreach (var item in _list)
            {
                if (recieved > 0)
                {
                    if (recieved > item.Debt)
                    {
                        item.Received += item.Debt;
                        recieved = (recieved - item.Debt);
                        item.Debt = 0;
                        item.IsDebt = false;
                        await _receptionReceptionDetail.Update(item);
                    }
                    else if (recieved < item.Debt)
                    {
                        item.Received += recieved;
                        item.Debt = item.Debt - recieved;
                        await _receptionReceptionDetail.Update(item);
                        recieved = 0;
                    }
                    else if (recieved == item.Debt)
                    {
                        item.Received += recieved;
                        item.Debt = 0;
                        await _receptionReceptionDetail.Update(item);
                        recieved = 0;
                    }
                }
            }
            return new CashCheckResponseDTO { Id = cashCheck.Id, exception = null };
        }
        catch (Exception ex)
        {
            return new CashCheckResponseDTO { Id = null, exception = ex };
        }
    }
    public async Task DeleteCashCheckForAnyReceptionDetail(Guid checkId)
    {
        try
        {
            var _check = await _dbContext.CashChecks.Where(p => p.Id == checkId).FirstOrDefaultAsync();
            var _cash = await _dbContext.Cashes.Where(p => p.Id == _check.CashId).FirstOrDefaultAsync();
            var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.Id == _check.ReceptionId).ToListAsync();
            foreach (var item in _list)
            {
                if (_check.Cost > 0)
                {
                    if (_check.Cost > item.Received)
                    {
                        _check.Cost = (_check.Cost - item.Received);
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                    }
                    else if (_check.Cost < item.Received)
                    {
                        item.Received = item.Received - _check.Cost;
                        item.Debt = item.Debt + _check.Cost;
                        await _receptionReceptionDetail.Update(item);
                        _check.Cost = 0;
                    }
                    else if (_check.Cost == item.Debt)
                    {
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                        _check.Cost = 0;
                    }
                }
            }
            await _cashCheckRepository.Delete(_check);
            await _cashRepository.Delete(_cash);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
