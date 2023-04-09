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
    public async Task<Guid> InsertServiceToMemberShipAsync(Guid officeId, string discount, Guid serviceId, Guid memberShipId)
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

        return memberShipService.Id;
    }

    public async Task<Guid> UpdateServiceOfMemberShipAsync(string discount, Guid OfficeId, Guid id, Guid serviceId, Guid memberShipId)
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

        return memberShipService.Id;
    }

    public async Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(Guid officeId, Guid memberShipId)
    {
        List<ServicesOfMemeberShipListDTO> servicesOfMemeberShipListDTOs = new List<ServicesOfMemeberShipListDTO>();

        var services = await _dbContext.Services.Where(p => p.OfficeId == officeId && p.IsDeleted == false).Include(p => p.MemberShipServices).Where(x => (x.MemberShipServices.Where(y => y.MembershipId == memberShipId).Any())).ToListAsync();

        var membershipServices = await _dbContext.MemberShipServices.Include(c => c.Service).Where(c => c.MembershipId == memberShipId && c.OfficeId == officeId && c.Service.IsDeleted == false && c.IsDeleted == false).ToListAsync();

        //foreach (var item in services)
        //{
        //    ServicesOfMemeberShipListDTO servicesOfMemeberShipDTO = new ServicesOfMemeberShipListDTO()
        //    {
        //        ServicesName = item.Name,
        //        Discount = item.MemberShipServices.Select(p => new { p.Discount, p.MembershipId }).Where(p => p.MembershipId == memberShipId).FirstOrDefault().Discount,
        //        Id = item.Id
        //    };

        //    servicesOfMemeberShipListDTOs.Add(servicesOfMemeberShipDTO);
        //}

        foreach (var item in membershipServices)
        {
            ServicesOfMemeberShipListDTO servicesOfMemeberShipDTO = new ServicesOfMemeberShipListDTO()
            {
                ServicesName = item.Service.Name,
                Discount = item.Discount,
                Id = item.Id
            };

            servicesOfMemeberShipListDTOs.Add(servicesOfMemeberShipDTO);
        }
        return servicesOfMemeberShipListDTOs;
    }
    public async Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShipBySearch(Guid officeId, Guid memberShipId, string name)
    {
        List<ServicesOfMemeberShipListDTO> servicesOfMemeberShipListDTOs = new List<ServicesOfMemeberShipListDTO>();

        var membershipServices = await _dbContext.MemberShipServices.Include(x => x.Service)
            .Where(c => c.MembershipId == memberShipId && c.OfficeId == officeId && c.Service.Name.Contains(name) && !c.Service.IsDeleted && !c.IsDeleted).ToListAsync();

        foreach (var item in membershipServices)
        {
            ServicesOfMemeberShipListDTO servicesOfMemeberShipDTO = new ServicesOfMemeberShipListDTO()
            {
                ServiceId = item.Service.Id,
                ServicesName = item.Service.Name,
                Discount = item.Discount,
                Id = item.Id
            };

            servicesOfMemeberShipListDTOs.Add(servicesOfMemeberShipDTO);
        }
        return servicesOfMemeberShipListDTOs;
    }
    public async Task<bool> CheckExistMemberShipServiceId(Guid officeId, Guid Id)
    {
        bool isExist = await _dbContext.MemberShipServices.AnyAsync(p => p.Id == Id && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<Guid> GetMembershipServiceId(Guid serviceId, Guid membershipId)
    {
        var memberShipService = await _dbContext.MemberShipServices.Where(p => p.ServiceId == serviceId || p.MembershipId == membershipId).FirstOrDefaultAsync();
        return memberShipService.Id;
    }
    public async Task<bool> CheckExistServiceIdofMembership(Guid officeId, Guid serviceId)
    {
        bool isExist = await _dbContext.MemberShipServices.AnyAsync(p => p.ServiceId == serviceId && p.OfficeId == officeId);
        return isExist;
    }
}
