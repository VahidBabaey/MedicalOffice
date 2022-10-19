using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugRepository : GenericRepository<Drug, Guid>, IDrugRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IGenericRepository<Drug, Guid> _repositoryDrug;
    public DrugRepository(IGenericRepository<Drug, Guid> repositoryDrug, ApplicationDbContext dbContext) : base(dbContext)
    {
        _repositoryDrug = repositoryDrug;
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<DrugListDTO>> GetAllDrugs()
    {
        var _list = await _repositoryDrug.TableNoTracking.Select(p => new DrugListDTO
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

        }).ToListAsync();
        return (IEnumerable<DrugListDTO>)_list;
    }
 }
