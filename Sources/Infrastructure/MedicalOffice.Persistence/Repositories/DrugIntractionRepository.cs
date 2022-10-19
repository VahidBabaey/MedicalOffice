using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugIntractionRepository : GenericRepository<DrugIntraction, Guid>, IDrugIntractionRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IGenericRepository<DrugIntraction, Guid> _repositoryDrug;
    public DrugIntractionRepository(IGenericRepository<DrugIntraction, Guid> repositoryDrug, ApplicationDbContext dbContext) : base(dbContext)
    {
        _repositoryDrug = repositoryDrug;
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<DrugIntractionListDTO>> GetAllDrugIntractions()
    {
        var _list = await _repositoryDrug.TableNoTracking.Select(p => new DrugIntractionListDTO
        {
            Id = p.Id,
            Group1 = p.Group1,
            Group2 = p.Group2,
            Effects = p.Effects,
            Method = p.Method,
            Control = p.Control,
            PDrugId = p.PDrugId,
            PDrugName = _dbContext.Drugs.Select(q => new { q.Id, q.Name }).Where(q => q.Id == p.PDrugId).FirstOrDefault().Name,
            SDrugId = p.SDrugId,
            SDrugName = _dbContext.Drugs.Select(q => new { q.Id, q.Name }).Where(q => q.Id == p.SDrugId).FirstOrDefault().Name,

        }).ToListAsync();
        return (IEnumerable<DrugIntractionListDTO>)_list;
    }
}
