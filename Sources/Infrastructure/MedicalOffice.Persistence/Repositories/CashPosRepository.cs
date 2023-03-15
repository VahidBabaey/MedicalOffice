using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashPosRepository : GenericRepository<CashPos, Guid>, ICashPosRepository
{
    private readonly IGenericRepository<Cash, Guid> _cashRepository;
    private readonly IGenericRepository<CashPos, Guid> _cashPosRepository;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;
    private readonly ApplicationDbContext _dbContext;

    public CashPosRepository(IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<Cash, Guid> cashRepository, IGenericRepository<CashPos, Guid> cashPosRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _receptionReceptionDetail = receptionReceptionDetail;
        _cashRepository = cashRepository;
        _cashPosRepository = cashPosRepository;
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
    public async Task<bool> CheckCashPosId(Guid cashPosId)
    {
        bool isExist = await _dbContext.CashPoses.AnyAsync(p => p.Id == cashPosId);
        return isExist;
    }
    public async Task<Guid> AddCashPosForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved, Guid bankid)
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

            CashPos cashPos = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Cost = recieved,
                CashId = cash.Id,
                BankId = bankid,
                CashType = (Domain.Enums.Cashtype?)1
            };
            await _cashPosRepository.Add(cashPos);

            var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.Id == receptionId).ToListAsync();
            foreach (var item in _list)
            {
                if (recieved > 0)
                {
                    if (recieved > item.Debt)
                    {
                        item.Received += item.Debt;
                        recieved = recieved - item.Debt;
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
            return cashPos.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task DeleteCashPosForAnyReceptionDetail(Guid posId)
    {
        try
        {
            var _pos = await _dbContext.CashPoses.Where(p => p.Id == posId).FirstOrDefaultAsync();
            var _cash = await _dbContext.Cashes.Where(p => p.Id == _pos.CashId).FirstOrDefaultAsync();
            var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.Id == _pos.ReceptionId).ToListAsync();
            foreach (var item in _list)
            {
                if (_pos.Cost> 0)
                {
                    if (_pos.Cost > item.Received)
                    {
                        _pos.Cost = _pos.Cost - item.Received;
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                    }
                    else if (_pos.Cost < item.Received)
                    {
                        item.Received = item.Received - _pos.Cost;
                        item.Debt = item.Debt + _pos.Cost;
                        await _receptionReceptionDetail.Update(item);
                        _pos.Cost = 0;
                    }
                    else if (_pos.Cost == item.Debt)
                    {
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                        _pos.Cost = 0;
                    }
                }
            }
            await _cashPosRepository.Delete(_pos);
            await _cashRepository.Delete(_cash);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
