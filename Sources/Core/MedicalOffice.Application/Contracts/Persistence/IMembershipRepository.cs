﻿using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMembershipRepository : IGenericRepository<Membership, Guid>
    {
        Task DeleteMembershipIdofServiceAsync(Guid membershipId);
        Task<List<MembershipListDTO>> GetMembership();
        Task<MemberShipService> InsertMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
        Task<string> SearchServicesforMemberShip(Guid memid);
        Task UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
    }
}
