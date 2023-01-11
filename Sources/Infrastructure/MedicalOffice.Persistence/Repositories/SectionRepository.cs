using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace MedicalOffice.Persistence.Repositories;

public class SectionRepository : GenericRepository<Section, Guid>, ISectionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistSectionId(Guid sectionId, Guid officeId)
    {
        bool isExist = await _dbContext.Sections.AnyAsync(p => p.Id == sectionId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<List<SectionListDTO>> GetAllSectionsByOfficeId(int skip, int take, Guid officeId)
    {
        List<SectionListDTO> sectionListDTOs = new List<SectionListDTO>();

        var sections = await _dbContext.Sections.Skip(skip).Take(take).Where(p => p.OfficeId == officeId).ToListAsync();

        foreach (var item in sections)
        {
            SectionListDTO sectionListDTO = new SectionListDTO()
            {
                //Name = item.Name,
                //IsActive = Convert.ToInt32(item.Status)
            };

            sectionListDTOs.Add(sectionListDTO);
        }
        return sectionListDTOs;
    }
}
