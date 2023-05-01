using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace MedicalOffice.Persistence.Repositories;

public class CashRepository : GenericRepository<Cash, Guid>, ICashRepository
{
    private readonly IGenericRepository<Cash, Guid> _cashRepository;
    private readonly IGenericRepository<Reception, Guid> _receptionReception;
    private readonly IGenericRepository<ReceptionDetail, Guid> _receptionReceptionDetail;
    private readonly ApplicationDbContext _dbContext;

    public CashRepository(IGenericRepository<ReceptionDetail, Guid> receptionReceptionDetail, IGenericRepository<Reception, Guid> receptionReception, IGenericRepository<Cash, Guid> cashRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _receptionReceptionDetail = receptionReceptionDetail;
        _dbContext = dbContext;
        _cashRepository = cashRepository;
        _receptionReception = receptionReception;
    }

    public async Task<Guid> AddCashForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved)
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
            return cash.Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<CashListDTO>> GetPatientCashes(Guid officeId, Guid receptionId)
    {
        List<CashListDTO> list = new();

        var _listPos = await _dbContext.CashPoses.Where(p => p.ReceptionId == receptionId && p.OfficeId == officeId).ToListAsync();
        var _listCheck = await _dbContext.CashChecks.Where(p => p.ReceptionId == receptionId && p.OfficeId == officeId).ToListAsync();
        var _listCart = await _dbContext.CashCarts.Where(p => p.ReceptionId == receptionId && p.OfficeId == officeId).ToListAsync();
        var _listMoney = await _dbContext.CashMoneies.Where(p => p.ReceptionId == receptionId && p.OfficeId == officeId).ToListAsync();

        foreach (var itemPos in _listPos)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itemPos.Id,
                CashId = itemPos.CashId,
                Cost = itemPos.Cost,
                CreatedDate = itemPos.CreatedDate,
                ReceptionId = itemPos.ReceptionId,
                CashType = itemPos.CashType,
            };
            list.Add(cashListDTO);
        }
        foreach (var itemCheck in _listCheck)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itemCheck.Id,
                CashId = itemCheck.CashId,
                Cost = itemCheck.Cost,
                CreatedDate = itemCheck.CreatedDate,
                ReceptionId = itemCheck.ReceptionId,
                CashType = itemCheck.CashType,
            };
            list.Add(cashListDTO);
        }
        foreach (var itemCart in _listCart)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itemCart.Id,
                CashId = itemCart.CashId,
                Cost = itemCart.Cost,
                CreatedDate = itemCart.CreatedDate,
                ReceptionId = itemCart.ReceptionId,
                CashType = itemCart.CashType,
            };
            list.Add(cashListDTO);
        }
        foreach (var itemMoney in _listMoney)
        {
            CashListDTO cashListDTO = new()
            {
                Id = itemMoney.Id,
                CashId = itemMoney.CashId,
                Cost = itemMoney.Cost,
                CreatedDate = itemMoney.CreatedDate,
                ReceptionId = itemMoney.ReceptionId,
                CashType = itemMoney.CashType,
            };
            list.Add(cashListDTO);
        }
        return list;
    }

    //public Task<float> GetCashDifferenceWithRecieved(Guid receptionId)
    //{
    //    float CashCost = 0;

    //    float receptionRecieved = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().TotalReceived;

    //    var _listPos = _dbContext.CashPoses.Where(p => p.ReceptionId == receptionId).ToList();
    //    var _listCheck = _dbContext.CashChecks.Where(p => p.ReceptionId == receptionId).ToList();
    //    var _listCart = _dbContext.CashCarts.Where(p => p.ReceptionId == receptionId).ToList();

    //    foreach (var itempos in _listPos)
    //    {
    //        CashCost += itempos.Cost;
    //    }
    //    foreach (var itemcheck in _listCheck)
    //    {
    //        CashCost += itemcheck.Cost;
    //    }
    //    foreach (var itemcart in _listCart)
    //    {
    //        CashCost += itemcart.Cost;
    //    }

    //    return receptionRecieved - CashCost;
    //}

    public async void UpdateDebtForReception(Guid receptionId)
    {
        long CashCost = 0;

        long receptionRecieved = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().TotalReceived;

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

        long difference = receptionRecieved - CashCost;

        long receptionDebt = _dbContext.Receptions.Where(p => p.Id == receptionId).FirstOrDefault().TotalDebt;
        long newreceptionDebt = receptionDebt + difference;

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
    public async Task<Guid> ReturnCash(Guid officeId, Guid cashId)
    {
        var cash = await _dbContext.Cashes.Where(p => p.OfficeId == officeId && p.Id == cashId).FirstOrDefaultAsync();
        cash.IsReturned = true;
        await _cashRepository.Update(cash);

        return cash.Id;
    }
    public async Task<long> GetTotalDebtofReception(Guid officeId, Guid receptionId, Guid patientId)
    {
        var totaldebt = _dbContext.Receptions.Where(p => p.PatientId == patientId && p.Id == receptionId && p.OfficeId == officeId).FirstOrDefault().TotalDebt;
        return totaldebt;
    }

    public async Task<List<Cash>> GetTotalRecievedByReceptionId(Guid officeId, Guid receptionId)
    {
        var cash = await _dbContext.Cashes
            .Include(x => x.CashCarts.Where(x => !x.IsDeleted))
            .Include(x => x.CashPoses.Where(x => !x.IsDeleted))
            .Include(x => x.CashChecks.Where(x => !x.IsDeleted))
            .Include(x => x.CashMoneies.Where(x => !x.IsDeleted))
            .Where(x => x.OfficeId == officeId && x.ReceptionId == receptionId && !x.IsDeleted).ToListAsync();
        return cash;
    }
}
