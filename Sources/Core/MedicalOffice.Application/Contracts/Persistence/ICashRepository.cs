﻿using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashRepository : IGenericRepository<Cash, Guid>
    {
        Task<Guid> AddCashForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved);
        Task<decimal> GetCashDifferenceWithRecieved(Guid receptionId);
        Task<List<CashListDTO>> GetPatientCashes(Guid receptionId);
    }
}
