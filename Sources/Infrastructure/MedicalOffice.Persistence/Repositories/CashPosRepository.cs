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
    public async Task<Guid> AddCashPosForAnyReceptionDetail(Guid OfficeId, Guid receptionId, Guid cashid, long recieved, Guid bankid)
    {
        try
        {
            CashPos cashPos = new()
            {
                OfficeId = OfficeId,
                ReceptionId = receptionId,
                Cost = recieved,
                CashId = cashid,
                BankId = bankid
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
}
