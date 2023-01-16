using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MemberShipServiceRepository : GenericRepository<MemberShipService, Guid>, IMemberShipServiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MemberShipServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<MemberShipService> InsertServiceToMemberShipAsync(Guid officeId, string discount, Guid serviceId, Guid memberShipId)
    {
        MemberShipService memberShipService = new MemberShipService()
        {
            ServiceId = serviceId,
            MembershipId = memberShipId,
            Discount = discount,
            OfficeId = officeId
        };

        if (memberShipService == null)
            throw new Exception();

        await Add(memberShipService);

        return memberShipService;
    }

    public async Task<MemberShipService> UpdateServiceOfMemberShipAsync(string discount, Guid OfficeId, Guid id, Guid serviceId, Guid memberShipId)
    {
        MemberShipService memberShipService = new MemberShipService()
        {
            OfficeId = OfficeId,
            Id = id,
            ServiceId = serviceId,
            MembershipId = memberShipId,
            Discount = discount
        };

        if (memberShipService == null)
            throw new Exception();

        await Update(memberShipService);

        return memberShipService;
    }

    public async Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(Guid officeId, Guid memberShipId)
    {
        List<ServicesOfMemeberShipListDTO> servicesOfMemeberShipListDTOs = new List<ServicesOfMemeberShipListDTO>();

        var services = await _dbContext.Services.Where(p => p.OfficeId == officeId).Include(p => p.MemberShipServices).Where(x => (x.MemberShipServices.Where(y => y.MembershipId == memberShipId).Any())).ToListAsync();

        foreach (var item in services)
        {
            ServicesOfMemeberShipListDTO servicesOfMemeberShipDTO = new ServicesOfMemeberShipListDTO()
            {
                ServicesName = item.Name,
                Discount = item.MemberShipServices.Select(p => new { p.Discount, p.MembershipId }).Where(p => p.MembershipId == memberShipId).FirstOrDefault().Discount
            };

        servicesOfMemeberShipListDTOs.Add(servicesOfMemeberShipDTO);
        }
        return servicesOfMemeberShipListDTOs;
    }
    public async Task<bool> CheckExistMemberShipServiceId(Guid Id)
    {
        bool isExist = await _dbContext.MemberShipServices.AnyAsync(p => p.Id == Id);
        return isExist;
    }
}
