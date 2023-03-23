using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashMoneyRepository : GenericRepository<CashMoney, Guid>, ICashMoneyRepository
{
    private readonly IGenericRepository<Cash, Guid> _cashRepository;
    private readonly IGenericRepository<CashMoney, Guid> _cashmoneyrepository;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;

    private readonly ApplicationDbContext _dbContext;

    public CashMoneyRepository(IGenericRepository<Cash, Guid> cashRepository, IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<CashMoney, Guid> cashmoneyrepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashRepository = cashRepository;
        _receptionReceptionDetail = receptionReceptionDetail;
        _cashmoneyrepository = cashmoneyrepository;
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
    public async Task<bool> CheckCashMoneyId(Guid cashMoneyId)
    {
        bool isExist = await _dbContext.CashMoneies.AnyAsync(p => p.Id == cashMoneyId);
        return isExist;
    }
    public async Task<Guid> AddCashMoneyForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved)
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

            CashMoney cashMoney = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Cost = recieved,
                CashId = cash.Id,
                CashType = (Domain.Enums.Cashtype?)4
            };
            await _cashmoneyrepository.Add(cashMoney);

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
            return cashMoney.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task DeleteCashMoneyForAnyReceptionDetail(Guid moneyId)
    {
        try
        {
            var _money = await _dbContext.CashMoneies.Where(p => p.Id == moneyId).FirstOrDefaultAsync();
            var _cash = await _dbContext.Cashes.Where(p => p.Id == _money.CashId).FirstOrDefaultAsync();
            var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.Id == _money.ReceptionId).ToListAsync();
            foreach (var item in _list)
            {
                if (_money.Cost > 0)
                {
                    if (_money.Cost > item.Received)
                    {
                        _money.Cost = (_money.Cost - item.Received);
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                    }
                    else if (_money.Cost < item.Received)
                    {
                        item.Received = item.Received - _money.Cost;
                        item.Debt = item.Debt + _money.Cost;
                        await _receptionReceptionDetail.Update(item);
                        _money.Cost = 0;
                    }
                    else if (_money.Cost == item.Debt)
                    {
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                        _money.Cost = 0;
                    }
                }
            }
            await _cashmoneyrepository.Delete(_money);
            await _cashRepository.Delete(_cash);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
