using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashCartRepository : GenericRepository<CashCart, Guid>, ICashCartRepository
{
    private readonly IGenericRepository<Cash, Guid> _cashRepository;
    private readonly IGenericRepository<CashCart, Guid> _cashCartRepository;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;
    private readonly ApplicationDbContext _dbContext;

    public CashCartRepository(IGenericRepository<Cash, Guid> cashRepository, IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<CashCart, Guid> cashCartRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashRepository = cashRepository;
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
    public async Task<Guid> AddCashCartForAnyReceptionDetail(Guid OfficeId, Guid receptionId, string cartnumber, long recieved, Guid bankid)
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

            CashCart cashCart = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                CartNumber = cartnumber,
                Cost = recieved,
                CashId = cash.Id,
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
            return cashCart.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task DeleteCashCartForAnyReceptionDetail(Guid checkId)
    {
        try
        {
            var _cart = await _dbContext.CashCarts.Where(p => p.Id == checkId).FirstOrDefaultAsync();
            var _cash = await _dbContext.Cashes.Where(p => p.Id == _cart.CashId).FirstOrDefaultAsync();
            var _list = await _dbContext.ReceptionDetails.Include(p => p.Reception).Where(p => p.Reception.Id == _cart.ReceptionId).ToListAsync();
            foreach (var item in _list)
            {
                if (_cart.Cost > 0)
                {
                    if (_cart.Cost > item.Received)
                    {
                        _cart.Cost = (_cart.Cost - item.Received);
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                    }
                    else if (_cart.Cost < item.Received)
                    {
                        item.Received = item.Received - _cart.Cost;
                        item.Debt = item.Debt + _cart.Cost;
                        await _receptionReceptionDetail.Update(item);
                        _cart.Cost = 0;
                    }
                    else if (_cart.Cost == item.Debt)
                    {
                        item.Debt = item.Received;
                        item.Received = 0;
                        await _receptionReceptionDetail.Update(item);
                        _cart.Cost = 0;
                    }
                }
            }
            await _cashCartRepository.Delete(_cart);
            await _cashRepository.Delete(_cash);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
