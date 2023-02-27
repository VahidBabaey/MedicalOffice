using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IShiftRepository : IGenericRepository<Shift, Guid>
    {
        Task<bool> CheckExistShiftId(Guid officeId, Guid shiftId);
        Task<bool> CheckExistShiftName(Guid officeId, string shiftName);
        Task<bool> CheckTimeConflict(string startTime, string endTime, bool nextday);
        Task<List<Shift>> GetShiftBySearch(string name, Guid officeId);
    }
}
