using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashCartRepository : GenericRepository<CashCart, Guid>, ICashCartRepository
{
    private readonly IGenericRepository<CashCart, Guid> _cashCartRepository;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;
    private readonly ApplicationDbContext _dbContext;

    public CashCartRepository(IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<CashCart, Guid> cashCartRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashCartRepository = cashCartRepository;
        _receptionReceptionDetail = receptionReceptionDetail;
    }

    public async Task<bool> CheckExistReceptionId(Guid officeId,Guid receptonId)
    {
        bool isExist = await _dbContext.Receptions.AnyAsync(p => p.OfficeId == officeId && p.Id == receptonId);
        return isExist;
    }
    public async Task<bool> CheckExistCashId(Guid officeId, Guid cashId)
    {
        bool isExist = await _dbContext.Cashes.AnyAsync(p => p.OfficeId == officeId && p.Id == cashId);
        return isExist;
    }
    public async Task<bool> CheckCashCartId(Guid cashCartId)
    {
        bool isExist = await _dbContext.CashCarts.AnyAsync(p => p.Id == cashCartId);
        return isExist;
    }
    public async Task<Guid> AddCashCartForAnyReceptionDetail(Guid OfficeId, Guid receptionId, Guid cashid, long recieved, Guid bankid)
    {
        try
        {
            CashCart cashCart = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Cost = recieved,
                CashId = cashid,
                BankId = bankid
            };
            await _cashCartRepository.Add(cashCart);

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
            return cashCart.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
