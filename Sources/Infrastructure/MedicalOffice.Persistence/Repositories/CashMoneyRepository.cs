using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashMoneyRepository : GenericRepository<CashMoney, Guid>, ICashMoneyRepository
{
    private readonly IGenericRepository<CashMoney, Guid> _cashmoneyrepository;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;

    private readonly ApplicationDbContext _dbContext;

    public CashMoneyRepository(IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<CashMoney, Guid> cashmoneyrepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
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
    public async Task<Guid> AddCashMoneyForAnyReceptionDetail(Guid OfficeId, Guid receptionId, Guid cashid, long recieved)
    {
        try
        {
            CashMoney cashMoney = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Cost = recieved,
                CashId = cashid
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
                        recieved = recieved - item.Debt;
                        item.Debt = 0;
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
}
