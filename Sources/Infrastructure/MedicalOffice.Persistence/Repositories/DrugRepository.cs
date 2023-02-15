using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugRepository : GenericRepository<Drug, Guid>, IDrugRepository
{
    private readonly ApplicationDbContext _dbContext;
    public DrugRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<DrugListDTO>> GetAllDrugs(Guid officeId, int take, int skip)
    {
        var _list = await _dbContext.Drugs.Where(p => p.OfficeId == officeId && p.IsDeleted == false).Select(p => new DrugListDTO
        {
            Id = p.Id,
            Name = p.Name,
            GenericCode = p.GenericCode,
            BrandName = p.BrandName,
            DrugSectionId = p.DrugSectionId,
            DrugSectionName = _dbContext.DrugSections.Select(q => new {q.Id, q.SectionDrug}).Where(q=> q.Id == p.DrugSectionId).FirstOrDefault().SectionDrug,
            DrugShapeId = p.DrugShapeId,
            DrugShapeName = _dbContext.DrugShapes.Select(q => new { q.Id, q.ShapeDrug }).Where(q => q.Id == p.DrugShapeId).FirstOrDefault().ShapeDrug,
            DrugConsumptionId = p.DrugConsumptionId,
            DrugConsumptionName = _dbContext.DrugConsumptions.Select(q => new { q.Id, q.ConsumptionDrug }).Where(q => q.Id == p.DrugConsumptionId).FirstOrDefault().ConsumptionDrug,
            DrugUsageId = p.DrugUsageId,
            DrugUsageName = _dbContext.DrugUsages.Select(q => new { q.Id, q.UsageDrug }).Where(q => q.Id == p.DrugUsageId).FirstOrDefault().UsageDrug,
            Consumption = p.Consumption,
            Number = p.Number,
            IsShow = p.IsShow,
            IsHybrid = p.IsHybrid
        }).Take(take).Skip(skip).ToListAsync();

        return (IEnumerable<DrugListDTO>)_list;
    }
    public async Task<bool> CheckExistDrugSectionId(Guid drugSectionId)
    {
        bool isExist = await _dbContext.DrugSections.AnyAsync(p => p.Id == drugSectionId);
        return isExist;
    }
    public async Task<bool> CheckExistDrugUsageId(Guid drugUsageId)
    {
        bool isExist = await _dbContext.DrugUsages.AnyAsync(p => p.Id == drugUsageId);
        return isExist;
    }
    public async Task<bool> CheckExistDrugConsuptionId(Guid drugConsumptionId)
    {
        bool isExist = await _dbContext.DrugConsumptions.AnyAsync(p => p.Id == drugConsumptionId);
        return isExist;
    }
    public async Task<bool> CheckExistDrugShapeId(Guid drugShapeId)
    {
        bool isExist = await _dbContext.DrugShapes.AnyAsync(p => p.Id == drugShapeId);
        return isExist;
    }
    public async Task<bool> CheckExistDrugId(Guid officeId, Guid drugId)
    {
        bool isExist = await _dbContext.Drugs.AnyAsync(p => p.Id == drugId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<List<Drug>> GetDrugBySearch(string name, int take, int skip)
    {
        var drugs = await _dbContext.Drugs.Where(p => p.Name.Contains(name)).Take(take).Skip(skip).ToListAsync();

        return drugs;
    }
}
