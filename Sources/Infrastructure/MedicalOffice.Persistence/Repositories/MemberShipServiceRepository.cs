using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MemberShipServiceRepository : GenericRepository<MemberShipService, Guid>, IMembershipServiceRepository
{
    private readonly IMembershipServiceRepository _reposytory;
    private readonly ApplicationDbContext _dbContext;

    public MemberShipServiceRepository(IMembershipServiceRepository reposytory, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _reposytory = reposytory;
    }
    public async Task<MemberShipService> InsertServiceToMemberShipAsync(string discount, Guid serviceId, Guid memberShipId)
    {
        MemberShipService memberShipService = new MemberShipService()
        {
            ServiceId = serviceId,
            MembershipId = memberShipId,
            Discount = discount
        };

        if (memberShipService == null)
            throw new Exception();

        await _reposytory.Add(memberShipService);
        return memberShipService;
    }

}
