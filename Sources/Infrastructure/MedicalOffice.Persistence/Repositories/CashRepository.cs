using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace MedicalOffice.Persistence.Repositories;

public class CashRepository : GenericRepository<Cash, Guid>, ICashRepository
{
    private readonly IGenericRepository<Cash, Guid> _cashRepository;
    private readonly IGenericRepository<Reception, Guid> _receptionReception;
    private readonly ApplicationDbContext _dbContext;

    public CashRepository(IGenericRepository<Reception, Guid> receptionReception, IGenericRepository<Cash, Guid> cashRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashRepository = cashRepository;
        _receptionReception = receptionReception;
    }

    public async Task<Guid> AddCashForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved)
    {
        Cash cash = new()
        {
            OfficeId = OfficeId,
            ReceptionId = receptionId,
            Recieved = recieved
        };
        await _cashRepository.Add(cash);

        return cash.Id;
    }

    public Task<List<CashListDTO>> GetPatientCashes(Guid receptionId)
    {
        List<CashListDTO> list = new();

        var _listPos = _dbContext.CashPoses.Where(p => p.ReceptionId == receptionId).ToList();
        var _listCheck = _dbContext.CashChecks.Where(p => p.ReceptionId == receptionId).ToList();
        var _listCart = _dbContext.CashCarts.Where(p => p.ReceptionId == receptionId).ToList();

        foreach (var itempos in _listPos)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itempos.Id,
                CashId = itempos.CashId,
                Cost = itempos.Cost,
                CreatedDate = itempos.CreatedDate,
                ReceptionId = itempos.ReceptionId,
            };
            list.Add(cashListDTO);
        }
        foreach (var itemcheck in _listCheck)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itemcheck.Id,
                CashId = itemcheck.CashId,
                Cost = itemcheck.Cost,
                CreatedDate = itemcheck.CreatedDate,
                ReceptionId = itemcheck.ReceptionId,
            };
            list.Add(cashListDTO);
        }
        foreach (var itemcart in _listCart)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itemcart.Id,
                CashId = itemcart.CashId,
                Cost = itemcart.Cost,
                CreatedDate = itemcart.CreatedDate,
                ReceptionId = itemcart.ReceptionId,
            };
            list.Add(cashListDTO);
        }

        return Task.FromResult(list);
    }

    public Task<decimal> GetCashDifferenceWithRecieved(Guid receptionId)
    {
        long CashCost = 0;

        decimal receptionRecieved = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().TotalReceived;

        var _listPos = _dbContext.CashPoses.Where(p => p.ReceptionId == receptionId).ToList();
        var _listCheck = _dbContext.CashChecks.Where(p => p.ReceptionId == receptionId).ToList();
        var _listCart = _dbContext.CashCarts.Where(p => p.ReceptionId == receptionId).ToList();

        foreach (var itempos in _listPos)
        {
            CashCost += itempos.Cost;
        }
        foreach (var itemcheck in _listCheck)
        {
            CashCost += itemcheck.Cost;
        }
        foreach (var itemcart in _listCart)
        {
            CashCost += itemcart.Cost;
        }

        return Task.FromResult(receptionRecieved - CashCost);
    }

    public async void UpdateDebtForReception(Guid receptionId)
    {
        long CashCost = 0;

        decimal receptionRecieved = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().TotalReceived;

        var _listPos = _dbContext.CashPoses.Where(p => p.ReceptionId == receptionId).ToList();
        var _listCheck = _dbContext.CashChecks.Where(p => p.ReceptionId == receptionId).ToList();
        var _listCart = _dbContext.CashCarts.Where(p => p.ReceptionId == receptionId).ToList();

        foreach (var itempos in _listPos)
        {
            CashCost += itempos.Cost;
        }
        foreach (var itemcheck in _listCheck)
        {
            CashCost += itemcheck.Cost;
        }
        foreach (var itemcart in _listCart)
        {
            CashCost += itemcart.Cost;
        }

        decimal difference = receptionRecieved - CashCost;

        decimal receptionDebt = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().TotalDebt;
        decimal newreceptionDebt = receptionDebt + difference;

        var reception = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault();
        reception.TotalDebt = newreceptionDebt;
        reception.TotalReceived = CashCost;

        await _receptionReception.Update(reception);
    }
    public async Task<bool> CheckExistReceptionId(Guid officeId, Guid receptionId)
    {
        bool isExist = await _dbContext.Receptions.AnyAsync(p => p.OfficeId == officeId && p.Id == receptionId);
        return isExist;
    }
    public async Task<bool> CheckExistCashId(Guid officeId, Guid cashId)
    {
        bool isExist = await _dbContext.Cashes.AnyAsync(p => p.OfficeId == officeId && p.Id == cashId);
        return isExist;
    }
}
