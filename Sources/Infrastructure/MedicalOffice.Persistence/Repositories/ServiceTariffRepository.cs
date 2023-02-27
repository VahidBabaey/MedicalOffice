using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ServiceTariffRepository : GenericRepository<Tariff, Guid>, IServiceTariffRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ServiceTariffRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistInsuranceId(Guid officeId, Guid insuranceId)
    {
        bool isExist = await _dbContext.Insurances.AnyAsync(p => p.OfficeId == officeId && p.Id == insuranceId);
        return isExist;
    }
    public async Task<bool> CheckExistServiceId(Guid officeId, Guid serviceId)
    {
        bool isExist = await _dbContext.Services.AnyAsync(p => p.OfficeId == officeId && p.Id == serviceId);
        return isExist;
    }
    public async Task<List<TariffListDTO>> GetTariffsofService(Guid officeId, Guid serviceId)
    {
        List<TariffListDTO> tariffListDTOs = new();
        var tariffs = await _dbContext.Tariffs.Where(p => p.ServiceId == serviceId && p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
        foreach (var item in tariffs)
        {
            TariffListDTO tariffListDTO = new();
            tariffListDTO.Id = item.Id;
            tariffListDTO.ServiceId = item.ServiceId;
            tariffListDTO.InsuranceId = item.InsuranceId;
            tariffListDTO.TariffValue = item.TariffValue;
            tariffListDTO.InternalTariffValue = item.InternalTariffValue;
            tariffListDTO.Difference = item.Difference;
            tariffListDTO.InsurancePercent = item.InsurancePercent;
            tariffListDTO.Discount = item.Discount;
            tariffListDTO.AdjunctPrice = item.AdjunctPrice;
            tariffListDTO.InsuranceName = _dbContext.Insurances.Select(p => new { p.Id, p.Name }).Where(p => p.Id == item.InsuranceId).FirstOrDefault().Name;
            tariffListDTOs.Add(tariffListDTO);
        }
        return tariffListDTOs;
    }
    public async Task<bool> CheckExistTariffId(Guid officeId, Guid tariffId)
    {
        bool isExist = await _dbContext.Tariffs.AnyAsync(p => p.Id == tariffId && p.OfficeId == officeId);
        return isExist;
    }

